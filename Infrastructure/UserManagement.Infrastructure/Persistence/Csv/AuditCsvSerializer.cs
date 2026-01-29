namespace UserManagement.Infrastructure.Persistence.Csv;

internal class AuditCsvSerializer : ICsvSerializer<UserAuditRecord>
{
  private readonly string _delimiter;
  public AuditCsvSerializer(string delimiter) => _delimiter = delimiter;

  public IReadOnlyList<UserAuditRecord> Deserialize(string content)
  {
    var list = new List<UserAuditRecord>();
    if (string.IsNullOrWhiteSpace(content)) return list;

    foreach (var line in CsvHelpers.Lines(content))
    {
      if (line.StartsWith("AuditId", StringComparison.OrdinalIgnoreCase))
        continue;

      var p = line.Split(_delimiter);
      if (p.Length < 7) throw new FormatException($"Invalid audit line: '{line}'");

      list.Add(new UserAuditRecord(
          Guid.Parse(p[0].Trim()),
          Guid.Parse(p[1].Trim()),
          DateTime.Parse(p[2].Trim(), CsvHelpers.Invariant, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal),
          p[3].Trim(),
          ChangeTypeMapping.FromCode(p[4]),
          p[5].Trim(),
          p[6].Trim()
      ));
    }
    return list;
  }

  public string Serialize(IEnumerable<UserAuditRecord> items)
  {
    var sb = new StringBuilder();
    sb.AppendLine("AuditId;UserId;ChangedAtUtc;ChangedByUserName;ChangeType;OldSnapshotJson;NewSnapshotJson");
    foreach (var x in items)
      sb.Append(SerializeLine(x));
    return sb.ToString();
  }

  public string SerializeHeaderOnly()
      => "AuditId;UserId;ChangedAtUtc;ChangedByUserName;ChangeType;OldSnapshotJson;NewSnapshotJson\n";

  public string SerializeLine(UserAuditRecord item)
  {
    var oldB64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(item.OldSnapshotJson ?? ""));
    var newB64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(item.NewSnapshotJson ?? ""));

    return $"{item.AuditId};{item.UserId};{item.ChangedAtUtc:O};{item.ChangedByUserName};{item.ChangeType};{oldB64};{newB64}\n";
  }
}
