using FluentValidation;

namespace MyGlobalProject.Application.Features.Roles.Queries.GetByIdRole
{
    public class GetByIdRoleQueryValidator : AbstractValidator<GetByIdRoleQuery>
    {
        public GetByIdRoleQueryValidator()
        {
            RuleFor(r => r.Id).NotEmpty().NotNull();
        }
    }
}
