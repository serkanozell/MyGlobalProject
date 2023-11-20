using MyGlobalProject.Application.Dto.RoleDtos;
using MyGlobalProject.Application.Dto.UserDtos;
using System.Security.Claims;

namespace MyGlobalProject.Infrastructure.Token
{
    public class TokenModel
    {
        public TokenOptions TokenOptions { get; set; }
        public UserTokenDTO User { get; set; }
        public RoleTokenDTO Role { get; set; }
    }
}
