using System.Collections;
using Carter;
using MediatR;
using Order.App.Dtos;
using Order.App.Orders.Queries;

namespace Order.API.Endpoints;

public sealed record GetOrderByCustomerResponse(IEnumerable<OrderDto> Orders);
public class GetOrderByCustomerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
        {

            var query = new GetOrderByCustomerQuery(customerId);
            var result = await sender.Send(query);
            return Results.Ok(result);
        }).Produces<GetOrderByCustomerResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get order by customer")
        .WithDescription("Get order by customer");
    }
}
