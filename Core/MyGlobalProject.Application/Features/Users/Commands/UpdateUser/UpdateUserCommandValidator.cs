using FluentValidation;

namespace MyGlobalProject.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(u => u.Id).NotEmpty().NotNull();
            RuleFor(u => u.FirstName).NotEmpty().NotNull();
            RuleFor(u => u.LastName).NotEmpty().NotNull();
            RuleFor(u => u.UserName).NotEmpty().NotNull();
            RuleFor(u=>u.EMail).NotEmpty().NotNull();
        }
    }
}
