using FluentValidation;

namespace MyGlobalProject.Application.Features.UserAddresses.Commands.UpdateUserAddress
{
    public class UpdateUserAddressCommandValidator : AbstractValidator<UpdateUserAddressCommand>
    {
        public UpdateUserAddressCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Address).NotEmpty().NotNull();
            RuleFor(x => x.AddressTitle).NotEmpty().NotNull();
        }
    }
}
