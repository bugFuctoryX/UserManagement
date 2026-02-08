namespace UserManager.Tests.Integration.Infrastructore;

public sealed class TestAuthHandler(
  IOptionsMonitor<AuthenticationSchemeOptions> options,
  ILoggerFactory logger,
  UrlEncoder encoder)
  : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
  public const string SchemeName = "Test";

  protected override Task<AuthenticateResult> HandleAuthenticateAsync()
  {
    var claims = new[]
    {
      new Claim(ClaimTypes.NameIdentifier, "test-user-id"),
      new Claim(ClaimTypes.Name, "test-user"),
      new Claim(ClaimTypes.Role, "Admin"),
    };

    var identity = new ClaimsIdentity(claims, SchemeName);
    var principal = new ClaimsPrincipal(identity);
    var ticket = new AuthenticationTicket(principal, SchemeName);

    return Task.FromResult(AuthenticateResult.Success(ticket));
  }
}