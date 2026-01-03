"use client";
import { Link } from "react-router-dom";
import { ProductsNotFound, FetchingProducts, ProductCard } from "@/components";
import { useGetProductsQuery } from "@/features/products/products.api";

export function ProductList() {
  const { data, isLoading } = useGetProductsQuery();

  if (isLoading) {
    return <FetchingProducts />;
  }

  if (!data || data?.length === 0) {
    return <ProductsNotFound />;
  }

  return (
    <div className="lg:w-9/12 w-full  mx-auto space-y-4">
      <div className="flex flex-col gap-4">
        {data?.map((product) => (
          <Link key={product.id} to={`/products/${product.id}`}>
            <ProductCard product={product} />
          </Link>
        ))}
      </div>
    </div>
  );
}
