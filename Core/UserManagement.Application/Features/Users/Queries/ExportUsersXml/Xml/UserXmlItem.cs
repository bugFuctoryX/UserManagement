namespace UserManagement.Application.Features.Users.Queries.ExportUsersXml.Xml;

public sealed class UserXmlItem
{
  public Guid UserId { get; set; }
  public string UserName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string BirthDate { get; set; } = string.Empty;
  public string BirthPlace { get; set; } = string.Empty;
  public string City { get; set; } = string.Empty;
  public string FullName { get; set; } = string.Empty;
}
