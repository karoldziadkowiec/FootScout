using FootScout.WebAPI.Entities;

namespace FootScout.WebAPI.Repositories.Interfaces
{
    public interface IPlayerAdvertisementFavoriteRepository
    {
        Task AddToFavorites(PlayerAdvertisementFavorite playerAdvertisementFavorite);
        Task DeleteFromFavorites(int playerAdvertisementFavoriteId);
        Task<int> CheckPlayerAdvertisementIsFavorite(int playerAdvertisementId, string userId);
    }
}