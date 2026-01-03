using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProductsDomain.Application.Commands.SeedDb;
using ProductsDomain.Infrastructure.Persistence;

namespace ProductsDomain.Tests.Unit.Application;

public class SeedDbCommandHandlerTests
{
    private ApplicationDbContext CreateDbContext()
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task HandleAsync_WithEmptyDatabase_ShouldSeedData()
    {
        // Arrange
        await using ApplicationDbContext context = CreateDbContext();
        SeedDbCommandHandler handler = new SeedDbCommandHandler(context);

        // Act
        await handler.HandleAsync();

        // Assert
        context.Products.Should().HaveCount(3);
        context.Products.SelectMany(p => p.Images).Should().HaveCount(3);
    }
}
