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
ALTER TABLE tankers DROP CONSTRAINT IF EXISTS uq_tankers_imo;
ALTER TABLE tankers ADD CONSTRAINT uq_tankers_imo UNIQUE (imo);
CREATE TABLE IF NOT EXISTS tracked_tankers (
    tracked_id BIGSERIAL PRIMARY KEY,
    imo VARCHAR(20),
    mmsi VARCHAR(20),
    source_trial VARCHAR(100),
    notes TEXT,
    is_active BOOLEAN DEFAULT FALSE,
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
    voyage_id BIGINT,
    staging_id BIGINT,
    timestamp_utc TIMESTAMP NOT NULL,
    latitude DOUBLE PRECISION NOT NULL,
    longitude DOUBLE PRECISION NOT NULL,
    raw_imo VARCHAR(20),
    imo_status VARCHAR(20),
    raw_mmsi VARCHAR(20),
    mmsi_status VARCHAR(20),
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
    ON DELETE CASCADE,

CONSTRAINT fk_positions_voyage
    FOREIGN KEY (voyage_id)
    REFERENCES voyages(voyage_id)
    ON DELETE SET NULL,

CONSTRAINT fk_positions_staging
    FOREIGN KEY (staging_id)
    REFERENCES tanker_staging(staging_id)
    ON DELETE SET NULL
);



-- Anomality table










