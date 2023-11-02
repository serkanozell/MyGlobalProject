using MyGlobalProject.Application.Dto.RoleDtos;

namespace MyGlobalProject.Application.ServiceInterfaces.RolesServices
{
    public interface IRoleService
    {
        Task<List<RoleListDTO>> GetAllRole();
        Task<GetByIdRoleDTO> GetByIdRole(Guid roleId);
    }
}
