import requests
import pandas as pd
import zipfile
import io
from datetime import date, timedelta
from pathlib import Path

output_file = Path("tanker_imos.txt")

# load previous results
if output_file.exists():
    with open(output_file) as f:
        all_imos = set(line.strip() for line in f)
else:
    all_imos = set()

print("Starting with", len(all_imos), "known IMOs")

#start = date(2022,1,1) First start. --- IGNORE ---
#start = date(2022,4,1)
#start = date(2022,8,1)

#start = date(2025,1,1)  
#end = date(2025,12,31)

start = date(2024,1,1)
end = date(2024,12,31)

processed_months = set()

d = start

while d <= end:

    year = d.year
    month = f"{d.month:02d}"
    day = f"{d.day:02d}"

    daily_url = f"http://aisdata.ais.dk/{year}/aisdk-{year}-{month}-{day}.zip"
    monthly_url = f"http://aisdata.ais.dk/{year}/aisdk-{year}-{month}.zip"

    try:

        r = requests.head(daily_url)

        if r.status_code == 200:

            url = daily_url
            print("Daily:", url)

        else:

            if (year, month) in processed_months:
                d += timedelta(days=1)
                continue

            r = requests.head(monthly_url)

            if r.status_code == 200:

                url = monthly_url
                processed_months.add((year, month))
                print("Monthly:", url)

            else:

                print("Skipping:", daily_url)
                d += timedelta(days=1)
                continue

        r = requests.get(url, stream=True)

        z = zipfile.ZipFile(io.BytesIO(r.content))
        csv_name = z.namelist()[0]

        with z.open(csv_name) as f:

            for chunk in pd.read_csv(f, chunksize=100000):

                tankers = chunk[chunk["Ship type"] == "Tanker"]

                imos = tankers["IMO"].dropna()

                all_imos.update(str(i) for i in imos)

        # save progress
        with open(output_file, "w") as f:
            for imo in sorted(all_imos):
                f.write(imo + "\n")

        print("Saved IMOs:", len(all_imos))

    except Exception as e:

        print("Error processing", url)

    d += timedelta(days=1)

print("FINAL UNIQUE IMOs:", len(all_imos))