namespace UserManagement.Application.Common;

public sealed class Result<T>
{
  public bool IsSuccess { get; }
  public T? Value { get; }
  public AppError? Error { get; }

  private Result(bool isSuccess, T? value, AppError? error)
  {
    IsSuccess = isSuccess;
    Value = value;
    Error = error;
  }

  public Result<TOut> Map<TOut>(Func<T, TOut> mapper)
  {
    if (IsSuccess)
      return Result<TOut>.Ok(mapper(Value!));

    return Result<TOut>.FromError(Error!);
  }

  public static Result<T> Ok(T value) => new(true, value, null);

  public static Result<T> Fail(ErrorType type, string message) =>
    new(false, default, new AppError(type, message));

  public static Result<T> Validation(IReadOnlyDictionary<string, string[]> failures) =>
    new(false, default, new AppError(ErrorType.Validation, "Validation failed", failures));

  public static Result<T> FromError(AppError error) => new(false, default, error);
}
