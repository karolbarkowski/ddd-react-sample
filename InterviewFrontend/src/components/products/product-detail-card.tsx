import { useState } from "react";
import { Card, CardDescription, CardTitle } from "@/components";
import type { Product } from "@/features/products/products.types";

interface ProductDetailCardProps {
  product: Product;
  onSave?: (updatedProduct: { number: string; description: string }) => void;
}

export function ProductDetailCard({ product, onSave }: ProductDetailCardProps) {
  const [isEditing, setIsEditing] = useState(false);
  const [editedNumber, setEditedNumber] = useState(product.number);
  const [editedDescription, setEditedDescription] = useState(product.description);

  const handleEdit = () => {
    setIsEditing(true);
  };

  const handleSave = () => {
    onSave?.({ number: editedNumber, description: editedDescription });
    setIsEditing(false);
  };

  const handleCancel = () => {
    setEditedNumber(product.number);
    setEditedDescription(product.description);
    setIsEditing(false);
  };

  return (
    <Card className="group flex flex-col sm:flex-row overflow-hidden hover:shadow-md hover:bg-gray-50/90 transition-all duration-300">
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
        {isEditing ? (
          <>
            <div className="mb-4">
              <label htmlFor="product-number" className="block text-sm font-medium text-gray-700 mb-1">
                Product Number
              </label>
              <input
                id="product-number"
                type="text"
                value={editedNumber}
                onChange={(e) => setEditedNumber(e.target.value)}
                className="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-xl sm:text-2xl font-semibold"
              />
            </div>
            <div className="mb-4 flex-1">
              <label htmlFor="product-description" className="block text-sm font-medium text-gray-700 mb-1">
                Description
              </label>
              <textarea
                id="product-description"
                value={editedDescription}
                onChange={(e) => setEditedDescription(e.target.value)}
                rows={4}
                className="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-sm sm:text-base resize-none"
              />
            </div>
            <div className="flex gap-2 mt-auto">
              <button
                onClick={handleSave}
                className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition-colors font-medium"
              >
                Save
              </button>
              <button
                onClick={handleCancel}
                className="px-4 py-2 bg-gray-200 text-gray-800 rounded-md hover:bg-gray-300 transition-colors font-medium"
              >
                Cancel
              </button>
            </div>
          </>
        ) : (
          <>
            <CardTitle className="mb-4 text-xl sm:text-2xl">
              {product.number}
            </CardTitle>
            <CardDescription className="text-sm sm:text-base flex-1">
              {product.description}
            </CardDescription>
            <div className="mt-4">
              <button
                onClick={handleEdit}
                className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition-colors font-medium"
              >
                Edit
              </button>
            </div>
          </>
        )}
      </div>
    </Card>
  );
}
