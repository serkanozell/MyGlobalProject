using MyGlobalProject.Domain.Common;

namespace MyGlobalProject.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public Guid RoleId { get; set; }

        public virtual ICollection<UserAddress> Address { get; set; }
        public virtual Role Role { get; set; }
    }
}
