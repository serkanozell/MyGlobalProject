using AutoMapper;
using MyGlobalProject.Application.Dto.OrderItemDtos;
using MyGlobalProject.Application.Features.OrderItems.Commands.CreateOrderItem;
using MyGlobalProject.Application.Features.OrderItems.Commands.DeleteOrderItem;
using MyGlobalProject.Application.Features.OrderItems.Commands.DeleteOrderItemByOrderId;
using MyGlobalProject.Application.Features.OrderItems.Commands.UpdateOrderItem;
using MyGlobalProject.Application.Features.OrderItems.Queries.GetAllOrderItem;
using MyGlobalProject.Application.Features.OrderItems.Queries.GetAllOrderItemByOrderId;
using MyGlobalProject.Application.Features.OrderItems.Queries.GetByIdOrderItem;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.OrderItems.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderItem, CreateOrderItemDTO>().ReverseMap();
            CreateMap<OrderItem, CreateOrderItemCommand>().ReverseMap();

            CreateMap<OrderItem, UpdateOrderItemDTO>().ReverseMap();
            CreateMap<OrderItem, UpdateOrderItemCommand>().ReverseMap();

            CreateMap<OrderItem, DeleteOrderItemDTO>().ReverseMap();
            CreateMap<OrderItem, DeleteOrderItemCommand>().ReverseMap();

            CreateMap<OrderItem, DeleteOrderItemByOrderIdDTO>().ReverseMap();
            CreateMap<OrderItem, DeleteOrderItemByOrderIdCommand>().ReverseMap();

            CreateMap<OrderItem, GetByIdOrderItemDTO>().ReverseMap();
            CreateMap<OrderItem, GetByIdOrderItemQuery>().ReverseMap();

            CreateMap<OrderItem, OrderItemListDTO>().ReverseMap();
            CreateMap<OrderItem, GetAllOrderItemQuery>().ReverseMap();

            CreateMap<OrderItem, GetAllOrderItemByOrderIdQuery>().ReverseMap();
        }
    }
}
