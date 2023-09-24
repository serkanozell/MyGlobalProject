using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.OrderDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using Serilog;

namespace MyGlobalProject.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<GenericResponse<UpdateOrderDTO>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Address { get; set; }
        public string AddressTitle { get; set; }
        public string PhoneNumber { get; set; }

        public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, GenericResponse<UpdateOrderDTO>>
        {
            private readonly IOrderReadRepository _orderReadRepository;
            private readonly IOrderWriteRepository _orderWriteRepository;
            private readonly IMapper _mapper;
            public UpdateOrderCommandHandler(IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository, IMapper mapper)
            {
                _orderReadRepository = orderReadRepository;
                _orderWriteRepository = orderWriteRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<UpdateOrderDTO>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<UpdateOrderDTO>();

                var currentOrder = await _orderReadRepository.GetByIdAsync(request.Id);

                if (currentOrder is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Wrong id";

                    return response;
                }

                currentOrder.FirstName = request.FirstName;
                currentOrder.LastName = request.LastName;
                currentOrder.EMail = request.EMail;
                currentOrder.Address = request.Address;
                currentOrder.AddressTitle = request.AddressTitle;
                currentOrder.PhoneNumber = request.PhoneNumber;

                var updatedOrder = await _orderWriteRepository.UpdateAsync(currentOrder);

                var updatedOrderDto = _mapper.Map<UpdateOrderDTO>(updatedOrder);

                response.Data = updatedOrderDto;
                response.Message = "Order updated successfully";

                Log.Information($"Order updated. " +
                    $"OrderId = {updatedOrder.Id}" +
                    $"Old FirstName = {currentOrder.FirstName} - New FirstName = {updatedOrder.FirstName} \n" +
                    $"Old LastName = {currentOrder.LastName} - New LastName = {updatedOrder.LastName} \n" +
                    $"Old Email = {currentOrder.EMail} - New EMail = {updatedOrder.EMail} \n" +
                    $"Old Address = {currentOrder.Address} - New Address = {updatedOrder.Address} \n" +
                    $"Old AddressTitle = {currentOrder.AddressTitle} - New AddressTitle = {updatedOrder.AddressTitle} \n" +
                    $"Old PhoneNumber = {currentOrder.PhoneNumber} - New PhoneNumber = {updatedOrder.PhoneNumber}");

                return response;
            }
        }
    }
}
