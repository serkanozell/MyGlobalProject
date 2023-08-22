using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.ProductDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;

namespace MyGlobalProject.Application.Features.Products.Queries.GetAllProductByCategoryId
{
    public class GetAllProductByCategoryIdQuery : IRequest<List<ProductListDTO>>
    {
        public Guid Id { get; set; }

        public class GetAllProductByCategoryIdQueryHandler : IRequestHandler<GetAllProductByCategoryIdQuery, List<ProductListDTO>>
        {
            private readonly IProductReadRepository _productReadRepository;
            private readonly IMapper _mapper;

            public GetAllProductByCategoryIdQueryHandler(IProductReadRepository productReadRepository, IMapper mapper)
            {
                _productReadRepository = productReadRepository;
                _mapper = mapper;
            }

            public async Task<List<ProductListDTO>> Handle(GetAllProductByCategoryIdQuery request, CancellationToken cancellationToken)
            {
                var products = await _productReadRepository.GetBy(x => x.CategoryId == request.Id && x.IsActive && !x.IsDeleted).Include(y => y.Category).ToListAsync();

                var mappedProductListDTO = _mapper.Map<List<ProductListDTO>>(products);

                return mappedProductListDTO;
            }
        }
    }
}
