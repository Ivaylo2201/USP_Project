import axios from "axios";

export const http = axios.create({
  baseURL: 'https://localhost:7168/api',
})