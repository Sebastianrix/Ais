--This script is: "Idempotent". Meaning it' safe to re-run. We had to decoupled it from ingestion. Both because we already have 2 bilion rows of data, and secondly we can now retroactively anomaly detect, when we scale more types

-- This runs on Tankers, only a few tusands. can run on the whole table

-- COUNTRY CODE :  INVALID_MMSI_MID. The country code is in the MMSI prefix. So we see if it resolve to a real country. 
--If not, it's (flag spoofing signal). Well something like, more like my country is not excsiting, so it's disguise, but spoofing implies fronting a trusted source I think. 
INSERT INTO anomaly_flags (tanker_id, anomaly_type_id, source, notes)
SELECT t.tanker_id,
       (SELECT anomaly_type_id FROM anomaly_types WHERE code = 'INVALID_MMSI_MID'),
       'system',
       'MMSI prefix ' || COALESCE(LEFT(TRIM(t.mmsi), 3), 'NULL') || ' not a known MID'
FROM tankers t
WHERE t.flag = 'UN'
  AND NOT EXISTS (
      SELECT 1 FROM anomaly_flags af
      WHERE af.tanker_id = t.tanker_id
        AND af.anomaly_type_id = (SELECT anomaly_type_id FROM anomaly_types WHERE code='INVALID_MMSI_MID')
  );