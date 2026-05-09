-- We expandt this now to include parameterized for day.
-- This overwhole is related Queue-table coming
-- and Date-archive-table, so we can keep track 


-- This script functions as our Transforme and load part of - ETL processing
-- And funny thing is we just process the Tanker, but we still store the raw data
-- Meaning we could potiencially scale easily to all vessels. 
--Regardless lets hope the DAGS will treat this well

INSERT INTO tankers (
    imo, mmsi, vessel_name, callsign,
    ship_type, cargo_type, type_of_mobile,
    width, length, size_a, size_b, size_c, size_d, flag
)
SELECT DISTINCT ON (TRIM(s.imo))
    TRIM(s.imo),
    TRIM(s.mmsi),
    TRIM(s.vessel_name),
    TRIM(s.callsign),
    TRIM(s.ship_type),
    TRIM(s.cargo_type),
    TRIM(s.type_of_mobile),
    NULLIF(REPLACE(s.width_raw, ',', '.'), '')::NUMERIC,
    NULLIF(REPLACE(s.length_raw, ',', '.'), '')::NUMERIC,
    s.size_a, s.size_b, s.size_c, s.size_d,
    COALESCE(mcc.country_code, 'UN') AS flag
FROM tanker_staging s
LEFT JOIN mmsi_country_codes mcc 
    ON LEFT(TRIM(s.mmsi), 3) = mcc.mid_code
WHERE s.source_batch_date = %(target_day)s
  AND LOWER(TRIM(s.ship_type)) = 'tanker'
  AND TRIM(s.imo) ~ '^[0-9]{7}$'
ON CONFLICT (imo) DO UPDATE SET
    mmsi          = EXCLUDED.mmsi,
    vessel_name   = EXCLUDED.vessel_name,
    callsign      = EXCLUDED.callsign,
    ship_type     = EXCLUDED.ship_type,
    cargo_type    = EXCLUDED.cargo_type,
    type_of_mobile = EXCLUDED.type_of_mobile,
    width         = EXCLUDED.width,
    length        = EXCLUDED.length,
    size_a        = EXCLUDED.size_a,
    size_b        = EXCLUDED.size_b,
    size_c        = EXCLUDED.size_c,
    size_d        = EXCLUDED.size_d,
    flag          = EXCLUDED.flag,
    updated_at    = CURRENT_TIMESTAMP;

-- 2) Procress positions for known tankers (valid IMO).
WITH inserted_known AS (
    INSERT INTO tanker_positions (
        tanker_id, voyage_id, staging_id, timestamp_utc, latitude, longitude,
        raw_imo, imo_status, anomaly_flag,
        navigational_status, rot, sog, cog, heading,
        draught, destination, eta, position_fixing_device, data_source_type
    )
    SELECT
        t.tanker_id,
        NULL,
        s.staging_id,
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
        CASE WHEN s.eta_raw IS NULL OR TRIM(s.eta_raw) = '' THEN NULL
             ELSE TO_TIMESTAMP(s.eta_raw, 'DD/MM/YYYY HH24:MI:SS') END,
        NULLIF(TRIM(s.position_fixing_device), ''),
        NULLIF(TRIM(s.data_source_type), '')
    FROM tanker_staging s
    JOIN tankers t ON t.imo = TRIM(s.imo)
    WHERE s.source_batch_date = %(target_day)s
      AND LOWER(TRIM(s.ship_type)) = 'tanker'
      AND TRIM(s.imo) ~ '^[0-9]{7}$'
      AND REPLACE(s.latitude_raw, ',', '.')::DOUBLE PRECISION BETWEEN -90 AND 90
      AND REPLACE(s.longitude_raw, ',', '.')::DOUBLE PRECISION BETWEEN -180 AND 180
    ON CONFLICT (tanker_id, timestamp_utc, latitude, longitude)
        WHERE tanker_id IS NOT NULL
        DO NOTHING
    RETURNING 1
),

-- 3)  Anomaly/unknown/missing (IMO).
inserted_unknown AS (
    INSERT INTO tanker_positions (
        tanker_id, voyage_id, staging_id, timestamp_utc, latitude, longitude,
        raw_imo, imo_status, anomaly_flag,
        navigational_status, rot, sog, cog, heading,
        draught, destination, eta, position_fixing_device, data_source_type
    )
    SELECT
        NULL,
        NULL,
        s.staging_id,
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
        CASE WHEN s.eta_raw IS NULL OR TRIM(s.eta_raw) = '' THEN NULL
             ELSE TO_TIMESTAMP(s.eta_raw, 'DD/MM/YYYY HH24:MI:SS') END,
        NULLIF(TRIM(s.position_fixing_device), ''),
        NULLIF(TRIM(s.data_source_type), '')
    FROM tanker_staging s
    WHERE s.source_batch_date = %(target_day)s
      AND LOWER(TRIM(s.ship_type)) = 'tanker'
      AND (s.imo IS NULL OR TRIM(s.imo) = '' OR LOWER(TRIM(s.imo)) = 'unknown')
      AND REPLACE(s.latitude_raw, ',', '.')::DOUBLE PRECISION BETWEEN -90 AND 90
      AND REPLACE(s.longitude_raw, ',', '.')::DOUBLE PRECISION BETWEEN -180 AND 180
    RETURNING 1
)

-- 4) Return total positions inserted. We need this for keeping stats for totally points analyised.
-- this is our idea anyways. 
SELECT 
    (SELECT COUNT(*) FROM inserted_known) + (SELECT COUNT(*) FROM inserted_unknown) AS positions_inserted;