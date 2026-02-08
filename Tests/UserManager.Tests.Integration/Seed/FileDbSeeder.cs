namespace UserManager.Tests.Integration.Seed;

internal sealed class FileDbSeeder(IFileDbContext context)
{
  public async Task SeedIfEmptyAsync(CancellationToken ct)
  {
    var credentialRecords = await context.ReadCredentialsAsync(ct);
    var userRecords = await context.ReadUsersAsync(ct);

    if (credentialRecords.Count > 0 || userRecords.Count > 0)
    {
      var credentialRecord = credentialRecords.FirstOrDefault(x => x.UserId == SeedUsers.UserId);

      if(credentialRecord is null)
      {
        var seededUsers = SeedUsers.BuildUsers();
        var seededCredentials = SeedUsers.BuildCredentials();

        await context.WriteUsersAsync(seededUsers, ct);
        await context.WriteCredentialsAsync(seededCredentials, ct);
      }
    }
  }
}
