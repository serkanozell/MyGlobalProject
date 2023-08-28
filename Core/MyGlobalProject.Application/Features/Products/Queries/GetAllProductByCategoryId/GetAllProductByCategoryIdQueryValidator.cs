using FluentValidation;

namespace MyGlobalProject.Application.Features.Products.Queries.GetAllProductByCategoryId
{
    public class GetAllProductByCategoryIdQueryValidator : AbstractValidator<GetAllProductByCategoryIdQuery>
    {
        public GetAllProductByCategoryIdQueryValidator()
        {
            RuleFor(p => p.Id).NotEmpty()
                              .NotNull();
        }
    }
}
