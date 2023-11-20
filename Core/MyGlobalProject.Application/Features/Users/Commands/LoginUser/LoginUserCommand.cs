using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.RoleDtos;
using MyGlobalProject.Application.Dto.UserDtos;
using MyGlobalProject.Application.Extensions;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.JWT;
using MyGlobalProject.Application.ServiceInterfaces.Token;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<GenericResponse<AccessToken>>
    {
        public string EMail { get; set; }
        public string Password { get; set; }

        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, GenericResponse<AccessToken>>
        {
            private readonly IUserReadRepository _userReadRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHandler _tokenHandler;
            private readonly IRoleReadRepository _roleReadRepository;

            public LoginUserCommandHandler(IUserReadRepository userReadRepository, IMapper mapper, ITokenHandler tokenHandler, IRoleReadRepository roleReadRepository)
            {
                _userReadRepository = userReadRepository;
                _mapper = mapper;
                _tokenHandler = tokenHandler;
                _roleReadRepository = roleReadRepository;
            }

            public async Task<GenericResponse<AccessToken>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<AccessToken>();

                var mappedUser = _mapper.Map<User>(request);
                mappedUser.Password = mappedUser.Password.ToSHA256Hash();

                var isUserExist = await _userReadRepository.GetBy(u => u.EMail == mappedUser.EMail && u.IsActive && !u.IsDeleted).FirstOrDefaultAsync();



                if (isUserExist is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Wrong e-mail or password";

                    return response;
                }

                if (isUserExist.Password != mappedUser.Password)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Wrong e-mail or password";
                    return response;

                }

                var userRole = await _roleReadRepository.GetByIdAsync(isUserExist.RoleId);

                var userTokenDto = _mapper.Map<UserTokenDTO>(isUserExist);
                var roleTokenDto = _mapper.Map<RoleTokenDTO>(userRole);

                AccessToken token = await _tokenHandler.CreateAccessTokenAsync(userTokenDto, roleTokenDto);

                response.Data = token;
                response.Success = true;
                response.Message = "Login successfull";

                return response;
            }
        }
    }
}
