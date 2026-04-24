TRUNCATE TABLE tanker_positions RESTART IDENTITY;
INSERT INTO tankers (imo, mmsi, vessel_name, callsign, ship_type, cargo_type, 
                     type_of_mobile, width, length, size_a, size_b, size_c, size_d)
SELECT DISTINCT
    TRIM(s.imo),
    TRIM(s.mmsi),
    TRIM(s.vessel_name),
    TRIM(s.callsign),
    TRIM(s.ship_type),
    TRIM(s.cargo_type),
    TRIM(s.type_of_mobile),
    NULLIF(REPLACE(s.width_raw, ',', '.'), '')::NUMERIC,
    NULLIF(REPLACE(s.length_raw, ',', '.'), '')::NUMERIC,
    s.size_a, s.size_b, s.size_c, s.size_d
FROM tanker_staging s
JOIN tracked_tankers tt
  ON TRIM(s.imo) = tt.imo
WHERE LOWER(TRIM(s.ship_type)) = 'tanker'
ON CONFLICT (imo) DO NOTHING;

INSERT INTO tanker_positions (
    tanker_id, timestamp_utc, latitude, longitude,
    raw_imo, imo_status, anomaly_flag,
    navigational_status, rot, sog, cog, heading,
    draught, destination, eta, position_fixing_device, data_source_type
)
SELECT
    t.tanker_id,
    TO_TIMESTAMP(s.timestamp_raw, 'DD/MM/YYYY HH24:MI:SS'),
    REPLACE(s.latitude_raw, ',', '.')::DOUBLE PRECISION,
    REPLACE(s.longitude_raw, ',', '.')::DOUBLE PRECISION,
    TRIM(s.imo),
    'valid',
    FALSE,
    NULLIF(TRIM(s.navigational_status), ''),
    NULLIF(REPLACE(s.rot_raw, ',', '.'), '')::DOUBLE PRECISION,
    NULLIF(REPLACE(s.sog_raw, ',', '.'), '')::DOUBLE PRECISION,
    NULLIF(REPLACE(s.cog_raw, ',', '.'), '')::DOUBLE PRECISION,
    NULLIF(REPLACE(s.heading_raw, ',', '.'), '')::DOUBLE PRECISION,
    NULLIF(REPLACE(s.draught_raw, ',', '.'), '')::DOUBLE PRECISION,
    NULLIF(TRIM(s.destination), ''),
    CASE
        WHEN s.eta_raw IS NULL OR TRIM(s.eta_raw) = '' THEN NULL
        ELSE TO_TIMESTAMP(s.eta_raw, 'DD/MM/YYYY HH24:MI:SS')
    END,
    NULLIF(TRIM(s.position_fixing_device), ''),
    NULLIF(TRIM(s.data_source_type), '')
FROM tanker_staging s
JOIN tankers t
  ON t.imo = TRIM(s.imo)
WHERE LOWER(TRIM(s.ship_type)) = 'tanker'
  AND TRIM(s.imo) ~ '^[0-9]{7}$'
  AND REPLACE(s.latitude_raw, ',', '.')::DOUBLE PRECISION BETWEEN -90 AND 90
  AND REPLACE(s.longitude_raw, ',', '.')::DOUBLE PRECISION BETWEEN -180 AND 180
ON CONFLICT (tanker_id, timestamp_utc, latitude, longitude)
WHERE tanker_id IS NOT NULL
DO NOTHING;

INSERT INTO tanker_positions (
    tanker_id, timestamp_utc, latitude, longitude,
    raw_imo, imo_status, anomaly_flag,
    navigational_status, rot, sog, cog, heading,
    draught, destination, eta, position_fixing_device, data_source_type
)
SELECT
    NULL,
    TO_TIMESTAMP(s.timestamp_raw, 'DD/MM/YYYY HH24:MI:SS'),
    REPLACE(s.latitude_raw, ',', '.')::DOUBLE PRECISION,
    REPLACE(s.longitude_raw, ',', '.')::DOUBLE PRECISION,
    TRIM(s.imo),
    'unknown',
    TRUE,
    NULLIF(TRIM(s.navigational_status), ''),
    NULLIF(REPLACE(s.rot_raw, ',', '.'), '')::DOUBLE PRECISION,
    NULLIF(REPLACE(s.sog_raw, ',', '.'), '')::DOUBLE PRECISION,
    NULLIF(REPLACE(s.cog_raw, ',', '.'), '')::DOUBLE PRECISION,
    NULLIF(REPLACE(s.heading_raw, ',', '.'), '')::DOUBLE PRECISION,
    NULLIF(REPLACE(s.draught_raw, ',', '.'), '')::DOUBLE PRECISION,
    NULLIF(TRIM(s.destination), ''),
    CASE
        WHEN s.eta_raw IS NULL OR TRIM(s.eta_raw) = '' THEN NULL
        ELSE TO_TIMESTAMP(s.eta_raw, 'DD/MM/YYYY HH24:MI:SS')
    END,
    NULLIF(TRIM(s.position_fixing_device), ''),
    NULLIF(TRIM(s.data_source_type), '')
FROM tanker_staging s
WHERE LOWER(TRIM(s.ship_type)) = 'tanker'
  AND (
      s.imo IS NULL
      OR TRIM(s.imo) = ''
      OR LOWER(TRIM(s.imo)) = 'unknown'
  )
  AND REPLACE(s.latitude_raw, ',', '.')::DOUBLE PRECISION BETWEEN -90 AND 90
  AND REPLACE(s.longitude_raw, ',', '.')::DOUBLE PRECISION BETWEEN -180 AND 180;
