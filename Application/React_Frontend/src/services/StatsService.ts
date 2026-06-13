import { api } from "./Api";
import type { Stats } from "../types/Stats";

export const statsService = {
  getStats: async (): Promise<Stats> => {
    const res = await api.get("/Stats");
    return res.data;
  },
};