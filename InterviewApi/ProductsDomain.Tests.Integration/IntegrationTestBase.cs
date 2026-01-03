using Microsoft.EntityFrameworkCore;
using ProductsDomain.Infrastructure.Persistence;
using ProductsDomain.Infrastructure.Persistence.Repositories;
using ProductsDomain.Domain.Repositories;

namespace ProductsDomain.Tests.Integration;

public abstract class IntegrationTestBase : IDisposable
{
    protected readonly ApplicationDbContext Context;
    protected readonly IProductRepository Repository;

    protected IntegrationTestBase()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Context = new ApplicationDbContext(options);
        Repository = new ProductRepository(Context);
    }

    public void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }
}
