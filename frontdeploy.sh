set -e

git pull
docker compose down frontend
docker compose build frontend --no-cache
docker compose up up -d --no-deps frontend
