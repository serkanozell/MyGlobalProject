using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.OrderDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;
using MyGlobalProject.Domain.Enums;
using Serilog;

namespace MyGlobalProject.Application.Features.Orders.Commands.CreateOrderWithoutRegister
{
    public class CreateOrderWithoutRegisterCommand : IRequest<GenericResponse<CreateOrderWithoutRegisterDTO>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Address { get; set; }
        public string AddressTitle { get; set; }
        public string PhoneNumber { get; set; }

        public class CreateOrderWithoutRegisterCommandHandler : IRequestHandler<CreateOrderWithoutRegisterCommand, GenericResponse<CreateOrderWithoutRegisterDTO>>
        {
            private readonly IOrderWriteRepository _orderWriteRepository;
            private readonly IMapper _mapper;
            public CreateOrderWithoutRegisterCommandHandler(IOrderWriteRepository orderWriteRepository, IMapper mapper)
            {
                _orderWriteRepository = orderWriteRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<CreateOrderWithoutRegisterDTO>> Handle(CreateOrderWithoutRegisterCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<CreateOrderWithoutRegisterDTO>();

                var mappedOrder = _mapper.Map<Order>(request);

                mappedOrder.OrderCreateDate = DateTime.Now;
                mappedOrder.OrderStatus = OrderStatusEnum.Pending;

                var createdOrder = await _orderWriteRepository.AddAsync(mappedOrder, cancellationToken);

                var resultCreatedOrder = _mapper.Map<CreateOrderWithoutRegisterDTO>(createdOrder);

                response.Data = resultCreatedOrder;
                response.Message = "Your order has been received";

                Log.Information($"Order created withoud registered. OrderId = {createdOrder.Id}");

                return response;
            }
        }
    }
}
