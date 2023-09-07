using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyGlobalProject.Application.Features.OrderItems.Commands.CreateOrderItem;
using MyGlobalProject.Application.Features.OrderItems.Commands.CreateOrderItemWithoutRegister;

namespace MyGlobalProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateOrderItemCommand createOrderItemCommand)
        {
            var result = await _mediator.Send(createOrderItemCommand);

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddWithoutRegister(CreateOrderItemWithoutRegisterCommand createOrderItemWithoutRegisterCommand)
        {
            var result = await _mediator.Send(createOrderItemWithoutRegisterCommand);

            return Ok(result);
        }
    }
}
