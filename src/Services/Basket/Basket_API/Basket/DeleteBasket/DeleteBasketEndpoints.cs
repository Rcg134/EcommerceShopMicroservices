namespace Basket_API.Basket.DeleteBasket;

public record DeleteBasketRequest(string UserName);

public record DeleteBasketResponse(bool IsSuccess);


public class DeleteBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async ([AsParameters] DeleteBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<DeleteBasketCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<DeleteBasketResponse>();

            return Results.Ok(response);

        })
         .WithName("DeleteBasket")
         .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Delete Basket")
         .WithDescription("Delete Basket");
    }
}
