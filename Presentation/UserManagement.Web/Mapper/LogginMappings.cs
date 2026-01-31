namespace UserManagement.Web.Mapper;

public static class LogginMappings
{
  public static LoginCommand ToLoginCommand(this LoginModel model)
          => new(model.UserName, model.Password);
}
