using FluentValidation;

namespace MyGlobalProject.Application.Features.OrderItems.Commands.CreateOrderItemWithoutRegister
{
    public class CreateOrderItemWithoutRegisterCommandValidator : AbstractValidator<CreateOrderItemWithoutRegisterCommand>
    {
        public CreateOrderItemWithoutRegisterCommandValidator()
        {
            RuleFor(o => o.FirstName).NotEmpty().NotNull();
            RuleFor(o => o.LastName).NotEmpty().NotNull();
            RuleFor(o => o.EMail).NotEmpty().NotNull().EmailAddress();
            RuleFor(o => o.Address).NotEmpty().NotNull();
            RuleFor(o => o.AddressTitle).NotEmpty().NotNull();
            RuleFor(o => o.PhoneNumber).NotEmpty().NotNull();
            RuleFor(o=>o.OrderItems).NotEmpty().NotNull();
        }
    }
}
