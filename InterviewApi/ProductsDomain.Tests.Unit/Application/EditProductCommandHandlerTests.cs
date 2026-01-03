using FluentAssertions;
using Moq;
using ProductsDomain.Application.Commands.EditProduct;
using ProductsDomain.Domain.Entities;
using ProductsDomain.Domain.Repositories;

namespace ProductsDomain.Tests.Unit.Application;

public class EditProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly EditProductCommandHandler _handler;

    public EditProductCommandHandlerTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _handler = new EditProductCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WithExistingProduct_ShouldUpdateProduct()
    {
        // Arrange
        Product product = Product.Create("Original Name", "P001", "Original Description");
        _repositoryMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        EditProductCommand command = new EditProductCommand
        {
            ProductId = 1,
            Name = "Updated Name",
            Number = "P001",
            Description = "Updated Description"
        };

        // Act
        await _handler.HandleAsync(command);

        // Assert
        product.Name.Should().Be("Updated Name");
        product.Description.Should().Be("Updated Description");
        _repositoryMock.Verify(r => r.UpdateAsync(product, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WithNonExistingProduct_ShouldThrowException()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product?)null);

        EditProductCommand command = new EditProductCommand
        {
            ProductId = 999,
            Name = "Name",
            Number = "P999",
            Description = "Description"
        };

        // Act
        Func<Task> act = async () => await _handler.HandleAsync(command);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*not found*");
    }
}
