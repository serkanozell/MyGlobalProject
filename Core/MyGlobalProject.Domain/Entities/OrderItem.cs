using MyGlobalProject.Domain.Common;

namespace MyGlobalProject.Domain.Entities
{
    public class OrderItem:BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
