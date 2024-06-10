
using Basket.API.Data;

namespace Basket.API.Basket.StoreBasket;

public sealed record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public sealed record StoreBasketResult(bool IsSuccess);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull();
        RuleFor(x => x.Cart.UserName).NotEmpty();
    }
}

public class StoreBasketHandler(
    IBasketRepository basketRepository
) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        var shoppingCart = request.Cart;
        await basketRepository.StoreBasket(shoppingCart, cancellationToken);
        return new StoreBasketResult(true);
    }
}