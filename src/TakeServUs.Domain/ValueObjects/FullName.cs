namespace TakeServUs.Domain.ValueObjects;

public class FullName : ValueObject
{
  public string FirstName { get; private set; } = string.Empty;
  public string LastName { get; private set; } = string.Empty;
  public string? MiddleName { get; private set; }

  private FullName() { }

  public FullName(string firstName, string lastName, string? middleName = null)
  {
    if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name required.", nameof(firstName));
    if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name required.", nameof(lastName));

    FirstName = firstName.Trim();
    LastName = lastName.Trim();
    MiddleName = string.IsNullOrWhiteSpace(middleName) ? null : middleName.Trim();
  }

  public string GetFullName() =>
      string.IsNullOrWhiteSpace(MiddleName) ? $"{FirstName} {LastName}" : $"{FirstName} {MiddleName} {LastName}";

  public override string ToString() => GetFullName();

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return FirstName.ToLowerInvariant();
    yield return MiddleName?.ToLowerInvariant();
    yield return LastName.ToLowerInvariant();
  }
}
