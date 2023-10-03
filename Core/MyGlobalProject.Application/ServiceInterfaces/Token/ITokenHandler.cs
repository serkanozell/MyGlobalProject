using MyGlobalProject.Application.ServiceInterfaces.Token;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.ServiceInterfaces.JWT
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(User user, Role role);
    }
}
