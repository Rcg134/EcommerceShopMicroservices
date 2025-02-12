namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResponse(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            var command = new GetProductsIdQuery(id);

            var result = await sender.Send(command);

            return Results.Ok(result.Product);

        })
         .WithName("GetProductById")
         .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get Product By Id")
         .WithDescription("Get Product By Id");
    }
}
