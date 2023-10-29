using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.RoleDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;
using Serilog;

namespace MyGlobalProject.Application.Features.Roles.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<GenericResponse<CreateRoleDTO>>
    {
        public string Name { get; set; }

        public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, GenericResponse<CreateRoleDTO>>
        {
            private readonly IRoleWriteRepository _roleWriteRepository;
            private readonly IMapper _mapper;
            public CreateRoleCommandHandler(IRoleWriteRepository roleWriteRepository, IMapper mapper)
            {
                _roleWriteRepository = roleWriteRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<CreateRoleDTO>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<CreateRoleDTO>();

                var mappedRole = _mapper.Map<Role>(request);

                var addedRole = await _roleWriteRepository.AddAsync(mappedRole, cancellationToken);

                var addedRoleDto = _mapper.Map<CreateRoleDTO>(addedRole);

                response.Data = addedRoleDto;
                response.Message = "Success";

                Log.Information($"Role added. RoleId = {addedRole.Id}");

                return response;
            }
        }
    }
}
