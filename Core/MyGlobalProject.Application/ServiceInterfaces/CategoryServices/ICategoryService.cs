using MyGlobalProject.Application.Dto.CategoryDtos;

namespace MyGlobalProject.Application.ServiceInterfaces.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<CategoryListDTO>> GetAllAsync();
        Task<GetByIdCategoryDTO> GetCategoryById(Guid id);
        Task LogAllCategories();
    }
}
