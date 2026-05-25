--This one is the heaviest of our detections, but it's parameterized for a day only. Honestly just looking at the large amount of
--discarded postions, I think this would absolutely bload the table, if run rampid. Well its heavy and requires day as a prameter. 
INSERT INTO anomaly_flags (tanker_id, staging_id, anomaly_type_id, source, notes)
SELECT t.tanker_id, s.staging_id,
       (SELECT anomaly_type_id FROM anomaly_types WHERE code = 'INVALID_POSITION'),
       'system',
       'latlon out of range '  s.latitude_raw  ', '  s.longitude_raw
FROM tanker_staging s
LEFT JOIN tankers t ON t.imo = TRIM(s.imo)
WHERE s.source_batch_date = %(target_day)s
  AND LOWER(TRIM(s.ship_type)) = 'tanker'
  AND s.latitude_raw  ~ '^-[0-9.,]+$' -- We cast only numbers, so character artifects wont be picked up
  AND s.longitude_raw ~ '^-[0-9.,]+$'
  AND (
        REPLACE(s.latitude_raw,  ',', '.')DOUBLE PRECISION NOT BETWEEN -90  AND 90
     OR REPLACE(s.longitude_raw, ',', '.')DOUBLE PRECISION NOT BETWEEN -180 AND 180
      )
  AND NOT EXISTS (
      SELECT 1 FROM anomaly_flags af
      WHERE af.staging_id = s.staging_id
        AND af.anomaly_type_id = (SELECT anomaly_type_id FROM anomaly_types WHERE code='INVALID_POSITION')
  );