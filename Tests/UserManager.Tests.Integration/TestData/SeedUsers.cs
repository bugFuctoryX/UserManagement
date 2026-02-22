namespace UserManager.Tests.Integration.TestData.Users;

internal static class SeedUsers
{
  public static readonly Guid TestUserId = Guid.Parse("8cdb4a83-8d27-4f0f-bd71-7198bb245e01");
  private static readonly Guid AdminUserId = Guid.Parse("2f6e7b3d-0d2d-4c7d-9d2e-5b9c10f62d1a");
  private const string TestEmail = "test@gmail.com";
  private const string TestPassword = "Test1234!";
  private const string AdminEmail = "admin@gmail.com";
  private const string AdminPassword = "Admin1234!";

  internal static IReadOnlyList<UserProfileRecord> BuildUsers()
    => new[]
    {
      new UserProfileRecord(
        TestUserId,
        TestEmail,
        "test",
        "test",
        new DateOnly(1999, 9, 9),
        "Budapest",
        "Budapest"),
      new UserProfileRecord(
        AdminUserId,
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

    var testHashed = hasher.Hash(TestPassword);
    var adminHashed = hasher.Hash(AdminPassword);

    return new List<CredentialRecord>
    {
      new CredentialRecord(TestUserId, TestEmail, testHashed),
      new CredentialRecord(AdminUserId, AdminEmail, adminHashed)
    };
  }
}