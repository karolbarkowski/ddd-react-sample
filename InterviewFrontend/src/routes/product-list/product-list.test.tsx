import { describe, it, expect } from "vitest";
import { render } from "@/lib/redux.test.renderer";
import { ProductList } from "./product-list";
import { type Product } from "@/features/products/products.types";
import type { RootState } from "@/config/redux.store";
import { waitFor, screen } from "@testing-library/react";

const mockProducts: Product[] = [
  {
    id: 1,
    name: "Product 1",
    number: "PROD-001",
    description: "Description for product 1",
    images: [
      {
        url: "https://example.com/image1.jpg",
        name: "Product 1 Image",
      },
    ],
  },
  {
    id: 2,
    name: "Product 2",
    number: "PROD-002",
    description: "Description for product 2",
    images: [],
  },
];

const initStateFulfilled = {
  products: {
    selectedProductId: mockProducts[0].id,
  },
  api: {
    queries: {
      "getProducts(undefined)": {
        status: "fulfilled",
        data: mockProducts,
      },
    },
  },
} as unknown as Partial<RootState>;

const initStateFulfilledEmpty = {
  products: {
    selectedProductId: undefined,
  },
  api: {
    queries: {
      "getProducts(undefined)": {
        status: "fulfilled",
        data: undefined,
      },
    },
  },
} as unknown as Partial<RootState>;

const initStatePending = {
  products: {
    selectedProductId: undefined,
  },
  api: {
    queries: {
      "getProducts(undefined)": {
        status: "pending",
        data: undefined,
      },
    },
  },
} as unknown as Partial<RootState>;

describe("ProductList", () => {
  it("displays loading spinner while fetching products", () => {
    render(<ProductList />, initStatePending);

    expect(screen.getByRole("status")).toBeInTheDocument();
    expect(screen.getByText(/fetching/i)).toBeInTheDocument();
  });

  it('displays "no products found" when product list is empty', async () => {
    render(<ProductList />, initStateFulfilledEmpty);

    await waitFor(() => {
      expect(screen.getByText(/No Products Found/i)).toBeInTheDocument();
    });
  });

  it("displays list of products when data is available", async () => {
    render(<ProductList />, initStateFulfilled);

    await waitFor(() => {
      expect(screen.getByText("Products List")).toBeInTheDocument();
    });

    //assert
    mockProducts.forEach((product) => {
      expect(screen.getByText(product.number)).toBeInTheDocument();
      expect(screen.getByText(product.description)).toBeInTheDocument();
    });
  });
});
