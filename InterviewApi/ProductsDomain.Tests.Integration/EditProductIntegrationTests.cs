using FluentAssertions;
using ProductsDomain.Application.Commands.EditProduct;
using ProductsDomain.Application.Queries.GetProductById;
using ProductsDomain.Domain.Entities;

namespace ProductsDomain.Tests.Integration;

public class EditProductIntegrationTests : IntegrationTestBase
{
    [Fact]
    public async Task EditProduct_ShouldPersistChangesToDatabase()
    {
        // Arrange - Create a product
        var product = Product.Create("Original Name", "P001", "Original Description");
        product.AddImage("https://example.com/image.jpg", "Original Image");
        await Repository.AddAsync(product);

        var editHandler = new EditProductCommandHandler(Repository);
        var getByIdHandler = new GetProductByIdQueryHandler(Repository);

        // Act - Edit the product
        var editCommand = new EditProductCommand
        {
            ProductId = product.Id,
            Name = "Updated Name",
            Number = "P001",
            Description = "Updated Description"
        };
        await editHandler.HandleAsync(editCommand);

        // Act - Retrieve the updated product
        var getQuery = new GetProductByIdQuery { ProductId = product.Id };
        var result = await getByIdHandler.HandleAsync(getQuery);

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be("Updated Name");
        result.Description.Should().Be("Updated Description");
        result.Images.Should().HaveCount(1); // Images should remain unchanged
    }

    [Fact]
    public async Task EditProduct_WithNonExistentProduct_ShouldThrowException()
    {
        // Arrange
        var editHandler = new EditProductCommandHandler(Repository);

        var command = new EditProductCommand
        {
            ProductId = 999,
            Name = "Name",
            Number = "N999",
            Description = "Description"
        };

        // Act
        var act = async () => await editHandler.HandleAsync(command);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*not found*");
    }

    [Fact]
    public async Task EditProduct_MultipleEdits_ShouldApplyAllChanges()
    {
        // Arrange - Create a product
        var product = Product.Create("Version 1", "V001", "Description 1");
        await Repository.AddAsync(product);

        var editHandler = new EditProductCommandHandler(Repository);
        var getByIdHandler = new GetProductByIdQueryHandler(Repository);

        // Act - First edit
        await editHandler.HandleAsync(new EditProductCommand
        {
            ProductId = product.Id,
            Name = "Version 2",
            Number = "V001",
            Description = "Description 2"
        });

        // Act - Second edit
        await editHandler.HandleAsync(new EditProductCommand
        {
            ProductId = product.Id,
            Name = "Version 3",
            Number = "V001",
            Description = "Description 3"
        });

        // Act - Retrieve the final state
        var result = await getByIdHandler.HandleAsync(new GetProductByIdQuery { ProductId = product.Id });

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be("Version 3");
        result.Description.Should().Be("Description 3");
    }
}
