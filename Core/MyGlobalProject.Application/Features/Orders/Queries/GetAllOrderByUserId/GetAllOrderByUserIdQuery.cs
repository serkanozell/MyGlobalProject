using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.OrderDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;

namespace MyGlobalProject.Application.Features.Orders.Queries.GetAllOrderByUserId
{
    public class GetAllOrderByUserIdQuery : IRequest<GenericResponse<List<OrderListDTO>>>
    {
        public Guid UserId { get; set; }

        public class GetAllOrderByUserIdQueryHandler : IRequestHandler<GetAllOrderByUserIdQuery, GenericResponse<List<OrderListDTO>>>
        {
            private readonly IUserReadRepository _userReadRepository;
            private readonly IOrderReadRepository _orderReadRepository;
            private readonly IMapper _mapper;

            public GetAllOrderByUserIdQueryHandler(IUserReadRepository userReadRepository, IOrderReadRepository orderReadRepository, IMapper mapper)
            {
                _userReadRepository = userReadRepository;
                _orderReadRepository = orderReadRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<List<OrderListDTO>>> Handle(GetAllOrderByUserIdQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<List<OrderListDTO>>();

                var currentUser = await _userReadRepository.GetByIdAsync(request.UserId);

                if (currentUser is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Invalid user";

                    return response;
                }

                var orderList = await _orderReadRepository.GetBy(o => o.UserId == currentUser.Id).ToListAsync();

                var orderListDto = _mapper.Map<List<OrderListDTO>>(orderList);

                response.Data = orderListDto;
                response.Message = "Success";

                return response;
            }
        }
    }
}
