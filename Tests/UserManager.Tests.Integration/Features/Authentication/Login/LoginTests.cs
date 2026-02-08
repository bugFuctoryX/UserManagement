namespace UserManager.Tests.Integration.Features.Authentication.Login;

[Collection(ApiCollection.Name)]
public class LoginTests
{
  private readonly HttpClient _client;
  private readonly LoginApi _api;

  public LoginTests(ApiFixture fixture)
  {
    _client = fixture.Client;
    _api = new LoginApi(fixture.Client);
  }

  [Fact]
  public async Task SignIn_WithValidCredentials_ReturnsOk_AndSetsCookie()
  {
    // Arrange
    var content = LoginRequests.ValidAdmin();
    // Act
    var response = await _api.SingInAsync(content);
    // Location header
    Assert.NotNull(response.Headers.Location);
    Assert.Equal("/users", response.Headers.Location!.OriginalString);

    // Set-Cookie header
    Assert.True(response.Headers.TryGetValues("Set-Cookie", out var setCookies));
    Assert.Contains(setCookies, v => v.Contains(".AspNetCore.", StringComparison.OrdinalIgnoreCase)
      || v.Contains("Cookies", StringComparison.OrdinalIgnoreCase));
  }

  [Fact]
  public async Task SignIn_WithWrongCredentials_RedirectsToLoginFailed()
  {
    // Arrange
    var req = LoginRequests.WrongCredentials();
    // Act
    var response = await _api.SingInAsync(req);
    // Location header
    Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
    Assert.NotNull(response.Headers.Location);
    Assert.Equal("/login?failed=1", response.Headers.Location!.OriginalString);

    if (response.Headers.TryGetValues("Set-Cookie", out var setCookies))
    {
      Assert.DoesNotContain(setCookies, v => v.Contains("Cookies", StringComparison.OrdinalIgnoreCase));
    }
  }
}
