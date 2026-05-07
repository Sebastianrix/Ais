from airflow.decorators import dag, task
from datetime import datetime, timedelta
import requests, zipfile, io, psycopg2, pandas as pd
import os


@dag(
    dag_id='ais_daily',
    schedule='0 19 * * *',
    start_date=datetime(2026, 1, 1),
    catchup=False,
    default_args={
        'retries': 10,
        'retry_delay': timedelta(minutes=8),
    }
)
def ais_daily():

    @task()
    def download_and_stage(ds=None):
        target = datetime.strptime(ds, '%Y-%m-%d').date() - timedelta(days=3)
        url = f"http://aisdata.ais.dk/aisdk-{target}.zip"

        r = requests.get(url, timeout=300)
        r.raise_for_status()

        z = zipfile.ZipFile(io.BytesIO(r.content))
        csv_name = z.namelist()[0]
        conn = psycopg2.connect(
            host='host.docker.internal',
            port=5432,
            dbname=os.environ['AIS_DB_NAME'],
            user=os.environ['AIS_DB_USER'],
            password=os.environ['AIS_DB_PASS']
        )

        col_map = {
            '# Timestamp': 'timestamp_raw',
            'Type of mobile': 'type_of_mobile',
            'MMSI': 'mmsi',
            'Latitude': 'latitude_raw',
            'Longitude': 'longitude_raw',
            'Navigational status': 'navigational_status',
            'ROT': 'rot_raw', 'SOG': 'sog_raw', 'COG': 'cog_raw',
            'Heading': 'heading_raw', 'IMO': 'imo', 'Callsign': 'callsign',
            'Name': 'vessel_name', 'Ship type': 'ship_type',
            'Cargo type': 'cargo_type', 'Width': 'width_raw',
            'Length': 'length_raw',
            'Type of position fixing device': 'position_fixing_device',
            'Draught': 'draught_raw', 'Destination': 'destination',
            'ETA': 'eta_raw', 'Data source type': 'data_source_type',
            'A': 'size_a', 'B': 'size_b', 'C': 'size_c', 'D': 'size_d'
        }

        with z.open(csv_name) as f:
            for chunk in pd.read_csv(f, chunksize=100000):
                chunk = chunk.rename(columns=col_map)
                chunk['source_file_name'] = f"aisdk-{target}.zip"
                chunk['source_batch_date'] = str(target)
                buf = io.StringIO()
                chunk.to_csv(buf, index=False, header=False)
                buf.seek(0)
                with conn.cursor() as cur:
                    cur.copy_expert(
                        f"COPY tanker_staging ({','.join(chunk.columns)}) FROM STDIN WITH CSV",
                        buf
                    )
                conn.commit()
        conn.close()
