using FootScout.WebAPI.Models.DTOs;

namespace FootScout.WebAPI.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> Register(RegisterDTO registerDTO);
        Task<string> Login(LoginDTO loginDTO);
    }
}