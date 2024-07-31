using FootScout.WebAPI.DbManager;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootScout.WebAPI.Repositories.Classes
{
    public class ClubHistoryRepository : IClubHistoryRepository
    {
        private readonly AppDbContext _dbContext;

        public ClubHistoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ClubHistory> GetClubHistory(int clubHistoryId)
        {
            return await _dbContext.ClubHistories
                .Include(ch => ch.Achievements)
                .Include(ch => ch.User)
                .FirstOrDefaultAsync(ch => ch.Id == clubHistoryId);
        }

        public async Task<IEnumerable<ClubHistory>> GetAllClubHistory()
        {
            return await _dbContext.ClubHistories
                .Include(ch => ch.Achievements)
                .Include(ch => ch.User)
                .ToListAsync();
        }

        public async Task<ClubHistory> GetUserClubHistory(string userId, int clubHistoryId)
        {
            return await _dbContext.ClubHistories
                .Include(ch => ch.Achievements)
                .Include(ch => ch.User)
                .FirstOrDefaultAsync(ch => ch.UserId == userId && ch.Id == clubHistoryId);
        }

        public async Task<IEnumerable<ClubHistory>> GetUserAllClubHistory(string userId)
        {
            return await _dbContext.ClubHistories
                .Include(ch => ch.Achievements)
                .Include(ch => ch.User)
                .Where(ch => ch.UserId == userId)
            .ToListAsync();
        }

        public async Task CreateClubHistory(ClubHistory clubHistory)
        {
            await _dbContext.ClubHistories.AddAsync(clubHistory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateClubHistory(ClubHistory clubHistory)
        {
            _dbContext.ClubHistories.Update(clubHistory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteClubHistory(int clubHistoryId)
        {
            var clubHistory = await _dbContext.ClubHistories.FindAsync(clubHistoryId);
            if (clubHistory != null)
            {
                _dbContext.ClubHistories.Remove(clubHistory);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
