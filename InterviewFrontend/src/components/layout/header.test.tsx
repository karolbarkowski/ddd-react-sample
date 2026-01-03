import { describe, it, expect } from "vitest";
import { render, screen } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import { Header } from "./header";

// Header needs Router context because it uses <Link>
const renderWithRouter = (component: React.ReactElement) => {
  return render(<BrowserRouter>{component}</BrowserRouter>);
};

describe("Header", () => {
  it("renders header component", () => {
    renderWithRouter(<Header />);
    const header = screen.getByRole("banner");
    expect(header).toBeInTheDocument();
  });

  it("displays app title", () => {
    renderWithRouter(<Header />);
    const title = screen.getByRole("heading", { name: /react sample app/i });
    expect(title).toBeInTheDocument();
    expect(title).toHaveClass("text-xl", "font-semibold");
  });

  it("renders logo image", () => {
    renderWithRouter(<Header />);
    const logo = screen.getByTestId("logo-img");
    expect(logo).toBeInTheDocument();
    expect(logo).toHaveAttribute("src", "/react.svg");
  });

  it("renders link to home page", () => {
    renderWithRouter(<Header />);
    const homeLink = screen.getByRole("link");
    expect(homeLink).toHaveAttribute("href", "/");
  });
});
