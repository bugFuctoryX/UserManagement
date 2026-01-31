namespace UserManagement.Web.Models;

public sealed record LoginModel
{
  [Required]
  public string UserName { get; set; } = string.Empty;

  [Required]
  public string Password { get; set; } = string.Empty;
}