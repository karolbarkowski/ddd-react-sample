using ProductsDomain.Domain.Entities;

namespace ProductsDomain.Application.Services;

public class ImageValidationService(IHttpClientFactory httpClientFactory) : IImageValidationService
{
    public async Task<IReadOnlyList<Product>?> FilterOutNonExistingImagesAsync(IReadOnlyList<Product>? products)
    {
        if (products == null || !products.Any())
        {
            return products;
        }

        foreach (var product in products)
        {
            await FilterOutNonExistingImagesAsync(product);
        }

        return products;
    }

    public async Task<Product?> FilterOutNonExistingImagesAsync(Product? product)
    {
        if (product?.Images == null || product.Images.Count == 0)
        {
            return product;
        }

        var httpClient = httpClientFactory.CreateClient("ImageValidation");
        var validImages = new List<ProductImage>();

        foreach (var image in product.Images)
        {
            if (string.IsNullOrWhiteSpace(image?.Url))
            {
                continue;
            }

            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, image.Url);
                using var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    validImages.Add(image);
                }
            }
            catch
            {
                // If we can't verify, assume the image is valid
                validImages.Add(image);
            }
        }

        product.SetImages(validImages);
        return product;
    }
}
