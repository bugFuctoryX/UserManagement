namespace UserManagement.Infrastructure.Persistence.Records;

internal sealed record class UserAuditRecord(
  Guid AuditId,
  Guid UserId,
  DateTime ChangedAtUtc,
  string ChangedByUserName,
  ChangeType ChangeType,
  string OldSnapshotJson,
  string NewSnapshotJson
);