-- Lookup table ( this helps to fill flag column on the vessels. 
-- You basiclly derive the country from MMSI.
CREATE TABLE IF NOT EXISTS mmsi_country_codes (
    mid_code VARCHAR(3) PRIMARY KEY,
    country_code VARCHAR(2) NOT NULL UNIQUE,
    country_name VARCHAR(100) NOT NULL,
    region VARCHAR(50),
    created_at TIMESTAMP DEFAULT NOW()
);
-- https://www.itu.int/en/ITU-R/terrestrial/fmd/pages/mid.aspx
-- so this site have the look-up table. We should dynamiclly read this,
-- since it could be updated, but we really don't have time and it's
-- maybe it even useless. Just this could be Data-Drift.
INSERT INTO mmsi_country_codes (mid_code, country_code, country_name, region) VALUES
('201', 'AL', 'Albania', 'Southeast Europe'), -- I totally wrote all this manually ( not) 
('202', 'AD', 'Andorra', 'Southwest Europe'),
('203', 'AT', 'Austria', 'Central Europe'),
('204', 'PT', 'Portugal (Azores)', 'Atlantic'),
('205', 'BE', 'Belgium', 'Northern Europe'),
('206', 'BY', 'Belarus', 'Eastern Europe'),
('207', 'BG', 'Bulgaria', 'Southeast Europe'),
('208', 'VA', 'Vatican City', 'Southern Europe'),
('209', 'CY', 'Cyprus', 'Eastern Mediterranean'),
('210', 'CY', 'Cyprus', 'Eastern Mediterranean'),
('211', 'DE', 'Germany', 'Northern Europe'),
('212', 'CY', 'Cyprus', 'Eastern Mediterranean'),
('213', 'GE', 'Georgia', 'South Caucasus'),
('214', 'MD', 'Moldova', 'Eastern Europe'),
('215', 'MT', 'Malta', 'Central Mediterranean'),
('216', 'AM', 'Armenia', 'South Caucasus'),
('218', 'DE', 'Germany', 'Northern Europe'),
('219', 'DK', 'Denmark', 'Northern Europe'),
('220', 'DK', 'Greenland', 'North Atlantic'),
('224', 'ES', 'Spain', 'Southwest Europe'),
('225', 'ES', 'Spain', 'Southwest Europe'),
('226', 'FR', 'France', 'Western Europe'),
('227', 'FR', 'France', 'Western Europe'),
('228', 'FR', 'France', 'Western Europe'),
('229', 'MT', 'Malta', 'Central Mediterranean'),
('230', 'FI', 'Finland', 'Northern Europe'),
('231', 'DK', 'Faroe Islands', 'North Atlantic'),
('232', 'GB', 'United Kingdom', 'Northern Europe'),
('233', 'GB', 'United Kingdom', 'Northern Europe'),
('234', 'GB', 'United Kingdom', 'Northern Europe'),
('235', 'GB', 'United Kingdom', 'Northern Europe'),
('236', 'GB', 'Gibraltar', 'Mediterranean'),
('237', 'GR', 'Greece', 'Southeast Europe'),
('238', 'HR', 'Croatia', 'Southeast Europe'),
('239', 'GR', 'Greece', 'Southeast Europe'),
('240', 'GR', 'Greece', 'Southeast Europe'),
('241', 'GR', 'Greece', 'Southeast Europe'),
('242', 'MA', 'Morocco', 'North Africa'),
('243', 'HU', 'Hungary', 'Central Europe'),
('244', 'NL', 'Netherlands', 'Northern Europe'),
('245', 'NL', 'Netherlands', 'Northern Europe'),
('246', 'NL', 'Netherlands', 'Northern Europe'),
('247', 'IT', 'Italy', 'Southern Europe'),
('248', 'MT', 'Malta', 'Central Mediterranean'),
('249', 'MT', 'Malta', 'Central Mediterranean'),
('250', 'IE', 'Ireland', 'Western Europe'),
('251', 'IS', 'Iceland', 'North Atlantic'),
('252', 'LI', 'Liechtenstein', 'Central Europe'),
('253', 'LU', 'Luxembourg', 'Western Europe'),
('254', 'MC', 'Monaco', 'Southwest Europe'),
('255', 'PT', 'Madeira', 'Atlantic'),
('256', 'MT', 'Malta', 'Central Mediterranean'),
('257', 'NO', 'Norway', 'Northern Europe'),
('258', 'NO', 'Norway', 'Northern Europe'),
('259', 'NO', 'Norway', 'Northern Europe'),
('261', 'PL', 'Poland', 'Northern Europe'),
('262', 'ME', 'Montenegro', 'Southeast Europe'),
('263', 'PT', 'Portugal', 'Southwest Europe'),
('264', 'RO', 'Romania', 'Southeast Europe'),
('265', 'SE', 'Sweden', 'Northern Europe'),
('266', 'SE', 'Sweden', 'Northern Europe'),
('267', 'SK', 'Slovakia', 'Central Europe'),
('268', 'SM', 'San Marino', 'Southern Europe'),
('269', 'CH', 'Switzerland', 'Central Europe'),
('270', 'CZ', 'Czech Republic', 'Central Europe'),
('271', 'TR', 'Turkey', 'Eastern Mediterranean'),
('272', 'UA', 'Ukraine', 'Eastern Europe'),
('273', 'RU', 'Russia', 'Eastern Europe'),
('274', 'MK', 'North Macedonia', 'Southeast Europe'),
('275', 'LV', 'Latvia', 'Northern Europe'),
('276', 'EE', 'Estonia', 'Northern Europe'),
('277', 'LT', 'Lithuania', 'Northern Europe'),
('278', 'SI', 'Slovenia', 'Central Europe'),
('279', 'RS', 'Serbia', 'Southeast Europe'),
('301', 'AI', 'Anguilla', 'Caribbean'),
('303', 'US', 'Alaska', 'North America'),
('304', 'AG', 'Antigua and Barbuda', 'Caribbean'),
('305', 'AG', 'Antigua and Barbuda', 'Caribbean'),
('306', 'NL', 'Netherlands Caribbean', 'Caribbean'),
('307', 'AW', 'Aruba', 'Caribbean'),
('308', 'BS', 'Bahamas', 'Caribbean'),
('309', 'BS', 'Bahamas', 'Caribbean'),
('310', 'BM', 'Bermuda', 'North Atlantic'),
('311', 'BS', 'Bahamas', 'Caribbean'),
('312', 'BZ', 'Belize', 'Central America'),
('314', 'BB', 'Barbados', 'Caribbean'),
('316', 'CA', 'Canada', 'North America'),
('319', 'KY', 'Cayman Islands', 'Caribbean'),
('321', 'CR', 'Costa Rica', 'Central America'),
('323', 'CU', 'Cuba', 'Caribbean'),
('325', 'DM', 'Dominica', 'Caribbean'),
('327', 'DO', 'Dominican Republic', 'Caribbean'),
('329', 'GP', 'Guadeloupe', 'Caribbean'),
('330', 'GD', 'Grenada', 'Caribbean'),
('331', 'GL', 'Greenland', 'North Atlantic'),
('332', 'GT', 'Guatemala', 'Central America'),
('334', 'HN', 'Honduras', 'Central America'),
('336', 'HT', 'Haiti', 'Caribbean'),
('338', 'US', 'United States', 'North America'),
('339', 'JM', 'Jamaica', 'Caribbean'),
('341', 'KN', 'Saint Kitts and Nevis', 'Caribbean'),
('343', 'LC', 'Saint Lucia', 'Caribbean'),
('345', 'MX', 'Mexico', 'North America'),
('347', 'MQ', 'Martinique', 'Caribbean'),
('348', 'MS', 'Montserrat', 'Caribbean'),
('350', 'NI', 'Nicaragua', 'Central America'),
('351', 'PA', 'Panama', 'Central America'),
('352', 'PA', 'Panama', 'Central America'),
('353', 'PA', 'Panama', 'Central America'),
('354', 'PA', 'Panama', 'Central America'),
('355', 'PA', 'Panama', 'Central America'),
('356', 'PA', 'Panama', 'Central America'),
('357', 'PA', 'Panama', 'Central America'),
('358', 'PR', 'Puerto Rico', 'Caribbean'),
('359', 'SV', 'El Salvador', 'Central America'),
('361', 'PM', 'Saint Pierre and Miquelon', 'North Atlantic'),
('362', 'TT', 'Trinidad and Tobago', 'Caribbean'),
('364', 'TC', 'Turks and Caicos Islands', 'Caribbean'),
('366', 'US', 'United States', 'North America'),
('367', 'US', 'United States', 'North America'),
('368', 'US', 'United States', 'North America'),
('369', 'US', 'United States', 'North America'),
('370', 'PA', 'Panama', 'Central America'),
('371', 'PA', 'Panama', 'Central America'),
('372', 'PA', 'Panama', 'Central America'),
('373', 'PA', 'Panama', 'Central America'),
('374', 'PA', 'Panama', 'Central America'),
('375', 'VC', 'Saint Vincent and the Grenadines', 'Caribbean'),
('376', 'VC', 'Saint Vincent and the Grenadines', 'Caribbean'),
('377', 'VC', 'Saint Vincent and the Grenadines', 'Caribbean'),
('378', 'VG', 'British Virgin Islands', 'Caribbean'),
('379', 'VI', 'U.S. Virgin Islands', 'Caribbean'),
('401', 'AF', 'Afghanistan', 'South Asia'),
('403', 'SA', 'Saudi Arabia', 'Middle East'),
('405', 'BD', 'Bangladesh', 'South Asia'),
('408', 'BH', 'Bahrain', 'Persian Gulf'),
('410', 'BT', 'Bhutan', 'South Asia'),
('412', 'CN', 'China', 'East Asia'),
('413', 'CN', 'China', 'East Asia'),
('414', 'CN', 'China', 'East Asia'),
('416', 'TW', 'Taiwan', 'East Asia'),
('417', 'LK', 'Sri Lanka', 'South Asia'),
('419', 'IN', 'India', 'South Asia'),
('422', 'IR', 'Iran', 'Middle East'),
('423', 'AZ', 'Azerbaijan', 'South Caucasus'),
('425', 'IQ', 'Iraq', 'Middle East'),
('428', 'IL', 'Israel', 'Middle East'),
('431', 'JP', 'Japan', 'East Asia'),
('432', 'JP', 'Japan', 'East Asia'),
('434', 'TM', 'Turkmenistan', 'Central Asia'),
('436', 'KZ', 'Kazakhstan', 'Central Asia'),
('437', 'UZ', 'Uzbekistan', 'Central Asia'),
('438', 'JO', 'Jordan', 'Middle East'),
('440', 'KR', 'South Korea', 'East Asia'),
('441', 'KR', 'South Korea', 'East Asia'),
('443', 'PS', 'Palestine', 'Middle East'),
('445', 'KP', 'North Korea', 'East Asia'),
('447', 'KW', 'Kuwait', 'Persian Gulf'),
('450', 'LB', 'Lebanon', 'Middle East'),
('451', 'KG', 'Kyrgyzstan', 'Central Asia'),
('453', 'MO', 'Macao', 'East Asia'),
('455', 'MV', 'Maldives', 'South Asia'),
('457', 'MN', 'Mongolia', 'East Asia'),
('459', 'NP', 'Nepal', 'South Asia'),
('461', 'OM', 'Oman', 'Arabian Peninsula'),
('463', 'PK', 'Pakistan', 'South Asia'),
('466', 'QA', 'Qatar', 'Persian Gulf'),
('468', 'SY', 'Syria', 'Middle East'),
('470', 'AE', 'United Arab Emirates', 'Persian Gulf'),
('471', 'AE', 'United Arab Emirates', 'Persian Gulf'),
('472', 'TJ', 'Tajikistan', 'Central Asia'),
('473', 'YE', 'Yemen', 'Arabian Peninsula'),
('475', 'YE', 'Yemen', 'Arabian Peninsula'),
('477', 'HK', 'Hong Kong', 'East Asia'),
('478', 'BA', 'Bosnia and Herzegovina', 'Southeast Europe'),
('501', 'AQ', 'French Southern Territories', 'Antarctic'),
('503', 'AU', 'Australia', 'Oceania'),
('506', 'MM', 'Myanmar', 'Southeast Asia'),
('508', 'BN', 'Brunei', 'Southeast Asia'),
('510', 'FM', 'Micronesia', 'Pacific'),
('511', 'PW', 'Palau', 'Pacific'),
('512', 'NZ', 'New Zealand', 'Oceania'),
('514', 'KH', 'Cambodia', 'Southeast Asia'),
('515', 'KH', 'Cambodia', 'Southeast Asia'),
('516', 'CX', 'Christmas Island', 'Indian Ocean'),
('518', 'CK', 'Cook Islands', 'Pacific'),
('520', 'FJ', 'Fiji', 'Pacific'),
('523', 'CC', 'Cocos Islands', 'Indian Ocean'),
('525', 'ID', 'Indonesia', 'Southeast Asia'),
('529', 'KI', 'Kiribati', 'Pacific'),
('531', 'LA', 'Laos', 'Southeast Asia'),
('533', 'MY', 'Malaysia', 'Southeast Asia'),
('536', 'MP', 'Northern Mariana Islands', 'Pacific'),
('538', 'MH', 'Marshall Islands', 'Pacific'),
('540', 'NC', 'New Caledonia', 'Pacific'),
('542', 'NU', 'Niue', 'Pacific'),
('544', 'NR', 'Nauru', 'Pacific'),
('546', 'PF', 'French Polynesia', 'Pacific'),
('548', 'PH', 'Philippines', 'Southeast Asia'),
('550', 'TL', 'Timor-Leste', 'Southeast Asia'),
('553', 'PG', 'Papua New Guinea', 'Pacific'),
('555', 'PN', 'Pitcairn Islands', 'Pacific'),
('557', 'SB', 'Solomon Islands', 'Pacific'),
('559', 'AS', 'American Samoa', 'Pacific'),
('561', 'WS', 'Samoa', 'Pacific'),
('563', 'SG', 'Singapore', 'Southeast Asia'),
('564', 'SG', 'Singapore', 'Southeast Asia'),
('565', 'SG', 'Singapore', 'Southeast Asia'),
('566', 'SG', 'Singapore', 'Southeast Asia'),
('567', 'TH', 'Thailand', 'Southeast Asia'),
('570', 'TO', 'Tonga', 'Pacific'),
('572', 'TV', 'Tuvalu', 'Pacific'),
('574', 'VN', 'Vietnam', 'Southeast Asia'),
('576', 'VU', 'Vanuatu', 'Pacific'),
('577', 'VU', 'Vanuatu', 'Pacific'),
('578', 'WF', 'Wallis and Futuna', 'Pacific'),
('601', 'ZA', 'South Africa', 'Southern Africa'),
('603', 'AO', 'Angola', 'Southern Africa'),
('605', 'DZ', 'Algeria', 'North Africa'),
('607', 'TF', 'French Southern Territories', 'Indian Ocean'),
('608', 'SH', 'Ascension Island', 'Atlantic'),
('609', 'BI', 'Burundi', 'East Africa'),
('610', 'BJ', 'Benin', 'West Africa'),
('611', 'BW', 'Botswana', 'Southern Africa'),
('612', 'CF', 'Central African Republic', 'Central Africa'),
('613', 'CM', 'Cameroon', 'Central Africa'),
('615', 'CG', 'Congo', 'Central Africa'),
('616', 'KM', 'Comoros', 'Indian Ocean'),
('617', 'CV', 'Cabo Verde', 'Atlantic'),
('618', 'TF', 'Crozet Archipelago', 'Indian Ocean'),
('619', 'CI', 'Côte d''Ivoire', 'West Africa'),
('620', 'KM', 'Comoros', 'Indian Ocean'),
('621', 'DJ', 'Djibouti', 'East Africa'),
('622', 'EG', 'Egypt', 'North Africa'),
('624', 'ET', 'Ethiopia', 'East Africa'),
('625', 'ER', 'Eritrea', 'East Africa'),
('626', 'GA', 'Gabon', 'Central Africa'),
('627', 'GH', 'Ghana', 'West Africa'),
('629', 'GM', 'Gambia', 'West Africa'),
('630', 'GW', 'Guinea-Bissau', 'West Africa'),
('631', 'GQ', 'Equatorial Guinea', 'Central Africa'),
('632', 'GN', 'Guinea', 'West Africa'),
('633', 'BF', 'Burkina Faso', 'West Africa'),
('634', 'KE', 'Kenya', 'East Africa'),
('635', 'TF', 'Kerguelen Islands', 'Indian Ocean'),
('636', 'LR', 'Liberia', 'West Africa'),
('637', 'LR', 'Liberia', 'West Africa'),
('638', 'SS', 'South Sudan', 'East Africa'),
('642', 'LY', 'Libya', 'North Africa'),
('644', 'LS', 'Lesotho', 'Southern Africa'),
('645', 'MU', 'Mauritius', 'Indian Ocean'),
('647', 'MG', 'Madagascar', 'Indian Ocean'),
('649', 'ML', 'Mali', 'West Africa'),
('650', 'MZ', 'Mozambique', 'Southern Africa'),
('654', 'MR', 'Mauritania', 'West Africa'),
('655', 'MW', 'Malawi', 'Southern Africa'),
('656', 'NE', 'Niger', 'West Africa'),
('657', 'NG', 'Nigeria', 'West Africa'),
('659', 'NA', 'Namibia', 'Southern Africa'),
('660', 'RE', 'Réunion', 'Indian Ocean'),
('661', 'RW', 'Rwanda', 'Central Africa'),
('662', 'SD', 'Sudan', 'East Africa'),
('663', 'SN', 'Senegal', 'West Africa'),
('664', 'SC', 'Seychelles', 'Indian Ocean'),
('665', 'SH', 'Saint Helena', 'Atlantic'),
('666', 'SO', 'Somalia', 'East Africa'),
('667', 'SL', 'Sierra Leone', 'West Africa'),
('668', 'ST', 'São Tomé and Príncipe', 'Central Africa'),
('669', 'SZ', 'Eswatini', 'Southern Africa'),
('670', 'TD', 'Chad', 'Central Africa'),
('671', 'TG', 'Togo', 'West Africa'),
('672', 'TN', 'Tunisia', 'North Africa'),
('674', 'TZ', 'Tanzania', 'East Africa'),
('675', 'UG', 'Uganda', 'East Africa'),
('676', 'CD', 'Democratic Republic of the Congo', 'Central Africa'),
('677', 'TZ', 'Tanzania', 'East Africa'),
('678', 'ZM', 'Zambia', 'Southern Africa'),
('679', 'ZW', 'Zimbabwe', 'Southern Africa'),
('701', 'AR', 'Argentina', 'South America'),
('710', 'BR', 'Brazil', 'South America'),
('720', 'BO', 'Bolivia', 'South America'),
('725', 'CL', 'Chile', 'South America'),
('730', 'CO', 'Colombia', 'South America'),
('735', 'EC', 'Ecuador', 'South America'),
('740', 'FK', 'Falkland Islands', 'South Atlantic'),
('745', 'GF', 'French Guiana', 'South America'),
('750', 'GY', 'Guyana', 'South America'),
('755', 'PY', 'Paraguay', 'South America'),
('760', 'PE', 'Peru', 'South America'),
('765', 'SR', 'Suriname', 'South America'),
('770', 'UY', 'Uruguay', 'South America'),
('775', 'VE', 'Venezuela', 'South America');


-- Index / These are important since we are running old-hardware!
-- We are currently deployed on phyiscal harddrives.

CREATE INDEX IF NOT EXISTS idx_mmsi_country_mid ON mmsi_country_codes(mid_code);

ALTER TABLE tankers 
  ADD COLUMN IF NOT EXISTS flag_country_code VARCHAR(2);

CREATE INDEX IF NOT EXISTS idx_tankers_flag_country_code
  ON tankers(flag_country_code);

CREATE INDEX IF NOT EXISTS idx_tracked_tankers_imo
ON tracked_tankers(imo);

CREATE INDEX IF NOT EXISTS idx_tankers_imo
ON tankers(imo);

CREATE INDEX IF NOT EXISTS idx_tankers_mmsi
ON tankers(mmsi);

CREATE INDEX IF NOT EXISTS idx_voyages_tanker_id
ON voyages(tanker_id);

CREATE INDEX IF NOT EXISTS idx_voyages_start_time
ON voyages(start_time_utc);

CREATE INDEX IF NOT EXISTS idx_voyages_end_time
ON voyages(end_time_utc);

CREATE INDEX IF NOT EXISTS idx_tanker_positions_tanker_id
ON tanker_positions(tanker_id);

CREATE INDEX IF NOT EXISTS idx_tanker_positions_voyage_id
ON tanker_positions(voyage_id);

CREATE INDEX IF NOT EXISTS idx_tanker_positions_staging_id
ON tanker_positions(staging_id);

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

CREATE INDEX IF NOT EXISTS idx_tanker_staging_staging_id_desc 
  ON tanker_staging (staging_id DESC);

CREATE INDEX IF NOT EXISTS idx_tanker_staging_ship_type 
  ON tanker_staging (LOWER(TRIM(ship_type)));

CREATE INDEX idx_tanker_timestamp_raw 
ON tankerstaging("Timestamp_Raw" DESC);
