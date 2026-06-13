import { apiV2 } from "./Api";

export interface TankerPositionQuery {
  page?: number;
  pageSize?: number;
  tankerId?: number;
  startDate?: string;
  endDate?: string;
  imo?: string;
}

export const tankerPositionService = {
  getAll: async (query: TankerPositionQuery = {}) => {
    const res = await apiV2.get("/v2/TankerPositions", {
      params: query,
    });

    return res.data;
  },
};