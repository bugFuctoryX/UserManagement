namespace UserManagement.Domain.Entities;

public class UserAuditEntry
{
  public Guid AuditId { get; set; }
  public Guid UserId { get; set; }
  public DateTime ChangedAtUtc { get; set; }
  public string ChangedByUserName { get; set; } = default!;
  public ChangeType Type { get; set; }
  public string OldSnapshotJson { get; set; } = default!;
  public string NewSnapshotJson { get; set; } = default!;
}
