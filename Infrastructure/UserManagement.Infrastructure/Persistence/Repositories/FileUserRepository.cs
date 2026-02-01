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

    var creditialRecord = credentialRecords.FirstOrDefault(x => x.UserId == id);
    if (creditialRecord is null) return null;

    var profileRecord = userRecords.FirstOrDefault(x => x.UserId == id);
    if (profileRecord is null) return null;

    return UserMapper.ToUser(creditialRecord, profileRecord);
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

  public async Task<Guid> AddUserAsync(User user, CancellationToken ct)
  {
    var changedBy = user.Credential?.UserName ?? "system";
    string oldSnapshot = string.Empty;
    string newSnapshot = user.Snapshot();
    await _context.WithWriteLockAsync(async state =>
    {
      if (IndexOfByUserId(state.Users, user.Id) >= 0)
        throw new InvalidOperationException("User already exists.");

      if (user.Credential is not null && IndexOfByUserId(state.Credentials, user.Id) >= 0)
        throw new InvalidOperationException("User credentials already exist.");

      state.Users.Add(UserMapper.ToProfileRecord(user.Profile));

      if (user.Credential is not null)
      {
        state.Credentials.Add(user.Credential.ToCredentialRecord());
      }

      return true;
    }, ct);

    var auditEntry = new UserAuditEntry
    {
      AuditId = Guid.NewGuid(),
      UserId = user.Id,
      ChangedAtUtc = DateTime.UtcNow,
      ChangedByUserName = changedBy,
      Type = ChangeType.UpdateProfile,
      OldSnapshotJson = oldSnapshot,
      NewSnapshotJson = newSnapshot
    };

    return user.Id;
  }

  public async Task UpdateAsync(User user, CancellationToken ct)
  {
    var changedBy = user.Credential?.UserName ?? "system";

    string oldSnapshot = string.Empty;
    string newSnapshot = user.Snapshot();

    await _context.WithWriteLockAsync(async state =>
    {
      var old = await BuildCurrentUserSnapshotOrNull(state, user.Id, ct);
      if (old is null)
        throw new InvalidOperationException("User not found.");

      oldSnapshot = old;

      var profileIdx = IndexOfByUserId(state.Users, user.Id);
      if (profileIdx < 0) throw new InvalidOperationException("User profile not found.");

      state.Users[profileIdx] = UserMapper.ToProfileRecord(user.Profile);

      var credIdx = IndexOfByUserId(state.Credentials, user.Id);
      if (credIdx < 0) throw new InvalidOperationException("User credentials not found.");

      if (user.Credential is not null)
      {
        var newCredRecord = user.Credential.ToCredentialRecord();
        state.Credentials[credIdx] = newCredRecord;
      }

      return true;
   }, ct);

   var auditEntry = new UserAuditEntry
   {
     AuditId = Guid.NewGuid(),
     UserId = user.Id,
     ChangedAtUtc = DateTime.UtcNow,
     ChangedByUserName = changedBy,
     Type = ChangeType.UpdateProfile,
     OldSnapshotJson = oldSnapshot,
     NewSnapshotJson = newSnapshot
   };

   await _audit.WriteUserAuditAsync(auditEntry, ct);
  }

  private static int IndexOfByUserId<T>(IList<T> list, Guid userId) where T : class
  {
    for (var i = 0; i < list.Count; i++)
    {
      dynamic item = list[i]!;
      if ((Guid)item.UserId == userId) return i;
    }
    return -1;
  }

  public async Task<bool> DeleteByIdAsync(Guid userId, CancellationToken ct)
  {
    if (userId == Guid.Empty) return false;

    string? oldSnapshot = null;

    var deleted = await _context.WithWriteLockAsync(async state =>
    {
      oldSnapshot = await BuildCurrentUserSnapshotOrNull(state, userId, ct);
      if (oldSnapshot is null) return false;

      var userProfile = state.Users.FirstOrDefault(x => x.UserId == userId);
      var creditional = state.Credentials.FirstOrDefault(x => x.UserId == userId);
      if (userProfile is null || creditional is null) return false;

      state.Users.Remove(userProfile);
      state.Credentials.Remove(creditional);

      return true;
    }, ct);

    if (!deleted) return false;

    var auditEntry = new UserAuditEntry
    {
      AuditId = Guid.NewGuid(),
      UserId = userId,
      ChangedAtUtc = DateTime.UtcNow,
      ChangedByUserName = "system",
      Type = ChangeType.DeleteUser,
      OldSnapshotJson = oldSnapshot ?? string.Empty,
      NewSnapshotJson = string.Empty
    };

    await _audit.WriteUserAuditAsync(auditEntry, ct);

    return true;
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
