﻿using Basket_API.Data;

namespace Basket_API.Basket.GetBasket;

public record GetBasketRequest(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);

public class GetBasketHandler(IBasketRepository repository)
    : IQueryHandler<GetBasketRequest, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketRequest query, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(query.UserName, cancellationToken);

        return new GetBasketResult(basket);
    }
}

