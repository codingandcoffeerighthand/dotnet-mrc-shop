namespace Basket.API.Basket.DeleteBasket;

public sealed record DeleteBasketResponse(bool Success);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", DeleteBasketByUserName)
        .Produces<DeleteBasketResponse>(StatusCodes.Status202Accepted)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete basket")
        .WithDescription("Delete basket");
    }

    public async Task<IResult> DeleteBasketByUserName(
        [FromServices] ISender sender,
        string userName
    )
    {
        var command = new DeleteBasketCommand(userName);
        var result = await sender.Send(command);
        var response = result.Adapt<DeleteBasketResponse>();
        return Results.Ok(response);
    }
}
