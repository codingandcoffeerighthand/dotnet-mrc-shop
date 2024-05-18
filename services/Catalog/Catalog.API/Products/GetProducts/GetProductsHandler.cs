
namespace Catalog.API.Products.GetProducts;
public sealed record GetProductsQuery() : IQuery<GetProductsResult>;
public sealed record GetProductsResult(IEnumerable<Product> Products);
public sealed class GetProductsHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductsResult(products);
    }

}
