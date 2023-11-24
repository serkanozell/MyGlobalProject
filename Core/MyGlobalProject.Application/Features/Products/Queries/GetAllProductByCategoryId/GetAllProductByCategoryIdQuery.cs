using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.ProductDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;
using MyGlobalProject.Application.Wrappers;

namespace MyGlobalProject.Application.Features.Products.Queries.GetAllProductByCategoryId
{
    public class GetAllProductByCategoryIdQuery : IRequest<GenericResponse<List<ProductListDTO>>>
    {
        public Guid Id { get; set; }

        public class GetAllProductByCategoryIdQueryHandler : IRequestHandler<GetAllProductByCategoryIdQuery, GenericResponse<List<ProductListDTO>>>
        {
            private readonly IProductReadRepository _productReadRepository;
            private readonly ICategoryReadRepository _categoryReadRepository;
            private readonly IMapper _mapper;
            private readonly ICacheService _cacheService;

            public GetAllProductByCategoryIdQueryHandler(IProductReadRepository productReadRepository, IMapper mapper, ICategoryReadRepository categoryReadRepository, ICacheService cacheService)
            {
                _productReadRepository = productReadRepository;
                _categoryReadRepository = categoryReadRepository;
                _mapper = mapper;
                _cacheService = cacheService;
            }

            public async Task<GenericResponse<List<ProductListDTO>>> Handle(GetAllProductByCategoryIdQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<List<ProductListDTO>>();
                List<ProductListDTO> productListDTOs = await _cacheService.GetAsync<List<ProductListDTO>>($"productsbycategoryid-{request.Id}");

                if (productListDTOs is not null)
                {
                    response.Data = productListDTOs;
                    response.Message = "Success";
                    return response;
                }

                var existCategory = _categoryReadRepository.GetByIdAsync(request.Id);

                if (existCategory is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Wrong category id";

                    return response;
                }

                var products = await _productReadRepository.GetBy(x => x.CategoryId == request.Id).Include(y => y.Category).ToListAsync();

                var mappedProductListDTO = _mapper.Map<List<ProductListDTO>>(products);

                response.Data = mappedProductListDTO;
                response.Message = "Success";

                await _cacheService.SetAsync($"productsbycategoryid-{request.Id}", mappedProductListDTO);

                return response;
            }
        }
    }
}
