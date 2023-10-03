using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyGlobalProject.Application.Features.Users.Commands.CreateUser;
using MyGlobalProject.Application.Features.Users.Commands.LoginUser;
using MyGlobalProject.Application.Features.Users.Commands.RegisterUser;

namespace MyGlobalProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateUserCommand createUserCommand)
        {
            var result = await _mediator.Send(createUserCommand);

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterUserCommand registerUserCommand)
        {
            var result = await _mediator.Send(registerUserCommand);

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommand loginUserCommand)
        {
            var result = await _mediator.Send(loginUserCommand);

            return Ok(result);
        }
    }
}
