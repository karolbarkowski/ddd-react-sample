import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { env } from "./environment";

export const baseApi = createApi({
  reducerPath: "api",
  baseQuery: fetchBaseQuery({
    baseUrl: env.productsApiUrl,
  }),
  tagTypes: ["Products"],
  endpoints: () => ({}),
});
