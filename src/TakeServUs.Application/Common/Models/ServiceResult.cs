namespace TakeServUs.Application.Common.Models;

public class ServiceResult<T>
{
  public bool Success { get; set; }
  public T? Data { get; set; }
  public string? Message { get; set; }
  public List<string> Errors { get; set; } = new();
  public int? StatusCode { get; set; }

  public static ServiceResult<T> SuccessResult(T data, string? message = null)
  {
    return new ServiceResult<T>
    {
      Success = true,
      Data = data,
      Message = message,
      StatusCode = 200
    };
  }

  public static ServiceResult<T> FailureResult(string error, int statusCode = 400)
  {
    return new ServiceResult<T>
    {
      Success = false,
      Errors = new List<string> { error },
      StatusCode = statusCode
    };
  }

  public static ServiceResult<T> FailureResult(List<string> errors, int statusCode = 400)
  {
    return new ServiceResult<T>
    {
      Success = false,
      Errors = errors,
      StatusCode = statusCode
    };
  }
}
