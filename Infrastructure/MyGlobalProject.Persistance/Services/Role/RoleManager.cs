using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.RoleDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;
using MyGlobalProject.Application.ServiceInterfaces.RolesServices;

namespace MyGlobalProject.Persistance.Services.Role
{
    public class RoleManager : IRoleService
    {
        private readonly IRoleReadRepository _roleReadRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public RoleManager(IRoleReadRepository roleReadRepository, IMapper mapper, ICacheService cacheService)
        {
            _roleReadRepository = roleReadRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<List<RoleListDTO>> GetAllRole()
        {
            var roleListFromCache = await _cacheService.GetAsync<List<RoleListDTO>>("roles");

            if (roleListFromCache is not null)
                return roleListFromCache;

            var roleList = _mapper.Map<List<RoleListDTO>>(await _roleReadRepository.GetAll(x => x.IsActive && !x.IsDeleted).ToListAsync());

            return roleList;
        }

        public async Task<GetByIdRoleDTO> GetByIdRole(Guid roleId)
        {
            var roleFromCache = await _cacheService.GetAsync<GetByIdRoleDTO>($"getbyidrole-{roleId}");

            if (roleFromCache is not null)
                return roleFromCache;

            var result = _mapper.Map<GetByIdRoleDTO>(await _roleReadRepository.GetByIdAsync(roleId));

            return result;
        }
    }
}
