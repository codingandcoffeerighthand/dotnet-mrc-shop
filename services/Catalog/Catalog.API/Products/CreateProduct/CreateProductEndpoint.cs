using Category.API.Products.CreateProduct;

namespace Catalog.API.Products.CreateProduct;

public sealed record CreateProductRequest(
    string Name,
    string Description,
    string ImageFile,
    List<string> Category,
    decimal Price
);

public sealed record CreateProductResponse(Guid Id);

public sealed class CreateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"/products/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create product")
        .WithDescription("Create product");
    }
}