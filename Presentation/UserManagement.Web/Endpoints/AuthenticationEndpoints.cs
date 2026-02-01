namespace UserManagement.Web.Endpoints;

/// <summary>
/// HTTP authentication endpoints required for cookie-based auth in Blazor Interactive Server.
/// Cookie issuance is performed here because Razor component events execute via SignalR
/// and do not support HTTP response header modification.
/// </summary>
public static class AuthenticationEndpoints
{
  public static RouteGroupBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("/auth");

    group.MapPost("/signin", SignInAsync);
    group.MapPost("/signout", SignOutAsync);

    return group;
  }

  private static async Task<IResult> SignInAsync(
      HttpContext http,
      IMediator mediator,
      IWebAuthService webAuth,
      CancellationToken ct)
  {
    var form = await http.Request.ReadFormAsync(ct);
    var username = form["UserName"].FirstOrDefault() ?? string.Empty;
    var password = form["Password"].FirstOrDefault() ?? string.Empty;

    var returnUrl = http.Request.Query["returnUrl"].FirstOrDefault();
    if (string.IsNullOrWhiteSpace(returnUrl))
      returnUrl = "/users";

    var result = await mediator.Send(new LoginCommand(username, password), ct);
    if (!result.IsSuccess || result.Value is null)
      return Results.Redirect("/login?failed=1");

    var user = new LoginUser
    {
      UserId = result.Value.UserId,
      UserName = result.Value.UserName,
      Role = string.Empty
    };

    var signInResult = await webAuth.SignInAsync(user, ct);
    if (!signInResult.IsSuccess)
      return Results.Redirect("/login?failed=1");

    return Results.Redirect(returnUrl);
  }

  private static async Task<IResult> SignOutAsync(
      IWebAuthService webAuth,
      CancellationToken ct)
  {
    await webAuth.SignOutAsync(ct);
    return Results.Ok(new { ok = true });
  }
}
