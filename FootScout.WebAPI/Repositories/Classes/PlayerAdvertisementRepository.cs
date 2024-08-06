using FootScout.WebAPI.DbManager;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootScout.WebAPI.Repositories.Classes
{
    public class PlayerAdvertisementRepository : IPlayerAdvertisementRepository
    {
        private readonly AppDbContext _dbContext;

        public PlayerAdvertisementRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ClubHistory> GetPlayerAdvertisement(int playerAdvertisementId)
        {
            return await _dbContext.ClubHistories
                .Include(ch => ch.Achievements)
                .Include(ch => ch.User)
                .FirstOrDefaultAsync(ch => ch.Id == playerAdvertisementId);
        }

        public async Task<IEnumerable<ClubHistory>> GetAllPlayerAdvertisements()
        {
            return await _dbContext.ClubHistories
                .Include(ch => ch.Achievements)
                .Include(ch => ch.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClubHistory>> GetUserPlayerAdvertisement(string userId)
        {
            return await _dbContext.ClubHistories
                .Include(ch => ch.Achievements)
                .Include(ch => ch.User)
                .Where(ch => ch.UserId == userId)
                .OrderByDescending(ch => ch.StartDate)
                .ThenByDescending(ch => ch.EndDate)
                .ToListAsync();
        }

        public async Task CreatePlayerAdvertisement(ClubHistory clubHistory)
        {
            await _dbContext.ClubHistories.AddAsync(clubHistory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePlayerAdvertisement(ClubHistory clubHistory)
        {
            _dbContext.ClubHistories.Update(clubHistory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePlayerAdvertisement(int playerAdvertisementId)
        {
            var clubHistory = await _dbContext.ClubHistories.FindAsync(playerAdvertisementId);
            if (clubHistory == null)
                throw new ArgumentException($"No club history found with ID {playerAdvertisementId}");

            if (clubHistory.AchievementsId != null)
            {
                var achievements = await _dbContext.Achievements.FindAsync(clubHistory.AchievementsId);
                if (achievements != null)
                    _dbContext.Achievements.Remove(achievements);
            }

            _dbContext.ClubHistories.Remove(clubHistory);
            await _dbContext.SaveChangesAsync();
        }
    }
}