namespace TakeServUs.Domain.ValueObjects;

public class Money : ValueObject
{
  public decimal Amount { get; private set; }
  public Currency Currency { get; private set; }

  private Money() { }

  public Money(decimal amount, Currency currency = Currency.TRY)
  {
    if (amount < 0) throw new ArgumentException("Amount must be positive.");
    Amount = amount;
    Currency = currency;
  }

  public Money Add(Money other)
  {
    if (Currency != other.Currency)
      throw new InvalidOperationException("Currencies must match.");
    return new Money(Amount + other.Amount, Currency);
  }

  public Money Subtract(Money other)
  {
    if (Currency != other.Currency)
      throw new InvalidOperationException("Currencies must match.");
    return new Money(Amount - other.Amount, Currency);
  }

  public Money Multiply(decimal factor) => new(Amount * factor, Currency);
  public Money Divide(decimal divisor) => new(Amount / divisor, Currency);

  public override string ToString()
  {
    var symbol = Currency switch
    {
      Currency.TRY => "₺",
      Currency.USD => "$",
      Currency.EUR => "€",
      Currency.GBP => "£",
      Currency.CHF => "₣",
      Currency.JPY => "¥",
      _ => Currency.ToString()
    };
    return $"{symbol}{Amount:N2} {Currency}";
  }

  public static Money operator +(Money a, Money b) => a.Add(b);
  public static Money operator -(Money a, Money b) => a.Subtract(b);
  public static Money operator *(Money m, decimal f) => m.Multiply(f);
  public static Money operator /(Money m, decimal d) => m.Divide(d);

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Amount;
    yield return Currency;
  }
}

public enum Currency
{
  TRY = 1,
  USD = 2,
  EUR = 3,
  GBP = 4,
  CHF = 5,
  JPY = 6
}
