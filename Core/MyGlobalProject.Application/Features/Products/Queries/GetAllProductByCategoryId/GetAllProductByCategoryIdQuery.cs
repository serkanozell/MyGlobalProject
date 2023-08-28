using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.ProductDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
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

            public GetAllProductByCategoryIdQueryHandler(IProductReadRepository productReadRepository, IMapper mapper, ICategoryReadRepository categoryReadRepository)
            {
                _productReadRepository = productReadRepository;
                _categoryReadRepository = categoryReadRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<List<ProductListDTO>>> Handle(GetAllProductByCategoryIdQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<List<ProductListDTO>>();

                var existCategory = _categoryReadRepository.GetByIdAsync(request.Id);
                if (existCategory is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Wrong category id";

                    return response;
                }
                var products = await _productReadRepository.GetBy(x => x.CategoryId == request.Id && x.IsActive && !x.IsDeleted).Include(y => y.Category).ToListAsync();

                var mappedProductListDTO = _mapper.Map<List<ProductListDTO>>(products);

                response.Data = mappedProductListDTO;
                response.Message = "Success";

                return response;
            }
        }
    }
}
