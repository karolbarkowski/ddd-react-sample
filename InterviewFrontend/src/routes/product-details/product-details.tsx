import { useParams, Navigate } from "react-router-dom";
import { useGetProductsQuery } from "@/features/products/products.api";
import { FetchingProducts, ProductDetailCard } from "@/components";
import { MoveLeft } from "lucide-react";

export const ProductDetail = () => {
  const { id } = useParams<{ id: string }>();
  const { data, isLoading } = useGetProductsQuery();

  const product = data?.find((p) => p.id === Number(id));

  if (isLoading) {
    return <FetchingProducts />;
  }

  if (!product) {
    return <Navigate to="/404" replace />;
  }

  return (
    <div className="lg:w-9/12 w-full  mx-auto space-y-4">
      <header
        className="flex flex-row gap-4 flex-1 items-center cursor-pointer mb-8"
        onClick={() => window.history.back()}
      >
        <MoveLeft className="cursor-pointer" />
        <span>Back</span>
      </header>
      <ProductDetailCard product={product} />
    </div>
  );
};
