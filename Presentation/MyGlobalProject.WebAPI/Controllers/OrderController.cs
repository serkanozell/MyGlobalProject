using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyGlobalProject.Application.Features.Orders.Commands.CreateOrder;
using MyGlobalProject.Application.Features.Orders.Commands.CreateOrderWithoutRegister;
using MyGlobalProject.Application.Features.Orders.Commands.DeleteOrder;
using MyGlobalProject.Application.Features.Orders.Commands.UpdateOrder;
using MyGlobalProject.Application.Features.Orders.Queries.GetAllOrder;
using MyGlobalProject.Application.Features.Orders.Queries.GetAllOrderByUserId;
using MyGlobalProject.Application.Features.Orders.Queries.GetByIdOrder;

namespace MyGlobalProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateOrderCommand createOrderCommand)
        {
            var result = await _mediator.Send(createOrderCommand);

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddWithoutRegister(CreateOrderWithoutRegisterCommand createOrderWithoutRegisterCommand)
        {
            var result = await _mediator.Send(createOrderWithoutRegisterCommand);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateOrderCommand updateOrderCommand)
        {
            var result = await _mediator.Send(updateOrderCommand);

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteOrderCommand deleteOrderCommand)
        {
            var result = await _mediator.Send(deleteOrderCommand);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllOrderQuery());

            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdOrderQuery getByIdOrderQuery)
        {
            var result = await _mediator.Send(getByIdOrderQuery);

            return Ok(result);
        }

        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> GetAllByUserId([FromRoute] GetAllOrderByUserIdQuery getAllOrderByUserIdQuery)
        {
            var result = await _mediator.Send(getAllOrderByUserIdQuery);

            return Ok(result);
        }
    }
}
