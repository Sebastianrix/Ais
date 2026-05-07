#!/bin/bash
set -e  # This will stop the script of any failure

#cd ais  (not needed longer, we moved the file inside the repo for doucmentability)
docker compose down
git pull
docker compose build --no-cache
docker compose up
