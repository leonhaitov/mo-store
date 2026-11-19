using Microsoft.AspNetCore.Http.HttpResults;

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

    private static Ok<List<ProductResponse>> GetProducts()
    {
        return TypedResults.Ok(new List<ProductResponse>());
    }
}
