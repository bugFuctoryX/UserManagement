namespace UserManagement.Application.Features.Users.Commands.Add;

public sealed class AddUserValidation : AbstractValidator<AddUserCommand>
{
  public AddUserValidation()
  {
    RuleFor(x => x.UserName)
        .NotEmpty().WithMessage("Username is required.")
        .MinimumLength(3).WithMessage("Username must be at least 3 characters.")
        .MaximumLength(50).WithMessage("Username must be at most 50 characters.")
        .Matches("^[a-zA-Z0-9._-]+$").WithMessage("Username can contain letters, numbers, '.', '_' and '-' only.");

    RuleFor(x => x.Email)
        .NotEmpty().WithMessage("Email is required.")
        .MaximumLength(254).WithMessage("Email must be at most 254 characters.")
        .EmailAddress().WithMessage("Email is not valid.");

    RuleFor(x => x.Password)
        .NotEmpty().WithMessage("Password is required.")
        .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
        .MaximumLength(128).WithMessage("Password must be at most 128 characters.");

    RuleFor(x => x.FirstName)
        .NotEmpty().WithMessage("First name is required.")
        .MaximumLength(100).WithMessage("First name must be at most 100 characters.");

    RuleFor(x => x.LastName)
        .NotEmpty().WithMessage("Last name is required.")
        .MaximumLength(100).WithMessage("Last name must be at most 100 characters.");

    RuleFor(x => x.BirthPlace)
        .NotEmpty().WithMessage("Birth place is required.")
        .MaximumLength(120).WithMessage("Birth place must be at most 120 characters.");

    RuleFor(x => x.City)
        .NotEmpty().WithMessage("City is required.")
        .MaximumLength(120).WithMessage("City must be at most 120 characters.");

    RuleFor(x => x.BirthDate)
        .Must(BeValidBirthDate)
        .WithMessage("Birth date is not valid.");
  }

  private static bool BeValidBirthDate(DateOnly birthDate)
  {
    var today = DateOnly.FromDateTime(DateTime.UtcNow);

    if (birthDate > today) return false;

    if (birthDate < today.AddYears(-120)) return false;

    return true;
  }
}
