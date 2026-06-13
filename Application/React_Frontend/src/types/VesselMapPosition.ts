export interface VesselMapPosition {
  Tanker_Id: number;

  Mmsi: string | null;

  Vessel_Name: string | null;

  Ship_Type: string | null;

  Flag: string | null;

  Latitude: number;
  Longitude: number;

  Timestamp_Utc: string;

  Sog: number | null;
  Cog: number | null;
  Heading: number | null;

  Navigational_Status: string | null;

  Is_Anomalous: boolean;
}