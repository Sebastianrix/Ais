export interface TankerStaging {
  Staging_Id: number | null;

  Timestamp_Raw: string | null;
  Type_Of_Mobile: string | null;

  Mmsi: string | null;

  Latitude_Raw: string | null;
  Longitude_Raw: string | null;

  Navigational_Status: string | null;

  Rot_Raw: string | null;
  Sog_Raw: string | null;
  Cog_Raw: string | null;
  Heading_Raw: string | null;

  Imo: string | null;
  Callsign: string | null;
  Vessel_Name: string | null;

  Ship_Type: string | null;
  Cargo_Type: string | null;

  Width_Raw: string | null;
  Length_Raw: string | null;

  Position_Fixing_Device: string | null;

  Draught_Raw: string | null;
  Destination: string | null;

  Eta_Raw: string | null;

  Data_Source_Type: string | null;

  Size_A: number | null;
  Size_B: number | null;
  Size_C: number | null;
  Size_D: number | null;

  Source_File_Name: string | null;

  Source_Batch_Date: string | null;

  Created_At: string | null;
  Updated_At: string | null;
}