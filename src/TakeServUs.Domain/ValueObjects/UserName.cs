using System.Text.RegularExpressions;

namespace TakeServUs.Domain.ValueObjects;

public class UserName : ValueObject
{
  public string Value { get; private set; } = string.Empty;

  private UserName() { }

  public UserName(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      throw new ArgumentException("Username cannot be empty.", nameof(value));

    if (value.Length < 3 || value.Length > 20)
      throw new ArgumentException("Username must be between 3 and 20 characters.", nameof(value));

    if (!Regex.IsMatch(value, @"^[a-zA-Z0-9._-]+$"))
      throw new ArgumentException("Username can only contain letters, numbers, '.', '-' and '_'.", nameof(value));

    Value = value.Trim();
  }

  public override string ToString() => Value;

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Value.ToLowerInvariant();
  }

  public static implicit operator string(UserName username) => username.Value;
  public static implicit operator UserName(string value) => new(value);
}
