
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Basket.GetBasket;

public sealed record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", GetBasket)
            .WithName("GetBasket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .WithSummary("Get basket")
            .WithDescription("Get basket by id");
    }

    public async Task<IResult> GetBasket(
        [FromServices] ISender sender,
        string userName
    )
    {
        var result = await sender.Send(new GetBasketQuery(userName));
        var response = result.Adapt<GetBasketResponse>();
        return Results.Ok(response);
    }
}