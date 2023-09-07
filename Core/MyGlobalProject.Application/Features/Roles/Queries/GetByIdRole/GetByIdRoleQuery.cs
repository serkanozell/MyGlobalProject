using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.RoleDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.Roles.Queries.GetByIdRole
{
    public class GetByIdRoleQuery : IRequest<GenericResponse<GetByIdRoleDTO>>
    {
        public Guid Id { get; set; }

        public class GetByIdRoleQueryHandler : IRequestHandler<GetByIdRoleQuery, GenericResponse<GetByIdRoleDTO>>
        {
            private readonly IRoleReadRepository _roleReadRepository;
            private readonly IMapper _mapper;

            public GetByIdRoleQueryHandler(IRoleReadRepository roleReadRepository, IMapper mapper)
            {
                _roleReadRepository = roleReadRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<GetByIdRoleDTO>> Handle(GetByIdRoleQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<GetByIdRoleDTO>();

                var mappedRole = _mapper.Map<Role>(request);

                var currentRole = await _roleReadRepository.GetByIdAsync(mappedRole.Id);

                if (currentRole is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Fail";

                    return response;
                }

                var getRoleDto = _mapper.Map<GetByIdRoleDTO>(currentRole);

                response.Data = getRoleDto;
                response.Message = "Success";

                return response;
            }
        }
    }
}
