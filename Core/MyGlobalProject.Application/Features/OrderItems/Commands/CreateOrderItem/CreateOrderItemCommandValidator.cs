using FluentValidation;

namespace MyGlobalProject.Application.Features.OrderItems.Commands.CreateOrderItem
{
    public class CreateOrderItemCommandValidator : AbstractValidator<CreateOrderItemCommand>
    {
        public CreateOrderItemCommandValidator()
        {
            RuleFor(o => o.UserId).NotEmpty().NotNull();
            RuleFor(o => o.OrderItems).NotEmpty().NotNull();
        }
    }
}
