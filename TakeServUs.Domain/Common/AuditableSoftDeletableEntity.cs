namespace TakeServUs.Domain.Common
{
  public abstract class AuditableSoftDeletableEntity : AuditableEntity, ISoftDelete
  {
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
  }
}
