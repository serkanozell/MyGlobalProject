using MyGlobalProject.Application.Dto.OrderItemDtos;

namespace MyGlobalProject.Application.ServiceInterfaces.OrderItemsServices
{
    public interface IOrderItemService
    {
        Task<List<OrderItemListDTO>> GetAllOrderItem();
        Task<List<OrderItemListDTO>> GetAllOrderItemByOrderId(Guid orderId);
        Task<GetByIdOrderItemDTO> GetByIdOrderItem(Guid orderItemId);
    }
}
