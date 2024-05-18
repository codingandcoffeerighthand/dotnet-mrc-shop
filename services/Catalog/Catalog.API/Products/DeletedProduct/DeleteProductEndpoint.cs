namespace Catalog.API.Products.DeleteProduct;

public sealed class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/produts/{id}", async (
            Guid id,
            ISender sender
        ) =>
        {
            var command = new DeleteProductCommand(id);
            var result = await sender.Send(command);
            return Results.NoContent();
        })
        .WithDescription("Delete product")
        .WithSummary("Delete product")
        .WithName("DeleteProduct")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }

}
