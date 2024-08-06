using FootScout.WebAPI.Entities;

namespace FootScout.WebAPI.Repositories.Interfaces
{
    public interface IPlayerAdvertisementRepository
    {
        Task<PlayerAdvertisement> GetPlayerAdvertisement(int playerAdvertisementId);
        Task<IEnumerable<PlayerAdvertisement>> GetPlayerAdvertisements();
        Task CreatePlayerAdvertisement(PlayerAdvertisement playerAdvertisement);
        Task UpdatePlayerAdvertisement(PlayerAdvertisement playerAdvertisement);
        Task DeletePlayerAdvertisement(int playerAdvertisementId);
    }
}