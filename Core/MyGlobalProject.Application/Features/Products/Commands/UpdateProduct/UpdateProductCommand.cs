﻿using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.ProductDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;
using MyGlobalProject.Application.Wrappers;
using Serilog;

namespace MyGlobalProject.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<GenericResponse<UpdateProductDTO>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, GenericResponse<UpdateProductDTO>>
        {
            private readonly IProductReadRepository _productReadRepository;
            private readonly IProductWriteRepository _productWriteRepository;
            private readonly IMapper _mapper;
            private readonly ICacheService _cacheService;
            public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IMapper mapper, ICacheService cacheService)
            {
                _productReadRepository = productReadRepository;
                _productWriteRepository = productWriteRepository;
                _mapper = mapper;
                _cacheService = cacheService;
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

                await _productWriteRepository.UpdateAsync(currentProduct, cancellationToken);

                var mappedProduct = _mapper.Map<UpdateProductDTO>(currentProduct);

                response.Data = mappedProduct;
                response.Message = "Product updated successfully";

                Log.Information($"Product updated. \n" +
                    $"Product Id = {currentProduct.Id} \n" +
                    $"Old name = {currentProduct.Name} - New name = {request.Name} \n" +
                    $"$Old description = {currentProduct.Description} - New description = {request.Description} \n" +
                    $"$Old stock = {currentProduct.Stock} - New stock = {request.Stock} \n" +
                    $"$Old price = {currentProduct.Price} - New price = {request.Price} \n" +
                    $"$Old categoryId = {currentProduct.CategoryId} - New categoryId = {request.CategoryId}"
                    );

                await _cacheService.RemoveAllKeysAsync();

                return response;
            }
        }
    }
}
