namespace UserManagement.Application.Abstractions.Persistence;

public interface IFileAuditWriter
{
  public Task WriteUserAuditAsync(UserAuditEntry entry, CancellationToken ct);
}
