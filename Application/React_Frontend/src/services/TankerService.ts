import { apiV2 } from "./Api";
import type { Tanker } from "../types/Tankers";

export interface TankerQuery {
  page?: number;
  pageSize?: number;
  isActive?: boolean;
  imo?: string;
  mmsi?: string;
  search?: string;
}

export const tankerService = {
  getAll: async (
    query: TankerQuery = {}
  ): Promise<Tanker[]> => {
    const res = await apiV2.get("/v2/Tankers", {
      params: query,
    });

    return res.data.items;
  },
};