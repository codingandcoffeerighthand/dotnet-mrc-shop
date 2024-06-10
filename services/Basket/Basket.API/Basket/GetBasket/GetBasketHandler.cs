
using Basket.API.Data;

namespace Basket.API.Basket.GetBasket;

public sealed record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public sealed record GetBasketResult(ShoppingCart Cart);


public class GetBasketQueryValidator : AbstractValidator<GetBasketQuery>
{
    public GetBasketQueryValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
    }
}

public class GetBasketHandler(
    IBasketRepository basketRepository
) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var shoppingCart = await basketRepository.GetBasket(request.UserName, cancellationToken);
        return new GetBasketResult(shoppingCart);
    }
}