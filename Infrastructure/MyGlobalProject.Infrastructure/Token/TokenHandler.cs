using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyGlobalProject.Application.ServiceInterfaces.JWT;
using MyGlobalProject.Application.ServiceInterfaces.Token;
using MyGlobalProject.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyGlobalProject.Infrastructure.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenOptions = configuration.GetSection("Token").Get<TokenOptions>()!;
        }

        public AccessToken CreateAccessToken(User user, Role role)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.EMail),
                new Claim(ClaimTypes.Role,role.Name),
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.MobilePhone,user.PhoneNumber)
            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwt = CreateJwtToken(_tokenOptions, signingCredentials, claims);

            JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
            var token = securityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        private JwtSecurityToken CreateJwtToken(TokenOptions tokenOptions, SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwt = new JwtSecurityToken(
                audience: _tokenOptions.Audience,
                issuer: _tokenOptions.Issuer,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials,
                claims: claims);

            return jwt;
        }
    }
}
