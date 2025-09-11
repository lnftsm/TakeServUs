namespace TakeServUs.Application.Common.Exceptions;

using FluentValidation.Results;

public class ValidationException : ApiException
{
  public IDictionary<string, string[]> Errors { get; }

  public ValidationException()
      : base("One or more validation failures have occurred.", 400)
  {
    Errors = new Dictionary<string, string[]>();
  }

  public ValidationException(IEnumerable<ValidationFailure> failures)
      : this()
  {
    Errors = failures
        .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
        .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
  }
}
