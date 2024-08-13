using FootScout.WebAPI.DbManager;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootScout.WebAPI.Repositories.Classes
{
    public class ClubAdvertisementFavoriteRepository : IClubAdvertisementFavoriteRepository
    {
        private readonly AppDbContext _dbContext;

        public ClubAdvertisementFavoriteRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddToFavorites(ClubAdvertisementFavorite clubAdvertisementFavorite)
        {
            await _dbContext.ClubAdvertisementFavorites.AddAsync(clubAdvertisementFavorite);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteFromFavorites(int clubAdvertisementFavoriteId)
        {
            var clubAdvertisementFavorite = await _dbContext.ClubAdvertisementFavorites.FindAsync(clubAdvertisementFavoriteId);
            if (clubAdvertisementFavorite == null)
                throw new ArgumentException($"No Favorite Club Advertisement found with ID {clubAdvertisementFavoriteId}");

            _dbContext.ClubAdvertisementFavorites.Remove(clubAdvertisementFavorite);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CheckClubAdvertisementIsFavorite(int clubAdvertisementId, string userId)
        {
            var isFavorite = await _dbContext.ClubAdvertisementFavorites
                .FirstOrDefaultAsync(pa => pa.ClubAdvertisementId == clubAdvertisementId && pa.UserId == userId);

            return isFavorite?.Id ?? 0;
        }
    }
}