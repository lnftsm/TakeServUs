using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Persistence.DbContexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

  public DbSet<Company> Companies { get; set; }
  public DbSet<Customer> Customers { get; set; }
  public DbSet<Feedback> Feedbacks { get; set; }
  public DbSet<Invoice> Invoices { get; set; }
  public DbSet<InvoiceItem> InvoiceItems { get; set; }
  public DbSet<Job> Jobs { get; set; }
  public DbSet<JobAssignment> JobAssignments { get; set; }
  public DbSet<JobMaterial> JobMaterials { get; set; }
  public DbSet<JobStatus> JobStatuses { get; set; }
  public DbSet<Location> Locations { get; set; }
  public DbSet<Material> Materials { get; set; }
  public DbSet<Notification> Notifications { get; set; }
  public DbSet<QueuedEmail> QueuedEmails { get; set; }
  public DbSet<Quote> Quotes { get; set; }
  public DbSet<QuoteItem> QuoteItems { get; set; }
  public DbSet<Technician> Technicians { get; set; }
  public DbSet<User> Users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    base.OnModelCreating(modelBuilder);
  }
}
