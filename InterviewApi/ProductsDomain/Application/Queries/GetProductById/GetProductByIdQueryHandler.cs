using ProductsDomain.Application.DTOs;
using ProductsDomain.Application.Mappings;
using ProductsDomain.Domain.Entities;
using ProductsDomain.Domain.Repositories;

namespace ProductsDomain.Application.Queries.GetProductById;

public class GetProductByIdQueryHandler(IProductRepository productRepository)
{
    public async Task<ProductDto?> HandleAsync(GetProductByIdQuery query, CancellationToken cancellationToken = default)
    {
        Product? product = await productRepository.GetByIdAsync(query.ProductId, cancellationToken);
        return product?.ToDto();
    }
}
