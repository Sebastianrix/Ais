# AIS Tanker Database

http://aisdata.ais.dk/ (This is data source where from we downloaded data)

A one-day AIS dataset was used as a proof of concept to test the database design, cleaning, duplicate handling, and anomaly preservation.

## Files

* 01_schema.sql = creates database tables and indexes
* 02_Load_data.sql = loads cleaned tanker data from tanker_staging
* 03_queries.sql = contains verification and helper queries

## Main Tables

* tanker_staging -> stores raw AIS CSV data
* tracked_tankers -> stores valid tanker IMO numbers
* tankers -> stores clean tanker metadata
* tanker_positions -> stores tanker movement and position history

## Important Design Choices

* tanker_id is the primary key
* imo is used as a unique external identifier
* unknown IMO rows are preserved for anomaly and spoofing analysis

