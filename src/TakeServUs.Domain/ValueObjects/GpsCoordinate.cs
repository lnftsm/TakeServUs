namespace TakeServUs.Domain.ValueObjects;

using System;
using System.Collections.Generic;
using System.Globalization;
public class GpsCoordinate : ValueObject
{
  public decimal Latitude { get; private set; }
  public decimal Longitude { get; private set; }
  public DateTime Timestamp { get; private set; }
  public decimal? Accuracy { get; private set; } // meter

  private GpsCoordinate() { }

  public GpsCoordinate(decimal latitude, decimal longitude, DateTime? timestamp = null, decimal? accuracy = null)
  {
    ValidateLatitude(latitude);
    ValidateLongitude(longitude);
    ValidateAccuracy(accuracy);

    Latitude = latitude;
    Longitude = longitude;
    Timestamp = timestamp ?? DateTime.UtcNow;
    Accuracy = accuracy;
  }

  public static GpsCoordinate FromStrings(string latitude, string longitude, DateTime? timestamp = null, string? accuracy = null)
  {
    if (string.IsNullOrWhiteSpace(latitude)) throw new ArgumentException("Latitude cannot be null or empty", nameof(latitude));
    if (string.IsNullOrWhiteSpace(longitude)) throw new ArgumentException("Longitude cannot be null or empty", nameof(longitude));

    if (!decimal.TryParse(latitude, NumberStyles.Float, CultureInfo.InvariantCulture, out var lat))
      throw new ArgumentException($"Invalid latitude format: {latitude}", nameof(latitude));

    if (!decimal.TryParse(longitude, NumberStyles.Float, CultureInfo.InvariantCulture, out var lng))
      throw new ArgumentException($"Invalid longitude format: {longitude}", nameof(longitude));

    decimal? acc = null;
    if (!string.IsNullOrWhiteSpace(accuracy))
    {
      if (!decimal.TryParse(accuracy, NumberStyles.Float, CultureInfo.InvariantCulture, out var accValue))
        throw new ArgumentException($"Invalid accuracy format: {accuracy}", nameof(accuracy));
      acc = accValue;
    }

    return new GpsCoordinate(lat, lng, timestamp, acc);
  }

  // Calculate distance to another GpsCoordinates using Haversine formula
  public double DistanceTo(GpsCoordinate other)
  {
    if (other == null)
      throw new ArgumentNullException(nameof(other));

    const double earthRadiusKm = 6371.0;

    var lat1Rad = ToRadians((double)Latitude);
    var lat2Rad = ToRadians((double)other.Latitude);
    var deltaLatRad = ToRadians((double)(other.Latitude - Latitude));
    var deltaLngRad = ToRadians((double)(other.Longitude - Longitude));

    var a = Math.Sin(deltaLatRad / 2) * Math.Sin(deltaLatRad / 2) +
            Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
            Math.Sin(deltaLngRad / 2) * Math.Sin(deltaLngRad / 2);

    var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

    return earthRadiusKm * c;
  }

  public double DistanceToInMiles(GpsCoordinate other)
  {
    const double kmToMiles = 0.621371;
    return DistanceTo(other) * kmToMiles;
  }

  public bool IsWithinRadius(GpsCoordinate other, double radiusInKilometers)
  {
    if (radiusInKilometers < 0)
      throw new ArgumentOutOfRangeException(nameof(radiusInKilometers), "Radius cannot be negative");

    return DistanceTo(other) <= radiusInKilometers;
  }

  public string ToFormattedString(int decimalPlaces = 6)
  {
    if (decimalPlaces < 0)
      throw new ArgumentOutOfRangeException(nameof(decimalPlaces), "Decimal places cannot be negative");

    return $"{Latitude.ToString($"F{decimalPlaces}", CultureInfo.InvariantCulture)}, " +
           $"{Longitude.ToString($"F{decimalPlaces}", CultureInfo.InvariantCulture)}";
  }
  public string ToGoogleMapsUrl()
  {
    return $"https://www.google.com/maps?q={Latitude.ToString(CultureInfo.InvariantCulture)},{Longitude.ToString(CultureInfo.InvariantCulture)}";
  }

  public TimeSpan Age => DateTime.UtcNow - Timestamp;

  public bool IsStale(TimeSpan maxAge) => Age > maxAge;

  public bool HasSufficientAccuracy(decimal requiredAccuracy)
  {
    return Accuracy.HasValue && Accuracy.Value <= requiredAccuracy;
  }

  public static void ValidateLatitude(decimal latitude)
  {
    if (latitude < -90 || latitude > 90)
      throw new ArgumentOutOfRangeException(nameof(latitude), latitude, "Latitude must be between -90 and 90 degrees");
  }

  public static void ValidateLongitude(decimal longitude)
  {
    if (longitude < -180 || longitude > 180)
      throw new ArgumentOutOfRangeException(nameof(longitude), longitude, "Longitude must be between -180 and 180 degrees");
  }

  private static void ValidateAccuracy(decimal? accuracy)
  {
    if (accuracy.HasValue && accuracy < 0)
      throw new ArgumentOutOfRangeException(nameof(accuracy), accuracy, "Accuracy cannot be negative");
  }

  private static double ToRadians(double degrees) => degrees * Math.PI / 180;

  public override string ToString()
  {
    var accuracyText = Accuracy.HasValue ? $" (±{Accuracy.Value}m)" : "";
    return $"{ToFormattedString()}{accuracyText} @ {Timestamp:yyyy-MM-dd HH:mm:ss UTC}";
  }

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Latitude;
    yield return Longitude;
  }
}
