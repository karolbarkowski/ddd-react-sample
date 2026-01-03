import { describe, it, expect, beforeEach } from "vitest";
import { render } from "@/lib/redux.test.renderer";
import { ProductDetail } from "./product-details";
import { configureStore } from "@reduxjs/toolkit";
import { baseApi } from "@/config/api";
import productsReducer from "@/features/products/products.slice";
import { type Product } from "@/features/products/products.types";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Provider } from "react-redux";
import type { waitFor, screen } from "@testing-library/react";

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

function createMockStore(initialState?: any) {
  return configureStore({
    reducer: {
      [baseApi.reducerPath]: baseApi.reducer,
      products: productsReducer,
    },
    middleware: (getDefaultMiddleware) =>
      getDefaultMiddleware().concat(baseApi.middleware),
    preloadedState: initialState,
  });
}

function renderWithRouter(productId: string, store: any) {
  window.history.pushState({}, "Test page", `/products/${productId}`);

  return render(
    <Provider store={store}>
      <BrowserRouter>
        <Routes>
          <Route path="/products/:id" element={<ProductDetail />} />
        </Routes>
      </BrowserRouter>
    </Provider>
  );
}

describe("ProductDetail", () => {
  beforeEach(() => {
    const store = createMockStore();
    store.dispatch(baseApi.util.resetApiState());
  });

  it("displays loading spinner while fetching product", () => {
    const store = createMockStore({
      [baseApi.reducerPath]: {
        queries: {
          "getProducts(undefined)": {
            status: "pending",
          },
        },
      },
    });

    renderWithRouter("1", store);

    expect(screen.getByRole("status")).toBeInTheDocument();
    expect(screen.getByText(/loading product\.\.\./i)).toBeInTheDocument();
  });

  it("displays error message when API request fails", async () => {
    const store = createMockStore({
      [baseApi.reducerPath]: {
        queries: {
          "getProducts(undefined)": {
            status: "rejected",
            error: { status: 500, data: "Internal Server Error" },
          },
        },
        mutations: {},
        provided: {},
        subscriptions: {},
        config: {
          online: true,
          focused: true,
          middlewareRegistered: true,
          refetchOnFocus: false,
          refetchOnReconnect: false,
          refetchOnMountOrArgChange: false,
          keepUnusedDataFor: 60,
          reducerPath: "api",
        },
      },
    });

    renderWithRouter("1", store);

    await waitFor(() => {
      expect(screen.getByText("Error")).toBeInTheDocument();
      expect(screen.getByText("Failed to load product")).toBeInTheDocument();
    });
  });

  it('displays "product not found" when product ID does not exist', async () => {
    const store = createMockStore({
      [baseApi.reducerPath]: {
        queries: {
          "getProducts(undefined)": {
            status: "fulfilled",
            data: mockProducts,
          },
        },
        mutations: {},
        provided: {},
        subscriptions: {},
        config: {
          online: true,
          focused: true,
          middlewareRegistered: true,
          refetchOnFocus: false,
          refetchOnReconnect: false,
          refetchOnMountOrArgChange: false,
          keepUnusedDataFor: 60,
          reducerPath: "api",
        },
      },
    });

    renderWithRouter("999", store);

    await waitFor(() => {
      expect(screen.getByText("Product Not Found")).toBeInTheDocument();
      expect(screen.getByText("Product ID: 999")).toBeInTheDocument();
    });
  });

  it("displays product details when product is found", async () => {
    const store = createMockStore({
      [baseApi.reducerPath]: {
        queries: {
          "getProducts(undefined)": {
            status: "fulfilled",
            data: mockProducts,
          },
        },
        mutations: {},
        provided: {},
        subscriptions: {},
        config: {
          online: true,
          focused: true,
          middlewareRegistered: true,
          refetchOnFocus: false,
          refetchOnReconnect: false,
          refetchOnMountOrArgChange: false,
          keepUnusedDataFor: 60,
          reducerPath: "api",
        },
      },
    });

    renderWithRouter("1", store);

    await waitFor(() => {
      expect(screen.getByText("Product Detail")).toBeInTheDocument();
      expect(screen.getByText("Product ID: 1")).toBeInTheDocument();
      expect(screen.getByText("Product 1")).toBeInTheDocument();
    });
  });

  it("displays correct product for different IDs", async () => {
    const store = createMockStore({
      [baseApi.reducerPath]: {
        queries: {
          "getProducts(undefined)": {
            status: "fulfilled",
            data: mockProducts,
          },
        },
        mutations: {},
        provided: {},
        subscriptions: {},
        config: {
          online: true,
          focused: true,
          middlewareRegistered: true,
          refetchOnFocus: false,
          refetchOnReconnect: false,
          refetchOnMountOrArgChange: false,
          keepUnusedDataFor: 60,
          reducerPath: "api",
        },
      },
    });

    renderWithRouter("2", store);

    await waitFor(() => {
      expect(screen.getByText("Product Detail")).toBeInTheDocument();
      expect(screen.getByText("Product ID: 2")).toBeInTheDocument();
      expect(screen.getByText("Product 2")).toBeInTheDocument();
    });
  });

  it("applies correct styling to product name", async () => {
    const store = createMockStore({
      [baseApi.reducerPath]: {
        queries: {
          "getProducts(undefined)": {
            status: "fulfilled",
            data: mockProducts,
          },
        },
        mutations: {},
        provided: {},
        subscriptions: {},
        config: {
          online: true,
          focused: true,
          middlewareRegistered: true,
          refetchOnFocus: false,
          refetchOnReconnect: false,
          refetchOnMountOrArgChange: false,
          keepUnusedDataFor: 60,
          reducerPath: "api",
        },
      },
    });

    renderWithRouter("1", store);

    await waitFor(() => {
      const productName = screen.getByText("Product 1");
      expect(productName).toHaveClass(
        "text-2xl",
        "font-semibold",
        "text-blue-600"
      );
    });
  });
});
