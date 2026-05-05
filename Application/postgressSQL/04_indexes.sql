CREATE INDEX IF NOT EXISTS idx_tanker_staging_staging_id_desc 
  ON tanker_staging (staging_id DESC);

CREATE INDEX IF NOT EXISTS idx_tanker_staging_ship_type 
  ON tanker_staging (LOWER(TRIM(ship_type)));

CREATE INDEX idx_tanker_timestamp_raw 
ON tankerstaging("Timestamp_Raw" DESC);
