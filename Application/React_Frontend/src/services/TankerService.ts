import { api } from "./Api";
import type { Tanker } from "../types/Tankers";

export const tankerService = {
  getAll: async (): Promise<Tanker[]> => {
    const res = await api.get("/v1/Tankers");
    return res.data;
  },
};