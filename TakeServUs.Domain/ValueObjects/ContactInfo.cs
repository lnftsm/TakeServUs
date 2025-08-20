namespace TakeServUs.Domain.ValueObjects;

public class ContactInfo : ValueObject
{
  public Email? Email { get; private set; }
  public PhoneNumber? Phone { get; private set; }
  public PhoneNumber? AlternatePhone { get; private set; }

  private ContactInfo() { }

  public ContactInfo(Email? email = null, PhoneNumber? phone = null, PhoneNumber? alternatePhone = null)
  {
    if (email == null && phone == null)
      throw new ArgumentException("At least one of Email or Phone must be provided.");

    Email = email;
    Phone = phone;
    AlternatePhone = alternatePhone;
  }

  public override string ToString()
  {
    var parts = new List<string>();
    if (Email != null) parts.Add(Email.ToString());
    if (Phone != null) parts.Add(Phone.ToString());
    if (AlternatePhone != null) parts.Add($"Alt: {AlternatePhone}");
    return string.Join(", ", parts);
  }

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Email;
    yield return Phone;
    yield return AlternatePhone;
  }
}
