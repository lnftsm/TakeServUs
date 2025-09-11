using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using TakeServUs.Application.Interfaces.Infrastructure;
using TakeServUs.Application.Interfaces.Persistence;
using TakeServUs.Domain.Common;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Persistence.DbContexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
  private readonly ICurrentUserService? _currentUserService;
  private readonly IDateTimeService? _dateTimeService;
  private IDbContextTransaction? _currentTransaction;

  public ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options) : base(options)
  {
  }

  public ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    ICurrentUserService? currentUserService,
    IDateTimeService dateTimeService) : base(options)
  {
    _currentUserService = currentUserService;
    _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
  }

  #region DbSets
  public DbSet<Company> Companies => Set<Company>();
  public DbSet<Customer> Customers => Set<Customer>();
  public DbSet<Feedback> Feedbacks => Set<Feedback>();
  public DbSet<Invoice> Invoices => Set<Invoice>();
  public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
  public DbSet<Job> Jobs => Set<Job>();
  public DbSet<JobAssignment> JobAssignments => Set<JobAssignment>();
  public DbSet<JobMaterial> JobMaterials => Set<JobMaterial>();
  public DbSet<JobStatus> JobStatuses => Set<JobStatus>();
  public DbSet<Location> Locations => Set<Location>();
  public DbSet<Material> Materials => Set<Material>();
  public DbSet<Notification> Notifications => Set<Notification>();
  public DbSet<QueuedEmail> QueuedEmails => Set<QueuedEmail>();
  public DbSet<Quote> Quotes => Set<Quote>();
  public DbSet<QuoteItem> QuoteItems => Set<QuoteItem>();
  public DbSet<Technician> Technicians => Set<Technician>();
  public DbSet<User> Users => Set<User>();

  #endregion

  public new DbSet<TEntity> Set<TEntity>() where TEntity : class
  {
    return base.Set<TEntity>();
  }

  public new EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
  {
    return base.Entry(entity);
  }

  public new EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class
  {
    return base.Add(entity);
  }

  public new EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class
  {
    return base.Update(entity);
  }

  public new EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class
  {
    return base.Remove(entity);
  }

  public new void AddRange(params object[] entities)
  {
    base.AddRange(entities);
  }

  public new void UpdateRange(params object[] entities)
  {
    base.UpdateRange(entities);
  }

  public new void RemoveRange(params object[] entities)
  {
    base.RemoveRange(entities);
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    UpdateAuditFields();
    return await base.SaveChangesAsync(cancellationToken);
  }
  public async Task<int> SaveChangesAsync(string? userId, CancellationToken cancellationToken = default)
  {
    UpdateAuditFields(userId);
    return await base.SaveChangesAsync(cancellationToken);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
    {
      // Apply global query filter for soft deleted entities
      if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
      {
        // Create the expression tree for the filter: e => !e.IsDeleted
        var parameter = Expression.Parameter(entityType.ClrType, "e");
        var property = Expression.Property(parameter, nameof(ISoftDelete.IsDeleted));
        var filter = Expression.Not(property);
        var lambda = Expression.Lambda(filter, parameter);

        modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
      }
    }
  }

  private void UpdateAuditFields(string? userId = null)
  {
    var entries = ChangeTracker.Entries<BaseEntity>();
    var now = _dateTimeService?.UtcNow ?? DateTime.UtcNow;
    userId ??= _currentUserService?.UserIdString;

    foreach (var entry in entries)
    {
      // Handle AuditableEntity and its derivatives
      if (entry.Entity is AuditableEntity auditableEntity)
      {
        switch (entry.State)
        {
          case EntityState.Added:
            auditableEntity.CreatedAt = now;
            auditableEntity.CreatedBy = userId;
            auditableEntity.LastModifiedAt = now;
            auditableEntity.LastModifiedBy = userId;
            break;

          case EntityState.Modified:
            auditableEntity.LastModifiedAt = now;
            auditableEntity.LastModifiedBy = userId;

            // Prevent modification of CreatedAt and CreatedBy
            entry.Property(nameof(AuditableEntity.CreatedAt)).IsModified = false;
            entry.Property(nameof(AuditableEntity.CreatedBy)).IsModified = false;

            // Handle soft delete if applicable
            if (entry.Entity is ISoftDelete softDeleteEntity)
            {
              var isDeletedProperty = entry.Property(nameof(ISoftDelete.IsDeleted));

              if (isDeletedProperty.IsModified &&
                  isDeletedProperty.CurrentValue!.Equals(true) &&
                  isDeletedProperty.OriginalValue!.Equals(false))
              {
                softDeleteEntity.DeletedAt = now;
                softDeleteEntity.DeletedBy = userId;
              }
            }
            break;
        }
      }
      // Handle AuditableSoftDeletableEntity specifically
      else if (entry.Entity is ISoftDelete softDeleteOnlyEntity &&
               entry.State == EntityState.Modified)
      {
        var isDeletedProperty = entry.Property(nameof(ISoftDelete.IsDeleted));

        if (isDeletedProperty.IsModified &&
            isDeletedProperty.CurrentValue!.Equals(true) &&
            isDeletedProperty.OriginalValue!.Equals(false))
        {
          softDeleteOnlyEntity.DeletedAt = now;
          softDeleteOnlyEntity.DeletedBy = userId;
        }
      }
    }
  }
}
