
using Marten.Pagination;

namespace Catalog.API.Products.GetProductsByCategory;
public sealed record GetProductsByCategoryQuery(string Category, uint Page = 1, uint Limit = 10) : IQuery<GetProductsByCategoryResult>;
public sealed record GetProductsByCategoryResult(IPagedList<Product> Products);
public sealed class GetProductsByCategoryHandler(IDocumentSession session) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = session.Query<Product>().Where(x => x.Category.Contains(query.Category));
        var products = await dbQuery.ToPagedListAsync((int)query.Page, (int)query.Limit, cancellationToken);
        return new GetProductsByCategoryResult(products);
    }

}
