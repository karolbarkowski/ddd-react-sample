using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProductsDomain.Infrastructure.Persistence;

/// <summary>
/// Design-time factory for creating ApplicationDbContext instances during migrations.
/// This allows EF Core tools to create the DbContext without needing to run the full application.
/// </summary>
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlite("Data Source=Infrastructure/Persistence/Db/products.db");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
