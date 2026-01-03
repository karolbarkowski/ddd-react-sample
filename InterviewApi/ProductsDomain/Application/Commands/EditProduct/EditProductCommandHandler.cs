using ProductsDomain.Domain.Entities;
using ProductsDomain.Domain.Repositories;

namespace ProductsDomain.Application.Commands.EditProduct;

public class EditProductCommandHandler(IProductRepository productRepository)
{
    public async Task HandleAsync(EditProductCommand command, CancellationToken cancellationToken = default)
    {
        Product product = await productRepository.GetByIdAsync(command.ProductId, cancellationToken)
            ?? throw new InvalidOperationException($"Product with ID {command.ProductId} not found");

        product.UpdateDetails(command.Name, command.Number, command.Description);

        await productRepository.UpdateAsync(product, cancellationToken);
    }
}
