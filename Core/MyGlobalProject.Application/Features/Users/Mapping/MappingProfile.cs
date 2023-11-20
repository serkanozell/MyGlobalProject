using AutoMapper;
using MyGlobalProject.Application.Dto.UserDtos;
using MyGlobalProject.Application.Features.Users.Commands.CreateUser;
using MyGlobalProject.Application.Features.Users.Commands.DeleteUser;
using MyGlobalProject.Application.Features.Users.Commands.LoginUser;
using MyGlobalProject.Application.Features.Users.Commands.RegisterUser;
using MyGlobalProject.Application.Features.Users.Commands.UpdateUser;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.Users.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, CreateUserCommand>().ReverseMap();

            CreateMap<User, UpdateUserDTO>().ReverseMap();
            CreateMap<User, UpdateUserCommand>().ReverseMap();

            CreateMap<User, DeleteUserDTO>().ReverseMap();
            CreateMap<User, DeleteUserCommand>().ReverseMap();

            CreateMap<User, RegisterUserDTO>().ReverseMap();
            CreateMap<User, RegisterUserCommand>().ReverseMap();

            //CreateMap<User, LoginUserDTO>().ReverseMap();
            CreateMap<User, LoginUserCommand>().ReverseMap();

            CreateMap<User,UserTokenDTO>().ReverseMap();
        }
    }
}
