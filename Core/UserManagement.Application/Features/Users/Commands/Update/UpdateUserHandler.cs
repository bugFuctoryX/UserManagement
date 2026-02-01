using MediatR;

namespace UserManagement.Application.Features.Users.Commands.Update;

internal sealed class UpdateUserHandler(IFileUserRepository _userRepository, 
  IPasswordHasher _hasher) : IRequestHandler<UpdateUserCommand, Result<UpdateUserResponse>>
{
  public async Task<Result<UpdateUserResponse>> Handle(UpdateUserCommand command, CancellationToken ct)
  {
    var existing = await _userRepository.GetByIdAsync(command.UserId, ct);
    if (existing is null)
    {
      return Result<UpdateUserResponse>.Fail(ErrorType.NotFound,"User not found.");
    }

    var passwordHash =
    string.IsNullOrWhiteSpace(command.Password)
        ? existing.Credential.PasswordHash
        : _hasher.Hash(command.Password);

    User updated = command.ToUser(passwordHash, DateTime.UtcNow);

    updated.Credential.CreatedAtUtc = existing.Credential.CreatedAtUtc;
    updated.Credential.LastLoginAtUtc = existing.Credential.LastLoginAtUtc;

    await _userRepository.UpdateAsync(updated, ct);

    return Result<UpdateUserResponse>.Ok(new UpdateUserResponse(command.UserId));
  }
}
