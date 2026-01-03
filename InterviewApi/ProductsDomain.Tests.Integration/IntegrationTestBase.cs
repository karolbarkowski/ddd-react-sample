using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductsDomain.Infrastructure.Persistence;
using ProductsDomain.Infrastructure.Persistence.Repositories;
using ProductsDomain.Domain.Repositories;
using ProductsDomain.Application.Services;

namespace ProductsDomain.Tests.Integration;

public abstract class IntegrationTestBase : IDisposable
{
    protected readonly ApplicationDbContext Context;
    protected readonly IProductRepository Repository;
    protected readonly IImageValidationService ImageValidationService;

    protected IntegrationTestBase()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Context = new ApplicationDbContext(options);
        Repository = new ProductRepository(Context);

        var services = new ServiceCollection();
        services.AddHttpClient("ImageValidation", client =>
        {
            client.Timeout = TimeSpan.FromSeconds(5);
        });
        var serviceProvider = services.BuildServiceProvider();
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

        ImageValidationService = new ImageValidationService(httpClientFactory);
    }

    public void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }
}
