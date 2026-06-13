export interface TrackedTanker {
  Tracked_Id: number | null;

  Imo: string | null;

  Source_Trial: string | null;
  Notes: string | null;

  Is_Active: boolean | null;

  Created_At: string;
  Updated_At: string;
}