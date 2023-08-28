using AutoMapper;
using MyGlobalProject.Application.Dto.UserAddressDtos;
using MyGlobalProject.Application.Features.UserAddresses.Commands.CreateUserAddress;
using MyGlobalProject.Application.Features.UserAddresses.Commands.DeleteUserAddress;
using MyGlobalProject.Application.Features.UserAddresses.Commands.UpdateUserAddress;
using MyGlobalProject.Application.Features.UserAddresses.Queries.GetAllUserAddress;
using MyGlobalProject.Application.Features.UserAddresses.Queries.GetAllUserAddressByUserId;
using MyGlobalProject.Application.Features.UserAddresses.Queries.GetByIdUserAddress;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.UserAddresses.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserAddress, CreateUserAddresDTO>().ReverseMap();
            CreateMap<UserAddress, CreateUserAddressCommand>().ReverseMap();

            CreateMap<UserAddress, UpdateUserAddressDTO>().ReverseMap();
            CreateMap<UserAddress, UpdateUserAddressCommand>().ReverseMap();

            CreateMap<UserAddress, DeleteUserAddressDTO>().ReverseMap();
            CreateMap<UserAddress, DeleteUserAddressCommand>().ReverseMap();

            CreateMap<UserAddress, GetByIdUserAddressDTO>().ReverseMap();
            CreateMap<UserAddress, GetByIdUserAddressQuery>().ReverseMap();

            CreateMap<UserAddress, UserAddressListDTO>().ReverseMap();
            CreateMap<UserAddress, GetAllUserAddressQuery>().ReverseMap();

            CreateMap<UserAddress, GetAllUserAddressByUserIdQuery>().ReverseMap();
        }
    }
}
