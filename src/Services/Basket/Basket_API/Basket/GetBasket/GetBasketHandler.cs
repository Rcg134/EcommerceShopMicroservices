namespace Basket_API.Basket.GetBasket;

public record GetBasketRequest(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);

public class GetBasketHandler : IQueryHandler<GetBasketRequest, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketRequest query, CancellationToken cancellationToken)
    {


        return new GetBasketResult(new ShoppingCart("sample"));
    }
}

