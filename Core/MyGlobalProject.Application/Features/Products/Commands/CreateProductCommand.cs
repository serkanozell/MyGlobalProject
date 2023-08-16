using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.ProductDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobalProject.Application.Features.Products.Commands
{
    public class CreateProductCommand : IRequest<CreateProductDTO>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductDTO>
        {
            private readonly IProductReadRepository _productReadRepository;
            private readonly IProductWriteRepository _productWriteRepository;
            private readonly IMapper _mapper;
            public CreateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IMapper mapper)
            {
                _productReadRepository = productReadRepository;
                _productWriteRepository = productWriteRepository;
                _mapper = mapper;
            }

            public async Task<CreateProductDTO> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var mappedProduct = _mapper.Map<Product>(request);

                mappedProduct.IsActive = true;
                mappedProduct.IsDeleted = false;
                mappedProduct.CreatedDate = DateTime.Now;

                var createdProduct = await _productWriteRepository.AddAsync(mappedProduct);

                var resultProductDTO = _mapper.Map<CreateProductDTO>(createdProduct);

                return resultProductDTO;
            }
        }
    }
}
