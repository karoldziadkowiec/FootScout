using FootScout.WebAPI.Models.DTOs;

namespace FootScout.WebAPI.Services.Interfaces
{
    public interface IAccountService
    {
        Task Register(RegisterDTO registerDTO);
        Task<string> Login(LoginDTO loginDTO);
        Task<IEnumerable<string>> GetRoles();
        Task MakeAnAdmin(string userId);
    }
}