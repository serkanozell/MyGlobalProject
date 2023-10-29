using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.RoleDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;
using Serilog;

namespace MyGlobalProject.Application.Features.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommand : IRequest<GenericResponse<UpdateRoleDTO>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, GenericResponse<UpdateRoleDTO>>
        {
            private readonly IRoleReadRepository _roleReadRepository;
            private readonly IRoleWriteRepository _roleWriteRepository;
            private readonly IMapper _mapper;

            public UpdateRoleCommandHandler(IRoleReadRepository roleReadRepository, IRoleWriteRepository roleWriteRepository, IMapper mapper)
            {
                _roleReadRepository = roleReadRepository;
                _roleWriteRepository = roleWriteRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<UpdateRoleDTO>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<UpdateRoleDTO>();

                var mappedRole = _mapper.Map<Role>(request);

                var currentRole = await _roleReadRepository.GetByIdAsync(mappedRole.Id);

                if (currentRole is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Role doesn't exist";

                    return response;
                }

                currentRole.Name = mappedRole.Name;

                var updatedRole = await _roleWriteRepository.UpdateAsync(currentRole, cancellationToken);

                var updatedRoleDto = _mapper.Map<UpdateRoleDTO>(updatedRole);

                response.Data = updatedRoleDto;
                response.Message = "Success";

                Log.Information($"Role Updated. \n" +
                    $"Old name = {currentRole.Name} - New name = {updatedRole.Name}");

                return response;
            }
        }
    }
}
