using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobalProject.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator:AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("Id can't be empty");
            RuleFor(c => c.Name).NotNull().Length(3, 20).WithMessage("Name must be between 3-20 characters");
        }
    }
}
