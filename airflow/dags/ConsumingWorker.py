"""
ais_worker pulls one job from data_consumer_queue, processes it end-to-end,
records results in data_date_archive.

"""
from airflow.decorators import dag, task
from airflow.exceptions import AirflowSkipException
from datetime import datetime, timedelta, date
import requests, zipfile, io, psycopg2, pandas as pd
import os
import time


#Connection helper
def _connect():
    return psycopg2.connect(
        host='host.docker.internal',
        port=5432,
        dbname=os.environ['AIS_DB_NAME'],
        user=os.environ['AIS_DB_USER'],
        password=os.environ['AIS_DB_PASS'],
    )


#Constants
COL_MAP = {
    'Timestamp': 'timestamp_raw',
    'Type of mobile': 'type_of_mobile',
    'MMSI': 'mmsi',
    'Latitude': 'latitude_raw',
    'Longitude': 'longitude_raw',
    'Navigational status': 'navigational_status',
    'ROT': 'rot_raw',
    'SOG': 'sog_raw',
    'COG': 'cog_raw',
    'Heading': 'heading_raw',
    'IMO': 'imo',
    'Callsign': 'callsign',
    'Name': 'vessel_name',
    'Ship type': 'ship_type',
    'Cargo type': 'cargo_type',
    'Width': 'width_raw',
    'Length': 'length_raw',
    'Type of position fixing device': 'position_fixing_device',
    'Draught': 'draught_raw',
    'Destination': 'destination',
    'ETA': 'eta_raw',
    'Data source type': 'data_source_type',
    'A': 'size_a',
    'B': 'size_b',
    'C': 'size_c',
    'D': 'size_d',
}

BUDGET_BYTES = 1_800 * 1024**3   # 1.8 TB ( We should put abit higher, but our ZTF ZPOOL matters here )

