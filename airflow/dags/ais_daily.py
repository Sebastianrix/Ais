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
