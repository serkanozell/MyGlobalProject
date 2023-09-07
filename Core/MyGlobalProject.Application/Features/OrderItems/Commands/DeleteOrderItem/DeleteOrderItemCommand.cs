using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.OrderItemDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.OrderItems.Commands.DeleteOrderItem
{
    public class DeleteOrderItemCommand : IRequest<GenericResponse<DeleteOrderItemDTO>>
    {
        public Guid Id { get; set; }

        public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, GenericResponse<DeleteOrderItemDTO>>
        {
            private readonly IOrderItemReadRepository _orderItemReadRepository;
            private readonly IOrderItemWriteRepository _orderItemWriteRepository;
            private readonly IMapper _mapper;

            public DeleteOrderItemCommandHandler(IOrderItemReadRepository orderItemReadRepository, IOrderItemWriteRepository orderItemWriteRepository, IMapper mapper)
            {
                _orderItemReadRepository = orderItemReadRepository;
                _orderItemWriteRepository = orderItemWriteRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<DeleteOrderItemDTO>> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<DeleteOrderItemDTO>();

                var mappedOrderItem = _mapper.Map<OrderItem>(request);

                var currentOrderItem = await _orderItemReadRepository.GetByIdAsync(mappedOrderItem.Id);

                if (currentOrderItem is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Wrong id";

                    return response;
                }

                var deletedOrderItem = await _orderItemWriteRepository.DeleteAsync(mappedOrderItem);

                var deletedOrderItemDto = _mapper.Map<DeleteOrderItemDTO>(deletedOrderItem);

                response.Data = deletedOrderItemDto;
                response.Message = "Deleted successfully";

                return response;
            }
        }
    }
}
