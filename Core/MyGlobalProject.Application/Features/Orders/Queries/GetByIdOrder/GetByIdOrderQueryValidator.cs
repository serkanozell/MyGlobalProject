using FluentValidation;

namespace MyGlobalProject.Application.Features.Orders.Queries.GetByIdOrder
{
    public class GetByIdOrderQueryValidator : AbstractValidator<GetByIdOrderQuery>
    {
        public GetByIdOrderQueryValidator()
        {
            RuleFor(o => o.Id).NotEmpty().NotNull();
        }
    }
}
