namespace UserManagement.Web.Helper;

public interface IUsersXmlExporter
{
  string Export(IEnumerable<UserModel> users);
}
