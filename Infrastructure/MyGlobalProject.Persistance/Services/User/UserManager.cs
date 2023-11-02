using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.UserDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;
using MyGlobalProject.Application.ServiceInterfaces.UserServices;

namespace MyGlobalProject.Persistance.Services.User
{
    public class UserManager : IUserService
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public UserManager(IUserReadRepository userReadRepository, IMapper mapper, ICacheService cacheService)
        {
            _userReadRepository = userReadRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<List<UserListDTO>> GetAllUser()
        {
            var userListFromCache = await _cacheService.GetAsync<List<UserListDTO>>("users");

            if (userListFromCache is not null)
                return userListFromCache;

            var result = _mapper.Map<List<UserListDTO>>(await _userReadRepository.GetBy(u => u.IsActive && !u.IsDeleted).ToListAsync());

            return result;
        }

        public async Task<GetByIdUserDTO> GetByIdUser(Guid userId)
        {
            var result = _mapper.Map<GetByIdUserDTO>(await _userReadRepository.GetByIdAsync(userId));

            return result;
        }
    }
}
