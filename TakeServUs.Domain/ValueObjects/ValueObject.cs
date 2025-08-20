namespace TakeServUs.Domain.ValueObjects;

public abstract class ValueObject
{
  protected abstract IEnumerable<object?> GetEqualityComponents();

  public override bool Equals(object? obj)
  {
    if (obj is null || obj.GetType() != GetType())
      return false;

    return GetEqualityComponents().SequenceEqual(((ValueObject)obj).GetEqualityComponents());
  }

  public override int GetHashCode()
  {
    return GetEqualityComponents()
          .Aggregate(1, (hash, obj) => HashCode.Combine(hash, obj?.GetHashCode() ?? 0));
  }
}
