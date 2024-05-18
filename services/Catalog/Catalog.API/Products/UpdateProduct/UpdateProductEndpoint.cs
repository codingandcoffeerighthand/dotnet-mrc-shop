
namespace Catalog.API.Products.UpdateProduct;
public sealed record UpdateProductRequest(
    string? Name, string? Description, string? ImageFile, decimal? Price, List<string>? Category);

public sealed record UpdateProductResponse(Guid Id);

public sealed class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id}", async (
            Guid id,
            UpdateProductRequest request,
            ISender sender
        ) =>
        {
            var command = new UpdateProductCommand(id, request.Name, request.Description, request.ImageFile, request.Price, request.Category);
            var result = await sender.Send(command);
            if (result.IsSuccess)
            {
                return Results.Accepted($"/products/{id}", id);
            }
            return Results.BadRequest();
        })
        .WithDescription("Update product")
        .WithSummary("Update product")
        .WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }

}
