import { baseApi } from "../../config/api";
import { type Product } from "./products.types";

interface UpdateProductRequest {
  productId: number;
  name: string;
  number: string;
  description: string;
}

export const productsApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    getProducts: builder.query<Product[], void>({
      query: () => "/Products",
      providesTags: ["Products"],
    }),
    updateProduct: builder.mutation<Product, UpdateProductRequest>({
      query: ({ productId, ...body }) => ({
        url: `/Products/${productId}`,
        method: "PUT",
        body: { productId, ...body },
      }),
      invalidatesTags: ["Products"],
    }),
  }),
});

export const { useGetProductsQuery, useUpdateProductMutation } = productsApi;
