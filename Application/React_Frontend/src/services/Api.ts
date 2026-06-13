import axios from "axios";

export const api = axios.create({
  baseURL: "https://aismap.dk",
  headers: {
    "Content-Type": "application/json",
  },
});

export const apiV2 = axios.create({
  baseURL: "https://aismap.dk",
  headers: {
    "Content-Type": "application/json",
  },
  params: {
    "api-version": "2",
  },
});