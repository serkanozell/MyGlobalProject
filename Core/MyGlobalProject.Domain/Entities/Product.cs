using MyGlobalProject.Domain.Common;

namespace MyGlobalProject.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