# We tried to apply some of our knowlegede from deep-learning here ... ☺
#DAG
@dag(
    dag_id="ais_worker",
    schedule="*/15 * * * *",                  # 30 seconds 
    start_date=datetime(2026, 1, 1),
    catchup=False,
    max_active_runs=1,                    # one worker run at a time
    default_args={
        'retries': 0,                     # failures are recorded in queue, no auto-retry
    },
)
def ais_worker():

    @task
    def claim_next_job() -> dict:
        """Pick highest-priority pending job, mark in_progress, return it."""
        conn = _connect()
        try:
            with conn.cursor() as cur:
                cur.execute("""
                    SELECT queue_id, source_batch_date
                    FROM data_consumer_queue
                    WHERE status = 'pending'
                    ORDER BY priority DESC, created_at ASC
                    LIMIT 1
                    FOR UPDATE SKIP LOCKED
                """)
                row = cur.fetchone()
                if row is None:
                    print("Queue is empty, nothing to do.")
                    raise AirflowSkipException("queue empty")

                queue_id, target_day = row
                cur.execute("""
                    UPDATE data_consumer_queue
                    SET status = 'in_progress'
                    WHERE queue_id = %s
                """, (queue_id,))
            conn.commit()
            print(f"Claimed queue_id={queue_id} for {target_day}")
            return {"queue_id": queue_id, "target_day": target_day.isoformat()}
        finally:
            conn.close()

    @task
    def check_already_done(job: dict) -> dict:
        """If date is already in archive, mark queue duplicate and skip."""
        conn = _connect()
        try:
            with conn.cursor() as cur:
                cur.execute(
                    "SELECT 1 FROM data_date_archive WHERE source_batch_date = %s",
                    (job["target_day"],),
                )
                if cur.fetchone():
                    cur.execute(
                        "UPDATE data_consumer_queue SET status='done' WHERE queue_id = %s",
                        (job["queue_id"],),
                    )
                    conn.commit()
                    print(f"{job['target_day']} already in archive — skipping")
                    raise AirflowSkipException("already archived")
            return job
        finally:
            conn.close()

    @task
    def check_budget(job: dict) -> dict:
        """Refuse to ingest if database is past storage budget."""
        conn = _connect()
        try:
            with conn.cursor() as cur:
                cur.execute("SELECT pg_database_size('ais_db')")
                db_size = cur.fetchone()[0]
            gb = db_size / 1024**3
            limit_gb = BUDGET_BYTES / 1024**3
            print(f"DB size: {gb:.1f} GB / {limit_gb:.0f} GB budget")
            if db_size >= BUDGET_BYTES:
                with conn.cursor() as cur:
                    cur.execute(
                        "UPDATE data_consumer_queue SET status='failed' WHERE queue_id = %s",
                        (job["queue_id"],),
                    )
                conn.commit()
                raise RuntimeError(f"Storage budget exceeded ({gb:.1f} GB)")
            return job
        finally:
            conn.close()

    @task
    def download_and_stage(job: dict) -> dict:
        """Fetch the zip, COPY all rows into tanker_staging, count totals."""
        target_day = datetime.strptime(job["target_day"], '%Y-%m-%d').date()
        url = f"http://aisdata.ais.dk/aisdk-{target_day}.zip"
        print(f"[{target_day}] Downloading {url}")

        # HEAD probe first — distinguishes "source missing this day" from real errors
        head = requests.head(url, timeout=60)
        if head.status_code == 404:
            conn = _connect()
            try:
                with conn.cursor() as cur:
                    cur.execute(
                        "UPDATE data_consumer_queue SET status='failed' WHERE queue_id = %s",
                        (job["queue_id"],),
                    )
                conn.commit()
            finally:
                conn.close()
            raise AirflowSkipException(f"source 404 for {target_day}")

        head.raise_for_status()

        r = requests.get(url, timeout=600)
        r.raise_for_status()
        z = zipfile.ZipFile(io.BytesIO(r.content))
        csv_name = z.namelist()[0]
        print(f"[{target_day}] CSV: {csv_name}")

        conn = _connect()
        total_rows = 0
        tanker_rows = 0
        chunk_num = 0
        start_t = time.time()
        try:
            with z.open(csv_name) as f:
                for chunk in pd.read_csv(f, sep=',', chunksize=20000, low_memory=False):
                    chunk_num += 1
                    chunk.columns = (
                        chunk.columns
                        .str.replace('#', '', regex=False)
                        .str.strip()
                    )
                    chunk = chunk.rename(columns=COL_MAP)
                    chunk['source_file_name'] = f"aisdk-{target_day}.zip"
                    chunk['source_batch_date'] = str(target_day)

                    total_rows += len(chunk)
                    tanker_rows += int(
                        chunk['ship_type'].str.lower().str.strip().eq('tanker').sum()
                    )

                    buf = io.StringIO()
                    chunk.to_csv(buf, index=False, header=False)
                    buf.seek(0)
                    with conn.cursor() as cur:
                        cur.copy_expert(
                            f"COPY tanker_staging ({','.join(chunk.columns)}) FROM STDIN WITH CSV",
                            buf,
                        )
                    conn.commit()

                    if chunk_num % 25 == 0:
                        elapsed = time.time() - start_t
                        rate = total_rows / elapsed
                        print(f"[{target_day}] chunk={chunk_num} rows={total_rows:,} "
                              f"tankers={tanker_rows:,} rate={rate:,.0f} r/s")

            elapsed = time.time() - start_t
            print(f"[{target_day}] STAGED: {total_rows:,} rows ({tanker_rows:,} tankers) in {elapsed:.0f}s")
        finally:
            conn.close()

        return {**job, "total_rows": total_rows, "tanker_rows": tanker_rows}

    @task
    def promote(job: dict) -> dict:
        """Run 02_Load_data.sql for this day, capture positions_inserted."""
        target_day = job["target_day"]
        with open('/opt/airflow/sql/02_Load_data.sql', 'r') as f:
            etl_sql = f.read()

        conn = _connect()
        try:
            with conn.cursor() as cur:
                cur.execute(etl_sql, {"target_day": target_day})
                positions_inserted = cur.fetchone()[0]
            conn.commit()
            print(f"[{target_day}] PROMOTED: {positions_inserted:,} positions inserted")
        finally:
            conn.close()

        return {**job, "positions_inserted": positions_inserted}

    @task
    def detect_anomalies(job: dict) -> dict:
        """Run anomaly detection."""
        with open('/opt/airflow/sql/03_anomaly_detection.sql', 'r') as f:
            detect_sql = f.read()
        conn = _connect()
        try:
            with conn.cursor() as cur:
                cur.execute(detect_sql)        
            conn.commit()
            print(f"[{job['target_day']}] anomaly detection complete")
        finally:
            conn.close()
        return job



    @task
    def finalize(job: dict):
        """Write archive row, mark queue done."""
        conn = _connect()
        try:
            with conn.cursor() as cur:
                cur.execute("""
                    INSERT INTO data_date_archive
                        (source_batch_date, total_rows, tanker_rows, positions_inserted)
                    VALUES (%s, %s, %s, %s)
                    ON CONFLICT (source_batch_date) DO UPDATE SET
                        total_rows = EXCLUDED.total_rows,
                        tanker_rows = EXCLUDED.tanker_rows,
                        positions_inserted = EXCLUDED.positions_inserted,
                        archived_at = NOW()
                """, (
                    job["target_day"], 
                    job["total_rows"],
                    job["tanker_rows"],
                    job["positions_inserted"],
                ))
                cur.execute(
                    "UPDATE data_consumer_queue SET status='done' WHERE queue_id = %s",
                    (job["queue_id"],),
                )
            conn.commit()
            print(f"[{job['target_day']}] FINALIZED — archive written, queue marked done")
        finally:
            conn.close()

    # Pipeline
    job = claim_next_job()
    job = check_already_done(job)
    job = check_budget(job)
    job = download_and_stage(job)
    job = promote(job)
    job = detect_anomalies(job)
    finalize(job)


ais_worker()