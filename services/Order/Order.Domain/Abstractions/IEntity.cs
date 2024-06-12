namespace Order.Domain.Abstractions;
public interface IEntity<T> : IEntity
{
    public T Id { get; }
}

public interface IEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? LastModifiedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? LastModifiedBy { get; set; }
}