import { apiV2 } from "./Api";
import type { Stats } from "../types/Stats";

export const statsService = {
  get: async (): Promise<Stats> => {
    const res = await apiV2.get("/Stats", {
      params: {
        "api-version": "2",
      },
    });

    return res.data;
  },
};