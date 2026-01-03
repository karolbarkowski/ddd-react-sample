import { useParams, Navigate } from "react-router-dom";
import {
  useGetProductsQuery,
  useUpdateProductMutation,
} from "@/features/products/products.api";
import { FetchingProducts, ProductDetailCard } from "@/components";
import { MoveLeft } from "lucide-react";

export const ProductDetail = () => {
  const { id } = useParams<{ id: string }>();
  const { data, isLoading } = useGetProductsQuery();
  const [updateProduct, { isLoading: isSaving }] = useUpdateProductMutation();

  const product = data?.find((p) => p.id === Number(id));

  const onSave = async (updatedProduct: {
    number: string;
    name: string;
    description: string;
  }) => {
    if (!product) return;

    await updateProduct({
      productId: product.id,
      ...updatedProduct,
    }).unwrap();
  };

  if (isLoading) {
    return <FetchingProducts />;
  }

  if (!product) {
    return <Navigate to="/404" replace />;
  }

  return (
    <div className="lg:w-9/12 w-full  mx-auto space-y-4">
      <header
        className="group flex flex-row gap-4 flex-1 items-center cursor-pointer mb-8"
        onClick={() => window.history.back()}
      >
        <MoveLeft className="group-hover:-ml-1 group-hover:mr-1 transition-all duration-150" />
        <span>Back</span>
      </header>
      <ProductDetailCard
        product={product}
        onSave={onSave}
        isSaving={isSaving}
      />
    </div>
  );
};
