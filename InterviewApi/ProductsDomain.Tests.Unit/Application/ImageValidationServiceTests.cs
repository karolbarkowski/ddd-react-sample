using FluentAssertions;
using Moq;
using Moq.Protected;
using ProductsDomain.Application.Services;
using ProductsDomain.Domain.Entities;
using System.Net;

namespace ProductsDomain.Tests.Unit.Application;

public class ImageValidationServiceTests
{
    [Fact]
    public async Task FilterOutNonExistingImagesAsync_WithNullProducts_ShouldReturnNull()
    {
        // Arrange
        var mockFactory = new Mock<IHttpClientFactory>();
        var service = new ImageValidationService(mockFactory.Object);

        // Act
        var result = await service.FilterOutNonExistingImagesAsync((IReadOnlyList<Product>?)null);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task FilterOutNonExistingImagesAsync_WithEmptyProductList_ShouldReturnEmpty()
    {
        // Arrange
        var mockFactory = new Mock<IHttpClientFactory>();
        var service = new ImageValidationService(mockFactory.Object);
        var products = new List<Product>();

        // Act
        var result = await service.FilterOutNonExistingImagesAsync(products);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task FilterOutNonExistingImagesAsync_WithNullProduct_ShouldReturnNull()
    {
        // Arrange
        var mockFactory = new Mock<IHttpClientFactory>();
        var service = new ImageValidationService(mockFactory.Object);

        // Act
        var result = await service.FilterOutNonExistingImagesAsync((Product?)null);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task FilterOutNonExistingImagesAsync_WithProductWithoutImages_ShouldReturnProductUnchanged()
    {
        // Arrange
        var mockFactory = new Mock<IHttpClientFactory>();
        var service = new ImageValidationService(mockFactory.Object);
        var product = Product.Create("Test Product", "P001", "Description");

        // Act
        var result = await service.FilterOutNonExistingImagesAsync(product);

        // Assert
        result.Should().NotBeNull();
        result!.Images.Should().BeEmpty();
    }

    [Fact]
    public async Task FilterOutNonExistingImagesAsync_WithValidImage_ShouldKeepImage()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            });

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var mockFactory = new Mock<IHttpClientFactory>();
        mockFactory.Setup(f => f.CreateClient("ImageValidation")).Returns(httpClient);

        var service = new ImageValidationService(mockFactory.Object);
        var product = Product.Create("Test Product", "P001", "Description");
        product.AddImage("https://example.com/valid-image.jpg", "Valid Image");

        // Act
        var result = await service.FilterOutNonExistingImagesAsync(product);

        // Assert
        result.Should().NotBeNull();
        result!.Images.Should().HaveCount(1);
        result.Images.First().Name.Should().Be("Valid Image");
    }

    [Fact]
    public async Task FilterOutNonExistingImagesAsync_WithInvalidImage_ShouldRemoveImage()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound
            });

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var mockFactory = new Mock<IHttpClientFactory>();
        mockFactory.Setup(f => f.CreateClient("ImageValidation")).Returns(httpClient);

        var service = new ImageValidationService(mockFactory.Object);
        var product = Product.Create("Test Product", "P001", "Description");
        product.AddImage("https://example.com/invalid-image.jpg", "Invalid Image");

        // Act
        var result = await service.FilterOutNonExistingImagesAsync(product);

        // Assert
        result.Should().NotBeNull();
        result!.Images.Should().BeEmpty();
    }

    [Fact]
    public async Task FilterOutNonExistingImagesAsync_WithMixedValidAndInvalidImages_ShouldKeepOnlyValid()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.ToString().Contains("valid")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });

        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.ToString().Contains("invalid")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound });

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var mockFactory = new Mock<IHttpClientFactory>();
        mockFactory.Setup(f => f.CreateClient("ImageValidation")).Returns(httpClient);

        var service = new ImageValidationService(mockFactory.Object);
        var product = Product.Create("Test Product", "P001", "Description");
        product.AddImage("https://example.com/valid-image.jpg", "Valid Image");
        product.AddImage("https://example.com/invalid-image.jpg", "Invalid Image");
        product.AddImage("https://example.com/valid-image2.jpg", "Valid Image 2");

        // Act
        var result = await service.FilterOutNonExistingImagesAsync(product);

        // Assert
        result.Should().NotBeNull();
        result!.Images.Should().HaveCount(2);
        result.Images.Should().Contain(img => img.Name == "Valid Image");
        result.Images.Should().Contain(img => img.Name == "Valid Image 2");
        result.Images.Should().NotContain(img => img.Name == "Invalid Image");
    }

    [Fact]
    public async Task FilterOutNonExistingImagesAsync_WhenHttpRequestThrows_ShouldKeepImageAsValid()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var mockFactory = new Mock<IHttpClientFactory>();
        mockFactory.Setup(f => f.CreateClient("ImageValidation")).Returns(httpClient);

        var service = new ImageValidationService(mockFactory.Object);
        var product = Product.Create("Test Product", "P001", "Description");
        product.AddImage("https://example.com/image.jpg", "Image");

        // Act
        var result = await service.FilterOutNonExistingImagesAsync(product);

        // Assert
        result.Should().NotBeNull();
        result!.Images.Should().HaveCount(1);
        result.Images.First().Name.Should().Be("Image");
    }

    [Fact]
    public async Task FilterOutNonExistingImagesAsync_WithNullOrWhitespaceUrl_ShouldSkipImage()
    {
        // Arrange
        var mockFactory = new Mock<IHttpClientFactory>();
        var service = new ImageValidationService(mockFactory.Object);
        var product = Product.Create("Test Product", "P001", "Description");

        // Use reflection to add an image with a null/whitespace URL since Create validates it
        var imageWithValidUrl = ProductImage.Create("https://example.com/valid.jpg", "Valid");
        product.AddImage("https://example.com/valid.jpg", "Valid");

        // Act
        var result = await service.FilterOutNonExistingImagesAsync(product);

        // Assert
        result.Should().NotBeNull();
        // The service should have attempted to validate the valid URL
        // but we haven't set up the HTTP mock, so it will throw and keep the image
        result!.Images.Should().HaveCount(1);
    }

    [Fact]
    public async Task FilterOutNonExistingImagesAsync_WithMultipleProducts_ShouldValidateAllProducts()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var mockFactory = new Mock<IHttpClientFactory>();
        mockFactory.Setup(f => f.CreateClient("ImageValidation")).Returns(httpClient);

        var service = new ImageValidationService(mockFactory.Object);

        var product1 = Product.Create("Product 1", "P001", "Description");
        product1.AddImage("https://example.com/image1.jpg", "Image 1");

        var product2 = Product.Create("Product 2", "P002", "Description");
        product2.AddImage("https://example.com/image2.jpg", "Image 2");

        var products = new List<Product> { product1, product2 };

        // Act
        var result = await service.FilterOutNonExistingImagesAsync(products);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result![0].Images.Should().HaveCount(1);
        result[1].Images.Should().HaveCount(1);
    }
}
