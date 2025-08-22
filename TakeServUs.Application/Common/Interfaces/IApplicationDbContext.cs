using Microsoft.EntityFrameworkCore;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.Common.Interfaces;

// This interface decouples the Application layer from the concrete DbContext in Infrastructure.
public interface IApplicationDbContext
{
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
  DbSet<Quote> Quotes { get; }
  DbSet<QuoteItem> QuoteItems { get; }
  DbSet<Technician> Technicians { get; }
  DbSet<User> Users { get; }

  Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
