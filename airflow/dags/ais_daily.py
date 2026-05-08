from airflow.decorators import dag, task
from airflow.providers.postgres.hooks.postgres import PostgresHook
from datetime import datetime, timedelta
import requests, zipfile, io, psycopg2, pandas as pd
import os
import time   

@dag(
    dag_id='ais_daily',
    schedule=None,
   ## schedule='0 19 * * *',
    start_date=datetime(2026, 1, 1),
    catchup=False,
    default_args={
        'retries': 3,
        'retry_delay': timedelta(minutes=8),
    }
)
def ais_daily():

    @task()
    def download_and_stage(ds=None):

        target = datetime.strptime(ds, '%Y-%m-%d').date() - timedelta(days=3)

        url = f"http://aisdata.ais.dk/aisdk-{target}.zip"

        print(f"Downloading {url}")

        r = requests.get(url, timeout=300)
        r.raise_for_status()

        z = zipfile.ZipFile(io.BytesIO(r.content))
        csv_name = z.namelist()[0]

        print(f"CSV file: {csv_name}")

        conn = psycopg2.connect(
            host='host.docker.internal',
            port=5432,
            dbname=os.environ['AIS_DB_NAME'],
            user=os.environ['AIS_DB_USER'],
            password=os.environ['AIS_DB_PASS']
        )

        col_map = {
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
            'D': 'size_d'
        }

        with z.open(csv_name) as f:
            # INITIALIZE before loop
            total_rows = 0
            chunk_num = 0
            start = time.time()

            for chunk in pd.read_csv(
                f,
                sep=',',
                chunksize=20000,
                low_memory=False
            ):
                # Run PER ITERATION
                chunk_num += 1

                chunk.columns = (
                    chunk.columns
                    .str.replace('#', '', regex=False)
                    .str.strip()
                )
                chunk = chunk.rename(columns=col_map)
                chunk['source_file_name'] = f"aisdk-{target}.zip"
                chunk['source_batch_date'] = str(target)

                buf = io.StringIO()
                chunk.to_csv(buf, index=False, header=False)
                buf.seek(0)
                with conn.cursor() as cur:
                    cur.copy_expert(
                        f"""
                        COPY tanker_staging ({','.join(chunk.columns)})
                        FROM STDIN WITH CSV
                        """,
                        buf
                    )
                conn.commit()

                total_rows += len(chunk)

                # Progress print every 25 chunks ( around 500k rows)
                if chunk_num % 25 == 0:
                    elapsed = time.time() - start
                    rate = total_rows / elapsed
                    print(f"[{target}] chunk={chunk_num} rows={total_rows:,} "
                          f"elapsed={elapsed:.0f}s rate={rate:,.0f} rows/s")

            # SUMMARIZE after loop
            elapsed = time.time() - start
            print(f"[{target}] DONE: {chunk_num} chunks, {total_rows:,} rows, {elapsed:.0f}s")

        conn.close()

    @task()
    def run_etl():
        hook = PostgresHook(postgres_conn_id="ais_postgres")
        conn = hook.get_conn()
        with conn:
            with conn.cursor() as cur:
                with open('/opt/airflow/sql/02_Load_data.sql', 'r') as f:
                    sql = f.read()
                cur.execute(sql)
        conn.close()

    download_and_stage() >> run_etl()

ais_daily()
