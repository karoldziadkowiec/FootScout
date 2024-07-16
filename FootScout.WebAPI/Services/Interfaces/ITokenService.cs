using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FootScout.WebAPI.Services.Interfaces
{
    public interface ITokenService
    {
        JwtSecurityToken GetTokenJWT(IEnumerable<Claim> authClaims);
    }
}
