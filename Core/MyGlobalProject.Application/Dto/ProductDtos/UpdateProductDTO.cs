using MyGlobalProject.Application.Dto.Common;

namespace MyGlobalProject.Application.Dto.ProductDtos
{
    public class UpdateProductDTO : BaseUpdateDTO
    {
        public string Description { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
