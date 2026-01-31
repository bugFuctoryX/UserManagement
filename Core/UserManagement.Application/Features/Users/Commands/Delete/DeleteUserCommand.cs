namespace UserManagement.Application.Features.Users.Commands.Delete;

public sealed record DeleteUserCommand(Guid UserId) : IRequest<Result<bool>>;