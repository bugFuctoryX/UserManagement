namespace UserManagement.Application.Features.Users.Commands.Add;

internal sealed class AddUserHandler(IFileUserRepository _userRepository,
  IPasswordHasher _hasher) : IRequestHandler<AddUserCommand, Result<AddUserResponse>>
{
  public async Task<Result<AddUserResponse>> Handle(AddUserCommand command, CancellationToken ct)
  {
    var passwordHash = _hasher.Hash(command.Password);

    var result = await _userRepository.AddUserAsync(command.ToUser(passwordHash, DateTime.UtcNow), ct);

    return Result<AddUserResponse>.Ok(new AddUserResponse(result));
  }
}