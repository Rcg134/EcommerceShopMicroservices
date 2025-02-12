namespace Catalog.API.Products.GetProducts;

public record GetproductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
       app.MapGet("/products", async (ISender sender) => 
       {
           var result = await sender.Send(new GetproductsQuery());

           var response = result.Adapt<GetproductsResponse>();

           return Results.Ok(response);
       })
        .WithName("GetProduct")
        .Produces<GetproductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product")
        .WithDescription("Get Product"); ;
    }
}
