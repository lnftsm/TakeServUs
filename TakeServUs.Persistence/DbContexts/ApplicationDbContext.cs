using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Domain.Common;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Persistence.DbContexts;

public class ApplicationDbContext : DbContext
{
  public DbSet<Company> Companies { get; set; }
  public DbSet<Customer> Customers { get; set; }
  public DbSet<Feedback> Feedbacks { get; set; }
  public DbSet<Invoice> Invoices { get; set; }
  public DbSet<Job> Jobs { get; set; }
  public DbSet<JobAssignment> JobAssignments { get; set; }
  public DbSet<Location> Locations { get; set; }
  public DbSet<Material> Materials { get; set; }
  public DbSet<Notification> Notifications { get; set; }
  public DbSet<Quote> Quotes { get; set; }
  public DbSet<Technician> Technicians { get; set; }
  public DbSet<User> Users { get; set; }


  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // Tüm IEntityTypeConfiguration sınıflarını bu projeden (assembly) bulur ve uygular.
    // Bu sayede her yeni konfigürasyon eklediğinizde burayı değiştirmeniz gerekmez.
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    base.OnModelCreating(modelBuilder);
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    // Bu metot, veritabanına bir kayıt eklendiğinde veya güncellendiğinde
    // AuditableEntity'den gelen alanları otomatik olarak doldurur.
    foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
    {
      switch (entry.State)
      {
        case EntityState.Added:
          // TODO: Mevcut kullanıcı ID'sini alacak bir servis enjekte edilmeli.
          // entry.Entity.CreatedBy = _currentUserService.UserId;
          entry.Entity.CreatedAt = DateTime.UtcNow;
          break;

        case EntityState.Modified:
          // TODO: Mevcut kullanıcı ID'sini alacak bir servis enjekte edilmeli.
          // entry.Entity.LastModifiedBy = _currentUserService.UserId;
          entry.Entity.LastModifiedAt = DateTime.UtcNow;
          break;
      }
    }

    return await base.SaveChangesAsync(cancellationToken);
  }
}
