using FluentAssertions;
using ProductsDomain.Domain.Entities;

namespace ProductsDomain.Tests.Unit.Domain;

public class ProductImageTests
{
    [Fact]
    public void Create_WithValidData_ShouldCreateProductImage()
    {
        // Act
        ProductImage image = ProductImage.Create("https://example.com/image.jpg", "Image Name");

        // Assert
        image.Should().NotBeNull();
        image.Url.Should().Be("https://example.com/image.jpg");
        image.Name.Should().Be("Image Name");
    }

    [Theory]
    [InlineData("", "Name")]
    [InlineData(" ", "Name")]
    [InlineData(null, "Name")]
    public void Create_WithInvalidUrl_ShouldThrowException(string url, string name)
    {
        // Act
        Func<ProductImage> act = () => ProductImage.Create(url, name);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("*URL*");
    }

    [Theory]
    [InlineData("https://example.com/image.jpg", "")]
    [InlineData("https://example.com/image.jpg", " ")]
    [InlineData("https://example.com/image.jpg", null)]
    public void Create_WithInvalidName_ShouldThrowException(string url, string name)
    {
        // Act
        Func<ProductImage> act = () => ProductImage.Create(url, name);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("*name*");
    }
}
