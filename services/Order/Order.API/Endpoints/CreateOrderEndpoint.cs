using Carter;
using Mapster;
using MediatR;
using Order.App.Dtos;
using Order.App.Orders.Commands.CreateOrderCommand;

namespace Order.API.Endpoints;

public sealed record CreateOrderRequest(OrderDto Order);
public sealed record CreateOrderResponse(Guid Id);

public class CreateOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (
            CreateOrderRequest body,
            ISender sender
        ) =>
        {
            var command = body.Adapt<CreateOrderCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateOrderResponse>();
            return Results.Created($"/orders/{response.Id}", response);
        })
        .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create order")
        .WithDescription("Create order");
    }
}
