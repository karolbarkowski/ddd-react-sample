using ProductsDomain.Domain.Common;

namespace ProductsDomain.Domain.Entities;

public class Product : Entity
{
    private readonly List<ProductImage> _images = [];

    public string Name { get; private set; }

    public string Number { get; private set; }

    public string Description { get; private set; }

    public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();


    private Product(string name, string number, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty", nameof(name));

        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Product number cannot be empty", nameof(number));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Product description cannot be empty", nameof(description));

        Name = name;
        Number = number;
        Description = description;
    }


    public static Product Create(string name, string number, string description) => new(name, number, description);

    public void UpdateDetails(string name, string number, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty", nameof(name));

        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Product number cannot be empty", nameof(number));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Product description cannot be empty", nameof(description));

        Name = name;
        Description = description;
    }

    public void AddImage(string url, string name)
    {
        ProductImage image = ProductImage.Create(url, name);
        _images.Add(image);
    }
}
