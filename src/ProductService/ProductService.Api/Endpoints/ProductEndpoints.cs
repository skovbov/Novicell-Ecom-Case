using ProductService.Application.Queries;
using ProductService.Application.Queries.QueryDTOs;

namespace ProductService.Api.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("/api/products/{id}", async (
                string id,
                IProductQuery productQuery) =>
            {
                var product = await productQuery.GetByIdAsync(id);
                return product is null ? Results.NotFound() : Results.Ok(product);
            })
            .WithName("GetProductById")
            .WithTags("Products")
            .WithSummary("Hent et produkt på id")
            .Produces<ProductDto>()
            .Produces(404);
        
        app.MapGet("/api/products", async (
                IProductQuery productQuery,
                int pageNumber = 1,
                int pageSize = 20,
                string? searchName = null) =>
            {
                if (pageSize > 100) return Results.BadRequest("pageSize må ikke overstige 100");
        
                var products = await productQuery.GetAllAsync(pageNumber, pageSize, searchName);
                return Results.Ok(products);
            })
            .WithName("GetProducts")
            .WithTags("Products")
            .WithSummary("Hent en liste af produkter")
            .WithDescription("Understøtter pagination")
            .Produces<List<ProductDto>>()
            .Produces(400);
    }
}