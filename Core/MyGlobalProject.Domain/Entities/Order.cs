using MyGlobalProject.Domain.Common;
using MyGlobalProject.Domain.Enums;

namespace MyGlobalProject.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Address { get; set; }
        public string AddressTitle { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime OrderCreateDate { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
    }
}
