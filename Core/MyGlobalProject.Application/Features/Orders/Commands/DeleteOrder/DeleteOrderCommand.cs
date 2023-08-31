using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.OrderDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;

namespace MyGlobalProject.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<GenericResponse<DeleteOrderDTO>>
    {
        public Guid Id { get; set; }

        public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, GenericResponse<DeleteOrderDTO>>
        {
            private readonly IOrderReadRepository _orderReadRepository;
            private readonly IOrderWriteRepository _orderWriteRepository;
            private IMapper _mapper;

            public DeleteOrderCommandHandler(IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository, IMapper mapper)
            {
                _orderReadRepository = orderReadRepository;
                _orderWriteRepository = orderWriteRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<DeleteOrderDTO>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<DeleteOrderDTO>();

                var currentOrder = await _orderReadRepository.GetByIdAsync(request.Id);

                if (currentOrder is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Wrong order id";

                    return response;
                }

                currentOrder.IsActive = false;
                currentOrder.IsDeleted = true;

                var updatedOrderDto = await _orderWriteRepository.UpdateAsync(currentOrder);

                var deletedOrderDto = _mapper.Map<DeleteOrderDTO>(updatedOrderDto);

                response.Data = deletedOrderDto;
                response.Message = "Success";

                return response;
            }
        }
    }
}
