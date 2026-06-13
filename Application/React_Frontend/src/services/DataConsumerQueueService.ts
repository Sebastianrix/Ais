import { api } from "./Api";
import type { DataConsumerQueue } from "../types/DataConsumerQueue";

export const dataConsumerQueueService = {
  getAll: async (): Promise<DataConsumerQueue[]> => {
    const res = await api.get("/DataConsumerQueue");
    return res.data;
  },
};