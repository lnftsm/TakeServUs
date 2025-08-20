namespace TakeServUs.Domain.ValueObjects;

public class TimeRange : ValueObject
{
  public DateTime Start { get; private set; }
  public DateTime End { get; private set; }

  private TimeRange() { }

  public TimeRange(DateTime start, DateTime end)
  {
    if (end <= start) throw new ArgumentException("End must be greater than Start.");
    Start = start;
    End = end;
  }

  public TimeSpan Duration => End - Start;

  public bool Overlaps(TimeRange other) =>
      Start < other.End && End > other.Start;

  public override string ToString() => $"{Start:yyyy-MM-dd HH:mm} - {End:yyyy-MM-dd HH:mm}";

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Start;
    yield return End;
  }
}
