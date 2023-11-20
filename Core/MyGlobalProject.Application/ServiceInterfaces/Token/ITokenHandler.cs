using MyGlobalProject.Application.Dto.RoleDtos;
using MyGlobalProject.Application.Dto.UserDtos;
using MyGlobalProject.Application.ServiceInterfaces.Token;

namespace MyGlobalProject.Application.ServiceInterfaces.JWT
{
    public interface ITokenHandler
    {
        Task<AccessToken> CreateAccessTokenAsync(UserTokenDTO user, RoleTokenDTO role);
    }
}
