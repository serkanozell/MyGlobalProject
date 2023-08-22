using MediatR;
using MyGlobalProject.Application.Dto.OrderDtos;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobalProject.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<CreateOrderDTO>
    {
        public Guid? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Address { get; set; }
        public string AddressTitle { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime OrderCreateDate { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }

        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderDTO>
        {
            public async Task<CreateOrderDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<CreateOrderDTO>();

                var mappedOrder = new CreateOrderDTO();

                return mappedOrder;
            }
        }
    }
}
