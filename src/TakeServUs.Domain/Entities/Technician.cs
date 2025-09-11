using System.Collections.ObjectModel;
using TakeServUs.Domain.Common;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Domain.Entities
{
  public class Technician : AuditableSoftDeletableEntity
  {
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
    public string? EmployeeCode { get; set; }
    public string? Specialization { get; set; }
    public ICollection<Skill> Skills { get; set; } = new List<Skill>();
    public bool IsAvailable { get; set; } = true;
    public double? Rating { get; set; }
    public int TotalJobsCompleted { get; set; } = 0;

    // Current location for real-time tracking
    public GpsCoordinate? CurrentLocation { get; set; }

    // Working schedule
    public ICollection<DailySchedule> WorkingSchedule { get; set; } = new List<DailySchedule>();

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
      Skills = new Collection<Skill>();
      WorkingSchedule = new Collection<DailySchedule>();
    }
  }
}
