namespace UserManagement.Application.Mapping;

public static class UserMappings
{
  public static LoginResponse ToLoginResponse(this User user) => new(
    UserId: user.Id,
    UserName: user.Credential.UserName,
    FirstName: user.Profile.FirstName,
    LastName: user.Profile.LastName
   );
}
