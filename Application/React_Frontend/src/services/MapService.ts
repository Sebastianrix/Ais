import { api } from "./Api";
import type { VesselMapPosition } from "../types/VesselMapPosition";

export const mapService = {
  get: async (sinceHours: number = 168): Promise<VesselMapPosition[]> => {
    const res = await api.get("/v1/Map", {
      params: { sinceHours },
    });

    return res.data;
  },
};