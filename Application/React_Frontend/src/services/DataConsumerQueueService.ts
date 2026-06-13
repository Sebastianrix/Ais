import { apiV2 } from "./Api";

export const dataConsumerQueueService = {
  getAll: async () => {
    const res = await apiV2.get("/DataConsumerQueue", {
      params: {
        "api-version": "2",
      },
    });

    return res.data;
  },
};