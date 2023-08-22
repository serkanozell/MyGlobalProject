using FluentValidation;

namespace MyGlobalProject.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.FirstName).NotNull().NotEmpty();
            RuleFor(c => c.LastName).NotNull().NotEmpty();
            RuleFor(c => c.UserName).NotNull().NotEmpty();
            RuleFor(c => c.Password).NotNull().NotEmpty();
            RuleFor(c => c.EMail).NotNull().NotEmpty();
        }
    }
}
