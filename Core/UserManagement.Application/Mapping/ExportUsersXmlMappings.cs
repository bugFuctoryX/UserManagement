namespace UserManagement.Application.Mapping;

public static class ExportUsersXmlMappings
{
  public static UsersXmlPayload ToXml(this IReadOnlyList<UserExportRow> rows)
  {
    if (rows is null || rows.Count == 0)
      return new UsersXmlPayload();

    return new UsersXmlPayload
    {
      Users = rows.Select(r => new UserXmlItem
      {
        UserId = r.UserId,
        UserName = r.UserName,
        Email = r.Email,
        BirthDate = r.BirthDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
        BirthPlace = r.BirthPlace ?? string.Empty,
        City = r.City ?? string.Empty,
        FullName = r.FullName ?? string.Empty
      }).ToList()
    };
  }
}