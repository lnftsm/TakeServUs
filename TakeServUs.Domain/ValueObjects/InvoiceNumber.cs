using System.Text.RegularExpressions;

namespace TakeServUs.Domain.ValueObjects;

public class InvoiceNumber : ValueObject
{
  public string Value { get; private set; } = string.Empty;

  private InvoiceNumber() { }

  public InvoiceNumber(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      throw new ArgumentException("Invoice number required.", nameof(value));

    if (!Regex.IsMatch(value, @"^[A-Z0-9-]+$"))
      throw new ArgumentException("Invoice number must contain only uppercase letters, numbers, and '-'.", nameof(value));

    Value = value.Trim().ToUpperInvariant();
  }

  public override string ToString() => Value;

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Value;
  }

  public static implicit operator string(InvoiceNumber invoice) => invoice.Value;
  public static implicit operator InvoiceNumber(string value) => new(value);
}
