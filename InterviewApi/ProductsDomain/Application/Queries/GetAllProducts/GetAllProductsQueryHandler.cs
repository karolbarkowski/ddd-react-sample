using ProductsDomain.Application.DTOs;
using ProductsDomain.Application.Mappings;
using ProductsDomain.Domain.Entities;
using ProductsDomain.Domain.Repositories;

namespace ProductsDomain.Application.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(IProductRepository productRepository)
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<List<ProductDto>> HandleAsync(GetAllProductsQuery query, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<Product> products = await _productRepository.GetAllAsync(cancellationToken);
        return products.Select(p => p.ToDto()).ToList();
    }
}
