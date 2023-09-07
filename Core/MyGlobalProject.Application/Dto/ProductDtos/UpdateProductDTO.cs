using MyGlobalProject.Application.Dto.Common;

namespace MyGlobalProject.Application.Dto.ProductDtos
{
    public class UpdateProductDTO : BaseUpdateDTO
    {
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
