namespace UserManager.Tests.Integration.Features.Authentication.Login;

internal sealed class LoginApi(HttpClient _client)
{
  public async Task<HttpResponseMessage> SingInAsync(LoginRequest loginRequest)
  {
    var url = "/auth/signin?returnUrl=/users";
    var content = MultipartContentFactory.BuildLoginMultipart(loginRequest.UserName, loginRequest.Password);

    return await _client.PostAsync(url, content);
  }
}
