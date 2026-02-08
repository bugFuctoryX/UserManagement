namespace UserManager.Tests.Integration.Common.Http;

public static class MultipartContentFactory
{
  public static MultipartFormDataContent BuildLoginMultipart(string userName, string password)
  {
    return new MultipartFormDataContent
    {
      { new StringContent(userName), nameof(LoginRequest.UserName) },
      { new StringContent(password), nameof(LoginRequest.Password) }
    };
  }
}