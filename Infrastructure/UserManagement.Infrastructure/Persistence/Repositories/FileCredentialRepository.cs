namespace UserManagement.Infrastructure.Persistence.Repositories;

internal class FileCredentialRepository(IFileDbContext _db) : IFileCredentialRepository
{
  public async Task<UserCredential?> GetByUserIdAsync(Guid userId, CancellationToken ct)
  {
    var records = await _db.ReadCredentialsAsync(ct);
    var rec = records.FirstOrDefault(x => x.UserId == userId);
    return rec is null ? null : rec.ToCredential();
  }

  public async Task<UserCredential?> GetByUsernameAsync(string username, CancellationToken ct)
  {
    if (string.IsNullOrWhiteSpace(username))
      return null;

    var creditionalRecords = await _db.ReadCredentialsAsync(ct);
    var creditionalRecord = creditionalRecords.FirstOrDefault(x =>
        x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));

    return creditionalRecord is null ? null : creditionalRecord!.ToCredential();
  }

  public async Task UpdatePasswordHashAsync(Guid userId, string passwordHash, CancellationToken ct)
  {
    if (userId == Guid.Empty)
      throw new ArgumentException("UserId must not be empty.", nameof(userId));

    if (string.IsNullOrWhiteSpace(passwordHash))
      throw new ArgumentException("New password hash must not be empty.", nameof(passwordHash));

    await _db.WithWriteLockAsync(state =>
    {
      var idx = state.Credentials.ToList().FindIndex(x => x.UserId == userId);
      if (idx < 0)
        throw new InvalidOperationException("Credential not found.");

      var current = state.Credentials[idx];
      state.Credentials[idx] = current with { PasswordHash = passwordHash };

      return Task.FromResult(true);
    }, ct);
  }
}
