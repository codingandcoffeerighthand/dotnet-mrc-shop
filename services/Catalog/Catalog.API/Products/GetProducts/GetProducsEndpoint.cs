
namespace Catalog.API.Products.GetProducts;
public sealed record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
public sealed record GetProductsResponse(IEnumerable<Product> Products);
public sealed class GetProducsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (
            [AsParameters] GetProductsRequest request,
            ISender sender) =>
        {
            var query = new GetProductsQuery(request.PageNumber ?? 1, request.PageSize ?? 10);
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Get products")
        .WithSummary("Get products");
    }

}
