using ProductsDomain.Application.DTOs;
using ProductsDomain.Domain.Entities;

namespace ProductsDomain.Application.Mappings;

public static class ProductExtensions
{
    public static ProductDto ToDto(this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name.Capitalize(),
            Number = product.Number.Capitalize(),
            Description = product.Description.Capitalize(),
            Images = product.Images.Select(i => i.ToDto()).ToList()
        };
    }

    public static ProductImageDto ToDto(this ProductImage image)
    {
        return new ProductImageDto
        {
            Url = image.Url,
            Name = image.Name
        };
    }
}
