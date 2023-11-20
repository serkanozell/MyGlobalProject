using Microsoft.Extensions.Configuration;
using MyGlobalProject.Application.Dto.RoleDtos;
using MyGlobalProject.Application.Dto.UserDtos;
using MyGlobalProject.Application.ServiceInterfaces.JWT;
using MyGlobalProject.Application.ServiceInterfaces.Token;
using Newtonsoft.Json;
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

        public async Task<AccessToken> CreateAccessTokenAsync(UserTokenDTO user, RoleTokenDTO role)
        {
            var token = new TokenModel();
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.EMail),
                new Claim(ClaimTypes.Role,role.Name),
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.MobilePhone,user.PhoneNumber)
            };
            var uri = "https://localhost:7087/api/Token";
            var client = new HttpClient() { BaseAddress = new Uri(uri) };

            token.TokenOptions = _tokenOptions;
            token.User = user;
            token.Role = role;

            var json = JsonConvert.SerializeObject(token);
            var content = new StringContent(json, encoding: Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("Token", content);

            var response = await responseMessage.Content.ReadAsStringAsync();

            var tokenResult = JsonConvert.DeserializeObject<AccessToken>(response);

            return new AccessToken
            {
                Token = tokenResult.Token,
                Expiration = _accessTokenExpiration
            };
        }
    }
}
