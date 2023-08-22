using MyGlobalProject.Domain.Common;

namespace MyGlobalProject.Domain.Entities
{
    public class UserAddress : BaseEntity
    {
        public Guid UserId { get; set; }
        public string AddressTitle { get; set; }
        public string Address { get; set; }
        public User User { get; set; }
    }
}
