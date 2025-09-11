namespace TakeServUs.Application.Common.Exceptions;

public class ApiException : Exception
{
  public int StatusCode { get; }
  public string? Details { get; }

  public ApiException(string message, int statusCode = 500, string? details = null)
      : base(message)
  {
    StatusCode = statusCode;
    Details = details;
  }
}
