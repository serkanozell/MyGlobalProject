using AutoMapper;
using MyGlobalProject.Application.Dto.RoleDtos;
using MyGlobalProject.Application.Features.Roles.Commands.CreateRole;
using MyGlobalProject.Application.Features.Roles.Commands.DeleteRole;
using MyGlobalProject.Application.Features.Roles.Commands.UpdateRole;
using MyGlobalProject.Application.Features.Roles.Queries.GetAllRole;
using MyGlobalProject.Application.Features.Roles.Queries.GetByIdRole;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.Roles.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Role, CreateRoleCommand>().ReverseMap();
            CreateMap<Role, CreateRoleDTO>().ReverseMap();

            CreateMap<Role, UpdateRoleCommand>().ReverseMap();
            CreateMap<Role, UpdateRoleDTO>().ReverseMap();

            CreateMap<Role, DeleteRoleCommand>().ReverseMap();
            CreateMap<Role, DeleteRoleDTO>().ReverseMap();

            CreateMap<Role, GetByIdRoleQuery>().ReverseMap();
            CreateMap<Role, GetByIdRoleDTO>().ReverseMap();

            CreateMap<Role, GetAllRoleQuery>().ReverseMap();
            CreateMap<Role, RoleListDTO>().ReverseMap();
        }
    }
}
