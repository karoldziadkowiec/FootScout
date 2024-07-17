using FootScout.WebAPI.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace FootScout.WebAPI.Services.Interfaces
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateTokenJWT(User user);
    }
}