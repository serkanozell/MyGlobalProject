using FluentValidation;

namespace MyGlobalProject.Application.Features.UserAddresses.Queries.GetByIdUserAddress
{
    public class GetByIdUserAddressQueryValidator : AbstractValidator<GetByIdUserAddressQuery>
    {
        public GetByIdUserAddressQueryValidator()
        {
            RuleFor(u => u.Id).NotEmpty()
                              .NotNull()
                              .WithMessage("Please enter an id");
        }
    }
}
