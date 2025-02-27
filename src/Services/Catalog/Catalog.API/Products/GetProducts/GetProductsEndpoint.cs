namespace Catalog.API.Products.GetProducts;

public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);

public record GetproductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetproductsQuery>();

            var result = await sender.Send(query);

            var response = result.Adapt<GetproductsResponse>();

            return Results.Ok(response);
        })
         .WithName("GetProduct")
         .Produces<GetproductsResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get Product")
         .WithDescription("Get Product");
    }
}
