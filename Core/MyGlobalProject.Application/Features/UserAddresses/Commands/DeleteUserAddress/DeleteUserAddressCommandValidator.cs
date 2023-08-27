using FluentValidation;

namespace MyGlobalProject.Application.Features.UserAddresses.Commands.DeleteUserAddress
{
    public class DeleteUserAddressCommandValidator : AbstractValidator<DeleteUserAddressCommand>
    {
        public DeleteUserAddressCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}
