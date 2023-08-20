using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.ProductDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobalProject.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<GenericResponse<UpdateProductDTO>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, GenericResponse<UpdateProductDTO>>
        {
            private readonly IProductReadRepository _productReadRepository;
            private readonly IProductWriteRepository _productWriteRepository;
            private readonly IMapper _mapper;
            public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IMapper mapper)
            {
                _productReadRepository = productReadRepository;
                _productWriteRepository = productWriteRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<UpdateProductDTO>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<UpdateProductDTO>();

                var currentProduct = await _productReadRepository.GetByIdAsync(request.Id);

                if (currentProduct == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "There is no product to update";

                    return response;
                }

                currentProduct.Name = request.Name;
                currentProduct.Description = request.Description;
                currentProduct.Stock = request.Stock;
                currentProduct.Price = request.Price;
                currentProduct.CategoryId = request.CategoryId;

                await _productWriteRepository.UpdateAsync(currentProduct);

                var mappedProduct = _mapper.Map<UpdateProductDTO>(currentProduct);

                response.Data = mappedProduct;
                response.Message = "Product updated successfully";

                return response;
            }
        }
    }
}
