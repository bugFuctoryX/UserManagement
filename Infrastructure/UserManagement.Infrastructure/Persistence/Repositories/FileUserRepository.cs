namespace UserManagement.Infrastructure.Persistence.Repositories;

internal class FileUserRepository(IFileDbContext _context, IFileAuditWriter _audit) : IFileUserRepository
{
  public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct)
  {
    var credentialRecords = await _context.ReadCredentialsAsync(ct);
    var userRecords = await _context.ReadUsersAsync(ct);

    var profileById = userRecords.ToDictionary(x => x.UserId, x => x);

    var result = new List<User>();
    foreach (var credentialRecord in credentialRecords)
    {
      if (profileById.TryGetValue(credentialRecord.UserId, out var profileRecord))
        result.Add(UserMapper.ToUser(credentialRecord, profileRecord));
    }
    return result;
  }

  public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct)
  {
    var credentialRecords = await _context.ReadCredentialsAsync(ct);
    var userRecords = await _context.ReadUsersAsync(ct);

    var c = credentialRecords.FirstOrDefault(x => x.UserId == id);
    if (c is null) return null;

    var profileRecord = userRecords.FirstOrDefault(x => x.UserId == id);
    if (profileRecord is null) return null;

    return UserMapper.ToUser(c, profileRecord);
  }

  public async Task<User?> GetByUsernameAsync(string username, CancellationToken ct)
  {
    var credentionalRecords = await _context.ReadCredentialsAsync(ct);
    var credentionalRecord = credentionalRecords.FirstOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
    if (credentionalRecord is null) return null;

    var profileRecords = await _context.ReadUsersAsync(ct);
    var profileRecord = profileRecords.FirstOrDefault(x => x.UserId == credentionalRecord.UserId);
    if (profileRecord is null) return null;

    return UserMapper.ToUser(credentionalRecord, profileRecord);
  }

  public async Task UpdateAsync(User user, CancellationToken ct)
  {
    var changedBy = user.Credential?.UserName ?? "system";

    await _context.WithWriteLockAsync(async state =>
    {
      var old = await BuildCurrentUserSnapshotOrNull(state, user.Id, ct);
      if (old is null)
        throw new InvalidOperationException("User not found.");

      var profileIdx = state.Users.ToList().FindIndex(x => x.UserId == user.Id);
      if (profileIdx < 0) throw new InvalidOperationException("User profile not found.");

      state.Users[profileIdx] = UserMapper.ToProfileRecord(user.Profile);

      var credIdx = state.Credentials.ToList().FindIndex(x => x.UserId == user.Id);
      if (credIdx < 0) throw new InvalidOperationException("User credentials not found.");

      if(user.Credential is not null)
      {
        var newCredRecord = user.Credential.ToCredentialRecord();
        state.Credentials[credIdx] = newCredRecord;
      }

      var auditEntry = new UserAuditEntry
      {
        AuditId = Guid.NewGuid(),
        UserId = user.Id,
        ChangedAtUtc = DateTime.UtcNow,
        ChangedByUserName = changedBy,
        Type = ChangeType.UpdateProfile,
        OldSnapshotJson = old,
        NewSnapshotJson = user.Snapshot()
      };

      await _audit.WriteUserAuditAsync(auditEntry, ct);

      return true;
    }, ct);
  }

  private static Task<string?> BuildCurrentUserSnapshotOrNull(FileDbState state, Guid userId, CancellationToken ct)
  {
    var cred = state.Credentials.FirstOrDefault(x => x.UserId == userId);
    var prof = state.Users.FirstOrDefault(x => x.UserId == userId);
    if (cred is null || prof is null) return Task.FromResult<string?>(null);

    var u = UserMapper.ToUser(cred, prof);
    return Task.FromResult<string?>(UserMapper.Snapshot(u));
  }
}
