using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobalProject.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty()
                              .NotNull();

            RuleFor(p => p.Name).NotEmpty()
                                .Length(2, 45)
                                .WithMessage("Product name has to be between 2 - 45 characters");

            RuleFor(p => p.Description).NotEmpty()
                                       .MinimumLength(5);

            RuleFor(p => p.Stock).NotEmpty()
                                 .NotNull()
                                 .GreaterThan(0)
                                 .WithMessage("Stock count has to be greater than 0");

            RuleFor(p => p.Price).NotEmpty()
                                 .NotNull()
                                 .GreaterThan(0)
                                 .WithMessage("Every product has to be a price");

            RuleFor(p => p.CategoryId).NotEmpty()
                                      .NotNull()
                                      .WithMessage("Please enter a category for product");
        }
    }
}
