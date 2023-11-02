using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.OrderItemDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.OrderItemsServices;

namespace MyGlobalProject.Persistance.Services.OrderItem
{
    public class OrderItemManager : IOrderItemService
    {
        private readonly IOrderItemReadRepository _orderItemReadRepository;
        private readonly IMapper _mapper;

        public OrderItemManager(IOrderItemReadRepository orderItemReadRepository, IMapper mapper)
        {
            _orderItemReadRepository = orderItemReadRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderItemListDTO>> GetAllOrderItem()
        {
            return _mapper.Map<List<OrderItemListDTO>>(await _orderItemReadRepository.GetAll(o => o.IsActive && !o.IsDeleted).ToListAsync());
        }

        public async Task<List<OrderItemListDTO>> GetAllOrderItemByOrderId(Guid orderId)
        {
            return _mapper.Map<List<OrderItemListDTO>>(await _orderItemReadRepository.GetBy(o => o.OrderId == orderId && o.IsActive && !o.IsDeleted).ToListAsync());
        }

        public async Task<GetByIdOrderItemDTO> GetByIdOrderItem(Guid orderItemId)
        {
            return _mapper.Map<GetByIdOrderItemDTO>(await _orderItemReadRepository.GetByIdAsync(orderItemId));
        }
    }
}
