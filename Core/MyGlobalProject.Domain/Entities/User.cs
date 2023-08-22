using MyGlobalProject.Domain.Common;

namespace MyGlobalProject.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<UserAddress> Address { get; set; }
    }
}
