import { api } from "./Api";
import type { DataDateArchive } from "../types/DataDateArchive";

export const dataDateArchiveService = {
  getAll: async (): Promise<DataDateArchive[]> => {
    const res = await api.get("/DataDateArchive");
    return res.data;
  },
};