namespace Basket_API.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart) : ICommand<StoreBasketResponse>;

public record StoreBasketResponse(string UserName);

public class StoreBacketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<StoreBacketCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<StoreBasketResponse>();

            return Results.Created($"/basket/{response.UserName}", response);
        })
         .WithName("CreateBasket")
         .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Create Basket")
         .WithDescription("Create Basket");

    }
}
