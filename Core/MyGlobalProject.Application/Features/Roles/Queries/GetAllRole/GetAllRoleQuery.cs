using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.RoleDtos;
using MyGlobalProject.Application.RepositoryInterfaces;

namespace MyGlobalProject.Application.Features.Roles.Queries.GetAllRole
{
    public class GetAllRoleQuery : IRequest<List<RoleListDTO>>
    {
        public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, List<RoleListDTO>>
        {
            private readonly IRoleReadRepository _roleReadRepository;
            private readonly IMapper _mapper;

            public GetAllRoleQueryHandler(IRoleReadRepository roleReadRepository, IMapper mapper)
            {
                _roleReadRepository = roleReadRepository;
                _mapper = mapper;
            }

            public async Task<List<RoleListDTO>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
            {
                var currentRoleList = await _roleReadRepository.GetAll(x => x.IsActive && !x.IsDeleted).ToListAsync();

                var roleListDto = _mapper.Map<List<RoleListDTO>>(currentRoleList);

                return roleListDto;
            }
        }
    }
}