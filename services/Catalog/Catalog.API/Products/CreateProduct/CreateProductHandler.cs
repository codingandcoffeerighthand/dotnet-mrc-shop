namespace Category.API.Products.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string Description,
    string ImageFile,
    List<string> Category,
    decimal Price
) : ICommand<CreateProductResult>;

public sealed record CreateProductResult(Guid Id);

public sealed class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(10, cancellationToken);
        Product product = new()
        {
            Name = request.Name,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Category = request.Category,
            Price = request.Price
        };

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(product.Id);
    }
}