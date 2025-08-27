using System.Text.RegularExpressions;

namespace TakeServUs.Domain.ValueObjects;

public class Email : ValueObject
{
  private static readonly Regex EmailRegex = new(
      @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
      RegexOptions.Compiled | RegexOptions.IgnoreCase);

  public string Value { get; private set; } = string.Empty;

  private Email() { }

  public Email(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      throw new ArgumentException("Email cannot be null or empty.", nameof(value));

    value = value.Trim();
    if (!EmailRegex.IsMatch(value))
      throw new ArgumentException("Invalid email format.", nameof(value));

    Value = value.ToLowerInvariant();
  }

  public override string ToString() => Value;

  public static implicit operator string(Email email) => email.Value;
  public static implicit operator Email(string value) => new Email(value);

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Value;
  }
}
