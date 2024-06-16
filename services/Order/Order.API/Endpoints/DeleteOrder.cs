using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Order.App.Orders.Commands.DeleteOrderCommand;

namespace Order.API.Endpoints;

public class DeleteOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (
            Guid id,
            ISender sender
        ) =>
        {
            var command = new DeleteOrderCommand(id);
            var result = await sender.Send(command);
            return Results.Accepted(result.IsSuccess.ToString());
        })
            .WithName("DeleteOrder")
            .Produces(StatusCodes.Status202Accepted)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete order")
            .WithDescription("Delete order");
    }
}
