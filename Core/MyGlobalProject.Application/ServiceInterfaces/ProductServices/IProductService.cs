using MyGlobalProject.Application.Dto.ProductDtos;

namespace MyGlobalProject.Application.ServiceInterfaces.ProductServices
{
    public interface IProductService
    {
        Task<List<ProductListDTO>> GetAllProduct();
        Task<List<ProductListDTO>> GetAllProductByCategoryId(Guid categoryId);
        Task<GetByIdProductDTO> GetProductById(Guid productId);

    }
}
