using FootScout.WebAPI.Models.DTOs;
using Microsoft.AspNetCore.Identity.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FootScout.WebAPI.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> Register(RegisterDTO request);
        Task<string> Login(LoginDTO request);
    }
}