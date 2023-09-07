using FluentValidation;

namespace MyGlobalProject.Application.Features.OrderItems.Commands.UpdateOrderItem
{
    public class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
    {
        public UpdateOrderItemCommandValidator()
        {
            RuleFor(o => o.Id).NotEmpty().NotNull();
            RuleFor(o => o.OrderId).NotEmpty().NotNull();
            RuleFor(o => o.ProductId).NotEmpty().NotNull();

            RuleFor(o => o.Quantity).NotEmpty()
                                    .NotNull()
                                    .GreaterThanOrEqualTo(1)
                                    .WithMessage("Quantity count must be more than 1");

            RuleFor(o => o.Price).NotEmpty()
                                 .NotNull()
                                 .GreaterThanOrEqualTo(1)
                                 .WithMessage("Price value must be more than 1");
        }
    }
}
