namespace ProductsDomain.Application.DTOs;

public class ProductDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Number { get; set; }

    public required string Description { get; set; }

    public List<ProductImageDto> Images { get; set; } = [];
}
