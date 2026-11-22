using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MO.Store.Domain.Catalog;
using MO.Store.Infrastructure.Persistence;

namespace MO.Store.Api.Catalog;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products")
            .WithTags("Products");

        group.MapGet("/", GetProducts)
            .WithName("GetProducts");

        return app;
    }

    private static async Task<Ok<List<ProductResponse>>> GetProducts(StoreDbContext db, CancellationToken ct)
    {
        var products = await db.Products
            .Where(p => p.Status == ProductStatus.Active)
            .OrderBy(p => p.Name)
            .Select(p => new ProductResponse(p.Id, p.CategoryId, p.Name, p.Slug, p.Description, p.PriceAgorot, p.PrepTimeHours))
            .ToListAsync(ct);

        return TypedResults.Ok(products);
    }
}
