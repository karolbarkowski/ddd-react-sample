using ProductsDomain.Domain.Common;

namespace ProductsDomain.Domain.Entities;

public class ProductImage : Entity
{
    public string Url { get; private set; }
    public string Name { get; private set; }
    public int ProductId { get; private set; }

    private ProductImage()
    {
        Url = string.Empty;
        Name = string.Empty;
    }

    private ProductImage(string url, string name)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("Image URL cannot be empty", nameof(url));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Image name cannot be empty", nameof(name));

        Url = url;
        Name = name;
    }

    public static ProductImage Create(string url, string name)
    {
        return new ProductImage(url, name);
    }
}
