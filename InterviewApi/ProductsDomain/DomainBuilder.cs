using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsDomain.Application.Commands.EditProduct;
using ProductsDomain.Application.Commands.SeedDb;
using ProductsDomain.Application.Queries.GetAllProducts;
using ProductsDomain.Application.Queries.GetProductById;
using ProductsDomain.Application.Services;
using ProductsDomain.Domain.Repositories;
using ProductsDomain.Infrastructure.Persistence;
using ProductsDomain.Infrastructure.Persistence.Repositories;

namespace ProductsDomain
{
    public static class DomainBuilder
    {
        public static IServiceCollection AddProductsDomain(this IServiceCollection services, IConfiguration configuration)
        {
            // Infrastructure - DbContext with SQLite
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            // Infrastructure - Repositories
            services.AddScoped<IProductRepository, ProductRepository>();

            // Infrastructure - HttpClient for image validation
            services.AddHttpClient("ImageValidation", client =>
            {
                client.Timeout = TimeSpan.FromSeconds(5);
            });

            // Application - Services
            services.AddScoped<IImageValidationService, ImageValidationService>();

            // Application - Command/Query Handlers
            services.AddScoped<EditProductCommandHandler>();
            services.AddScoped<SeedDbCommandHandler>();
            services.AddScoped<GetProductByIdQueryHandler>();
            services.AddScoped<GetAllProductsQueryHandler>();

            return services;
        }
    }
}
