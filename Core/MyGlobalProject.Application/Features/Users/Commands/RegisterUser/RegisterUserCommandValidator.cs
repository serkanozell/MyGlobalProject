using FluentValidation;

namespace MyGlobalProject.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().NotNull();
            RuleFor(u => u.LastName).NotEmpty().NotNull();
            RuleFor(u => u.EMail).NotEmpty().NotNull().EmailAddress();
            RuleFor(u => u.Password).NotEmpty().NotNull().Length(8);
        }
    }
}
