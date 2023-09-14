using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.OrderItemDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;

namespace MyGlobalProject.Application.Features.OrderItems.Queries.GetAllOrderItem
{
    public class GetAllOrderItemQuery : IRequest<GenericResponse<List<OrderItemListDTO>>>
    {
        public class GetAllOrderItemQueryHandler : IRequestHandler<GetAllOrderItemQuery, GenericResponse<List<OrderItemListDTO>>>
        {
            private readonly IOrderItemReadRepository _orderItemReadRepository;
            private readonly IMapper _mapper;

            public GetAllOrderItemQueryHandler(IOrderItemReadRepository orderItemReadRepository, IMapper mapper)
            {
                _orderItemReadRepository = orderItemReadRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<List<OrderItemListDTO>>> Handle(GetAllOrderItemQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<List<OrderItemListDTO>>();

                var currentOrderItemList = await _orderItemReadRepository.GetAll(o=>o.IsActive&&!o.IsDeleted).ToListAsync();

                var mappedOderItemList = _mapper.Map<List<OrderItemListDTO>>(currentOrderItemList);

                response.Data = mappedOderItemList;
                response.Message = "Success";

                return response;
            }
        }
    }
}