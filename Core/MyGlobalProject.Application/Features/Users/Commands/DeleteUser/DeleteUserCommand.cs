using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.UserDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;
using MyGlobalProject.Application.Wrappers;
using Serilog;

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
            private readonly ICacheService _cacheService;

            public DeleteUserCommandHandler(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, IMapper mapper, ICacheService cacheService)
            {
                _userReadRepository = userReadRepository;
                _userWriteRepository = userWriteRepository;
                _mapper = mapper;
                _cacheService = cacheService;
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

                await _userWriteRepository.DeleteAsync(currentUser);

                var mappedUser = _mapper.Map<DeleteUserDTO>(currentUser);

                response.Data = mappedUser;
                response.Message = "User deleted successfully";

                Log.Information($"User deleted. UserId = {mappedUser.Id}");

                await _cacheService.RemoveAllKeysAsync();

                return response;
            }
        }
    }
}
