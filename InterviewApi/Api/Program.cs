using Microsoft.EntityFrameworkCore;
using ProductsDomain;
using ProductsDomain.Application.Commands.SeedDb;
using ProductsDomain.Infrastructure.Persistence;
using ProductsDomain.Middleware;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

//CORS configuration - allows all origins, methods, and headers
//(use only for testing/presentation purposes)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

//add our domain service
builder.Services.AddProductsDomain(builder.Configuration);

WebApplication app = builder.Build();

// Apply database migrations and seed sample data
using (IServiceScope scope = app.Services.CreateScope())
{
    ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();

    SeedDbCommandHandler seedDbHandler = scope.ServiceProvider.GetRequiredService<SeedDbCommandHandler>();
    await seedDbHandler.HandleAsync();
}

// Sample middlewares for exception handling and simple logging
app.UseExceptionHandling();
app.UseRequestLogging();
app.MapOpenApi();
app.MapScalarApiReference();

// Redirect root to API documentation
app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();

app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();

app.Run();
