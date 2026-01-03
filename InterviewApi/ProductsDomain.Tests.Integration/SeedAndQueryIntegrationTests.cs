using FluentAssertions;
using ProductsDomain.Application.Commands.SeedDb;
using ProductsDomain.Application.Queries.GetAllProducts;
using ProductsDomain.Application.Queries.GetProductById;

namespace ProductsDomain.Tests.Integration;

public class SeedAndQueryIntegrationTests : IntegrationTestBase
{
    [Fact]
    public async Task SeedDatabase_ThenGetAll_ShouldReturnSeededProducts()
    {
        // Arrange
        var seedHandler = new SeedDbCommandHandler(Context);
        var getAllHandler = new GetAllProductsQueryHandler(Repository);

        // Act - Seed the database
        await seedHandler.HandleAsync();

        // Act - Query all products
        var query = new GetAllProductsQuery();
        var result = await getAllHandler.HandleAsync(query);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(3);
    }

    [Fact]
    public async Task GetById_WithNonExistentProduct_ShouldReturnNull()
    {
        // Arrange
        var getByIdHandler = new GetProductByIdQueryHandler(Repository);

        // Act
        var query = new GetProductByIdQuery { ProductId = 999 };
        var result = await getByIdHandler.HandleAsync(query);

        // Assert
        result.Should().BeNull();
    }
}
