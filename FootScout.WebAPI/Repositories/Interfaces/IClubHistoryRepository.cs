using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Models.DTOs;

namespace FootScout.WebAPI.Repositories.Interfaces
{
    public interface IClubHistoryRepository
    {
        Task<ClubHistory> GetClubHistory(int clubHistoryId);
        Task<IEnumerable<ClubHistory>> GetAllClubHistory();
        Task CreateClubHistory(ClubHistory clubHistory);
        Task UpdateClubHistory(ClubHistory clubHistory);
        Task DeleteClubHistory(int clubHistoryId);
    }
}