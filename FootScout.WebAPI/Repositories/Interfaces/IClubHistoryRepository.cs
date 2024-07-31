using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Models.DTOs;

namespace FootScout.WebAPI.Repositories.Interfaces
{
    public interface IClubHistoryRepository
    {
        Task<ClubHistory> GetClubHistory(int clubHistoryId);
        Task<IEnumerable<ClubHistory>> GetAllClubHistory();
        Task<ClubHistory> GetUserClubHistory(string userId, int clubHistoryId);
        Task<IEnumerable<ClubHistory>> GetUserAllClubHistory(string userId);
        Task CreateClubHistory(ClubHistory clubHistory);
        Task UpdateClubHistory(ClubHistory clubHistory);
        Task DeleteClubHistory(int clubHistoryId);
    }
}