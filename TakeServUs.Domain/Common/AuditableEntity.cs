namespace TakeServUs.Domain.Common
{
  public abstract class AuditableEntity : BaseEntity
  {
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public string? LastModifiedBy { get; set; }
  }
}
