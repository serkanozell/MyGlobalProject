using FluentValidation;

namespace MyGlobalProject.Application.Features.Roles.Commands.CreateRole
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(r => r.Name).NotEmpty().NotNull();
        }
    }
}
