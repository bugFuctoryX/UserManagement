namespace UserManagement.Infrastructure.Persistence.Mappers;

public static class UserMapper
{
  public static User ToUser(this CredentialRecord cred, UserProfileRecord profile)
    => new User
    {
      Id = cred.UserId,
      Credential = new UserCredential
      {
        UserId = cred.UserId,
        UserName = cred.UserName,
        PasswordHash = cred.PasswordHash,
        CreatedAtUtc = DateTime.UtcNow
      },
      Profile = new UserProfile
      {
        UserId = profile.UserId,
        LastName = profile.LastName,
        FirstName = profile.FirstName,
        BirthDate = profile.BirthDate,
        BirthPlace = profile.BirthPlace,
        City = profile.City,
        UpdatedAtUtc = DateTime.UtcNow
      }
    };

  public static UserProfileRecord ToProfileRecord(this UserProfile p) => new UserProfileRecord(
      p.UserId,
      p.LastName,
      p.FirstName,
      p.BirthDate,
      p.BirthPlace,
      p.City
    );

  public static string Snapshot(this User user)
    => JsonSerializer.Serialize(new
    {
      user.Id,
      Credential = new { user.Credential.UserId, user.Credential.UserName, user.Credential.PasswordHash },
      Profile = new { user.Profile.UserId, user.Profile.LastName, user.Profile.FirstName, user.Profile.BirthDate, user.Profile.BirthPlace, user.Profile.City }
    });
}
