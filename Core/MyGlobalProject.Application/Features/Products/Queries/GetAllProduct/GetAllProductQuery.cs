using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.ProductDtos;
using MyGlobalProject.Application.RepositoryInterfaces;

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

            public GetAllProductQueryHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IMapper mapper)
            {
                _productReadRepository = productReadRepository;
                _productWriteRepository = productWriteRepository;
                _mapper = mapper;
            }


            public async Task<List<ProductListDTO>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
            {
                var productList = _productReadRepository.GetBy(x => x.IsActive && !x.IsDeleted).Include(p => p.Category).ToList();

                var productListDTO = _mapper.Map<List<ProductListDTO>>(productList);

                return productListDTO;
            }
        }
    }
}
