using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.ProductDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;
using MyGlobalProject.Application.Wrappers;
using Serilog;

namespace MyGlobalProject.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<GenericResponse<DeleteProductDTO>>
    {
        public Guid Id { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, GenericResponse<DeleteProductDTO>>
        {
            private readonly IProductReadRepository _productReadRepository;
            private readonly IProductWriteRepository _productWriteRepository;
            private readonly IMapper _mapper;
            private readonly ICacheService _cacheService;

            public DeleteProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IMapper mapper, ICacheService cacheService)
            {
                _productReadRepository = productReadRepository;
                _productWriteRepository = productWriteRepository;
                _mapper = mapper;
                _cacheService = cacheService;
            }

            public async Task<GenericResponse<DeleteProductDTO>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<DeleteProductDTO>();

                var currentProduct = await _productReadRepository.GetByIdAsync(request.Id);

                if (currentProduct is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Delete error";

                    return response;
                }

                await _productWriteRepository.DeleteAsync(currentProduct, cancellationToken);

                var deleteProductDTO = _mapper.Map<DeleteProductDTO>(currentProduct);

                response.Data = deleteProductDTO;
                response.Message = "Success";

                Log.Information($"Product deleted. ProductId = {deleteProductDTO.Id}");

                await _cacheService.RemoveAllKeysAsync();

                return response;
            }
        }
    }
}
