using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.UserDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;
using Serilog;

namespace MyGlobalProject.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<GenericResponse<UpdateUserDTO>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, GenericResponse<UpdateUserDTO>>
        {
            private readonly IUserReadRepository _userReadRepository;
            private readonly IUserWriteRepository _userWriteRepository;
            private readonly IMapper _mapper;
            private readonly ICacheService _cacheService;

            public UpdateUserCommandHandler(IUserReadRepository userReadRepository, IMapper mapper, IUserWriteRepository userWriteRepository, ICacheService cacheService)
            {
                _userReadRepository = userReadRepository;
                _userWriteRepository = userWriteRepository;
                _mapper = mapper;
                _cacheService = cacheService;
            }

            public async Task<GenericResponse<UpdateUserDTO>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<UpdateUserDTO>();

                var mappedUser = _mapper.Map<User>(request);

                var currentUser = await _userReadRepository.GetByIdAsync(mappedUser.Id);

                if (currentUser == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "User can't found";

                    return response;
                }

                var isUserNameExist = await _userReadRepository.GetBy(u => u.UserName == request.UserName).ToListAsync();
                if (isUserNameExist.Any())
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Username already exist";

                    return response;
                }

                currentUser.FirstName = mappedUser.FirstName;
                currentUser.LastName = mappedUser.LastName;
                currentUser.UserName = mappedUser.UserName;
                currentUser.EMail = mappedUser.EMail;
                currentUser.PhoneNumber = mappedUser.PhoneNumber;

                await _userWriteRepository.UpdateAsync(currentUser, cancellationToken);

                var userResult = _mapper.Map<UpdateUserDTO>(currentUser);

                response.Data = userResult;
                response.Success = true;
                response.Message = "User updated successfully";

                Log.Information($"User updated. \n" +
                    $"Old FirstName = {currentUser.FirstName} - New FirstName = {userResult.FirstName} \n" +
                    $"Old LastName = {currentUser.LastName} - New LastName = {userResult.LastName} \n" +
                    $"Old Email = {currentUser.EMail} - New Email = {userResult.EMail} \n" +
                    $"Old PhoneNumber = {currentUser.PhoneNumber} - Old PhoneNumber = {userResult.PhoneNumber}");

                await _cacheService.RemoveAllKeysAsync();

                return response;
            }
        }
    }
}
