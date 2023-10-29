using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.OrderDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Domain.Enums;

namespace MyGlobalProject.Application.Features.Orders.Queries.GetAllOrder
{
    public class GetAllOrderQuery : IRequest<List<OrderListDTO>>
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Address { get; set; }
        public string AddressTitle { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime OrderCreateDate { get; set; }
        public string OrderStatus { get; set; }

        public class GetAllOrderQueryHandeler : IRequestHandler<GetAllOrderQuery, List<OrderListDTO>>
        {
            private readonly IOrderReadRepository _orderReadRepository;
            private readonly IMapper _mapper;

            public GetAllOrderQueryHandeler(IOrderReadRepository orderReadRepository, IMapper mapper)
            {
                _orderReadRepository = orderReadRepository;
                _mapper = mapper;
            }

            public async Task<List<OrderListDTO>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var orderList = await _orderReadRepository.GetBy(o => o.IsActive && !o.IsDeleted).ToListAsync();

                    var orderListDto = _mapper.Map<List<OrderListDTO>>(orderList);

                    return orderListDto;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
