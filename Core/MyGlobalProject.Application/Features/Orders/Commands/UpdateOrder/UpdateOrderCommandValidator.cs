using FluentValidation;

namespace MyGlobalProject.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(o => o.Id).NotEmpty().NotNull();
            RuleFor(o => o.FirstName).NotEmpty().NotNull();
            RuleFor(o => o.EMail).NotEmpty().NotNull().EmailAddress();
            RuleFor(o => o.Address).NotEmpty().NotNull();
            RuleFor(o => o.AddressTitle).NotEmpty().NotNull();
            RuleFor(o => o.PhoneNumber).NotEmpty().NotNull();
        }
    }
}
