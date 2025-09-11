namespace TakeServUs.Application.Common.Helpers;

public static class JsonHelper
{
  public static List<string> ParseStringArray(string? json)
  {
    if (string.IsNullOrWhiteSpace(json))
      return new List<string>();

    try
    {
      // Remove brackets and quotes, then split
      var cleaned = json.Trim('[', ']').Replace("\"", "");
      if (string.IsNullOrWhiteSpace(cleaned))
        return new List<string>();

      return cleaned.Split(',')
          .Select(s => s.Trim())
          .Where(s => !string.IsNullOrEmpty(s))
          .ToList();
    }
    catch
    {
      return new List<string>();
    }
  }

  public static string ToJsonArray(ICollection<string> items)
  {
    if (items == null || items.Count == 0)
      return "[]";

    var escapedItems = items.Select(i => $"\"{i.Replace("\"", "\\\"")}\"");
    return $"[{string.Join(",", escapedItems)}]";
  }
}


