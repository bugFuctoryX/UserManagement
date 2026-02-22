namespace UserManager.Tests.Integration.Features.Authentication.Login;

internal sealed class LoginApi(HttpClient _client)
{
  private const string _url = "/auth/signin?returnUrl=/users";
  public async Task<HttpResponseMessage> SingInAsync(LoginRequest loginRequest)
  {
    var content = MultipartContentFactory.BuildLoginMultipart(loginRequest.UserName, loginRequest.Password);

    return await _client.PostAsync(_url, content);
  }
}