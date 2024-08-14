using FootScout.WebAPI.Entities;

namespace FootScout.WebAPI.Repositories.Interfaces
{
    public interface IFavoritePlayerAdvertisementRepository
    {
        Task AddToFavorites(FavoritePlayerAdvertisement favoritePlayerAdvertisement);
        Task DeleteFromFavorites(int favoritePlayerAdvertisementId);
        Task<int> CheckPlayerAdvertisementIsFavorite(int playerAdvertisementId, string userId);
    }
}