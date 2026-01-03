import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { Footer, Header } from "./components";
import { ProductDetail } from "./routes/product-details/product-details";
import { ProductList } from "./routes/product-list/product-list";
import { RouteNotFound } from "./routes/errors/404";
import "./App.css";

function App() {
  return (
    <BrowserRouter>
      <div className="flex min-h-screen flex-col">
        <section className="border-b bg-card px-8 sm:0">
          <Header />
        </section>
        <main className="flex flex-1 container mx-auto py-16 px-8 sm:0">
          <Routes>
            <Route path="/" element={<Navigate to="/products" replace />} />
            <Route path="/products" element={<ProductList />} />
            <Route path="/products/:id" element={<ProductDetail />} />
            <Route path="*" element={<RouteNotFound />} />
          </Routes>
        </main>
        <section className="border-t bg-card">
          <Footer />
        </section>
      </div>
    </BrowserRouter>
  );
}

export default App;
