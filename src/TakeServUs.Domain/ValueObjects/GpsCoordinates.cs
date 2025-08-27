namespace TakeServUs.Domain.ValueObjects;

public class GpsCoordinates : ValueObject
{
  public decimal Latitude { get; private set; }
  public decimal Longitude { get; private set; }
  public DateTime Timestamp { get; private set; }
  public decimal? Accuracy { get; private set; }

  private GpsCoordinates() { }

  public GpsCoordinates(decimal latitude, decimal longitude, DateTime? timestamp = null, decimal? accuracy = null)
  {
    if (latitude < -90 || latitude > 90) throw new ArgumentOutOfRangeException(nameof(latitude));
    if (longitude < -180 || longitude > 180) throw new ArgumentOutOfRangeException(nameof(longitude));
    if (accuracy.HasValue && accuracy < 0) throw new ArgumentOutOfRangeException(nameof(accuracy));

    Latitude = latitude;
    Longitude = longitude;
    Timestamp = timestamp ?? DateTime.UtcNow;
    Accuracy = accuracy;
  }

  public double DistanceTo(GpsCoordinates other)
  {
    const int R = 6371;
    var dLat = ToRadians((double)(other.Latitude - Latitude));
    var dLon = ToRadians((double)(other.Longitude - Longitude));
    var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(ToRadians((double)Latitude)) * Math.Cos(ToRadians((double)other.Latitude)) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
    var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
    return R * c;
  }

  private static double ToRadians(double deg) => deg * Math.PI / 180;

  public override string ToString() =>
      $"{Latitude}, {Longitude} (±{Accuracy ?? 0}m) @ {Timestamp:yyyy-MM-dd HH:mm:ss}";

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Latitude;
    yield return Longitude;
    yield return Timestamp;
    yield return Accuracy;
  }
}
