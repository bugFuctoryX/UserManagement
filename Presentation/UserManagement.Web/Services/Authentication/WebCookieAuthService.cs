namespace UserManagement.Web.Services.Authentication;

public sealed class WebCookieAuthService(IHttpContextAccessor httpContextAccessor) : IWebAuthService
{
  public async Task<Result<bool>> SignInAsync(LoginUser user, CancellationToken ct = default)
  {
    var http = httpContextAccessor.HttpContext;
    if (http is null)
      return Result<bool>.Fail(ErrorType.Unexpected, "No HttpContext available.");

    var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new(ClaimTypes.Name, user.UserName)
        };

    if (!string.IsNullOrWhiteSpace(user.Role))
      claims.Add(new Claim(ClaimTypes.Role, user.Role));

    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var principal = new ClaimsPrincipal(identity);

    await http.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        principal,
        new AuthenticationProperties { IsPersistent = true });

    return Result<bool>.Ok(true);
  }

  public async Task SignOutAsync(CancellationToken ct = default)
  {
    var http = httpContextAccessor.HttpContext;
    if (http is null) return;

    await http.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
  }
}
