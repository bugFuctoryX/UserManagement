namespace UserManagement.Infrastructure.Persistence.Records;

public sealed record class UserAuditRecord(
  Guid AuditId,
  Guid UserId,
  DateTime ChangedAtUtc,
  string ChangedByUserName,
  ChangeType ChangeType,
  string OldSnapshotJson,
  string NewSnapshotJson
);