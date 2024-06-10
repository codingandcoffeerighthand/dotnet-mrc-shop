
using Basket.API.Data;
using Discount.Grpc;
using static Discount.Grpc.DiscountProtoService;

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
    IBasketRepository basketRepository,
    DiscountProtoServiceClient discountClient
) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        {
            foreach (var item in request.Cart.Items)
            {
                var coupon = await discountClient.GetDiscountAsync(new GetDiscountRequest
                {
                    ProductName = item.ProductName
                }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }
        }
        var shoppingCart = request.Cart;
        await basketRepository.StoreBasket(shoppingCart, cancellationToken);
        return new StoreBasketResult(true);
    }
}