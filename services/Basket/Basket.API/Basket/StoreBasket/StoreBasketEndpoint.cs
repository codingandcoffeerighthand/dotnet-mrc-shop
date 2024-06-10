
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Basket.StoreBasket;

public sealed record StoreBasketRequest(ShoppingCart Cart);
public sealed record StoreBasketResponse(bool IsSuccess);

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/{userName}", StoreBasket)
            .WithName("StoreBasket")
            .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Store basket")
            .WithDescription("Store basket");
    }
    public async Task<IResult> StoreBasket(
        ISender sender,
        string userName,
        [FromBody] StoreBasketRequest request
    )
    {
        request.Cart.UserName = userName;
        var command = new StoreBasketCommand(request.Cart);
        var result = await sender.Send(command);
        var response = result.Adapt<StoreBasketResponse>();
        return Results.Ok(response);
    }
}
