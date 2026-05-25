SELECT COUNT(*) AS staging_rows FROM tanker_staging;

SELECT COUNT(*) AS tanker_rows
FROM tanker_staging
WHERE LOWER(TRIM(ship_type)) = 'tanker';

SELECT COUNT(*) AS tracked_imos FROM tracked_tankers;
SELECT COUNT(*) AS tankers_count FROM tankers;
SELECT COUNT(*) AS positions_count FROM tanker_positions;

SELECT imo_status, COUNT(*)
FROM tanker_positions
GROUP BY imo_status;