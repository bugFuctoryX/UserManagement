namespace UserManagement.Application.Features.Authentication.Login;

public sealed record LoginCommand(string UserName, string Password) : IRequest<Result<LoginResponse>>;