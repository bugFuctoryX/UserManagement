namespace UserManagement.Infrastructure.SeedGenerators;

internal class SeedGenerator(IFileDbContext _context,
  IPasswordHasher _passwordHasher) : ISeedGenerator
{
  public async Task SeedAsync(CancellationToken ct)
  {
    var credentials = await _context.ReadCredentialsAsync(ct);
    var users = await _context.ReadUsersAsync(ct);

    if (credentials.Count > 0 || users.Count > 0)
      return;

    var seededUsers = BuildSeedUsers();
    var seededCredentials = BuildSeedCredentials(seededUsers, _passwordHasher);

    await _context.WriteUsersAsync(seededUsers, ct);
    await _context.WriteCredentialsAsync(seededCredentials, ct);
  }

  private static IReadOnlyList<UserProfileRecord> BuildSeedUsers()
    => new List<UserProfileRecord>
    {
      new(
        Guid.Parse("3f6b2c7a-9f44-4b6a-8f1c-9a1e6c8d2b47"),
        "admin@gmail.com",
        "admin",
        "admin",
        new DateOnly(1999, 9, 9),
        "Budapest",
        "Budapest"),
      new(
        Guid.Parse("f9e2b28c-3f92-4a50-9245-5a1bd17a31e4"),
        "anna@gmail.com",
        "Anna",
        "Kovács",
        new DateOnly(1992, 3, 14),
        "Budapest",
        "Budapest"),
      new(
        Guid.Parse("4c8d0d88-2c2d-4b30-8d57-4d8a5c702a0a"),
        "nagy@email.com",
        "Nagy",
        "Bence",
        new DateOnly(1988, 11, 5),
        "Debrecen",
        "Debrecen"),
      new(
        Guid.Parse("6b85aa4a-9836-4d09-b9d3-768c9f94c284"),
        "szabo@gmail.com",
        "Szabó",
        "Dóra",
        new DateOnly(1995, 7, 22),
        "Szeged",
        "Szeged")
    };

  private static IReadOnlyList<CredentialRecord> BuildSeedCredentials(
    IEnumerable<UserProfileRecord> users,
    IPasswordHasher passwordHasher)
  {
    var defaultPassword = "Test1234!";
    var hashed = passwordHasher.Hash(defaultPassword);

    return users.Select(u => new CredentialRecord(u.UserId, $"{u.Email}", hashed)).ToList();
  }
}
