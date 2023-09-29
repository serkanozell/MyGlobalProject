using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.ProductDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.Products.Queries.GetByIdProduct
{
    public class GetByIdProductQuery : IRequest<GenericResponse<GetByIdProductDTO>>
    {
        public Guid Id { get; set; }

        public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, GenericResponse<GetByIdProductDTO>>
        {
            private readonly IProductReadRepository _productReadRepository;
            private readonly IMapper _mapper;
            private readonly ICacheService _cacheService;
            public GetByIdProductQueryHandler(IProductReadRepository productReadRepository, IMapper mapper, ICacheService cacheService)
            {
                _productReadRepository = productReadRepository;
                _mapper = mapper;
                _cacheService = cacheService;
            }

            public async Task<GenericResponse<GetByIdProductDTO>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<GetByIdProductDTO>();

                var getByIdProductDTO = await _cacheService.GetAsync<GetByIdProductDTO>($"getbyidproduct-{request.Id}");
                if (getByIdProductDTO is not null)
                {
                    response.Data = getByIdProductDTO;
                    response.Message = "Success";
                }

                var mappedProduct = _mapper.Map<Product>(request);
                var product = await _productReadRepository.GetBy(x => x.Id == mappedProduct.Id).Include(c => c.Category).FirstOrDefaultAsync();

                if (product is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "There is no product";

                    return response;
                }

                var getProductDTO = _mapper.Map<GetByIdProductDTO>(product);

                response.Data = getProductDTO;
                response.Message = "Success";

                await _cacheService.SetAsync($"getbyidproduct-{request.Id}", getProductDTO);

                return response;

            }
        }
    }
}
