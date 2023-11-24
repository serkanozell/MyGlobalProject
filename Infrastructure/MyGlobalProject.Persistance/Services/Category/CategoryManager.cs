using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.CategoryDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;
using MyGlobalProject.Application.ServiceInterfaces.CategoryServices;

namespace MyGlobalProject.Persistance.Services.Category
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly ICategoryWriteRepository _categoryWriteRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public CategoryManager(ICategoryReadRepository categoryReadRepository, IMapper mapper, ICacheService cacheService, ICategoryWriteRepository categoryWriteRepository)
        {
            _categoryReadRepository = categoryReadRepository;
            _mapper = mapper;
            _cacheService = cacheService;
            _categoryWriteRepository = categoryWriteRepository;
        }

        public async Task<List<CategoryListDTO>> GetAllAsync()
        {
            var categoryListFromCache = await _cacheService.GetAsync<List<CategoryListDTO>>("categories");

            if (categoryListFromCache is not null)
                return categoryListFromCache;

            var result = _mapper.Map<List<CategoryListDTO>>(await _categoryReadRepository.GetQueryableAllActive().ToListAsync());

            return result;
        }

        public async Task<GetByIdCategoryDTO> GetCategoryById(Guid id)
        {
            var result = _mapper.Map<GetByIdCategoryDTO>(await _categoryReadRepository.GetByIdAsync(id));

            return result;
        }

        public async Task LogAllCategories()
        {
            var categoryList = await _categoryReadRepository.GetQueryableAllActive().ToListAsync();

            foreach (var category in categoryList)
            {
                Serilog.Log.Information($"Category Id : {category.Id}, \n" +
                    $"Category Name : {category.Name}, \n");
            }
        }
    }
}
