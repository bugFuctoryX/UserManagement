namespace UserManagement.Application.Common.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(
  IEnumerable<IValidator<TRequest>> validators)
  : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
  public async Task<TResponse> Handle(
    TRequest request,
    RequestHandlerDelegate<TResponse> next,
    CancellationToken ct)
  {
    if (!validators.Any())
      return await next();

    var context = new ValidationContext<TRequest>(request);

    var results = await Task.WhenAll(
      validators.Select(v => v.ValidateAsync(context, ct)));

    var failures = results
      .SelectMany(r => r.Errors)
      .Where(f => f is not null)
      .GroupBy(f => f.PropertyName)
      .ToDictionary(
        g => g.Key,
        g => g.Select(f => f.ErrorMessage).Distinct().ToArray()
      );

    if (failures.Count == 0)
      return await next();

    return CreateValidationResponse<TResponse>(failures);
  }

  private static T CreateValidationResponse<T>(Dictionary<string, string[]> failures)
  {
    var t = typeof(T);

    if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Result<>))
    {
      var valueType = t.GetGenericArguments()[0];
      var method = typeof(Result<>).MakeGenericType(valueType).GetMethod(nameof(Result<int>.Validation));
      return (T)method!.Invoke(null, [failures])!;
    }

    throw new InvalidOperationException($"ValidationBehavior cannot create response for {typeof(T).Name}");
  }
}