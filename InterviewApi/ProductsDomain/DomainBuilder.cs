using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductsDomain.Application.Commands.EditProduct;
using ProductsDomain.Application.Commands.SeedDb;
using ProductsDomain.Application.Queries.GetAllProducts;
using ProductsDomain.Application.Queries.GetProductById;
using ProductsDomain.Domain.Repositories;
using ProductsDomain.Infrastructure.Persistence;
using ProductsDomain.Infrastructure.Persistence.Repositories;

namespace ProductsDomain
{
    public static class DomainBuilder
    {
        public static IServiceCollection AddProductsDomain(this IServiceCollection services)
        {
            // Infrastructure - DbContext with In-Memory database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("ProductsDomainDb"));

            // Infrastructure - Repositories
            services.AddScoped<IProductRepository, ProductRepository>();

            // Application - Command/Query Handlers
            services.AddScoped<EditProductCommandHandler>();
            services.AddScoped<SeedDbCommandHandler>();
            services.AddScoped<GetProductByIdQueryHandler>();
            services.AddScoped<GetAllProductsQueryHandler>();

            return services;
        }
    }
}
