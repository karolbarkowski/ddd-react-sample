namespace ProductsDomain.Application.Commands.EditProduct;

public class EditProductCommand
{
    public int ProductId { get; set; }

    public required string Name { get; set; }

    public required string Number { get; set; }

    public required string Description { get; set; }
}
