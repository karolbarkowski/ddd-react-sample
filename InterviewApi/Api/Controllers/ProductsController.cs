using Microsoft.AspNetCore.Mvc;
using ProductsDomain.Application.Commands.EditProduct;
using ProductsDomain.Application.DTOs;
using ProductsDomain.Application.Queries.GetAllProducts;
using ProductsDomain.Application.Queries.GetProductById;

namespace ProductsDomain.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(
    GetProductByIdQueryHandler getProductByIdHandler,
    GetAllProductsQueryHandler getAllProductsHandler,
    EditProductCommandHandler editProductHandler) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        List<ProductDto> products = await getAllProductsHandler.HandleAsync(new GetAllProductsQuery(), cancellationToken);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery { ProductId = id };
        ProductDto? product = await getProductByIdHandler.HandleAsync(query, cancellationToken);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] EditProductCommand command, CancellationToken cancellationToken)
    {
        command.ProductId = id;
        await editProductHandler.HandleAsync(command, cancellationToken);
        return NoContent();
    }
}
