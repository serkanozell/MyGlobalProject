using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobalProject.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandValidatior:AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidatior()
        {
            RuleFor(p=>p.Id).NotEmpty().NotNull();
        }
    }
}
