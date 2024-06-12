using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Order.Domain.Abstractions;

namespace Order.Infras.Data.Interceptors;

public class DispatchDomainEventsInterceptor(
    IMediator _mediator
) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEventsAsync(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        await DispatchDomainEventsAsync(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public async Task DispatchDomainEventsAsync(DbContext? context)
    {
        if (context == null) return;
        var aggregates = context.ChangeTracker.Entries<IAggergate>()
        .Where(a => a.Entity.DomainEvents.Any())
        .Select(a => a.Entity);

        var domainEvents = aggregates.SelectMany(a => a.DomainEvents).ToList();
        aggregates.ToList().ForEach(a => a.ClearDomainEvent());

        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent);
        }
    }
}
