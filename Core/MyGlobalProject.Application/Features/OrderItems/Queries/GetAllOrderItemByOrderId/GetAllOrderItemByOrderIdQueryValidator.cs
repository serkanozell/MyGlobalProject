using FluentValidation;

namespace MyGlobalProject.Application.Features.OrderItems.Queries.GetAllOrderItemByOrderId
{
    public class GetAllOrderItemByOrderIdQueryValidator : AbstractValidator<GetAllOrderItemByOrderIdQuery>
    {
        public GetAllOrderItemByOrderIdQueryValidator()
        {
            RuleFor(o => o.OrderId).NotEmpty().NotNull();
        }
    }
}
