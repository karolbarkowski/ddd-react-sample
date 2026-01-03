using FluentAssertions;
using Moq;
using ProductsDomain.Application.Queries.GetAllProducts;
using ProductsDomain.Application.Services;
using ProductsDomain.Domain.Entities;
using ProductsDomain.Domain.Repositories;

namespace ProductsDomain.Tests.Unit.Application;

public class GetAllProductsQueryHandlerTests
{
    [Fact]
    public async Task HandleAsync_WithNoProducts_ShouldReturnEmptyList()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var mockImageService = new Mock<IImageValidationService>();

        mockRepository
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Product>());

        var handler = new GetAllProductsQueryHandler(mockRepository.Object, mockImageService.Object);
        var query = new GetAllProductsQuery();

        // Act
        var result = await handler.HandleAsync(query);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
        mockImageService.Verify(s => s.FilterOutNonExistingImagesAsync(It.IsAny<IReadOnlyList<Product>>()), Times.Never);
    }

    [Fact]
    public async Task HandleAsync_WithProducts_ShouldCallImageValidationService()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var mockImageService = new Mock<IImageValidationService>();

        var product1 = Product.Create("Product 1", "P001", "Description 1");
        product1.AddImage("https://example.com/image1.jpg", "Image 1");

        var product2 = Product.Create("Product 2", "P002", "Description 2");
        product2.AddImage("https://example.com/image2.jpg", "Image 2");

        var products = new List<Product> { product1, product2 };

        mockRepository
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);

        mockImageService
            .Setup(s => s.FilterOutNonExistingImagesAsync(It.IsAny<IReadOnlyList<Product>>()))
            .ReturnsAsync((IReadOnlyList<Product> p) => p);

        var handler = new GetAllProductsQueryHandler(mockRepository.Object, mockImageService.Object);
        var query = new GetAllProductsQuery();

        // Act
        var result = await handler.HandleAsync(query);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        mockImageService.Verify(s => s.FilterOutNonExistingImagesAsync(It.IsAny<IReadOnlyList<Product>>()), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WithProducts_ShouldReturnMappedDtos()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var mockImageService = new Mock<IImageValidationService>();

        var product = Product.Create("Test Product", "P001", "Test Description");
        product.AddImage("https://example.com/image.jpg", "Test Image");

        var products = new List<Product> { product };

        mockRepository
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);

        mockImageService
            .Setup(s => s.FilterOutNonExistingImagesAsync(It.IsAny<IReadOnlyList<Product>>()))
            .ReturnsAsync((IReadOnlyList<Product> p) => p);

        var handler = new GetAllProductsQueryHandler(mockRepository.Object, mockImageService.Object);
        var query = new GetAllProductsQuery();

        // Act
        var result = await handler.HandleAsync(query);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result[0].Name.Should().Be("Test Product");
        result[0].Number.Should().Be("P001");
        result[0].Description.Should().Be("Test Description");
        result[0].Images.Should().HaveCount(1);
        result[0].Images[0].Name.Should().Be("Test Image");
    }

    [Fact]
    public async Task HandleAsync_WhenImageServiceFiltersImages_ShouldReturnUpdatedProducts()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var mockImageService = new Mock<IImageValidationService>();

        var product = Product.Create("Test Product", "P001", "Description");
        product.AddImage("https://example.com/valid.jpg", "Valid Image");
        product.AddImage("https://example.com/invalid.jpg", "Invalid Image");

        var products = new List<Product> { product };

        mockRepository
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);

        mockImageService
            .Setup(s => s.FilterOutNonExistingImagesAsync(It.IsAny<IReadOnlyList<Product>>()))
            .ReturnsAsync((IReadOnlyList<Product> p) =>
            {
                // Simulate filtering out the invalid image
                var filteredProduct = p[0];
                filteredProduct.SetImages(new List<ProductImage>
                {
                    ProductImage.Create("https://example.com/valid.jpg", "Valid Image")
                });
                return p;
            });

        var handler = new GetAllProductsQueryHandler(mockRepository.Object, mockImageService.Object);
        var query = new GetAllProductsQuery();

        // Act
        var result = await handler.HandleAsync(query);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result[0].Images.Should().HaveCount(1);
        result[0].Images[0].Name.Should().Be("Valid Image");
    }

    [Fact]
    public async Task HandleAsync_WithCancellationToken_ShouldPassToRepository()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var mockImageService = new Mock<IImageValidationService>();
        var cancellationToken = new CancellationToken();

        mockRepository
            .Setup(r => r.GetAllAsync(cancellationToken))
            .ReturnsAsync(new List<Product>());

        var handler = new GetAllProductsQueryHandler(mockRepository.Object, mockImageService.Object);
        var query = new GetAllProductsQuery();

        // Act
        await handler.HandleAsync(query, cancellationToken);

        // Assert
        mockRepository.Verify(r => r.GetAllAsync(cancellationToken), Times.Once);
    }
}
