using FluentValidation;

namespace MyGlobalProject.Application.Features.OrderItems.Commands.DeleteOrderItem
{
    public class DeleteOrderItemCommandValidator : AbstractValidator<DeleteOrderItemCommand>
    {
        public DeleteOrderItemCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}
