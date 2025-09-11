namespace TakeServUs.Application.Common.Models;

public class Result
{
  public bool IsSuccess { get; protected set; }
  public string? Error { get; protected set; }
  public bool IsFailure => !IsSuccess;

  protected Result(bool isSuccess, string? error)
  {
    if (isSuccess && !string.IsNullOrEmpty(error))
      throw new InvalidOperationException();
    if (!isSuccess && string.IsNullOrEmpty(error))
      throw new InvalidOperationException();

    IsSuccess = isSuccess;
    Error = error;
  }

  public static Result Success() => new(true, null);
  public static Result<T> Success<T>(T value) => new(value, true, null);
  public static Result Failure(string error) => new(false, error);
  public static Result<T> Failure<T>(string error) => new(default, false, error);
}

public class Result<T> : Result
{
  public T? Value { get; }

  protected internal Result(T? value, bool isSuccess, string? error)
      : base(isSuccess, error)
  {
    Value = value;
  }

  public static implicit operator Result<T>(T value) => Success(value);
}
