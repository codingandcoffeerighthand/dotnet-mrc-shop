
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;
public sealed record GetProductsQuery(int PageNumber = 1, int PageSize = 10) : IQuery<GetProductsResult>;
public sealed record GetProductsResult(IEnumerable<Product> Products);
public sealed class GetProductsHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().
            ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
        return new GetProductsResult(products);
    }

}
