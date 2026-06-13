import { api } from "./Api";
import type { TrackedTanker } from "../types/TrackedTanker";

export const trackedTankerService = {
  getAll: async (): Promise<TrackedTanker[]> => {
    const res = await api.get("/TrackedTanker");
    return res.data;
  },
};