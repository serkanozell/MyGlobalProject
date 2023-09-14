using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.OrderItemDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.OrderItems.Commands.DeleteOrderItemByOrderId
{
    public class DeleteOrderItemByOrderIdCommand : IRequest<GenericResponse<List<DeleteOrderItemByOrderIdDTO>>>
    {
        public Guid OrderId { get; set; }

        public class DeleteOrderItemByOrderIdCommandHandler : IRequestHandler<DeleteOrderItemByOrderIdCommand, GenericResponse<List<DeleteOrderItemByOrderIdDTO>>>
        {
            private readonly IOrderItemReadRepository _orderItemReadRepository;
            private readonly IOrderItemWriteRepository _orderItemWriteRepository;
            private readonly IOrderReadRepository _orderReadRepository;
            private readonly IOrderWriteRepository _orderWriteRepository;
            private readonly IMapper _mapper;

            public DeleteOrderItemByOrderIdCommandHandler(IOrderItemReadRepository orderItemReadRepository, IOrderItemWriteRepository orderItemWriteRepository, IMapper mapper, IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository)
            {
                _orderItemReadRepository = orderItemReadRepository;
                _orderItemWriteRepository = orderItemWriteRepository;
                _orderReadRepository = orderReadRepository;
                _orderWriteRepository = orderWriteRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<List<DeleteOrderItemByOrderIdDTO>>> Handle(DeleteOrderItemByOrderIdCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<List<DeleteOrderItemByOrderIdDTO>>();

                var currentOrder = await _orderReadRepository.GetByIdAsync(request.OrderId);
                
                if (currentOrder is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "There is no order";

                    return response;
                }

                var orderItems = await _orderItemReadRepository.GetBy(o => o.OrderId == currentOrder.Id && o.IsActive && !o.IsDeleted).ToListAsync();

                if (!orderItems.Any())
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "There is no order item";

                    return response;
                }

                var deletedOrderItems = await _orderItemWriteRepository.DeleteRangeAsync(orderItems);

                var mappedDeletedOrderItemsDto = _mapper.Map<List<DeleteOrderItemByOrderIdDTO>>(deletedOrderItems);

                await _orderWriteRepository.DeleteAsync(currentOrder);

                response.Data = mappedDeletedOrderItemsDto;
                response.Message = "Success";

                return response;
            }
        }
    }
}
