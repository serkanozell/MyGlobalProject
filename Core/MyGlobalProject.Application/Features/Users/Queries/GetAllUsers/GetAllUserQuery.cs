using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.UserDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;
using MyGlobalProject.Application.Wrappers;

namespace MyGlobalProject.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUserQuery : IRequest<GenericResponse<UserListDTO>>
    {
        public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, GenericResponse<UserListDTO>>
        {
            private readonly IUserReadRepository _userReadRepository;
            private readonly IMapper _mapper;
            private readonly ICacheService _cacheService;

            public GetAllUserQueryHandler(IUserReadRepository userReadRepository, IMapper mapper, ICacheService cacheService)
            {
                _userReadRepository = userReadRepository;
                _mapper = mapper;
                _cacheService = cacheService;
            }

            public async Task<GenericResponse<UserListDTO>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<UserListDTO>();

                var userListDTOs = await _cacheService.GetAsync<UserListDTO>("users");

                if (userListDTOs != null)
                {
                    response.Data = userListDTOs;
                    response.Message = "Success";

                    return response;
                }

                var userList = await _userReadRepository.GetBy(u => u.IsActive && !u.IsDeleted).ToListAsync();

                var mappedUserList = _mapper.Map<UserListDTO>(userList);

                response.Data = mappedUserList;
                response.Message = "Success";

                await _cacheService.SetAsync("users", mappedUserList);

                return response;
            }
        }
    }
}
