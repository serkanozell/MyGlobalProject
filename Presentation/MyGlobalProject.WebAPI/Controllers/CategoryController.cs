using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyGlobalProject.Application.Features.Categories.Commands.CreateCategory;
using MyGlobalProject.Application.Features.Categories.Commands.DeleteCategory;
using MyGlobalProject.Application.Features.Categories.Commands.UpdateCategory;
using MyGlobalProject.Application.Features.Categories.Queries.GetAllCategory;
using MyGlobalProject.Application.Features.Categories.Queries.GetByIdCategory;

namespace MyGlobalProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCategoryQuery());

            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdCategoryQuery getByIdQuery)
        {
            var result = await _mediator.Send(getByIdQuery);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(CreateCategoryCommand categoryAddDTO)
        {
            var result = await _mediator.Send(categoryAddDTO);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryCommand updateCategoryCommandRequest)
        {
            var result = await _mediator.Send(updateCategoryCommandRequest);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteCategoryCommand deleteCategoryCommand)
        {
            var result = await _mediator.Send(deleteCategoryCommand);

            return Ok(result);
        }
    }
}
