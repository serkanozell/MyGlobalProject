using FluentValidation;

namespace MyGlobalProject.Application.Features.Users.Queries.GetByIdUsers
{
    public class GetByIdUserQueryValidator : AbstractValidator<GetByIdUserQuery>
    {
        public GetByIdUserQueryValidator()
        {
            RuleFor(u => u.Id).NotEmpty().NotNull();
        }
    }
}
