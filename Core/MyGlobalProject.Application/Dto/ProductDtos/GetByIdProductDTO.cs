using MyGlobalProject.Application.Dto.Common;

namespace MyGlobalProject.Application.Dto.ProductDtos
{
    public class GetByIdProductDTO:BaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public string CategoryName { get; set; }
    }
}
