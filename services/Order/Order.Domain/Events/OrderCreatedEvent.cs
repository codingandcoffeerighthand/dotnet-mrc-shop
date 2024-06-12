using Order.Domain.Abstractions;

namespace Order.Domain.Events;

public sealed record OrderCreatedEvent(Models.Order Order) : IDomainEvent;