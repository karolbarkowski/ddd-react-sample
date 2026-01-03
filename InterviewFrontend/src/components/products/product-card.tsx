import { Card, CardDescription, CardTitle } from "@/components";
import type { Product } from "@/features/products/products.types";

export function ProductCard({ product }: { product: Product }) {
  return (
    <Card className="group flex flex-col sm:flex-row overflow-hidden cursor-pointer hover:shadow-md hover:bg-gray-50/90  transition-all duration-300">
      <div className="w-full sm:w-48 md:w-64 lg:w-80 h-48 sm:h-auto shrink-0 overflow-hidden">
        <img
          src={
            product.images.length > 0
              ? product.images[0]?.url
              : "/product_image_not_available.png"
          }
          alt={
            product.images.length > 0
              ? product.images[0]?.name
              : "Product image not available"
          }
          className="w-full h-full object-cover aspect-4/3 transition-transform duration-300 group-hover:scale-105"
        />
      </div>
      <div className="flex-1 p-6 sm:p-8 flex flex-col">
        <CardTitle className="mb-4 text-xl sm:text-2xl">
          {product.number}
        </CardTitle>
        <CardDescription className="text-sm sm:text-base">
          {product.description}
        </CardDescription>
      </div>
    </Card>
  );
}
