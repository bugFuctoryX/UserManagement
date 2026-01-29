namespace UserManagement.Infrastructure.Persistence.Context;

internal interface IFileDbContext
{
  Task<IReadOnlyList<CredentialRecord>> ReadCredentialsAsync(CancellationToken ct);
  Task<IReadOnlyList<UserProfileRecord>> ReadUsersAsync(CancellationToken ct);
  Task WriteCredentialsAsync(IEnumerable<CredentialRecord> items, CancellationToken ct);
  Task WriteUsersAsync(IEnumerable<UserProfileRecord> items, CancellationToken ct);
  Task AppendAuditAsync(UserAuditRecord entry, CancellationToken ct);
  Task<TResult> WithWriteLockAsync<TResult>(Func<FileDbState, Task<TResult>> action, CancellationToken ct);
}
