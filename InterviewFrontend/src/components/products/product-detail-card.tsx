import { useReducer } from "react";
import { toast } from "sonner";
import { Card, CardDescription, CardTitle, Toaster } from "@/components";
import {
  Drawer,
  DrawerClose,
  DrawerContent,
  DrawerHeader,
  DrawerTitle,
} from "@/components/ui/drawer/drawer";
import type { Product } from "@/features/products/products.types";

interface ProductDetailCardProps {
  product: Product;
  onSave?: (updatedProduct: {
    number: string;
    name: string;
    description: string;
  }) => void | Promise<void>;
  isSaving?: boolean;
}

interface State {
  isDrawerOpen: boolean;
  editedNumber: string;
  editedName: string;
  editedDescription: string;
}

type Action =
  | { type: "OPEN_DRAWER" }
  | { type: "UPDATE_NUMBER"; payload: string }
  | { type: "UPDATE_NAME"; payload: string }
  | { type: "UPDATE_DESCRIPTION"; payload: string }
  | { type: "SAVE" }
  | {
      type: "CANCEL";
      payload: { number: string; name: string; description: string };
    };

function reducer(state: State, action: Action): State {
  switch (action.type) {
    case "OPEN_DRAWER":
      return { ...state, isDrawerOpen: true };
    case "UPDATE_NUMBER":
      return { ...state, editedNumber: action.payload };
    case "UPDATE_NAME":
      return { ...state, editedName: action.payload };
    case "UPDATE_DESCRIPTION":
      return { ...state, editedDescription: action.payload };
    case "SAVE":
      return { ...state, isDrawerOpen: false };
    case "CANCEL":
      return {
        isDrawerOpen: false,
        editedNumber: action.payload.number,
        editedName: action.payload.name,
        editedDescription: action.payload.description,
      };
    default:
      return state;
  }
}

export function ProductDetailCard({
  product,
  onSave,
  isSaving = false,
}: ProductDetailCardProps) {
  const [state, dispatch] = useReducer(reducer, {
    isDrawerOpen: false,
    editedNumber: product.number,
    editedName: product.name,
    editedDescription: product.description,
  });

  const handleEdit = () => {
    dispatch({ type: "OPEN_DRAWER" });
  };

  const handleSave = async () => {
    try {
      await onSave?.({
        number: state.editedNumber,
        name: state.editedName,
        description: state.editedDescription,
      });
      dispatch({ type: "SAVE" });
      toast.success("Product saved successfully");
    } catch {
      toast.error("Failed to save product");
    }
  };

  const handleCancel = () => {
    dispatch({
      type: "CANCEL",
      payload: {
        number: product.number,
        name: product.name,
        description: product.description,
      },
    });
  };

  return (
    <>
      <Card
        className="flex flex-col sm:flex-row overflow-hidden"
        data-testid="product-details"
      >
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
            className="w-full h-full object-cover aspect-4/3"
          />
        </div>
        <div className="flex-1 p-6 sm:p-8 flex flex-col">
          <CardTitle className="mb-4 text-xl sm:text-2xl">
            {product.number}
          </CardTitle>
          <CardDescription className="text-sm sm:text-base flex-1 flex-col flex mt-4">
            <span className="text-sm">({product.name})</span>
            <span>{product.description}</span>
          </CardDescription>
          <div className="mt-8 text-right">
            <button
              onClick={handleEdit}
              className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition-colors font-medium cursor-pointer"
            >
              Edit
            </button>
          </div>
        </div>
      </Card>

      <Drawer
        direction="right"
        open={state.isDrawerOpen}
        onOpenChange={(open) => {
          if (!open) {
            handleCancel();
          }
        }}
      >
        <DrawerContent>
          <DrawerHeader>
            <DrawerTitle>Edit Product</DrawerTitle>
          </DrawerHeader>
          <div className="flex-1 overflow-y-auto p-4">
            <div className="mb-4">
              <label
                htmlFor="product-number"
                className="block text-sm font-medium text-gray-700 mb-1"
              >
                Number
              </label>
              <input
                id="product-number"
                type="text"
                value={state.editedNumber}
                onChange={(e) =>
                  dispatch({ type: "UPDATE_NUMBER", payload: e.target.value })
                }
                className="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
              />
            </div>
            <div className="mb-4">
              <label
                htmlFor="product-name"
                className="block text-sm font-medium text-gray-700 mb-1"
              >
                Name
              </label>
              <input
                id="product-name"
                type="text"
                value={state.editedName}
                onChange={(e) =>
                  dispatch({
                    type: "UPDATE_NAME",
                    payload: e.target.value,
                  })
                }
                className="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
              />
            </div>
            <div className="mb-4">
              <label
                htmlFor="product-description"
                className="block text-sm font-medium text-gray-700 mb-1"
              >
                Description
              </label>
              <textarea
                id="product-description"
                data-testid="product-description-input"
                value={state.editedDescription}
                onChange={(e) =>
                  dispatch({
                    type: "UPDATE_DESCRIPTION",
                    payload: e.target.value,
                  })
                }
                rows={6}
                className="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 resize-none"
              />
            </div>
          </div>
          <div className="flex gap-2 p-4 border-t">
            <button
              onClick={handleSave}
              disabled={isSaving}
              className="flex-1 px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition-colors font-medium cursor-pointer disabled:opacity-50 disabled:cursor-not-allowed"
            >
              {isSaving ? "Saving..." : "Save"}
            </button>
            <DrawerClose asChild>
              <button
                onClick={handleCancel}
                disabled={isSaving}
                className="flex-1 px-4 py-2 bg-gray-200 text-gray-800 rounded-md hover:bg-gray-300 transition-colors font-medium cursor-pointer disabled:opacity-50 disabled:cursor-not-allowed"
              >
                Cancel
              </button>
            </DrawerClose>
          </div>
        </DrawerContent>
      </Drawer>

      <Toaster position="top-center" />
    </>
  );
}
