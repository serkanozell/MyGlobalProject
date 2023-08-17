using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyGlobalProject.Application.Features.Products.Commands.CreateProduct;
using MyGlobalProject.Application.Features.Products.Commands.DeleteProduct;
using MyGlobalProject.Application.Features.Products.Commands.UpdateProduct;
using MyGlobalProject.Application.Features.Products.Queries.GetAllProduct;
using MyGlobalProject.Application.Features.Products.Queries.GetByIdProduct;

namespace MyGlobalProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateProductCommand createProductCommand)
        {
            var result = await _mediator.Send(createProductCommand);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductCommand updateProductCommand)
        {
            var result = await _mediator.Send(updateProductCommand);

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommand deleteProductCommand)
        {
            var result = await _mediator.Send(deleteProductCommand);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllProductQuery());

            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProductQuery getByIdProductQuery)
        {
            var result = await _mediator.Send(getByIdProductQuery);

            return Ok(result);
        }
    }
}
