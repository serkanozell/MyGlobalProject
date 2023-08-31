using FluentValidation;

namespace MyGlobalProject.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(u => u.Id).NotEmpty().NotNull();
        }
    }
}
