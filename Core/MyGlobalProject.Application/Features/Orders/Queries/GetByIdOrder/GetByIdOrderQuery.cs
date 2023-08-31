using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.OrderDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.Orders.Queries.GetByIdOrder
{
    public class GetByIdOrderQuery : IRequest<GenericResponse<GetByIdOrderDTO>>
    {
        public Guid Id { get; set; }

        public class GetByIdOrderQueryHandler : IRequestHandler<GetByIdOrderQuery, GenericResponse<GetByIdOrderDTO>>
        {
            private readonly IOrderReadRepository _orderReadRepository;
            private readonly IMapper _mapper;
            public GetByIdOrderQueryHandler(IOrderReadRepository orderReadRepository, IMapper mapper)
            {
                _orderReadRepository = orderReadRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<GetByIdOrderDTO>> Handle(GetByIdOrderQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<GetByIdOrderDTO>();

                var mappedOrder = _mapper.Map<Order>(request);

                var order = await _orderReadRepository.GetByIdAsync(mappedOrder.Id);

                if (order is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Wrong id";

                    return response;
                }

                var getByIdOrderDto = _mapper.Map<GetByIdOrderDTO>(order);

                response.Data = getByIdOrderDto;
                response.Message = "Success";

                return response;
            }
        }
    }
}
