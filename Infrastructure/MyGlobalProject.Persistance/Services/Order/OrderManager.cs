using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.OrderDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.OrdersServices;

namespace MyGlobalProject.Persistance.Services.Order
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IMapper _mapper;

        public OrderManager(IOrderReadRepository orderReadRepository, IMapper mapper)
        {
            _orderReadRepository = orderReadRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderListDTO>> GetAllOrder()
        {
            return _mapper.Map<List<OrderListDTO>>(await _orderReadRepository.GetQueryableAllActive().ToListAsync());
        }

        public async Task<List<OrderListDTO>> GetAllOrderByUserId(Guid userId)
        {
            return _mapper.Map<List<OrderListDTO>>(await _orderReadRepository.GetBy(o => o.UserId == userId).ToListAsync());
        }

        public async Task<GetByIdOrderDTO> GetByIdOrder(Guid orderId)
        {
            return _mapper.Map<GetByIdOrderDTO>(await _orderReadRepository.GetByIdAsync(orderId));
        }
    }
}
