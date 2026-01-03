using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProductsDomain.Application.Commands.EditProduct;
using ProductsDomain.Application.Queries.GetProductById;
using ProductsDomain.Domain.Entities;

namespace ProductsDomain.Tests.Integration;

public class FullWorkflowIntegrationTests : IntegrationTestBase
{
    [Fact]
    public async Task CompleteWorkflow_CreateEditAndQuery_ShouldWork()
    {
        // Step 1: Create a product directly via repository
        Product product = Product.Create("New Product", "NP001", "A brand new product");
        product.AddImage("https://example.com/new1.jpg", "Image 1");
        product.AddImage("https://example.com/new2.jpg", "Image 2");

        await Repository.AddAsync(product);
        var productId = product.Id;

        // Step 2: Query the product
        GetProductByIdQueryHandler getByIdHandler = new GetProductByIdQueryHandler(Repository);
        GetProductByIdQuery getQuery = new GetProductByIdQuery { ProductId = productId };
        var queriedProduct = await getByIdHandler.HandleAsync(getQuery);

        queriedProduct.Should().NotBeNull();
        queriedProduct!.Name.Should().Be("New Product");
        queriedProduct.Images.Should().HaveCount(2);

        // Step 3: Edit the product
        EditProductCommandHandler editHandler = new EditProductCommandHandler(Repository);
        EditProductCommand editCommand = new EditProductCommand
        {
            ProductId = productId,
            Name = "Updated Product",
            Number = "NP001",
            Description = "Updated description"
        };
        await editHandler.HandleAsync(editCommand);

        // Step 4: Query again to verify changes
        var updatedProduct = await getByIdHandler.HandleAsync(getQuery);
        updatedProduct.Should().NotBeNull();
        updatedProduct!.Name.Should().Be("Updated Product");
        updatedProduct.Description.Should().Be("Updated description");
        updatedProduct.Images.Should().HaveCount(2); // Images should persist
    }
}
