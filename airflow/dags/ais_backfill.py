from airflow.decorators import dag, task
from airflow.providers.postgres.hooks.postgres import PostgresHook
from datetime import datetime, timedelta
import requests, zipfile, io, psycopg2, pandas as pd
import os
import time 

@dag(
    dag_id="ais_backfill",
    schedule=None,
    catchup=False,
)
def ais_backfill():

    @task
    def generate_dates():
        start = datetime.today().date()
        floor = date(2026, 1, 1)

        dates = []

        current = start

        while current >= floor:
            dates.append(current.isoformat())
            current -= timedelta(days=1)

        return dates

    @task
    def process_day(day: str):

        # storage guard
        conn = psycopg2.connect(...)

        cur = conn.cursor()

        cur.execute("SELECT pg_database_size('ais_db')")
        db_size = cur.fetchone()[0]

        budget_bytes = 1_800 * 1024**3

        if db_size >= budget_bytes:
            raise Exception("Storage budget exceeded")

        # download
        download_and_stage_one_day(day)

        # ETL
        run_etl_one_pass(day)

        print(f"{day} complete")

    process_day.expand(
        day=generate_dates()
    )

ais_backfill()
