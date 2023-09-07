using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.UserDtos;
using MyGlobalProject.Application.Extensions;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;
using static System.Net.WebUtility;

namespace MyGlobalProject.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<GenericResponse<CreateUserDTO>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber { get; set; }
        public Guid RoleId { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GenericResponse<CreateUserDTO>>
        {
            private readonly IUserReadRepository _userReadRepository;
            private readonly IUserWriteRepository _userWriteRepository;
            private readonly IRoleReadRepository _roleReadRepository;
            private readonly IMapper _mapper;
            public CreateUserCommandHandler(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, IMapper mapper, IRoleReadRepository roleReadRepository)
            {
                _userReadRepository = userReadRepository;
                _userWriteRepository = userWriteRepository;
                _roleReadRepository = roleReadRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<CreateUserDTO>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<CreateUserDTO>();

                var mappedUser = _mapper.Map<User>(request);

                mappedUser.Password = HtmlEncode(request.Password.ToSHA256Hash());

                var isRoleExist = await _roleReadRepository.GetByIdAsync(mappedUser.RoleId);

                if (isRoleExist is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Role error";

                    return response;
                }

                mappedUser.RoleId = isRoleExist.Id;

                var createdUser = await _userWriteRepository.AddAsync(mappedUser);

                var resultUserDTO = _mapper.Map<CreateUserDTO>(createdUser);

                response.Data = resultUserDTO;
                response.Success = true;
                response.Message = "Successfull";

                return response;
            }
        }
    }
}
