import { baseApi } from "../../config/api";
import { type Product } from "./products.types";

export const productsApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    getProducts: builder.query<Product[], void>({
      query: () => "/Products",
      providesTags: ["Products"],
    }),
  }),
});

export const { useGetProductsQuery } = productsApi;
