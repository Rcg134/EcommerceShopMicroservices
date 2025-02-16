namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryResponse(Product product);

public class GetProductByCateogryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            var command = new GetProductByCategoryQuery(category);

            var result = await sender.Send(command);

            return Results.Ok(result.Product);
        })
         .WithName("GetProductByCategory")
         .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get Product By Category")
         .WithDescription("Get Product By Category");
    }
}