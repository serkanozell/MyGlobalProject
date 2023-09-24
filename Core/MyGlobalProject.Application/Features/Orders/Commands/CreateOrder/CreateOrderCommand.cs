using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.OrderDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;
using MyGlobalProject.Domain.Enums;
using Serilog;

namespace MyGlobalProject.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<GenericResponse<CreateOrderDTO>>
    {
        public Guid? UserId { get; set; }
        public Guid AddressId { get; set; }

        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, GenericResponse<CreateOrderDTO>>
        {
            private readonly IOrderReadRepository _orderReadRepository;
            private readonly IOrderWriteRepository _orderWriteRepository;
            private readonly IUserReadRepository _userReadRepository;
            private readonly IUserAddressReadRepository _userAddressReadRepository;
            private readonly IMapper _mapper;

            public CreateOrderCommandHandler(IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository, IMapper mapper, IUserReadRepository userReadRepository, IUserAddressReadRepository userAddressReadRepository)
            {
                _orderReadRepository = orderReadRepository;
                _orderWriteRepository = orderWriteRepository;
                _userReadRepository = userReadRepository;
                _userAddressReadRepository = userAddressReadRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<CreateOrderDTO>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                // order item tarafında ise alınacakları list olarak alıp kayıttaki gibi dönerek tek tek order açılacak şekilde güncelleme yapılmalı.
                // kaydı dinleyerek düzenlemelerini tamamla
                var response = new GenericResponse<CreateOrderDTO>();

                var isUserExist = await _userReadRepository.GetByIdAsync((Guid)request.UserId!);
                var chosenAddress = await _userAddressReadRepository.GetByIdAsync(request.AddressId);

                if (isUserExist == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "There is no such as user";

                    return response;
                }

                if (chosenAddress == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Address error";

                    return response;
                }

                var mappedOrder = _mapper.Map<Order>(request);

                mappedOrder.UserId = request.UserId;
                mappedOrder.FirstName = isUserExist.FirstName;
                mappedOrder.LastName = isUserExist.LastName;
                mappedOrder.EMail = isUserExist.EMail;
                mappedOrder.Address = chosenAddress.Address;
                mappedOrder.AddressTitle = chosenAddress.AddressTitle;
                mappedOrder.PhoneNumber = isUserExist.PhoneNumber;
                mappedOrder.OrderCreateDate = DateTime.Now;
                mappedOrder.OrderStatus = OrderStatusEnum.Pending;

                var createdOrder = await _orderWriteRepository.AddAsync(mappedOrder);

                var resultOrderDto = _mapper.Map<CreateOrderDTO>(createdOrder);

                response.Data = resultOrderDto;
                response.Message = "Your order has been received";

                Log.Information($"Order created. Id = {createdOrder.Id}");

                return response;
            }
        }
    }
}
