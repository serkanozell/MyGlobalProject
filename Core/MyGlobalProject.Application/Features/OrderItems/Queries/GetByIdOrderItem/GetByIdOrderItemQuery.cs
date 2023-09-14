using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.OrderItemDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.OrderItems.Queries.GetByIdOrderItem
{
    public class GetByIdOrderItemQuery : IRequest<GenericResponse<GetByIdOrderItemDTO>>
    {
        public Guid Id { get; set; }

        public class GetByIdOrderItemQueryHandler : IRequestHandler<GetByIdOrderItemQuery, GenericResponse<GetByIdOrderItemDTO>>
        {
            private readonly IOrderItemReadRepository _orderItemReadRepository;
            private readonly IMapper _mapper;

            public GetByIdOrderItemQueryHandler(IOrderItemReadRepository orderItemReadRepository, IMapper mapper)
            {
                _orderItemReadRepository = orderItemReadRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<GetByIdOrderItemDTO>> Handle(GetByIdOrderItemQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<GetByIdOrderItemDTO>();

                var mappedOrderItem = _mapper.Map<OrderItem>(request);

                var currentOrderItem = await _orderItemReadRepository.GetByIdAsync(mappedOrderItem.Id);

                if (currentOrderItem is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "There is no item";

                    return response;
                }

                var getByIdOrderItemDto = _mapper.Map<GetByIdOrderItemDTO>(currentOrderItem);

                response.Data = getByIdOrderItemDto;
                response.Message = "Success";

                return response;
            }
        }
    }
}
