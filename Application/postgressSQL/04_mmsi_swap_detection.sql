--MMSI SWAPPING : If a vessel IMO is observed broadcasting more than one distinct MMSI

-- This script is heavier (use Group by Staging). This one should be be run once every week. 

INSERT INTO anomaly_types (code, name, description, severity)
VALUES ('MMSI_SWAP', 'MMSI swap',
        'Vessel IMO observed broadcasting more than one distinct MMSI', 'high')
ON CONFLICT (code) DO NOTHING;

INSERT INTO anomaly_flags (tanker_id, anomaly_type_id, source, notes)
SELECT t.tanker_id,
       (SELECT anomaly_type_id FROM anomaly_types WHERE code = 'MMSI_SWAP'),
       'system',
       'IMO ' || swaps.imo || ' seen with ' || swaps.mmsi_count || ' distinct MMSIs'
FROM (
    SELECT TRIM(imo) AS imo, COUNT(DISTINCT TRIM(mmsi)) AS mmsi_count
    FROM tanker_staging
    WHERE LOWER(TRIM(ship_type)) = 'tanker'
      AND TRIM(imo) ~ '^[0-9]{7}$'
      AND TRIM(mmsi) <> ''
    GROUP BY TRIM(imo)
    HAVING COUNT(DISTINCT TRIM(mmsi)) > 1
) swaps
JOIN tankers t ON t.imo = swaps.imo
WHERE NOT EXISTS (
    SELECT 1 FROM anomaly_flags af
    WHERE af.tanker_id = t.tanker_id
      AND af.anomaly_type_id = (SELECT anomaly_type_id FROM anomaly_types WHERE code='MMSI_SWAP')
);