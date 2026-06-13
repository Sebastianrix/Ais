import { apiV2 } from "./Api";
import type { VesselMapPosition } from "../types/VesselMapPosition";

export const mapService = {
  get: async (sinceHours = 168): Promise<VesselMapPosition[]> => {
    const res = await apiV2.get("/v2/Map", {
      params: { sinceHours },
    });

    return res.data;
  },
};