using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.ProductDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;

namespace MyGlobalProject.Application.Features.Products.Queries.GetAllProduct
{
    public class GetAllProductQuery : IRequest<List<ProductListDTO>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public string CategoryName { get; set; }

        public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, List<ProductListDTO>>
        {
            private readonly IProductReadRepository _productReadRepository;
            private readonly IProductWriteRepository _productWriteRepository;
            private readonly IMapper _mapper;
            private readonly ICacheService _cacheService;

            public GetAllProductQueryHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IMapper mapper, ICacheService cacheService)
            {
                _productReadRepository = productReadRepository;
                _productWriteRepository = productWriteRepository;
                _mapper = mapper;
                _cacheService = cacheService;
            }


            public async Task<List<ProductListDTO>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
            {
                List<ProductListDTO>? productListDTOs = await _cacheService.GetAsync<List<ProductListDTO>>("products");

                if (productListDTOs is not null)
                    return productListDTOs;

                var productList = await _productReadRepository.GetQueryableAllActive().Include(p => p.Category).ToListAsync();

                var productListDTO = _mapper.Map<List<ProductListDTO>>(productList);

                await _cacheService.SetAsync("products", productListDTO);

                return productListDTO;
            }
        }
    }
}
