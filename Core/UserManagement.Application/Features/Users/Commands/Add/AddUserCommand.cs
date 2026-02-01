namespace UserManagement.Application.Features.Users.Commands.Add;

public sealed record AddUserCommand(Guid UserId,
  string UserName,
  string Email,
  string Password,
  string FirstName,
  string LastName,
  DateOnly BirthDate,
  string BirthPlace,
  string City) : IRequest<Result<AddUserResponse>>;