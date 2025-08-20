using TakeServUs.Domain.Common;

namespace TakeServUs.Domain.Entities
{
  public class Technician : AuditableSoftDeletableEntity
  {
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
    public string? EmployeeCode { get; set; }
    public string? Specialization { get; set; }
    public string? Skills { get; set; } // JSON array of skills
    public bool IsAvailable { get; set; } = true;
    public double? Rating { get; set; }
    public int TotalJobsCompleted { get; set; } = 0;

    // Current location for real-time tracking
    public double? CurrentLatitude { get; set; }
    public double? CurrentLongitude { get; set; }
    public DateTime? LastLocationUpdate { get; set; }

    // Working schedule
    public string? WorkingDays { get; set; } // JSON array of days
    public TimeSpan? WorkStartTime { get; set; }
    public TimeSpan? WorkEndTime { get; set; }

    // Vehicle information
    public string? VehicleType { get; set; }
    public string? VehiclePlateNumber { get; set; }

    // Relations
    public virtual User User { get; set; } = default!;
    public virtual Company Company { get; set; } = default!;
    public virtual ICollection<JobAssignment> JobAssignments { get; set; } = default!;
    public virtual ICollection<Location> LocationHistory { get; set; } = default!;

    public Technician()
    {
      JobAssignments = new HashSet<JobAssignment>();
      LocationHistory = new HashSet<Location>();
    }
  }
}
