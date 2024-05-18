
using Marten.Pagination;

namespace Catalog.API.Products.GetProductsByCategory;
public sealed record GetProductsByCategoryResponse(IPagedList<Product> Products, long PageNumber, long Pagesize, long TotalItems);
public sealed class GetProductsByCatetoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (
            string category,
            ISender sender,
            uint pageNumber = 1,
            uint pagesize = 10
            ) =>
        {
            var result = await sender.Send(new GetProductsByCategoryQuery(category, pageNumber, pagesize));
            var pagedList = result.Products;
            var response = new GetProductsByCategoryResponse(
                pagedList, pagedList.PageNumber, pagedList.PageSize, pagedList.TotalItemCount);
            return Results.Ok(response);
        })
        .WithDescription("Get products by category")
        .WithSummary("Get products by category")
        .WithName("GetProductsByCategory")
        .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }

}
