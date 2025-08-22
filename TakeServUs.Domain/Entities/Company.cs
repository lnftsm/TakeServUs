using TakeServUs.Domain.Common;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Domain.Entities;

public class Company : AuditableSoftDeletableEntity
{
  public required string Name { get; set; }
  public string? Description { get; set; }
  public string? LogoUrl { get; set; }
  public required Address Address { get; set; }
  public required PhoneNumber PhoneNumber { get; set; }
  public required string Email { get; set; }
  public string? Website { get; set; }
  public string? TaxNumber { get; set; }
  public string? RegistrationNumber { get; set; }
  public Currency Currency { get; set; } = Currency.TRY;
  public string TimeZone { get; set; } = "UTC";

  // Working Hours (JSON stored as string)
  public string? WorkingHours { get; set; }

  // Subscription/Plan info
  public string? SubscriptionPlan { get; set; }
  public DateTime? SubscriptionExpiry { get; set; }
  public int MaxTechnicians { get; set; } = 10;
  public int MaxJobsPerMonth { get; set; } = 100;

  // Relations
  public virtual ICollection<User> Users { get; set; } = default!;
  public virtual ICollection<Technician> Technicians { get; set; } = default!;
  public virtual ICollection<Job> Jobs { get; set; } = default!;
  public virtual ICollection<Customer> Customers { get; set; } = default!;
  public virtual ICollection<Material> Materials { get; set; } = default!;

  public Company()
  {
    Users = new HashSet<User>();
    Technicians = new HashSet<Technician>();
    Jobs = new HashSet<Job>();
    Customers = new HashSet<Customer>();
    Materials = new HashSet<Material>();
  }
}

