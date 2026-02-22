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

  public static MultipartFormDataContent BuildCreateUserMultipart(CreateUserRequest createUserRequest)
  {
    return new MultipartFormDataContent
    {
      { new StringContent(createUserRequest.UserId.ToString()), nameof(CreateUserRequest.UserId) },
      { new StringContent(createUserRequest.UserName), nameof(CreateUserRequest.UserName) },
      { new StringContent(createUserRequest.Email), nameof(CreateUserRequest.Email) },
      { new StringContent(createUserRequest.Password), nameof(CreateUserRequest.Password) },
      { new StringContent(createUserRequest.FirstName), nameof(CreateUserRequest.FirstName)  },
      { new StringContent(createUserRequest.LastName), nameof(CreateUserRequest.LastName) },
      { new StringContent(createUserRequest.BirthDate.ToString("yyyy-MM-dd")), nameof(CreateUserRequest.BirthDate) },
      { new StringContent(createUserRequest.BirthPlace), nameof(CreateUserRequest.BirthPlace) },
      { new StringContent(createUserRequest.City), nameof(CreateUserRequest.City) }
    };
  }
}