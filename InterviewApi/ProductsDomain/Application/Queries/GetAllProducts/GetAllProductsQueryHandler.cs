using ProductsDomain.Application.DTOs;
using ProductsDomain.Application.Mappings;
using ProductsDomain.Application.Services;
using ProductsDomain.Domain.Entities;
using ProductsDomain.Domain.Repositories;

namespace ProductsDomain.Application.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(IProductRepository productRepository, IImageValidationService imageValidationService)
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<List<ProductDto>> HandleAsync(GetAllProductsQuery query, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<Product> products = await _productRepository.GetAllAsync(cancellationToken);

        if (products == null || products.Count == 0)
        {
            return [];
        }

        await imageValidationService.FilterOutNonExistingImagesAsync(products);

        return products
            .Select(p => p.ToDto())
            .ToList();
    }
}
