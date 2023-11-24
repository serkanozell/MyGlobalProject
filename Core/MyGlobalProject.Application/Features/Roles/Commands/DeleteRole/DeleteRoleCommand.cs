using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.RoleDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;
using Serilog;

namespace MyGlobalProject.Application.Features.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommand : IRequest<GenericResponse<DeleteRoleDTO>>
    {
        public Guid Id { get; set; }

        public class DeleteCommandHandler : IRequestHandler<DeleteRoleCommand, GenericResponse<DeleteRoleDTO>>
        {
            private readonly IRoleReadRepository _roleReadRepository;
            private readonly IRoleWriteRepository _roleWriteRepository;
            private readonly IMapper _mapper;

            public DeleteCommandHandler(IRoleReadRepository roleReadRepository, IRoleWriteRepository roleWriteRepository, IMapper mapper)
            {
                _roleReadRepository = roleReadRepository;
                _roleWriteRepository = roleWriteRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<DeleteRoleDTO>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<DeleteRoleDTO>();

                var mappedRole = _mapper.Map<Role>(request);

                var currentRole = await _roleReadRepository.GetBy(x => x.Id == mappedRole.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (currentRole is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "There is no role";

                    return response;
                }

                var deletedRole = await _roleWriteRepository.DeleteAsync(currentRole, cancellationToken);

                var deletedRoleDto = _mapper.Map<DeleteRoleDTO>(deletedRole);

                response.Data = deletedRoleDto;
                response.Message = "Success";

                Log.Information($"Role deleted. RoleId = {deletedRole.Id}");

                return response;
            }
        }
    }
}
