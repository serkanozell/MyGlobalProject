using FluentValidation;

namespace MyGlobalProject.Application.Features.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(r => r.Id).NotEmpty().NotNull();
            RuleFor(r => r.Name).NotEmpty().NotNull();
        }
    }
}
