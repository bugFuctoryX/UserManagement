namespace UserManagement.Web.Models;

public sealed class UserViewModel
{
  public Guid UserId { get; set; }
  public string UserName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public DateOnly BirthDate { get; set; }
  public string BirthPlace { get; set; } = string.Empty;
  public string City { get; set; } = string.Empty;
  public string FullName { get; set; } = string.Empty;
}