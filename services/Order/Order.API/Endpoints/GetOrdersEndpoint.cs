using Carter;
using Mapster;
using MediatR;
using Order.App.Dtos;
using Order.App.Orders.Queries;
using Shared.Pagination;

namespace Order.API.Endpoints;
public sealed record GetOrdersResponse(PaginatedResult<OrderDto> Orders);
public class GetOrdersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async (ISender sender,
                    [AsParameters] PaginationRequest queryString
        ) =>
        {
            var query = new GetOrderQuery(queryString);
            var result = await sender.Send(query);
            var response = result.Adapt<GetOrdersResponse>();
            return Results.Ok(response);
        }).Produces<GetOrdersResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get orders")
        .WithDescription("Get orders");
    }
}
