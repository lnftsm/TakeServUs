namespace TakeServUs.Domain.ValueObjects;

public class PhoneNumber : ValueObject
{
  public string CountryCode { get; private set; }
  public string Number { get; private set; }

  public PhoneNumber(string countryCode, string number)
  {
    if (string.IsNullOrWhiteSpace(countryCode)) throw new ArgumentNullException(nameof(countryCode));
    if (string.IsNullOrWhiteSpace(number)) throw new ArgumentNullException(nameof(number));

    if (!countryCode.StartsWith("+")) countryCode = "+" + countryCode;

    if (number.Length < 7 || number.Length > 15) throw new ArgumentException("Invalid phone number length");

    CountryCode = countryCode;
    Number = number;
  }

  protected override IEnumerable<object> GetEqualityComponents()
  {
    yield return CountryCode;
    yield return Number;
  }

  public override string ToString()
  {
    return $"{CountryCode}{Number}";
  }
}
