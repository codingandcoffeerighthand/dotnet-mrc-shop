
namespace Catalog.API.Products.GetProductById;
public sealed record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public sealed record GetProductByIdResult(Product Product);
public sealed class GetProductByIdHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await session.Query<Product>().Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        if (product == null)
        {
            throw new ProductNotFoundException();
        }
        return new GetProductByIdResult(product);
    }
}
