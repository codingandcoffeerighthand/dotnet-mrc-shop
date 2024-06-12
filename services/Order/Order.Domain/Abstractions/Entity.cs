namespace Order.Domain.Abstractions;

public abstract class Entity<T> : IEntity<T>
{

    public T Id { get; init; } = default!;
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? LastModifiedAt { get; set; }

    public string? CreatedBy { get; set; }
    public string? LastModifiedBy { get; set; }

}