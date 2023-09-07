using FluentValidation;

namespace MyGlobalProject.Application.Features.Orders.Commands.CreateOrderWithoutRegister
{
    public class CreateOrderWithoutRegisterCommandValidator : AbstractValidator<CreateOrderWithoutRegisterCommand>
    {
        public CreateOrderWithoutRegisterCommandValidator()
        {
            RuleFor(o => o.FirstName).NotEmpty().NotNull().MinimumLength(3);
            RuleFor(o => o.LastName).NotEmpty().NotNull().MinimumLength(3);
            RuleFor(o => o.EMail).NotEmpty().NotNull().EmailAddress();
            RuleFor(o => o.Address).NotEmpty().NotNull();
            RuleFor(o => o.AddressTitle).NotEmpty().NotNull();
            RuleFor(o => o.PhoneNumber).NotEmpty().NotNull();
        }
    }
}
