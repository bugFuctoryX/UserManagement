namespace UserManagement.Domain.Entities;

public class UserProfile
{
  public Guid UserId { get; set; }
  public string FirstName { get; set; } = default!;
  public string LastName { get; set; } = default!;
  public DateOnly BirthDate { get; set; }
  public string BirthPlace { get; set; } = default!;
  public string City { get; set; } = default!;
  public DateTime UpdatedAtUtc { get; set; }
}
