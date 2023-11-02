using MyGlobalProject.Application.Dto.OrderDtos;

namespace MyGlobalProject.Application.ServiceInterfaces.OrdersServices
{
    public interface IOrderService
    {
        Task<List<OrderListDTO>> GetAllOrder();
        Task<List<OrderListDTO>> GetAllOrderByUserId(Guid userId);
        Task<GetByIdOrderDTO> GetByIdOrder(Guid orderId);
    }
}
