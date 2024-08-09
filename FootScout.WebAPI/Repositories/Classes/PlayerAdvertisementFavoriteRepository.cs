using FootScout.WebAPI.DbManager;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootScout.WebAPI.Repositories.Classes
{
    public class PlayerAdvertisementFavoriteRepository : IPlayerAdvertisementFavoriteRepository
    {
        private readonly AppDbContext _dbContext;

        public PlayerAdvertisementFavoriteRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddToFavorites(PlayerAdvertisementFavorite playerAdvertisementFavorite)
        {
            await _dbContext.PlayerAdvertisementFavorites.AddAsync(playerAdvertisementFavorite);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteFromFavorites(int playerAdvertisementFavoriteId)
        {
            var playerAdvertisementFavorite = await _dbContext.PlayerAdvertisementFavorites.FindAsync(playerAdvertisementFavoriteId);
            if (playerAdvertisementFavorite == null)
                throw new ArgumentException($"No Favorite Player Advertisement found with ID {playerAdvertisementFavoriteId}");

            _dbContext.PlayerAdvertisementFavorites.Remove(playerAdvertisementFavorite);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CheckPlayerAdvertisementIsFavorite(int playerAdvertisementId, string userId)
        {
            var isFavorite = await _dbContext.PlayerAdvertisementFavorites
                .FirstOrDefaultAsync(pa => pa.PlayerAdvertisementId == playerAdvertisementId && pa.UserId == userId);

            return isFavorite?.Id ?? 0;
        }
    }
}