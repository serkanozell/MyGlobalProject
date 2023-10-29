using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.ProductDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;
using Serilog;

namespace MyGlobalProject.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<GenericResponse<CreateProductDTO>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, GenericResponse<CreateProductDTO>>
        {
            private readonly IProductReadRepository _productReadRepository;
            private readonly IProductWriteRepository _productWriteRepository;
            private readonly ICategoryReadRepository _categoryReadRepository;
            private readonly IMapper _mapper;
            private readonly ICacheService _cacheService;
            public CreateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IMapper mapper, ICategoryReadRepository categoryReadRepository, ICacheService cacheService)
            {
                _productReadRepository = productReadRepository;
                _productWriteRepository = productWriteRepository;
                _mapper = mapper;
                _categoryReadRepository = categoryReadRepository;
                _cacheService = cacheService;
            }

            public async Task<GenericResponse<CreateProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<CreateProductDTO>();

                var mappedProduct = _mapper.Map<Product>(request);

                var isCategoryExist = await _categoryReadRepository.GetByIdAsync(mappedProduct.CategoryId);

                if (isCategoryExist == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Category is not found in system. Make sure that is a correct category id";

                    return response;
                }

                var createdProduct = await _productWriteRepository.AddAsync(mappedProduct, cancellationToken);

                var resultProductDTO = _mapper.Map<CreateProductDTO>(createdProduct);

                response.Data = resultProductDTO;
                response.Message = "Product added to system successfully";

                Log.Information($"Product created. ProductId = {createdProduct.Id}");

                await _cacheService.RemoveAllKeysAsync();

                return response;
            }
        }
    }
}
