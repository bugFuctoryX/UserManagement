namespace UserManagement.Web.Helper;

public interface IUsersXmlExporter
{
  string Export(IEnumerable<UserGridViewModel> users);
}
