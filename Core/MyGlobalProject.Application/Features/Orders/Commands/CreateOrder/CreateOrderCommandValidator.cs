using FluentValidation;

namespace MyGlobalProject.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(o => o.FirstName).NotEmpty().NotNull();
            RuleFor(o => o.LastName).NotEmpty().NotNull();
            RuleFor(o => o.EMail).NotEmpty().NotNull().EmailAddress();
            RuleFor(o => o.Address).NotEmpty().NotNull();
            RuleFor(o => o.AddressTitle).NotEmpty().NotNull();
            RuleFor(o => o.PhoneNumber).NotEmpty().NotNull();
        }
    }
}
