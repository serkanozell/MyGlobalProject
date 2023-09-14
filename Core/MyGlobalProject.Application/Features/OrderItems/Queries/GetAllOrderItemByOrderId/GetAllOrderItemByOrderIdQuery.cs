using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.OrderItemDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;

namespace MyGlobalProject.Application.Features.OrderItems.Queries.GetAllOrderItemByOrderId
{
    public class GetAllOrderItemByOrderIdQuery : IRequest<GenericResponse<List<OrderItemListDTO>>>
    {
        public Guid OrderId { get; set; }

        public class GetAllOrderItemByOrderIdQueryHandler : IRequestHandler<GetAllOrderItemByOrderIdQuery, GenericResponse<List<OrderItemListDTO>>>
        {
            private readonly IOrderItemReadRepository _orderItemReadRepository;
            private readonly IOrderReadRepository _orderReadRepository;
            private readonly IMapper _mapper;

            public GetAllOrderItemByOrderIdQueryHandler(IOrderItemReadRepository orderItemReadRepository, IOrderReadRepository orderReadRepository, IMapper mapper)
            {
                _orderItemReadRepository = orderItemReadRepository;
                _orderReadRepository = orderReadRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<List<OrderItemListDTO>>> Handle(GetAllOrderItemByOrderIdQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<List<OrderItemListDTO>>();

                var existOrder = await _orderReadRepository.GetByIdAsync(request.OrderId);

                if (existOrder == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Order error";

                    return response;
                }

                var orderItems = await _orderItemReadRepository.GetBy(o => o.OrderId == existOrder.Id && o.IsActive && !o.IsDeleted).ToListAsync();

                var mappedOrderItemListDto = _mapper.Map<List<OrderItemListDTO>>(orderItems);

                response.Data = mappedOrderItemListDto;
                response.Message = "Success";

                return response;
            }
        }
    }
}