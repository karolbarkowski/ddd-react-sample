import { createSlice, type PayloadAction } from "@reduxjs/toolkit";

export interface ProductsState {
  selectedProductId: number | null;
}

const initialState: ProductsState = {
  selectedProductId: null,
};

const productsSlice = createSlice({
  name: "products",
  initialState,
  reducers: {
    selectProduct: (state, action: PayloadAction<number | null>) => {
      state.selectedProductId = action.payload;
    },
  },
});

export const { selectProduct } = productsSlice.actions;

export default productsSlice.reducer;
