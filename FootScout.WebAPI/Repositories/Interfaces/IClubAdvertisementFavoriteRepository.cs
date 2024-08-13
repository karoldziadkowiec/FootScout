using FootScout.WebAPI.Entities;

namespace FootScout.WebAPI.Repositories.Interfaces
{
    public interface IClubAdvertisementFavoriteRepository
    {
        Task AddToFavorites(ClubAdvertisementFavorite clubAdvertisementFavorite);
        Task DeleteFromFavorites(int clubAdvertisementFavoriteId);
        Task<int> CheckClubAdvertisementIsFavorite(int clubAdvertisementId, string userId);
    }
}