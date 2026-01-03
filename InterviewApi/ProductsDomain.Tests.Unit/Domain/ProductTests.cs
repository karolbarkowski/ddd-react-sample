using FluentAssertions;
using ProductsDomain.Domain.Entities;

namespace ProductsDomain.Tests.Unit.Domain;

public class ProductTests
{
    [Fact]
    public void Create_WithValidData_ShouldCreateProduct()
    {
        // Act
        Product product = Product.Create("Product Name", "P001", "Product Description");

        // Assert
        product.Should().NotBeNull();
        product.Name.Should().Be("Product Name");
        product.Number.Should().Be("P001");
        product.Description.Should().Be("Product Description");
        product.Images.Should().BeEmpty();
    }

    [Theory]
    [InlineData("", "P001", "Description")]
    [InlineData(" ", "P001", "Description")]
    public void Create_WithInvalidName_ShouldThrowException(string name, string number, string description)
    {
        // Act
        Func<Product> act = () => Product.Create(name, number, description);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("*name*");
    }

    [Theory]
    [InlineData("Name", "", "Description")]
    [InlineData("Name", " ", "Description")]
    public void Create_WithInvalidNumber_ShouldThrowException(string name, string number, string description)
    {
        // Act
        Func<Product> act = () => Product.Create(name, number, description);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("*number*");
    }

    [Theory]
    [InlineData("Name", "P001", "")]
    [InlineData("Name", "P001", " ")]
    public void Create_WithInvalidDescription_ShouldThrowException(string name, string number, string description)
    {
        // Act
        Func<Product> act = () => Product.Create(name, number, description);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("*description*");
    }

    [Fact]
    public void UpdateDetails_WithValidData_ShouldUpdateProduct()
    {
        // Arrange
        Product product = Product.Create("Original Name", "P001", "Original Description");

        // Act
        product.UpdateDetails("New Name", "P001", "New Description");

        // Assert
        product.Name.Should().Be("New Name");
        product.Description.Should().Be("New Description");
    }

    [Fact]
    public void UpdateDetails_WithInvalidName_ShouldThrowException()
    {
        // Arrange
        Product product = Product.Create("Name", "P001", "Description");

        // Act
        Action act = () => product.UpdateDetails("", "P001", "New Description");

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("*name*");
    }

    [Fact]
    public void AddImage_WithValidData_ShouldAddImageToCollection()
    {
        // Arrange
        Product product = Product.Create("Name", "P001", "Description");

        // Act
        product.AddImage("https://example.com/image.jpg", "Image Name");

        // Assert
        product.Images.Should().HaveCount(1);
        product.Images.First().Url.Should().Be("https://example.com/image.jpg");
        product.Images.First().Name.Should().Be("Image Name");
    }

    [Fact]
    public void AddImage_MultipleTimes_ShouldAddAllImages()
    {
        // Arrange
        Product product = Product.Create("Name", "P001", "Description");

        // Act
        product.AddImage("https://example.com/image1.jpg", "Image 1");
        product.AddImage("https://example.com/image2.jpg", "Image 2");
        product.AddImage("https://example.com/image3.jpg", "Image 3");

        // Assert
        product.Images.Should().HaveCount(3);
    }
}
