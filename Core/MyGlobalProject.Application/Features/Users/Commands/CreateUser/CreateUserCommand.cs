using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.UserDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand:IRequest<GenericResponse<CreateUserDTO>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GenericResponse<CreateUserDTO>>
        {
            private readonly IUserReadRepository _userReadRepository;
            private readonly IUserWriteRepository _userWriteRepository;
            private readonly IMapper _mapper;
            public CreateUserCommandHandler(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, IMapper mapper)
            {
                _userReadRepository = userReadRepository;
                _userWriteRepository = userWriteRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<CreateUserDTO>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<CreateUserDTO>();

                var mappedUser = _mapper.Map<User>(request);

                mappedUser.IsActive = true;
                mappedUser.IsDeleted = false;
                mappedUser.CreatedDate = DateTime.Now;

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
