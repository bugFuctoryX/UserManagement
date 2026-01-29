namespace UserManagement.Domain.Entities;

public class User
{
  public Guid Id { get; set; }
  public UserCredential Credential { get; set; } = default!;
  public UserProfile Profile { get; set; } = default!;
}
