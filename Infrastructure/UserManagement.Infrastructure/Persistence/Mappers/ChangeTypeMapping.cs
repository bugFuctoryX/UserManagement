namespace UserManagement.Infrastructure.Persistence.Mappers;

public static class ChangeTypeMapping
{
  public static ChangeType FromCode(string? code) => code?.Trim() switch
  {
    "UpdateProfile" => ChangeType.UpdateProfile,
    "ChangePassword" => ChangeType.ChangePassword,
    "Create" => ChangeType.Create,
    _ => ChangeType.Default
  };

  public static string ToCode(this ChangeType type) => type switch
  {
    ChangeType.UpdateProfile => "UpdateProfile",
    ChangeType.ChangePassword => "ChangePassword",
    ChangeType.Create => "Create",
    _ => "Default"
  };
}
