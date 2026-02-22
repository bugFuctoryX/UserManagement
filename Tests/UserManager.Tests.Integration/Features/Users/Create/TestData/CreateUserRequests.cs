namespace UserManager.Tests.Integration.Features.Users.CreateUser.TestData;

public static class CreateUserRequests
{
  public static CreateUserRequest Valid()
    => new(
      UserId: Guid.Empty,
      UserName: "john.doe",
      Email: "john.doe@test.local",
      Password: "StrongPass123!",
      FirstName: "John",
      LastName: "Doe",
      BirthDate: new DateOnly(1995, 1, 15),
      BirthPlace: "Budapest",
      City: "Budapest"
    );
}