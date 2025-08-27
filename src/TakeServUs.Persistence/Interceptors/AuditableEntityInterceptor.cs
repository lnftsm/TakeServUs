using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Domain.Common;

namespace TakeServUs.Persistence.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
  private readonly ICurrentUserService _currentUserService;
  private readonly IDateTime _dateTime;

  public AuditableEntityInterceptor(ICurrentUserService currentUserService, IDateTime dateTime)
  {
    _currentUserService = currentUserService;
    _dateTime = dateTime;
  }

  public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
  {
    UpdateAuditableEntities(eventData.Context);
    return base.SavingChanges(eventData, result);
  }

  public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
      DbContextEventData eventData,
      InterceptionResult<int> result,
      CancellationToken cancellationToken = default)
  {
    UpdateAuditableEntities(eventData.Context);
    return base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  private void UpdateAuditableEntities(DbContext? context)
  {
    if (context == null) return;

    var userId = _currentUserService.UserId;
    var now = _dateTime.UtcNow; // UTC kullanmak daha güvenli

    context.ChangeTracker.Entries<AuditableEntity>()
        .Where(entry => entry.State is EntityState.Added or EntityState.Modified)
        .ToList()
        .ForEach(entry => UpdateAuditFields(entry, userId, now));
  }

  private static void UpdateAuditFields(
      Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry,
      string? userId,
      DateTime now)
  {
    switch (entry.State)
    {
      case EntityState.Added:
        entry.Entity.CreatedAt = now;
        entry.Entity.CreatedBy = userId;
        break;
      case EntityState.Modified:
        entry.Entity.LastModifiedAt = now;
        entry.Entity.LastModifiedBy = userId;
        break;
    }
  }
}
