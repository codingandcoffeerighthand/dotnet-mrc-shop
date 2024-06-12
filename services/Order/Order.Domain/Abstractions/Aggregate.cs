

namespace Order.Domain.Abstractions;

public abstract class Aggregate<TId> : IEntity<TId>, IAggergate<TId>
{
    public required TId Id { get; init; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? LastModifiedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? LastModifiedBy { get; set; }

    public readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Append(domainEvent);
    }

    public IDomainEvent[] ClearDomainEvent()
    {

        var domainEvents = _domainEvents.ToArray();
        _domainEvents.Clear();
        return domainEvents;
    }
}