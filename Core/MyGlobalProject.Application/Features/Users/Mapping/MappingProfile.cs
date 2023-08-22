using AutoMapper;
using MyGlobalProject.Application.Dto.UserDtos;
using MyGlobalProject.Application.Features.Categories.Commands.CreateCategory;
using MyGlobalProject.Application.Features.Users.Commands.CreateUser;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.Users.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, CreateUserCommand>().ReverseMap();
        }
    }
}
