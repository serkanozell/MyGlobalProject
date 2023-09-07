using MyGlobalProject.Application.Dto.Common;

namespace MyGlobalProject.Application.Dto.OrderItemDtos
{
    public class CreateOrderItemDTO : BaseDTO
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
