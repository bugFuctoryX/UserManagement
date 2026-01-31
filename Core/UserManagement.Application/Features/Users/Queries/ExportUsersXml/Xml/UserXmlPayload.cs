namespace UserManagement.Application.Features.Users.Queries.ExportUsersXml.Xml;

[XmlRoot("Users")]
public sealed class UsersXmlPayload
{
  [XmlElement("User")]
  public List<UserXmlItem> Users { get; set; } = new();
}