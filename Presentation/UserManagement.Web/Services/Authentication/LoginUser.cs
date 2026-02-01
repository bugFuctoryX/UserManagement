namespace UserManagement.Web.Services.Authentication;

public sealed record LoginUser 
{
  public Guid UserId { get; set; }
  public string UserName { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public string Role { get; set; } = string.Empty;
}