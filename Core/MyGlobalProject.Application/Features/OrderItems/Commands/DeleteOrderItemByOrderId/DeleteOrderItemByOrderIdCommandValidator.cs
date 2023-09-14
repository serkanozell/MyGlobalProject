using FluentValidation;

namespace MyGlobalProject.Application.Features.OrderItems.Commands.DeleteOrderItemByOrderId
{
    public class DeleteOrderItemByOrderIdCommandValidator : AbstractValidator<DeleteOrderItemByOrderIdCommand>
    {
        public DeleteOrderItemByOrderIdCommandValidator()
        {
            RuleFor(o => o.OrderId).NotEmpty().NotNull();
        }
    }
}
