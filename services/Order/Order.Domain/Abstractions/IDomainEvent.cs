using MediatR;

namespace Order.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTimeOffset? OccurredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName!;
}