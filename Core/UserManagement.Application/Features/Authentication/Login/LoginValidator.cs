namespace UserManagement.Application.Features.Authentication.Login;

public sealed class LoginValidator : AbstractValidator<LoginCommand>
{
  public LoginValidator()
  {
    RuleFor(x => x.UserName)
      .NotEmpty()
      .MinimumLength(3)
      .MaximumLength(64);

    RuleFor(x => x.Password)
      .NotEmpty()
      .MinimumLength(6)
      .MaximumLength(128);
  }
}