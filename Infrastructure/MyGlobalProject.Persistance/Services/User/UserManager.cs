using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.EmailDtos;
using MyGlobalProject.Application.Dto.UserDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;
using MyGlobalProject.Application.ServiceInterfaces.Notification;
using MyGlobalProject.Application.ServiceInterfaces.UserServices;

namespace MyGlobalProject.Persistance.Services.User
{
    public class UserManager : IUserService
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IEmailSender _emailSender;

        public UserManager(IUserReadRepository userReadRepository, IMapper mapper, ICacheService cacheService, IEmailSender emailSender)
        {
            _userReadRepository = userReadRepository;
            _mapper = mapper;
            _cacheService = cacheService;
            _emailSender = emailSender;
        }

        public async Task<List<UserListDTO>> GetAllUser()
        {
            var userListFromCache = await _cacheService.GetAsync<List<UserListDTO>>("users");

            if (userListFromCache is not null)
                return userListFromCache;

            var result = _mapper.Map<List<UserListDTO>>(await _userReadRepository.GetQueryableAllActive().ToListAsync());

            return result;
        }

        public async Task<GetByIdUserDTO> GetByIdUser(Guid userId)
        {
            var result = _mapper.Map<GetByIdUserDTO>(await _userReadRepository.GetByIdAsync(userId));

            return result;
        }

        public async Task SendMailToAllAdmins(CreateUserDTO createUserDTO)
        {
            var adminMails = await _userReadRepository.GetBy(x => x.RoleId == Guid.Parse("f5e60b49-62ab-4e76-cc5c-08dbc26c136e")
             )
             .Select(x => x.EMail)
             .ToListAsync();

            var mailDto = new EmailDTO
            {
                To = adminMails,
                Body = $"User added to system by admin at {DateTime.Now}",
                Subject = "User added"
            };

            await _emailSender.SendEmailAsync(mailDto);
        }
    }
}
