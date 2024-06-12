using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Order.Domain.Abstractions;

namespace Order.Infras.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    public void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<IEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = "system";
                entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
            }
            if (entry.State == EntityState.Modified || entry.State == EntityState.Added
                    || entry.HashChangedOwnedEntities())
            {

                entry.Entity.LastModifiedBy = "system";
                entry.Entity.LastModifiedAt = DateTimeOffset.UtcNow;
            }
        }
    }

}
public static class Extensions
{
    public static bool HashChangedOwnedEntities(this EntityEntry entry)
        => entry.References.Any(r =>
                r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
                r.TargetEntry?.State == EntityState.Modified ||
                r.TargetEntry?.State == EntityState.Added
        );
}