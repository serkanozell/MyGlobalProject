﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.UserDtos;
using MyGlobalProject.Application.Extensions;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;
using Serilog;
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
            private readonly ICacheService _cacheService;

            public CreateUserCommandHandler(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, IMapper mapper, IRoleReadRepository roleReadRepository, ICacheService cacheService)
            {
                _userReadRepository = userReadRepository;
                _userWriteRepository = userWriteRepository;
                _roleReadRepository = roleReadRepository;
                _mapper = mapper;
                _cacheService = cacheService;
            }

            public async Task<GenericResponse<CreateUserDTO>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<CreateUserDTO>();

                var mappedUser = _mapper.Map<User>(request);

                mappedUser.Password = HtmlEncode(request.Password.ToSHA256Hash());

                var existUser = await _userReadRepository.GetBy(u => u.EMail == mappedUser.EMail).FirstOrDefaultAsync();

                if (existUser != null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "This e-mail already exist";

                    return response;
                }

                var isRoleExist = await _roleReadRepository.GetByIdAsync(mappedUser.RoleId);

                if (isRoleExist is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Role error";

                    return response;
                }

                mappedUser.RoleId = isRoleExist.Id;

                var createdUser = await _userWriteRepository.AddAsync(mappedUser, cancellationToken);

                var resultUserDTO = _mapper.Map<CreateUserDTO>(createdUser);

                response.Data = resultUserDTO;
                response.Success = true;
                response.Message = "Successfull";

                Log.Information($"User created. UserId ={createdUser.Id}");

                await _cacheService.RemoveAllKeysAsync();

                return response;
            }
        }
    }
}
