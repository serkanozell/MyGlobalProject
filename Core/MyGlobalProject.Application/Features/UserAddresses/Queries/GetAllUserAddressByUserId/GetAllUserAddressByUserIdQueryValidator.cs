using FluentValidation;

namespace MyGlobalProject.Application.Features.UserAddresses.Queries.GetAllUserAddressByUserId
{
    public class GetAllUserAddressByUserIdQueryValidator : AbstractValidator<GetAllUserAddressByUserIdQuery>
    {
        public GetAllUserAddressByUserIdQueryValidator()
        {
            RuleFor(a => a.UserId).NotEmpty().NotNull();
        }
    }
}
