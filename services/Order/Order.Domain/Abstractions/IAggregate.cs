namespace Order.Domain.Abstractions;

public interface IAggergate<T> : IAggergate, IEntity<T>;

public interface IAggergate : IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    IDomainEvent[] ClearDomainEvent();
}