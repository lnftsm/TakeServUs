using TakeServUs.Domain.Common;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Domain.Entities
{
  public class Location : BaseEntity
  {
    public Guid TechnicianId { get; set; }
    public GpsCoordinate Coordinate { get; set; } = default!;
    public double? Speed { get; set; }
    public double? Heading { get; set; }
    public string? ActivityType { get; set; } // Driving, Walking, Stationary
    public DateTime RecordedAt { get; set; }

    // Relations
    public virtual Technician Technician { get; set; } = default!;
  }
}
