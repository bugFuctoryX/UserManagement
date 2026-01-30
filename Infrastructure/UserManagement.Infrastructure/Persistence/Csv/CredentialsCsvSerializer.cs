namespace UserManagement.Infrastructure.Persistence.Csv;

internal class CredentialsCsvSerializer: ICsvSerializer<CredentialRecord>
{
  private readonly string _delimiter;
  public CredentialsCsvSerializer(string delimiter) => _delimiter = delimiter;

  public IReadOnlyList<CredentialRecord> Deserialize(string content)
  {
    var list = new List<CredentialRecord>();
    if (string.IsNullOrWhiteSpace(content)) return list;

    foreach (var line in CsvHelpers.Lines(content))
    {
      if (line.StartsWith("UserId", StringComparison.OrdinalIgnoreCase))
        continue;

      var p = line.Split(_delimiter);
      if (p.Length < 3) throw new FormatException($"Invalid credentials line: '{line}'");

      list.Add(new CredentialRecord(
          Guid.Parse(CsvHelpers.NormalizeCell(p[0])),
          CsvHelpers.NormalizeCell(p[1]),
          CsvHelpers.NormalizeCell(p[2])
      ));
    }
    return list;
  }

  public string Serialize(IEnumerable<CredentialRecord> items)
  {
    var sb = new System.Text.StringBuilder();
    sb.AppendLine("UserId;UserName;PasswordHash");
    foreach (var x in items)
      sb.Append(SerializeLine(x));
    return sb.ToString();
  }

  public string SerializeHeaderOnly() => "UserId;UserName;PasswordHash\n";

  public string SerializeLine(CredentialRecord item)
      => $"{item.UserId};{item.UserName};{item.PasswordHash}\n";
}
