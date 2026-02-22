namespace UserManager.Tests.Integration.Features.Authentication.Login.TestData;

public static class LoginRequests
{
  public static LoginRequest AdminCredentials() => new("admin@gmail.com", "Admin1234!");
  public static LoginRequest Valid() => new("test@gmail.com", "Test1234!");

  public static LoginRequest WrongCredentials() => new("wrong-user", "wrong-pass");

  public static LoginRequest Empty() => new("", "");
}