namespace UserManagement.Application.Features.Users.Commands.Update;

public sealed class UpdateUserValidation : AbstractValidator<UpdateUserCommand>
{
  public UpdateUserValidation()
  {
    RuleFor(x => x.UserId)
        .NotEmpty();

    RuleFor(x => x.UserName)
        .NotEmpty()
        .MinimumLength(3)
        .MaximumLength(50);

    RuleFor(x => x.Email)
        .NotEmpty()
        .EmailAddress()
        .MaximumLength(254);

    RuleFor(x => x.FirstName)
        .NotEmpty()
        .MaximumLength(100);

    RuleFor(x => x.LastName)
        .NotEmpty()
        .MaximumLength(100);

    RuleFor(x => x.BirthPlace)
        .NotEmpty()
        .MaximumLength(120);

    RuleFor(x => x.City)
        .NotEmpty()
        .MaximumLength(120);

    RuleFor(x => x.BirthDate)
        .Must(d => d <= DateOnly.FromDateTime(DateTime.UtcNow))
        .WithMessage("A születési dátum nem lehet jövőbeli.");

    When(x => !string.IsNullOrWhiteSpace(x.Password), () =>
    {
      RuleFor(x => x.Password)
          .MinimumLength(8)
          .MaximumLength(128)
          .Matches("[A-Z]")
              .WithMessage("The password must contain at least one uppercase letter.")
          .Matches("[a-z]")
              .WithMessage("The password must contain at least one lowercase letter.")
          .Matches("[0-9]")
              .WithMessage("The password must contain at least one digit.");
    });

    RuleFor(x => x.FullName)
        .Must((cmd, fullName) =>
            string.IsNullOrWhiteSpace(fullName) ||
            fullName.Trim() == $"{cmd.FirstName} {cmd.LastName}".Trim())
        .WithMessage("FullName must match the combination of FirstName and LastName.");
  }
}