namespace UserManagement.Infrastructure.Persistence.Csv;

internal class UsersCsvSerializer : ICsvSerializer<UserProfileRecord>
{
  private readonly string _delimiter;
  public UsersCsvSerializer(string delimiter) => _delimiter = delimiter;

  public IReadOnlyList<UserProfileRecord> Deserialize(string content)
  {
    var list = new List<UserProfileRecord>();
    if (string.IsNullOrWhiteSpace(content)) return list;

    foreach (var line in CsvHelpers.Lines(content))
    {
      if (line.StartsWith("UserId", StringComparison.OrdinalIgnoreCase))
        continue;

      var p = line.Split(_delimiter);
      if (p.Length < 6) throw new FormatException($"Invalid users line: '{line}'");

      list.Add(new UserProfileRecord(
          Guid.Parse(CsvHelpers.NormalizeCell(p[0])),
          CsvHelpers.NormalizeCell(p[1]),
          CsvHelpers.NormalizeCell(p[2]),
          CsvHelpers.NormalizeCell(p[3]),
          DateOnly.Parse(CsvHelpers.NormalizeCell(p[4]), CsvHelpers.Invariant),
          CsvHelpers.NormalizeCell(p[5]),
          CsvHelpers.NormalizeCell(p[6])
      ));
    }
    return list;
  }

  public string Serialize(IEnumerable<UserProfileRecord> items)
  {
    var sb = new System.Text.StringBuilder();
    sb.AppendLine("UserId;Email;LastName;FirstName;BirthDate;BirthPlace;City");
    foreach (var x in items)
      sb.Append(SerializeLine(x));
    return sb.ToString();
  }

  public string SerializeHeaderOnly() => "UserId;Email;LastName;FirstName;BirthDate;BirthPlace;City\n";

  public string SerializeLine(UserProfileRecord item)
      => $"{item.UserId};{item.Email};{item.LastName};{item.FirstName};{item.BirthDate:yyyy-MM-dd};{item.BirthPlace};{item.City}\n";
}
