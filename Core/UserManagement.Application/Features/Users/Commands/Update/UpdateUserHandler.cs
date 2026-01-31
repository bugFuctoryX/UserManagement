namespace UserManagement.Application.Features.Users.Commands.Update;

internal sealed class UpdateUserHandler(IFileUserRepository _userRepository, 
  IPasswordHasher _hasher) : IRequestHandler<UpdateUserCommand, Result<UpdateUserResponse>>
{
  public async Task<Result<UpdateUserResponse>> Handle(UpdateUserCommand request, CancellationToken ct)
  {
    var existing = await _userRepository.GetByIdAsync(request.UserId, ct);
    if (existing is null)
    {
      return Result<UpdateUserResponse>.Fail(ErrorType.NotFound,"User not found.");
    }

    var passwordHash =
        string.IsNullOrWhiteSpace(request.Password)
            ? existing.Credential.PasswordHash
            : _hasher.Hash(request.Password);

    var utcNow = DateTime.UtcNow;

    User updated = request.ToUser(passwordHash, utcNow);

    updated.Credential.CreatedAtUtc = existing.Credential.CreatedAtUtc;
    updated.Credential.LastLoginAtUtc = existing.Credential.LastLoginAtUtc;

    await _userRepository.UpdateAsync(updated, ct);

    return Result<UpdateUserResponse>.Ok(new UpdateUserResponse(request.UserId));
  }
}
