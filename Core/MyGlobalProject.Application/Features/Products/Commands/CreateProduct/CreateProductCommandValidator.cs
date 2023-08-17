using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobalProject.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty()
                              .NotNull()
                              .Length(3, 40).WithMessage("Length must be between 3 - 40 characters");

            RuleFor(p => p.Description).NotEmpty()
                                     .NotNull()
                                     .MinimumLength(5)
                                     .WithMessage("Description length must be more than 5 characters");

            RuleFor(p => p.Stock).GreaterThanOrEqualTo(5)
                                 .WithMessage("Please enter more than or equal 5 products");

            RuleFor(p => p.Price).NotNull().GreaterThan(0);

            RuleFor(p => p.CategoryId).NotNull()
                                    .NotEmpty()
                                    .WithMessage("Product must have a category");
        }
    }
}
