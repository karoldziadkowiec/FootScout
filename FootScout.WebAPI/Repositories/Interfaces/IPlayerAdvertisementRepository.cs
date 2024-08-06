using FootScout.WebAPI.Entities;

namespace FootScout.WebAPI.Repositories.Interfaces
{
    public interface IPlayerAdvertisementRepository
    {
        Task<ClubHistory> GetPlayerAdvertisement(int playerAdvertisementId);
        Task<IEnumerable<ClubHistory>> GetAllPlayerAdvertisements();
        Task<IEnumerable<ClubHistory>> GetUserPlayerAdvertisement(string userId);
        Task CreatePlayerAdvertisement(ClubHistory clubHistory);
        Task UpdatePlayerAdvertisement(ClubHistory clubHistory);
        Task DeletePlayerAdvertisement(int playerAdvertisementId);
    }
}