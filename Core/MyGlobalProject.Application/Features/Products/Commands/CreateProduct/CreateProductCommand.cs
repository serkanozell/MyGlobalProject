using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.ProductDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
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
        public double Price { get; set; }
        public Guid CategoryId { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, GenericResponse<CreateProductDTO>>
        {
            private readonly IProductReadRepository _productReadRepository;
            private readonly IProductWriteRepository _productWriteRepository;
            private readonly ICategoryReadRepository _categoryReadRepository;
            private readonly IMapper _mapper;
            public CreateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IMapper mapper, ICategoryReadRepository categoryReadRepository)
            {
                _productReadRepository = productReadRepository;
                _productWriteRepository = productWriteRepository;
                _mapper = mapper;
                _categoryReadRepository = categoryReadRepository;
            }

            public async Task<GenericResponse<CreateProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<CreateProductDTO>();

                var mappedProduct = _mapper.Map<Product>(request);

                mappedProduct.IsActive = true;
                mappedProduct.IsDeleted = false;
                mappedProduct.CreatedDate = DateTime.Now;

                var isCategoryExist = await _categoryReadRepository.GetByIdAsync(mappedProduct.CategoryId);

                if (isCategoryExist == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Category is not found in system. Make sure that is a correct category id";

                    return response;
                }

                var createdProduct = await _productWriteRepository.AddAsync(mappedProduct);

                var resultProductDTO = _mapper.Map<CreateProductDTO>(createdProduct);

                response.Data = resultProductDTO;
                response.Message = "Product added to system successfully";

                Log.Information("Product Added");

                return response;
            }
        }
    }
}
