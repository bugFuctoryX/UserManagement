namespace UserManager.Tests.Integration.Common.Auth;

public static class AuthSession
{
  public static async Task SignInAsAdminAsync(HttpClient client, CancellationToken cancellationToken = default)
  {
    var api = new LoginApi(client);

    var response = await api.SingInAsync(LoginRequests.AdminCredentials());

    if ((int)response.StatusCode is < 300 or >= 400)
      response.EnsureSuccessStatusCode();
  }
}
