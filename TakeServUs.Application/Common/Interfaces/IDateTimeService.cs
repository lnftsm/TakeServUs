namespace TakeServUs.Application.Common.Interfaces;

// Provides the current time, making the application easier to test.
public interface IDateTimeService
{
  DateTime UtcNow { get; }
}
