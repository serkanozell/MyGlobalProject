using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.OrderItemDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.OrderItems.Commands.UpdateOrderItem
{
    public class UpdateOrderItemCommand : IRequest<GenericResponse<UpdateOrderItemDTO>>
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, GenericResponse<UpdateOrderItemDTO>>
        {
            private readonly IOrderItemReadRepository _orderItemReadRepository;
            private readonly IOrderItemWriteRepository _orderItemWriteRepository;
            private readonly IOrderReadRepository _orderReadRepository;
            private readonly IProductReadRepository _productReadRepository;
            private readonly IMapper _mapper;

            public UpdateOrderItemCommandHandler(IOrderItemReadRepository orderItemReadRepository, IOrderItemWriteRepository orderItemWriteRepository, IOrderReadRepository orderReadRepository, IProductReadRepository productReadRepository, IMapper mapper)
            {
                _orderItemReadRepository = orderItemReadRepository;
                _orderItemWriteRepository = orderItemWriteRepository;
                _orderReadRepository = orderReadRepository;
                _productReadRepository = productReadRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<UpdateOrderItemDTO>> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<UpdateOrderItemDTO>();

                var mappedOrderItem = _mapper.Map<OrderItem>(request);

                var currentOrderItem = await _orderItemReadRepository.GetByIdAsync(mappedOrderItem.Id);

                var currentOrder = await _orderReadRepository.GetByIdAsync(request.OrderId);
                var currentProduct = await _productReadRepository.GetByIdAsync(request.ProductId);

                if (currentOrderItem is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Wrong id";

                    return response;
                }

                if (currentOrder is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Wrong Order id";

                    return response;
                }

                if (currentProduct is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Wrong Product id";

                    return response;
                }

                currentOrderItem.OrderId = request.OrderId;
                currentOrderItem.ProductId = request.ProductId;
                currentOrderItem.Quantity = request.Quantity;
                currentOrderItem.Price = request.Price;

                var updatedOrderItem = await _orderItemWriteRepository.UpdateAsync(currentOrderItem);

                var updatedOrderItemDto = _mapper.Map<UpdateOrderItemDTO>(updatedOrderItem);

                response.Data = updatedOrderItemDto;
                response.Message = "Success";

                return response;
            }
        }
    }
}
