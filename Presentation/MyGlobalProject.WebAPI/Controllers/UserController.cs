using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyGlobalProject.Application.Features.Users.Commands.CreateUser;
using MyGlobalProject.Application.Features.Users.Commands.LoginUser;
using MyGlobalProject.Application.Features.Users.Commands.RegisterUser;
using MyGlobalProject.Infrastructure.Schedule.BackgroundJobs;

namespace MyGlobalProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public UserController(IMediator mediator, IBackgroundJobClient backgroundJobClient)
        {
            _mediator = mediator;
            _backgroundJobClient = backgroundJobClient;
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

        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateUserCommand createUserCommand)
        {
            var result = await _mediator.Send(createUserCommand);

            _backgroundJobClient.Schedule(() => DelayedJobs.SendRegisteredUserToAllAdmins(result.Data!), TimeSpan.FromSeconds(5));

            return Ok(result);
        }
    }
}
