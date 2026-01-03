import { test, expect } from "@playwright/test";

test.describe("Product Flow", () => {
  test("should open product, edit description, and cancel changes", async ({
    page,
  }) => {
    // Navigate to main page
    await page.goto("/");

    // Click on the first product
    const firstProduct = page.locator('a[href^="/products/"]').first();
    await firstProduct.click();

    // Wait for the product details page to load
    await page.waitForLoadState("networkidle");
    await expect(page.locator('[data-testid="product-details"]')).toBeVisible();

    // Click the Edit button
    const editButton = page.getByRole("button", { name: /edit/i });
    await editButton.click();

    // Wait for edit mode to be active
    await expect(
      page.locator('[data-testid="product-description-input"]')
    ).toBeVisible();

    // Change the product description
    const descriptionInput = page.locator(
      '[data-testid="product-description-input"]'
    );
    await descriptionInput.clear();
    await descriptionInput.fill(
      "This is a test description that will be cancelled"
    );

    // Click the Cancel button
    const cancelButton = page.getByRole("button", { name: /cancel/i });
    await cancelButton.click();

    // Verify we're back to view mode (Edit button should be visible again)
    await expect(editButton).toBeVisible();
  });

  test("should redirect to 404 page when navigating to non-existing product", async ({
    page,
  }) => {
    // Navigate to a non-existing product
    await page.goto("/products/999");

    // Wait for navigation to complete
    await page.waitForLoadState("networkidle");

    // Verify we were redirected to the 404 page
    await expect(page).toHaveURL(/\/404/);
  });
});
