using MyGlobalProject.Application.Dto.Common;
using MyGlobalProject.Domain.Enums;

namespace MyGlobalProject.Application.Dto.OrderDtos
{
    public class GetByIdOrderDTO : BaseDTO
    {
        public Guid? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Address { get; set; }
        public string AddressTitle { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime OrderCreateDate { get; set; }
        public string OrderStatus { get; set; }
    }
}
