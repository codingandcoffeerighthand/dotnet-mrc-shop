
namespace Catalog.API.Products.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid Id,
    string? Name, string? Description, string? ImageFile, decimal? Price, List<string>? Category
) : ICommand<UpdateProductResult>;
public sealed record UpdateProductResult(bool IsSuccess);

public sealed class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        // rule for price if price not null
        RuleFor(x => x.Price)
            .NotEmpty().GreaterThanOrEqualTo(0)
            .When(x => x.Price != null);
    }
}
public class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session
            .Query<Product>()
            .Where(x => x.Id == command.Id)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new ProductNotFoundException();
        if (command.Name != null) product.Name = command.Name;
        if (command.Description != null) product.Description = command.Description;
        if (command.ImageFile != null) product.ImageFile = command.ImageFile;
        if (command.Price != null) product.Price = (decimal)command.Price;
        if (command.Category != null) product.Category = command.Category;
        await session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);
    }
}
