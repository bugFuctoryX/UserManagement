namespace UserManager.Tests.Integration.TestData.Users;

internal static class SeedUsers
{
  internal static readonly Guid UserId = Guid.Parse("8cdb4a83-8d27-4f0f-bd71-7198bb245e01");
  internal const string AdminEmail = "test@gmail.com";
  internal const string AdminPassword = "Test1234!";

  internal static IReadOnlyList<UserProfileRecord> BuildUsers()
    => new[]
    {
      new UserProfileRecord(
        UserId,
        AdminEmail,
        "test",
        "test",
        new DateOnly(1999, 9, 9),
        "Budapest",
        "Budapest")
    };

  internal static IReadOnlyList<CredentialRecord> BuildCredentials()
  {
    IPasswordHasher hasher = new PasswordHasher();
    var hashed = hasher.Hash(AdminPassword);
    return new[] { new CredentialRecord(UserId, AdminEmail, hashed) };
  }
}