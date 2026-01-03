using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProductsDomain.Domain.Entities;
using ProductsDomain.Infrastructure.Persistence;
using ProductsDomain.Infrastructure.Persistence.Repositories;

namespace ProductsDomain.Tests.Unit.Infrastructure;

public class ProductRepositoryTests
{
    private ApplicationDbContext CreateDbContext()
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingProduct_ShouldReturnProduct()
    {
        // Arrange
        await using ApplicationDbContext context = CreateDbContext();
        var repository = new ProductRepository(context);

        Product product = Product.Create("Test Product", "T001", "Description");
        product.AddImage("https://example.com/image.jpg", "Image");
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(product.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be("Test Product");
        result.Images.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistingProduct_ShouldReturnNull()
    {
        // Arrange
        await using ApplicationDbContext context = CreateDbContext();
        var repository = new ProductRepository(context);

        // Act
        var result = await repository.GetByIdAsync(999);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllProducts()
    {
        // Arrange
        await using ApplicationDbContext context = CreateDbContext();
        var repository = new ProductRepository(context);

        Product product1 = Product.Create("Product 1", "P001", "Description 1");
        Product product2 = Product.Create("Product 2", "P002", "Description 2");
        Product product3 = Product.Create("Product 3", "P003", "Description 3");

        context.Products.AddRange(product1, product2, product3);
        await context.SaveChangesAsync();

        // Act
        var results = await repository.GetAllAsync();

        // Assert
        results.Should().HaveCount(3);
        results.Should().Contain(p => p.Name == "Product 1");
        results.Should().Contain(p => p.Name == "Product 2");
        results.Should().Contain(p => p.Name == "Product 3");
    }

    [Fact]
    public async Task GetAllAsync_WithEmptyDatabase_ShouldReturnEmptyList()
    {
        // Arrange
        await using ApplicationDbContext context = CreateDbContext();
        var repository = new ProductRepository(context);

        // Act
        var results = await repository.GetAllAsync();

        // Assert
        results.Should().BeEmpty();
    }

    [Fact]
    public async Task AddAsync_ShouldAddProductToDatabase()
    {
        // Arrange
        await using ApplicationDbContext context = CreateDbContext();
        var repository = new ProductRepository(context);

        Product product = Product.Create("New Product", "N001", "New Description");
        product.AddImage("https://example.com/new.jpg", "New Image");

        // Act
        var result = await repository.AddAsync(product);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);

        var savedProduct = await context.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == result.Id);
        savedProduct.Should().NotBeNull();
        savedProduct!.Name.Should().Be("New Product");
        savedProduct.Images.Should().HaveCount(1);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateExistingProduct()
    {
        // Arrange
        await using ApplicationDbContext context = CreateDbContext();
        var repository = new ProductRepository(context);

        Product product = Product.Create("Original Name", "O001", "Original Description");
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        // Modify the product
        product.UpdateDetails("Updated Name", "O001", "Updated Description");

        // Act
        await repository.UpdateAsync(product);

        // Assert
        Product? updatedProduct = await context.Products.FindAsync(product.Id);
        updatedProduct.Should().NotBeNull();
        updatedProduct!.Name.Should().Be("Updated Name");
        updatedProduct.Description.Should().Be("Updated Description");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldIncludeImages()
    {
        // Arrange
        await using ApplicationDbContext context = CreateDbContext();
        var repository = new ProductRepository(context);

        Product product = Product.Create("Product", "P001", "Description");
        product.AddImage("https://example.com/img1.jpg", "Image 1");
        product.AddImage("https://example.com/img2.jpg", "Image 2");
        product.AddImage("https://example.com/img3.jpg", "Image 3");

        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(product.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Images.Should().HaveCount(3);
        result.Images.Should().Contain(img => img.Name == "Image 1");
        result.Images.Should().Contain(img => img.Name == "Image 2");
        result.Images.Should().Contain(img => img.Name == "Image 3");
    }
}
