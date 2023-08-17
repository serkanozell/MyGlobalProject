using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobalProject.Application.Features.Products.Queries.GetByIdProduct
{
    public class GetByIdProductQueryValidator:AbstractValidator<GetByIdProductQuery>
    {
        public GetByIdProductQueryValidator()
        {
            RuleFor(p=>p.Id).NotEmpty().NotEmpty();
        }
    }
}
