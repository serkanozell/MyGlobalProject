using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.OrderItemDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;
using MyGlobalProject.Domain.Enums;
using Serilog;

namespace MyGlobalProject.Application.Features.OrderItems.Commands.CreateOrderItemWithoutRegister
{
    public class CreateOrderItemWithoutRegisterCommand : IRequest<GenericResponse<OrderItemSummaryDTO>>
    {
        public List<BasketDTO> OrderItems { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Address { get; set; }
        public string AddressTitle { get; set; }
        public string PhoneNumber { get; set; }

        public class CreateOrderItemWithoutRegisterCommandHandler : IRequestHandler<CreateOrderItemWithoutRegisterCommand, GenericResponse<OrderItemSummaryDTO>>
        {
            private readonly IOrderItemReadRepository _orderItemReadRepository;
            private readonly IOrderItemWriteRepository _orderItemWriteRepository;
            private readonly IOrderReadRepository _orderReadRepository;
            private readonly IOrderWriteRepository _orderWriteRepository;
            private readonly IProductReadRepository _productReadRepository;
            private readonly IMapper _mapper;

            public CreateOrderItemWithoutRegisterCommandHandler(IOrderItemReadRepository orderItemReadRepository, IOrderItemWriteRepository orderItemWriteRepository, IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository, IProductReadRepository productReadRepository, IMapper mapper)
            {
                _orderItemReadRepository = orderItemReadRepository;
                _orderItemWriteRepository = orderItemWriteRepository;
                _orderReadRepository = orderReadRepository;
                _orderWriteRepository = orderWriteRepository;
                _productReadRepository = productReadRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<OrderItemSummaryDTO>> Handle(CreateOrderItemWithoutRegisterCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<OrderItemSummaryDTO>();
                response.Data = new OrderItemSummaryDTO();
                var createdOrderItemList = new List<CreateOrderItemDTO>();

                var newOrder = await _orderWriteRepository.AddAsync(new Order
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    EMail = request.EMail,
                    Address = request!.Address,
                    AddressTitle = request.AddressTitle,
                    PhoneNumber = request.PhoneNumber,
                    OrderCreateDate = DateTime.Now,
                    OrderStatus = OrderStatusEnum.Pending
                });

                foreach (var item in request.OrderItems)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = newOrder.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = _productReadRepository.GetByIdAsync(item.ProductId).Result.Price,
                    };

                    createdOrderItemList.Add(_mapper.Map<CreateOrderItemDTO>(orderItem));
                    await _orderItemWriteRepository.AddAsync(orderItem);
                }

                createdOrderItemList.ForEach(item => { item.ProductName = _productReadRepository.GetByIdAsync(item.ProductId).Result.Name; });
                response.Data!.CreateOrderItems = createdOrderItemList;
                response.Data.FirstName = request.FirstName;
                response.Data.LastName = request.LastName;
                response.Data.EMail = request.EMail;
                response.Data.PhoneNumber = request.PhoneNumber;
                response.Data.Address = request.Address;
                response.Data.OrderCreateDate = newOrder.OrderCreateDate;
                response.Message = "Success";

                Log.Information($"Order created without registered user. Order Id = {newOrder.Id}");

                return response;
            }
        }
    }
}
