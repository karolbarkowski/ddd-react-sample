using ProductsDomain.Domain.Entities;

namespace ProductsDomain.Application.Services;

public interface IImageValidationService
{
    Task<IReadOnlyList<Product>?> FilterOutNonExistingImagesAsync(IReadOnlyList<Product>? products);

    Task<Product?> FilterOutNonExistingImagesAsync(Product? product);
}
