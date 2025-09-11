namespace TakeServUs.Domain.ValueObjects;

public class Address : ValueObject
{
  public string Street { get; private set; } = string.Empty;
  public string City { get; private set; } = string.Empty;
  public string State { get; private set; } = string.Empty;
  public string PostalCode { get; private set; } = string.Empty;
  public string Country { get; private set; } = string.Empty;
  public GpsCoordinate Coordinates { get; set; } = default!;

  private Address() { }

  public Address(string street, string city, string state, string postalCode, string country,
                  GpsCoordinate coordinates)
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
    Coordinates = coordinates;
  }

  public override string ToString()
  {
    var parts = new List<string> { Street, City, State, PostalCode, Country };
    return string.Join(", ", parts.Where(x => !string.IsNullOrWhiteSpace(x)));
  }

  public bool HasCoordinates() => Coordinates != null;

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Street;
    yield return City;
    yield return State;
    yield return PostalCode;
    yield return Country;
    yield return Coordinates;
  }
}
