using AutoMapper;
using MyGlobalProject.Application.Dto.OrderDtos;
using MyGlobalProject.Application.Features.Orders.Commands.CreateOrder;
using MyGlobalProject.Application.Features.Orders.Commands.CreateOrderWithoutRegister;
using MyGlobalProject.Application.Features.Orders.Commands.DeleteOrder;
using MyGlobalProject.Application.Features.Orders.Commands.UpdateOrder;
using MyGlobalProject.Application.Features.Orders.Queries.GetAllOrder;
using MyGlobalProject.Application.Features.Orders.Queries.GetAllOrderByUserId;
using MyGlobalProject.Application.Features.Orders.Queries.GetByIdOrder;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.Orders.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, CreateOrderDTO>().ReverseMap();
            CreateMap<Order, CreateOrderCommand>().ReverseMap();

            CreateMap<Order, CreateOrderWithoutRegisterDTO>().ReverseMap();
            CreateMap<Order, CreateOrderWithoutRegisterCommand>().ReverseMap();

            CreateMap<Order, UpdateOrderDTO>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();

            CreateMap<Order, DeleteOrderDTO>().ReverseMap();
            CreateMap<Order, DeleteOrderCommand>().ReverseMap();

            CreateMap<Order, GetByIdOrderDTO>().ReverseMap();
            CreateMap<Order, GetByIdOrderQuery>().ReverseMap();

            CreateMap<Order, OrderListDTO>().ReverseMap();
            CreateMap<Order, GetAllOrderQuery>().ReverseMap();

            CreateMap<Order, GetAllOrderByUserIdQuery>().ReverseMap();
        }
    }
}
