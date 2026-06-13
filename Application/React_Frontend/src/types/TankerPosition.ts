export interface TankerPosition {
  Position_Id: number | null;
  Tanker_Id: number | null;
  Voyage_Id: number | null;
  Staging_Id: number | null;

  Timestamp: string;

  Longitude: number;
  Latitude: number;

  Raw_Imo: string | null;
  Imo_Status: string | null;

  Raw_Mmsi: string | null;
  Mmsi_Status: string | null;

  Anomaly_Flag: boolean;

  Navigational_Status: string | null;

  Rot: number | null;
  Sog: number | null;
  Cog: number | null;
  Heading: number | null;

  Draught: number | null;

  Destination: string | null;

  Eta: string | null;

  Position_Fixing_Device: string | null;

  Data_Source_Type: string;

  Created_At: string;
}