using MyGlobalProject.Application.Dto.Common;

namespace MyGlobalProject.Application.Dto.OrderItemDtos
{
    public class UpdateOrderItemDTO : BaseDTO
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
