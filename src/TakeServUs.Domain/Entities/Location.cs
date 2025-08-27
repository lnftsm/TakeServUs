using TakeServUs.Domain.Common;

namespace TakeServUs.Domain.Entities
{
  public class Location : BaseEntity
  {
    public Guid TechnicianId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double? Accuracy { get; set; }
    public double? Speed { get; set; }
    public double? Heading { get; set; }
    public DateTime RecordedAt { get; set; }
    public string? ActivityType { get; set; } // Driving, Walking, Stationary

    // Relations
    public virtual Technician Technician { get; set; } = default!;
  }
}
