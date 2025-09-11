namespace TakeServUs.Application.Interfaces.Persistence;

using Microsoft.EntityFrameworkCore;
using TakeServUs.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

public interface IApplicationDbContext
{
  DbSet<TEntity> Set<TEntity>() where TEntity : class;
  DatabaseFacade Database { get; }
  ChangeTracker ChangeTracker { get; }
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
  Task<int> SaveChangesAsync(string? userId, CancellationToken cancellationToken = default);
  EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
  EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
  EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
  EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;
  void AddRange(params object[] entities);
  void UpdateRange(params object[] entities);
  void RemoveRange(params object[] entities);

  #region DbSets
  DbSet<Company> Companies { get; }
  DbSet<Customer> Customers { get; }
  DbSet<Feedback> Feedbacks { get; }
  DbSet<Invoice> Invoices { get; }
  DbSet<InvoiceItem> InvoiceItems { get; }
  DbSet<Job> Jobs { get; }
  DbSet<JobAssignment> JobAssignments { get; }
  DbSet<JobMaterial> JobMaterials { get; }
  DbSet<JobStatus> JobStatuses { get; }
  DbSet<Location> Locations { get; }
  DbSet<Material> Materials { get; }
  DbSet<Notification> Notifications { get; }
  DbSet<QueuedEmail> QueuedEmails { get; }
  DbSet<Quote> Quotes { get; }
  DbSet<QuoteItem> QuoteItems { get; }
  DbSet<Technician> Technicians { get; }
  DbSet<User> Users { get; }

  #endregion
}
