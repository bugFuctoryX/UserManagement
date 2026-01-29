namespace UserManagement.Application.Features.Authentication.Login;

internal sealed class LoginHandler(IAuthenticationService auth)
  : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
  public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken ct)
  {
    var user = await auth.LoginAsync(request.UserName, request.Password, ct);

    if (user is null)
      return Result<LoginResponse>.Fail(ErrorType.Unauthorized, "Invalid username or password.");

    return Result<LoginResponse>.Ok(user.ToLoginResponse());
  }
}