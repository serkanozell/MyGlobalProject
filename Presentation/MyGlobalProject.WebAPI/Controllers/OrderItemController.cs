using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyGlobalProject.Application.Features.OrderItems.Commands.CreateOrderItem;
using MyGlobalProject.Application.Features.OrderItems.Commands.CreateOrderItemWithoutRegister;
using MyGlobalProject.Application.Features.OrderItems.Commands.DeleteOrderItem;
using MyGlobalProject.Application.Features.OrderItems.Commands.DeleteOrderItemByOrderId;
using MyGlobalProject.Application.Features.OrderItems.Commands.UpdateOrderItem;
using MyGlobalProject.Application.Features.OrderItems.Queries.GetAllOrderItem;
using MyGlobalProject.Application.Features.OrderItems.Queries.GetAllOrderItemByOrderId;
using MyGlobalProject.Application.Features.OrderItems.Queries.GetByIdOrderItem;
using MyGlobalProject.Application.Features.Orders.Commands.UpdateOrder;

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

        [HttpPut]
        public async Task<IActionResult> Update(UpdateOrderItemCommand updateOrderItemCommand)
        {
            var result = await _mediator.Send(updateOrderItemCommand);

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteOrderItemCommand deleteOrderItemCommand)
        {
            var result = await _mediator.Send(deleteOrderItemCommand);

            return Ok(result);
        }

        [HttpDelete("[action]/{OrderId}")]
        public async Task<IActionResult> DeleteByOrderId([FromRoute] DeleteOrderItemByOrderIdCommand deleteOrderItemByOrderIdCommand)
        {
            var result = await _mediator.Send(deleteOrderItemByOrderIdCommand);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllOrderItemQuery());

            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdOrderItemQuery getByIdOrderItemQuery)
        {
            var result = await _mediator.Send(getByIdOrderItemQuery);

            return Ok(result);
        }

        [HttpGet("[action]/{OrderId}")]
        public async Task<IActionResult> GetAllByOrderId([FromRoute] GetAllOrderItemByOrderIdQuery getAllOrderItemByOrderIdQuery)
        {
            var result = await _mediator.Send(getAllOrderItemByOrderIdQuery);

            return Ok(result);
        }
    }
}
