using ProductsDomain.Application.DTOs;
using ProductsDomain.Domain.Entities;

namespace ProductsDomain.Application.Mappings;

public static class MappingExtensions
{
    public static ProductDto ToDto(this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Number = product.Number,
            Description = product.Description,
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
