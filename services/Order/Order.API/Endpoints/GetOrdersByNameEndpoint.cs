using Carter;
using Mapster;
using MediatR;
using Order.App.Dtos;
using Order.App.Orders.Queries;

namespace Order.API.Endpoints;
public sealed record GetOrderByNameResponse(IEnumerable<OrderDto> Orders);
public class GetOrdersByNameEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{name}", async (string name, ISender sender) =>
        {
            var command = new GetOrderByNameQuery(name);
            var result = await sender.Send(command);
            var response = result.Adapt<GetOrderByNameResponse>();
            return Results.Ok(response);
        }).Produces<GetOrderByNameResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get orders by name")
        .WithDescription("Get orders by name");
    }
}
