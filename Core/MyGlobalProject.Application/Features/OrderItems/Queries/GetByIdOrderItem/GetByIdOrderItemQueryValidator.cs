using FluentValidation;

namespace MyGlobalProject.Application.Features.OrderItems.Queries.GetByIdOrderItem
{
    public class GetByIdOrderItemQueryValidator : AbstractValidator<GetByIdOrderItemQuery>
    {
        public GetByIdOrderItemQueryValidator()
        {
            RuleFor(o => o.Id).NotEmpty().NotNull();
        }
    }
}
