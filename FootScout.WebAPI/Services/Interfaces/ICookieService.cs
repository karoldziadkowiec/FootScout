using System.IdentityModel.Tokens.Jwt;

namespace FootScout.WebAPI.Services.Interfaces
{
    public interface ICookieService
    {
        Task SetCookies(JwtSecurityToken token, string tokenString);
    }
}