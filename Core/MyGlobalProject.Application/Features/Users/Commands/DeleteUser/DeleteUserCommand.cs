using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.UserDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;

namespace MyGlobalProject.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<GenericResponse<DeleteUserDTO>>
    {
        public Guid Id { get; set; }

        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, GenericResponse<DeleteUserDTO>>
        {
            private readonly IUserReadRepository _userReadRepository;
            private readonly IUserWriteRepository _userWriteRepository;
            private readonly IMapper _mapper;

            public DeleteUserCommandHandler(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, IMapper mapper)
            {
                _userReadRepository = userReadRepository;
                _userWriteRepository = userWriteRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<DeleteUserDTO>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<DeleteUserDTO>();

                var currentUser = await _userReadRepository.GetByIdAsync(request.Id);

                if (currentUser == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "There is no user to delete";
                }

                currentUser!.IsActive = false;
                currentUser.IsDeleted = true;

                await _userWriteRepository.UpdateAsync(currentUser);

                var mappedUser = _mapper.Map<DeleteUserDTO>(currentUser);

                response.Data = mappedUser;
                response.Message = "User deleted successfully";

                return response;
            }
        }
    }
}
