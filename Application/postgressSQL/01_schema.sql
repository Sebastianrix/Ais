CREATE TABLE IF NOT EXISTS tanker_staging (
    size_b NUMERIC(10,2),
    size_c NUMERIC(10,2),
    size_d NUMERIC(10,2),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS tanker_positions (
    position_id BIGSERIAL PRIMARY KEY,
    tanker_id BIGINT,
    timestamp_utc TIMESTAMP NOT NULL,
    latitude DOUBLE PRECISION NOT NULL,
    longitude DOUBLE PRECISION NOT NULL,
    raw_imo VARCHAR(20),
    imo_status VARCHAR(20),
    anomaly_flag BOOLEAN DEFAULT FALSE,
    navigational_status VARCHAR(100),
    rot DOUBLE PRECISION,
    sog DOUBLE PRECISION,
    cog DOUBLE PRECISION,
    heading DOUBLE PRECISION,
    draught DOUBLE PRECISION,
    destination VARCHAR(255),
    eta TIMESTAMP,
    position_fixing_device VARCHAR(100),
    data_source_type VARCHAR(50),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_positions_tanker
        FOREIGN KEY (tanker_id)
        REFERENCES tankers(tanker_id)
        ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS idx_tracked_tankers_imo
ON tracked_tankers(imo);

CREATE INDEX IF NOT EXISTS idx_tankers_imo
ON tankers(imo);

CREATE INDEX IF NOT EXISTS idx_tankers_mmsi
ON tankers(mmsi);

CREATE INDEX IF NOT EXISTS idx_tanker_positions_tanker_id
ON tanker_positions(tanker_id);

CREATE INDEX IF NOT EXISTS idx_tanker_positions_timestamp
ON tanker_positions(timestamp_utc);

CREATE INDEX IF NOT EXISTS idx_tanker_positions_raw_imo
ON tanker_positions(raw_imo);

CREATE INDEX IF NOT EXISTS idx_tanker_positions_imo_status
ON tanker_positions(imo_status);

CREATE INDEX IF NOT EXISTS idx_tanker_positions_anomaly_flag
ON tanker_positions(anomaly_flag);

CREATE UNIQUE INDEX IF NOT EXISTS uq_tanker_position_known
ON tanker_positions (tanker_id, timestamp_utc, latitude, longitude)
WHERE tanker_id IS NOT NULL;