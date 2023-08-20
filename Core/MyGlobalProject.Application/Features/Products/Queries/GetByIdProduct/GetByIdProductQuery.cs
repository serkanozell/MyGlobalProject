using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.ProductDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
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
            public GetByIdProductQueryHandler(IProductReadRepository productReadRepository, IMapper mapper)
            {
                _productReadRepository = productReadRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<GetByIdProductDTO>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<GetByIdProductDTO>();

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

                return response;

            }
        }
    }
}
