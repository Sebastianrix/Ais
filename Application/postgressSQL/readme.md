# AIS Tanker Database

## Purpose
This database supports tanker-focused AIS analysis for anomaly detection and explainable AI.

## Files
- 01_schema.sql -> creates all database tables and indexes
- 02_load_data.sql -> loads cleaned tanker data from tanker_staging
- 03_queries.sql -> verification and API helper queries

## Tables
- tanker_staging -> raw AIS CSV import
- tracked_tankers -> unique valid tanker IMO shortlist
- tankers -> clean tanker metadata
- tanker_positions -> valid and unknown tanker positions

## Important design choices
- tanker_id is the primary key
- imo is a unique external identifier
- unknown IMO rows are preserved for anomaly/spoofing analysis
- database is used as a support tool, while AI/XAI remains the main project focus