using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.ProductDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;
using MyGlobalProject.Application.ServiceInterfaces.ProductServices;

namespace MyGlobalProject.Persistance.Services.Product
{
    public class ProductManager : IProductService
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public ProductManager(IProductReadRepository repository, IMapper mapper, ICacheService cacheService)
        {
            _productReadRepository = repository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<List<ProductListDTO>> GetAllProduct()
        {
            var productListFromCache = await _cacheService.GetAsync<List<ProductListDTO>>("products");

            if (productListFromCache is not null)
                return productListFromCache;

            var result = _mapper.Map<List<ProductListDTO>>(await _productReadRepository.GetBy(p => p.IsActive && !p.IsDeleted)
                                                                            .Include(p => p.Category)
                                                                            .ToListAsync());
            return result;
        }

        public async Task<List<ProductListDTO>> GetAllProductByCategoryId(Guid categoryId)
        {
            var productListFromCache = await _cacheService.GetAsync<List<ProductListDTO>>($"productsbycategoryid-{categoryId}");

            if (productListFromCache is not null)
                return productListFromCache;

            var result = _mapper.Map<List<ProductListDTO>>(await _productReadRepository.GetBy(x => x.CategoryId == categoryId
                                                                                  && x.IsActive
                                                                                  && !x.IsDeleted).Include(y => y.Category).ToListAsync());

            return result;
        }

        public async Task<GetByIdProductDTO> GetProductById(Guid productId)
        {
            var productFromCache = await _cacheService.GetAsync<GetByIdProductDTO>($"getbyidproduct-{productId}");

            if (productFromCache is not null)
                return productFromCache;

            var result = _mapper.Map<GetByIdProductDTO>(await _productReadRepository.GetByIdAsync(productId));

            return result;
        }
    }
}
