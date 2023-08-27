using FluentValidation;

namespace MyGlobalProject.Application.Features.UserAddresses.Commands.CreateUserAddress
{
    public class CreateUserAddressCommandValidator : AbstractValidator<CreateUserAddressCommand>
    {
        public CreateUserAddressCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty().NotNull();
            RuleFor(c => c.Address).NotEmpty().NotNull();
            RuleFor(c => c.AddressTitle).NotEmpty().NotNull();
        }
    }
}
