using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.OrderDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;
using MyGlobalProject.Domain.Enums;

namespace MyGlobalProject.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<GenericResponse<CreateOrderDTO>>
    {
        public Guid? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Address { get; set; }
        public string AddressTitle { get; set; }
        public string PhoneNumber { get; set; }

        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, GenericResponse<CreateOrderDTO>>
        {
            private readonly IOrderReadRepository _orderReadRepository;
            private readonly IOrderWriteRepository _orderWriteRepository;
            private readonly IUserReadRepository _userReadRepository;
            private readonly IMapper _mapper;

            public CreateOrderCommandHandler(IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository, IMapper mapper, IUserReadRepository userReadRepository)
            {
                _orderReadRepository = orderReadRepository;
                _orderWriteRepository = orderWriteRepository;
                _userReadRepository = userReadRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<CreateOrderDTO>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<CreateOrderDTO>();

                if (request.UserId != null)
                {
                    var isUserExist = await _userReadRepository.GetByIdAsync((Guid)request.UserId);
                    if (isUserExist == null)
                    {
                        response.Data = null;
                        response.Success = false;
                        response.Message = "There is no such as user";

                        return response;
                    }

                    request.FirstName = isUserExist.FirstName;
                    request.LastName= isUserExist.LastName;
                    request.EMail= isUserExist.EMail;
                    request.PhoneNumber= isUserExist.PhoneNumber;
                }

                var mappedOrder = _mapper.Map<Order>(request);

                mappedOrder.IsActive = true;
                mappedOrder.IsDeleted = false;
                mappedOrder.CreatedDate = DateTime.Now;
                mappedOrder.OrderCreateDate = DateTime.Now;
                mappedOrder.OrderStatus = OrderStatusEnum.Pending;

                var createdOrder = await _orderWriteRepository.AddAsync(mappedOrder);

                var resultOrderDto = _mapper.Map<CreateOrderDTO>(createdOrder);

                response.Data = resultOrderDto;
                response.Message = "Your order has been received";

                return response;
            }
        }
    }
}
