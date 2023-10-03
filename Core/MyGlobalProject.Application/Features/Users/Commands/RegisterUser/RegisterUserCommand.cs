using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.EmailDtos;
using MyGlobalProject.Application.Dto.UserDtos;
using MyGlobalProject.Application.Extensions;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Notification;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;
using Serilog;
using static System.Net.WebUtility;

namespace MyGlobalProject.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<GenericResponse<RegisterUserDTO>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }

        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, GenericResponse<RegisterUserDTO>>
        {
            private readonly IUserReadRepository _userReadRepository;
            private readonly IUserWriteRepository _userWriteRepository;
            private readonly IMapper _mapper;
            private readonly IRoleReadRepository _roleReadRepository;
            private readonly IEmailSender _emailSender;

            public RegisterUserCommandHandler(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, IMapper mapper, IRoleReadRepository roleReadRepository, IEmailSender emailSender)
            {
                _userReadRepository = userReadRepository;
                _userWriteRepository = userWriteRepository;
                _mapper = mapper;
                _roleReadRepository = roleReadRepository;
                _emailSender = emailSender;
            }

            public async Task<GenericResponse<RegisterUserDTO>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<RegisterUserDTO>();

                var mappedUser = _mapper.Map<User>(request);

                var currentUser = await _userReadRepository.GetBy(u => u.EMail == mappedUser.EMail).FirstOrDefaultAsync();

                var memberRole = await _roleReadRepository.GetBy(r => r.Name == "Member").FirstOrDefaultAsync();

                if (currentUser is not null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "This e-mail address already using";

                    return response;
                }

                var newUser = _mapper.Map<User>(mappedUser);
                newUser.Password = HtmlEncode(newUser.Password.ToSHA256Hash());
                newUser.RoleId = memberRole!.Id;

                var registeredUser = await _userWriteRepository.AddAsync(newUser);

                var mappedRegisteredUser = _mapper.Map<RegisterUserDTO>(registeredUser);

                await _emailSender.SendEmailAsync(new EmailDTO
                {
                    To = mappedRegisteredUser.EMail,
                    Subject = "New Registration",
                    Body = $"Congrats. Your account details: \n Email: {mappedRegisteredUser.EMail}, Password: {request.Password}"
                });

                Log.Information($"User registered. Email: {mappedRegisteredUser.EMail}");

                response.Data = mappedRegisteredUser;
                response.Success = true;
                response.Message = "Register success";

                return response;
            }
        }
    }
}
