namespace TakeServUs.Domain.ValueObjects;

public class DailySchedule
{
  public DayOfWeek Day { get; set; }
  public TimeOnly StartTime { get; set; }
  public TimeOnly EndTime { get; set; }
  public TimeSpan Duration => EndTime - StartTime;

  public DailySchedule(DayOfWeek day, TimeOnly startTime, TimeOnly endTime)
  {
    if (endTime <= startTime)
      throw new ArgumentException("End time must be after start time.");

    Day = day;
    StartTime = startTime;
    EndTime = endTime;
  }
}
