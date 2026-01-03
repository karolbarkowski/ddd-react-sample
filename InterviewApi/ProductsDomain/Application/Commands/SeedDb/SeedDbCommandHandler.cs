using ProductsDomain.Domain.Entities;
using ProductsDomain.Infrastructure.Persistence;

namespace ProductsDomain.Application.Commands.SeedDb;

public class SeedDbCommandHandler(ApplicationDbContext context)
{
    public async Task HandleAsync()
    {
        if (context.Products.Any())
            return;

        Product product1 = Product.Create(
       "b0006se5bq",
       "singing coach unlimited",
       "singing coach unlimited - electronic learning products(win me nt 2000 xp)");
        product1.AddImage("https://picsum.photos/400/300", "singing coach");
        product1.AddImage("https://broken.link.for.testing.notexistingtopleveldomain/400/300", "front side");

        Product product2 = Product.Create(
            "b00021xhzw",
            "adobe after effects professional 6.5 upgrade from standard to professional",
            "upgrade only; installation of after effects standard new disk caching tools speed up your interactive work save any combination of animation parameters as presets");

        Product product3 = Product.Create(
            "b00021xhzw",
            "domino designer/developer v5.0",
            "reference domino designer/developer r5 doc pack includes the following titles: application development with domino designer (intermediate - advanced) 536 pages");
        product3.AddImage("https://picsum.photos/400/300", "cover");

        context.Products.AddRange(product1, product2, product3);
        context.SaveChanges();
    }
}
