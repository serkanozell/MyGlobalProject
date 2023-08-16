using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobalProject.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(p => p.Name).NotNull()
                                .WithMessage("Category name can't be null")
                                .Length(3,20)
                                .WithMessage("Category name must be between 3-20 characters");

        }
    }
}
