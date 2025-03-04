namespace Basket_API.Basket.StoreBasket;

public record StoreBacketCommand(ShoppingCart Cart) : ICommand<StoreBacketResult>;

public record StoreBacketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBacketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart must not be empty");
        RuleFor(x => x.Cart.UserName).NotNull().WithMessage("Username must not be empty");
    }
}

public class StoreBacketCommandHandler(IBasketRepository repository)
    : ICommandHandler<StoreBacketCommand, StoreBacketResult>
{
    public async Task<StoreBacketResult> Handle(StoreBacketCommand command, CancellationToken cancellationToken)
    {
        var cart = command.Cart;

        await repository.StoreBasket(cart, cancellationToken);

        return new StoreBacketResult(cart.UserName);
    }
}