namespace UserManagement.Infrastructure.Persistence.Repositories.Audit;

internal class FileAuditWriter(IFileDbContext _context) : IFileAuditWriter
{
  public async Task WriteUserAuditAsync(UserAuditEntry AuditEntry, CancellationToken ct)
  {
    if (AuditEntry is null) throw new ArgumentNullException(nameof(AuditEntry));

    var auditRecord = new UserAuditRecord(
        AuditId: AuditEntry.AuditId == Guid.Empty ? Guid.NewGuid() : AuditEntry.AuditId,
        UserId: AuditEntry.UserId,
        ChangedAtUtc: AuditEntry.ChangedAtUtc == default ? DateTime.UtcNow : AuditEntry.ChangedAtUtc,
        ChangedByUserName: AuditEntry.ChangedByUserName,
        ChangeType: AuditEntry.Type, 
        OldSnapshotJson: AuditEntry.OldSnapshotJson ?? string.Empty,
        NewSnapshotJson: AuditEntry.NewSnapshotJson ?? string.Empty
    );

    await _context.AppendAuditAsync(auditRecord, ct);
  }
}
