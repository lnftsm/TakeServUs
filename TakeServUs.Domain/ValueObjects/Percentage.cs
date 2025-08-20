namespace TakeServUs.Domain.ValueObjects;

public class Percentage : ValueObject
{
  public decimal Value { get; private set; }

  private Percentage() { }

  public Percentage(decimal value)
  {
    if (value < 0 || value > 100)
      throw new ArgumentOutOfRangeException(nameof(value), "Percentage must be between 0 and 100.");
    Value = Math.Round(value, 2);
  }

  public decimal AsFraction() => Value / 100m;

  public override string ToString() => $"{Value:N2}%";

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Value;
  }

  public static implicit operator decimal(Percentage percentage) => percentage.Value;
  public static implicit operator Percentage(decimal value) => new(value);
}
