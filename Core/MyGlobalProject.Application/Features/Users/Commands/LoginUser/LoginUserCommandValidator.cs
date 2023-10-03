using FluentValidation;

namespace MyGlobalProject.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(u => u.EMail).NotEmpty().NotNull().EmailAddress();
            RuleFor(u => u.Password).NotEmpty().NotNull().MinimumLength(8);
        }
    }
}
