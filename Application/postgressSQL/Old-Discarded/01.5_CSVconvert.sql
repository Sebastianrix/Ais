COPY tanker_staging (
    timestamp_raw, type_of_mobile, mmsi, latitude_raw, longitude_raw,
    navigational_status, rot_raw, sog_raw, cog_raw, heading_raw, imo,
    callsign, vessel_name, ship_type, cargo_type, width_raw, length_raw,
    position_fixing_device, draught_raw, destination, eta_raw,
    data_source_type, size_a, size_b, size_c, size_d
)
FROM 'C:\Users\sebas\Desktop\Thesis\aisdk-2026-04-21\aisdk-2026-04-21.csv'
DELIMITER ','
CSV HEADER;