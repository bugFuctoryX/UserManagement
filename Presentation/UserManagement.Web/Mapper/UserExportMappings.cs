namespace UserManagement.Web.Mapper;

public static class UserExportMappings
{
  public static ExportUsersXmlQuery ToExportUsersXmlQuery(this List<UserGridViewModel> users)
  {
    var rows = users.ToExportRows();
    return new ExportUsersXmlQuery(rows);
  }

  public static IReadOnlyList<UserExportRow> ToExportRows(this List<UserGridViewModel> users)
  {
    if (users is null || users.Count == 0)
      return Array.Empty<UserExportRow>();

    return users.Select(u => new UserExportRow(
        UserId: u.UserId,
        UserName: u.UserName ?? string.Empty,
        Email: u.Email ?? string.Empty,
        BirthDate: u.BirthDate,
        BirthPlace: u.BirthPlace ?? string.Empty,
        City: u.City ?? string.Empty,
        FullName: u.FullName ?? string.Empty
    )).ToList();
  }
}
