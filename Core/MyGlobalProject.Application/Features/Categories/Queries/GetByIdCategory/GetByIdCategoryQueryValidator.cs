using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobalProject.Application.Features.Categories.Queries.GetByIdCategory
{
    public class GetByIdCategoryQueryValidator:AbstractValidator<GetByIdCategoryQuery>
    {
        public GetByIdCategoryQueryValidator()
        {
            RuleFor(c => c.Id).NotEmpty().NotNull();
        }
    }
}
