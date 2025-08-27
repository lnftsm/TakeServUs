namespace TakeServUs.Domain.ValueObjects;

public class Address : ValueObject
{
  public string Street { get; private set; } = string.Empty;
  public string City { get; private set; } = string.Empty;
  public string State { get; private set; } = string.Empty;
  public string PostalCode { get; private set; } = string.Empty;
  public string Country { get; private set; } = string.Empty;
  public decimal? Latitude { get; private set; }
  public decimal? Longitude { get; private set; }

  private Address() { }

  public Address(string street, string city, string state, string postalCode, string country,
                  decimal? latitude = null, decimal? longitude = null)
  {
    if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException(nameof(street));
    if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException(nameof(city));
    if (string.IsNullOrWhiteSpace(state)) throw new ArgumentException(nameof(state));
    if (string.IsNullOrWhiteSpace(postalCode)) throw new ArgumentException(nameof(postalCode));
    if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException(nameof(country));

    Street = street.Trim();
    City = city.Trim();
    State = state.Trim();
    PostalCode = postalCode.Trim();
    Country = country.Trim();
    Latitude = latitude;
    Longitude = longitude;
  }

  public override string ToString()
  {
    var parts = new List<string> { Street, City, State, PostalCode, Country };
    return string.Join(", ", parts.Where(x => !string.IsNullOrWhiteSpace(x)));
  }

  public bool HasCoordinates() => Latitude.HasValue && Longitude.HasValue;

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Street;
    yield return City;
    yield return State;
    yield return PostalCode;
    yield return Country;
    yield return Latitude;
    yield return Longitude;
  }
}
