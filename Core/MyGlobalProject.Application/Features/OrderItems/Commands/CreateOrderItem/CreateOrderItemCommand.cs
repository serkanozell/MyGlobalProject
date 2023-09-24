using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.OrderItemDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;
using MyGlobalProject.Domain.Enums;
using Serilog;

namespace MyGlobalProject.Application.Features.OrderItems.Commands.CreateOrderItem
{
    public class CreateOrderItemCommand : IRequest<GenericResponse<OrderItemSummaryDTO>>
    {
        public List<BasketDTO> OrderItems { get; set; }
        public Guid UserId { get; set; }

        public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, GenericResponse<OrderItemSummaryDTO>>
        {
            private readonly IOrderItemReadRepository _orderItemReadRepository;
            private readonly IOrderItemWriteRepository _orderItemWriteRepository;
            private readonly IOrderReadRepository _orderReadRepository;
            private readonly IOrderWriteRepository _orderWriteRepository;
            private readonly IProductReadRepository _productReadRepository;
            private readonly IUserReadRepository _userReadRepository;
            private readonly IUserAddressReadRepository _userAddressReadRepository;
            private readonly IMapper _mapper;

            public CreateOrderItemCommandHandler(IOrderItemReadRepository orderItemReadRepository, IOrderItemWriteRepository orderItemWriteRepository, IOrderReadRepository orderReadRepository, IProductReadRepository productReadRepository, IMapper mapper, IOrderWriteRepository orderWriteRepository, IUserReadRepository userReadRepository, IUserAddressReadRepository userAddressReadRepository)
            {
                _orderItemReadRepository = orderItemReadRepository;
                _orderItemWriteRepository = orderItemWriteRepository;
                _orderReadRepository = orderReadRepository;
                _orderWriteRepository = orderWriteRepository;
                _productReadRepository = productReadRepository;
                _userReadRepository = userReadRepository;
                _userAddressReadRepository = userAddressReadRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<OrderItemSummaryDTO>> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<OrderItemSummaryDTO>();
                response.Data = new OrderItemSummaryDTO();
                var createdOrderItemList = new List<CreateOrderItemDTO>();

                var currentUser = await _userReadRepository.GetByIdAsync(request.UserId);

                if (currentUser is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "There is no user";

                    return response;
                }

                var currentUserAddress = await _userAddressReadRepository.GetBy(x => x.UserId == currentUser.Id).FirstOrDefaultAsync();//buraya daha sonra seçili adres özelliği de eklenmeli

                if (currentUserAddress is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Address error";
                }

                var newOrder = await _orderWriteRepository.AddAsync(new Order
                {
                    UserId = currentUser.Id,
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    EMail = currentUser.EMail,
                    Address = currentUserAddress!.Address,
                    AddressTitle = currentUserAddress.AddressTitle,
                    PhoneNumber = currentUser.PhoneNumber,
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
                response.Data.FirstName = currentUser.FirstName;
                response.Data.LastName = currentUser.LastName;
                response.Data.EMail = currentUser.EMail;
                response.Data.PhoneNumber = currentUser.PhoneNumber;
                response.Data.Address = currentUserAddress.Address;
                response.Data.OrderCreateDate = newOrder.OrderCreateDate;
                response.Message = "Success";

                Log.Information($"Order created. \n UserId = {currentUser.Id}. Order Id = {newOrder.Id}");

                return response;
            }
        }
    }
}