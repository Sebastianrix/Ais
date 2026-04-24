CREATE TABLE IF NOT EXISTS tanker_staging (
    staging_id BIGSERIAL PRIMARY KEY,
    timestamp_raw VARCHAR(100),
    type_of_mobile VARCHAR(100),
    mmsi VARCHAR(20),
    latitude_raw VARCHAR(50),
    longitude_raw VARCHAR(50),
    navigational_status VARCHAR(100),
    rot_raw VARCHAR(50),
    sog_raw VARCHAR(50),
    cog_raw VARCHAR(50),
    heading_raw VARCHAR(50),
    imo VARCHAR(20),
    callsign VARCHAR(50),
    vessel_name VARCHAR(255),
    ship_type VARCHAR(100),
    cargo_type VARCHAR(100),
    width_raw VARCHAR(50),
    length_raw VARCHAR(50),
    position_fixing_device VARCHAR(100),
    draught_raw VARCHAR(50),
    destination VARCHAR(255),
    eta_raw VARCHAR(100),
    data_source_type VARCHAR(50),
    size_a NUMERIC(10,2),
    size_b NUMERIC(10,2),
    size_c NUMERIC(10,2),
    size_d NUMERIC(10,2),
    source_file_name VARCHAR(255),
    source_batch_date DATE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
CREATE TABLE IF NOT EXISTS tankers (
    tanker_id BIGSERIAL PRIMARY KEY,
    imo VARCHAR(20),
    mmsi VARCHAR(20),
    vessel_name VARCHAR(255),
    callsign VARCHAR(50),
    ship_type VARCHAR(100),
    cargo_type VARCHAR(100),
    type_of_mobile VARCHAR(100),
    width NUMERIC(10,2),
    length NUMERIC(10,2),
    size_a NUMERIC(10,2),
    size_b NUMERIC(10,2),
    size_c NUMERIC(10,2),
    size_d NUMERIC(10,2),
    flag VARCHAR(100),
    first_seen_at TIMESTAMP,
    last_seen_at TIMESTAMP,
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS tracked_tankers (
    tracked_id BIGSERIAL PRIMARY KEY,
    imo VARCHAR(20),
    source_trial VARCHAR(100),
    notes TEXT,
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
CREATE TABLE IF NOT EXISTS voyages (
    voyage_id BIGSERIAL PRIMARY KEY,
    tanker_id BIGINT NOT NULL,
    voyage_status VARCHAR(30) DEFAULT 'active',
    start_time_utc TIMESTAMP NOT NULL,
    end_time_utc TIMESTAMP,
    start_position_id BIGINT,
    end_position_id BIGINT,
    start_port_name VARCHAR(255),
    end_port_name VARCHAR(255),
    destination_final VARCHAR(255),
    eta_final TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_voyages_tanker
        FOREIGN KEY (tanker_id)
        REFERENCES tankers(tanker_id)
        ON DELETE CASCADE
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