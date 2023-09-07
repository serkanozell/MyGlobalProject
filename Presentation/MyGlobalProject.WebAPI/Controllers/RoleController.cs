using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyGlobalProject.Application.Features.Roles.Commands.CreateRole;
using MyGlobalProject.Application.Features.Roles.Commands.DeleteRole;
using MyGlobalProject.Application.Features.Roles.Commands.UpdateRole;
using MyGlobalProject.Application.Features.Roles.Queries.GetAllRole;
using MyGlobalProject.Application.Features.Roles.Queries.GetByIdRole;

namespace MyGlobalProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateRoleCommand createRoleCommand)
        {
            var result = await _mediator.Send(createRoleCommand);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRoleCommand updateRoleCommand)
        {
            var result = await _mediator.Send(updateRoleCommand);

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteRoleCommand deleteRoleCommand)
        {
            var result = await _mediator.Send(deleteRoleCommand);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllRoleQuery());

            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdRoleQuery getByIdRoleQuery)
        {
            var result = await _mediator.Send(getByIdRoleQuery);

            return Ok(result);
        }
    }
}
