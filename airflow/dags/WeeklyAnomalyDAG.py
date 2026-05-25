"""
This is weekly_mmsi_swap. 
This script does anomaly detection on the whole dataset.
This should be seperate from the dailyDAG, because it's so heavy.
"""
from airflow.decorators import dag, task
from datetime import datetime
import psycopg2
import os


def _connect():
    return psycopg2.connect(
        host='host.docker.internal',
        port=5432,
        dbname=os.environ['AIS_DB_NAME'],
        user=os.environ['AIS_DB_USER'],
        password=os.environ['AIS_DB_PASS'],
    )


@dag(
    dag_id="weekly_mmsi_swap",
    schedule="0 4 * * 0",         # 04:00 every Sunday
    start_date=datetime(2026, 1, 1),
    catchup=False,
    max_active_runs=1,
    default_args={'retries': 0},
)
def weekly_mmsi_swap():

    @task
    def detect_swaps():
        """Run the heavy swap-detection SQL / Idempotent"""
        with open('/opt/airflow/sql/04_mmsi_swap_detection.sql', 'r') as f:
            sql = f.read()
        conn = _connect()
        try:
            with conn.cursor() as cur:
                cur.execute(sql)
            conn.commit()
            print("weekly MMSI swap detection complete")
        finally:
            conn.close()

    detect_swaps()


weekly_mmsi_swap()