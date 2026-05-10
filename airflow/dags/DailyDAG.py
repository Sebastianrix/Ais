from airflow.decorators import dag, task
from airflow.sensors.python import PythonSensor
from datetime import datetime, timedelta
import requests
import psycopg2
import os

def _check_website():
    target_day = (datetime.now() - timedelta(days=3)).date()
    url = f"http://aisdata.ais.dk/aisdk-{target_day}.zip"
    try:
        return requests.head(url, timeout=10).status_code == 200
    except requests.RequestException:
        return False

@dag(
    dag_id="daily_package_sensor",
    schedule="0 15 * * *",
    start_date=datetime(2026, 5, 10),
    catchup=False,
    max_active_runs=1,
)
def daily_package_sensor_dag():

    wait_for_file = PythonSensor(
        task_id="wait_for_file",
        python_callable=_check_website,
        poke_interval=300,
        timeout=32400,
        mode="poke"
    )

    @task
    def queue_top_priority_job():
        target_day = (datetime.now() - timedelta(days=3)).date()
        conn = psycopg2.connect(
            host='host.docker.internal',
            port=5432,
            dbname=os.environ['AIS_DB_NAME'],
            user=os.environ['AIS_DB_USER'],
            password=os.environ['AIS_DB_PASS'],
        )
        try:
            with conn.cursor() as cur:
                cur.execute("""
                    INSERT INTO data_consumer_queue 
                        (source_batch_date, priority, requester, status)
                    VALUES (%s, %s, %s, %s)
                """, (target_day, 100, 'daily', 'pending'))
            conn.commit()
        finally:
            conn.close()

    wait_for_file >> queue_top_priority_job()

daily_package_sensor_dag()
