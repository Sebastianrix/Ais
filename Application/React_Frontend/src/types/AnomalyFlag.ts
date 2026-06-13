export interface AnomalyFlag {
  Anomaly_Flag_Id: number;

  Tanker_Id: number | null;
  Position_Id: number | null;
  Staging_Id: number | null;

  Anomaly_Type_Id: number;

  Source: string;

  Confidence: number | null;

  Notes: string | null;

  Created_At: string | null;
}