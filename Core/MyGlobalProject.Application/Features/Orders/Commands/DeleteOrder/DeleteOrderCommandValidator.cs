using FluentValidation;

namespace MyGlobalProject.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(o => o.Id).NotEmpty().NotNull();
        }
    }
}
