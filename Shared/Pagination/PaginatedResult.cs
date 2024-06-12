namespace Shared.Pagination;

public class PaginatedResult<TEntity>(
    int pageNumber, int pageSize, long totalCount, IEnumerable<TEntity> data
) where TEntity : class
{
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
    public long TotalCount { get; set; } = totalCount;
    public IEnumerable<TEntity> Data { get; set; } = data;
}

public sealed record PaginationRequest(int PageNumber = 1, int PageSize = 10);