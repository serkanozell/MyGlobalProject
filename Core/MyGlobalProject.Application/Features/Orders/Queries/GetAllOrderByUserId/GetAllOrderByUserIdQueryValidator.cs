using FluentValidation;

namespace MyGlobalProject.Application.Features.Orders.Queries.GetAllOrderByUserId
{
    public class GetAllOrderByUserIdQueryValidator : AbstractValidator<GetAllOrderByUserIdQuery>
    {
        public GetAllOrderByUserIdQueryValidator()
        {
            RuleFor(o => o.UserId).NotEmpty().NotNull();
        }
    }
}
