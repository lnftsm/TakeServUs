namespace TakeServUs.Application.Interfaces.Infrastructure;

public interface IDateTimeService
{
  DateTime Now { get; }
  DateTime UtcNow { get; }
  DateTime ConvertToLocal(DateTime utcDateTime);
  DateTime ConvertToUtc(DateTime localDateTime);
  int GetBusinessDays(DateTime startDate, DateTime endDate);
  DateTime AddBusinessDays(DateTime startDate, int businessDays);
  bool IsBusinessDay(DateTime date);
  bool IsWeekend(DateTime date);
  string GetHumanReadableTimeSpan(DateTime startDate, DateTime endDate);
  string GetTimeAgo(DateTime pastDate);
  string GetTimeUntil(DateTime futureDate);
}
