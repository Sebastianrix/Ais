export interface Voyage {
  Voyage_Id: number | null;
  Tanker_Id: number | null;

  Voyage_status: string | null;

  Start_Time_Utc: string | null;
  End_Time_Utc: string | null;

  Start_Position_Id: number | null;
  End_Position_Id: number | null;

  Start_Port_Name: string | null;
  End_Port_Name: string | null;

  Destination_Final: string | null;

  Eta_final: string | null;

  Reated_At: string;
  Updated_At: string;
}