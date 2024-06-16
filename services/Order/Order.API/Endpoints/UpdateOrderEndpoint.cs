using Carter;
using Mapster;
using MediatR;
using Order.App.Dtos;
using Order.App.Orders.Commands.UpdateOrderCommand;

namespace Order.API.Endpoints;
public sealed record UpdateOrderRequest(OrderDto Order);
public sealed record UpdateOrderResponse(bool IsSuccess);
public class UpdateOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (
            UpdateOrderRequest body,
            ISender sender) =>
        {
            var comand = body.Adapt<UpdateOrderCommand>();
            var result = await sender.Send(comand);
            var response = result.Adapt<UpdateOrderResponse>();
            return Results.Accepted();
        }).Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update order")
        .WithDescription("Update order");
    }
}
