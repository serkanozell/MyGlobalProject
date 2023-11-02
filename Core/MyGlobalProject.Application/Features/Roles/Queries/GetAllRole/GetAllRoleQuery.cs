using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.RoleDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;

namespace MyGlobalProject.Application.Features.Roles.Queries.GetAllRole
{
    public class GetAllRoleQuery : IRequest<List<RoleListDTO>>
    {
        public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, List<RoleListDTO>>
        {
            private readonly IRoleReadRepository _roleReadRepository;
            private readonly IMapper _mapper;
            private readonly ICacheService _cacheService;

            public GetAllRoleQueryHandler(IRoleReadRepository roleReadRepository, IMapper mapper, ICacheService cacheService)
            {
                _roleReadRepository = roleReadRepository;
                _mapper = mapper;
                _cacheService = cacheService;
            }

            public async Task<List<RoleListDTO>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
            {
                List<RoleListDTO> roleListDto = await _cacheService.GetAsync<List<RoleListDTO>>("roles");

                if (roleListDto is not null)
                    return roleListDto;

                var currentRoleList = await _roleReadRepository.GetAll(x => x.IsActive && !x.IsDeleted).ToListAsync();

                roleListDto = _mapper.Map<List<RoleListDTO>>(currentRoleList);

                await _cacheService.SetAsync("roles", roleListDto);

                return roleListDto;
            }
        }
    }
}