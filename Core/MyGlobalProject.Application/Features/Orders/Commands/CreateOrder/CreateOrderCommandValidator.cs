using FluentValidation;

namespace MyGlobalProject.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(o => o.UserId).NotEmpty().NotNull();
            RuleFor(o => o.AddressId).NotEmpty().NotNull();
        }
    }
}
