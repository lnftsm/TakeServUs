namespace TakeServUs.Domain.ValueObjects;

public class PhoneNumber : ValueObject
{
  public string CountryCode { get; private set; }
  public string Number { get; private set; }

  public PhoneNumber(string countryCode, string number)
  {
    CountryCode = countryCode ?? throw new ArgumentNullException(nameof(countryCode));
    Number = number ?? throw new ArgumentNullException(nameof(number));
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
