using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Models.DTOs;

namespace FootScout.WebAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDTO> GetUser(string userId);
        Task<IEnumerable<UserDTO>> GetUsers();
        Task UpdateUser(UserUpdateDTO userUpdateDTO);
        Task DeleteUser(string userId);
    }
}
