
namespace Basket_API.Data;

public class BasketRepository
    (IDocumentSession session) 
    : IBasketRepository
{
    public Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
