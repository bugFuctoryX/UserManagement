namespace UserManagement.Domain.Entities;

public class UserCredential
{
  public Guid UserId { get; set; }
  public string UserName { get; set; } = default!;
  public string PasswordHash { get; set; } = default!;
  public DateTime CreatedAtUtc { get; set; }
  public DateTime? LastLoginAtUtc { get; set; }
}
