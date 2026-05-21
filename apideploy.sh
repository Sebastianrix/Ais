set -e

git pull
docker compose down api
docker compose build api --no-cache
docker compose up api -d --no-deps api
