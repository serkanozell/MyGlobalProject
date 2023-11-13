using MyGlobalProject.Application.Dto.EmailDtos;
using MyGlobalProject.Application.Dto.UserDtos;

namespace MyGlobalProject.Application.ServiceInterfaces.UserServices
{
    public interface IUserService
    {
        Task<List<UserListDTO>> GetAllUser();
        Task<GetByIdUserDTO> GetByIdUser(Guid userId);
        Task SendMailToAllAdmins(CreateUserDTO createUserDTO);
    }
}
