namespace MyGlobalProject.Application.Dto.OrderItemDtos
{
    public class BasketDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
