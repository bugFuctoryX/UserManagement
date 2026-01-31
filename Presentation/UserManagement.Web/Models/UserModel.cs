namespace UserManagement.Web.Models;

public sealed record UserModel(Guid UserId,
  string UserName,
  string Email,
  DateOnly BirthDate, 
  string BirthPlace,
  string City,
  string FullName
);

