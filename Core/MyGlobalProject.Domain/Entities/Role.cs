using MyGlobalProject.Domain.Common;

namespace MyGlobalProject.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
