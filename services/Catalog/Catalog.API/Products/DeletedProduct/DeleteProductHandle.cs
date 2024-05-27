
namespace Catalog.API.Products.DeleteProduct;
public sealed record DeleteProductCommand(Guid Id) : ICommand;

public sealed class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{

    public DeleteProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}
public sealed class DeleteProductHandle(IDocumentSession session) : ICommandHandler<DeleteProductCommand>
{
    public Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        session.Delete<Product>(request.Id);
        return Unit.Task;
    }

}
