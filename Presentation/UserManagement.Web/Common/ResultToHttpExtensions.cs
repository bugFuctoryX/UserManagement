namespace UserManagement.Web.Common
{
  public static class ResultToHttpExtensions
  {
    public static IResult ToHttpResult<T>(this Result<T> result, Func<T, IResult> onSuccess)
    {
      if (result.IsSuccess)
        return onSuccess(result.Value!);

      var error = result.Error!;

      return error.Type switch
      {
        ErrorType.Validation => Results.ValidationProblem(
          error.ValidationErrors ?? new Dictionary<string, string[]>()
        ),

        ErrorType.NotFound => Results.NotFound(new { error = error.Message }),

        ErrorType.Conflict => Results.Conflict(new { error = error.Message }),

        _ => Results.BadRequest(new { error = error.Message })
      };
    }
  }
}