using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.ProductDtos;
using MyGlobalProject.Application.RepositoryInterfaces;

namespace MyGlobalProject.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<DeleteProductDTO>
    {
        public Guid Id { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeleteProductDTO>
        {
            private readonly IProductReadRepository _productReadRepository;
            private readonly IProductWriteRepository _productWriteRepository;
            private readonly IMapper _mapper;

            public DeleteProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IMapper mapper)
            {
                _productReadRepository = productReadRepository;
                _productWriteRepository = productWriteRepository;
                _mapper = mapper;
            }

            public async Task<DeleteProductDTO> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var currentProduct = await _productReadRepository.GetByIdAsync(request.Id);

                currentProduct.IsActive = false;
                currentProduct.IsDeleted = true;

                await _productWriteRepository.UpdateAsync(currentProduct);

                var deleteProductDTO = _mapper.Map<DeleteProductDTO>(currentProduct);

                return deleteProductDTO;
            }
        }
    }
}
