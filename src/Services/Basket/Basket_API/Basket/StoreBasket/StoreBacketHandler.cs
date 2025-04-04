using Discount.Grpc;

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

public class StoreBacketCommandHandler(IBasketRepository repository,
    DiscountProtoService.DiscountProtoServiceClient discountProto)
    : ICommandHandler<StoreBacketCommand, StoreBacketResult>
{
    public async Task<StoreBacketResult> Handle(StoreBacketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, cancellationToken);

        await repository.StoreBasket(command.Cart, cancellationToken);

        return new StoreBacketResult(command.Cart.UserName);
    }

    public async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        var discountTasks = cart.Items.Select(async item =>
        {
            var discount = await discountProto.GetDiscountAsync(new GetDiscountRequest
            {
                ProductName = item.ProductName
            }, cancellationToken: cancellationToken);

            if (discount.Amount > 0)
            {
                item.Price -= discount.Amount;
            }
        });

        await Task.WhenAll(discountTasks);
    }
}