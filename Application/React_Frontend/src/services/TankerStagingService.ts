import { api } from "./Api";
import type { TankerPosition } from "../types/TankerPosition";

export const tankerPositionService = {
  getAll: async (): Promise<TankerPosition[]> => {
    const res = await api.get("/v1/TankerPositions");
    return res.data;
  },
};