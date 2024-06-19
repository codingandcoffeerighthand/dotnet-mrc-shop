using Basket.API.Data;
using Basket.API.Dtos;
using MassTransit;
using Shared.Messaging.Events;

namespace Basket.API.Basket.CheckoutBasket;
public sealed record CheckoutBasketCommand(
    BasketCheckoutDto BasketCheckoutDto
) : ICommand<CheckoutBasketRessult>;
public sealed record CheckoutBasketRessult(bool IsSuccess);

public class CheckoutBasketValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketValidator()
    {
        RuleFor(x => x.BasketCheckoutDto).NotNull();
    }
}

public class CheckoutBasketHandler(
        IBasketRepository basketRepository,
        IPublishEndpoint publishEndpoint

) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketRessult>
{
    public async Task<CheckoutBasketRessult> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasket(request.BasketCheckoutDto.UserName, cancellationToken);
        if (basket is null)
        {
            return new CheckoutBasketRessult(false);
        }
        var eventMessage = request.BasketCheckoutDto.Adapt<BacketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;
        await publishEndpoint.Publish(eventMessage, cancellationToken);
        await basketRepository.DeleteBasket(request.BasketCheckoutDto.UserName, cancellationToken);
        return new CheckoutBasketRessult(true);
    }
}
