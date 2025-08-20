namespace TakeServUs.Domain.ValueObjects;

public class VatNumber : ValueObject
{
  public string Value { get; private set; } = string.Empty;
  public string CountryCode { get; private set; } = string.Empty;

  private VatNumber() { }

  public VatNumber(string value, string countryCode)
  {
    if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("VAT number required.", nameof(value));
    if (string.IsNullOrWhiteSpace(countryCode)) throw new ArgumentException("Country code required.", nameof(countryCode));

    Value = value.Trim().ToUpperInvariant();
    CountryCode = countryCode.Trim().ToUpperInvariant();

    // TODO: Ülkelere göre regex kontrolü
    if (Value.Length < 8)
      throw new ArgumentException("Invalid VAT number format.", nameof(value));
  }

  public override string ToString() => $"{CountryCode}{Value}";

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Value;
    yield return CountryCode;
  }
}
