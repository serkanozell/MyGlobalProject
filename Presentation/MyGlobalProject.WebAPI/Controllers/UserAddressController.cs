using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyGlobalProject.Application.Features.UserAddresses.Commands.CreateUserAddress;
using MyGlobalProject.Application.Features.UserAddresses.Commands.DeleteUserAddress;
using MyGlobalProject.Application.Features.UserAddresses.Commands.UpdateUserAddress;
using MyGlobalProject.Application.Features.UserAddresses.Queries.GetAllUserAddress;
using MyGlobalProject.Application.Features.UserAddresses.Queries.GetAllUserAddressByUserId;
using MyGlobalProject.Application.Features.UserAddresses.Queries.GetByIdUserAddress;

namespace MyGlobalProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserAddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateUserAddressCommand createUserAddressCommand)
        {
            var result = await _mediator.Send(createUserAddressCommand);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserAddressCommand updateUserAddressCommand)
        {
            var result = await _mediator.Send(updateUserAddressCommand);

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteUserAddressCommand deleteUserAddressCommand)
        {
            var result = await _mediator.Send(deleteUserAddressCommand);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllUserAddressQuery());

            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdUserAddressQuery getByIdUserAddressQuery)
        {
            var result = await _mediator.Send(getByIdUserAddressQuery);

            return Ok(result);
        }

        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> GetAllByUserId([FromRoute] GetAllUserAddressByUserIdQuery getAllUserAddressByUserIdQuery)
        {
            var result = await _mediator.Send(getAllUserAddressByUserIdQuery);

            return Ok(result);
        }
    }
}
