using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.ProductDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.Products.Queries.GetByIdProduct
{
    public class GetByIdProductQuery : IRequest<GetByIdProductDTO>
    {
        public Guid Id { get; set; }

        public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, GetByIdProductDTO>
        {
            private readonly IProductReadRepository _productReadRepository;
            private readonly IMapper _mapper;
            public GetByIdProductQueryHandler(IProductReadRepository productReadRepository, IMapper mapper)
            {
                _productReadRepository = productReadRepository;
                _mapper = mapper;
            }

            public async Task<GetByIdProductDTO> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
            {
                var mappedProduct = _mapper.Map<Product>(request);
                var product = await _productReadRepository.GetBy(x => x.Id == mappedProduct.Id).Include(c => c.Category).FirstOrDefaultAsync();

                var getProductDTO = _mapper.Map<GetByIdProductDTO>(product);
                return getProductDTO;

            }
        }
    }
}
