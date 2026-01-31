namespace UserManagement.Application.Features.Users.Commands.Update;

public sealed record UpdateUserCommand(Guid UserId,
  string UserName,
  string Email,
  string Password,
  string FirstName,
  string LastName,
  DateOnly BirthDate,
  string BirthPlace,
  string City,
  string FullName
) : IRequest<Result<UpdateUserResponse>>;
