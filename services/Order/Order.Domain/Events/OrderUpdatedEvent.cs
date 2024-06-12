using Order.Domain.Abstractions;

namespace Order.Domain.Events;

public sealed record OrderUpdatedEvent(
    Models.Order Order
) : IDomainEvent;