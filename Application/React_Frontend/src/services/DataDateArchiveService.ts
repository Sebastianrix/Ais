import { apiV2 } from "./Api";

export const dataDateArchiveService = {
  getAll: async () => {
    const res = await apiV2.get("/DataDateArchive", {
      params: {
        "api-version": "2",
      },
    });

    return res.data;
  },
};