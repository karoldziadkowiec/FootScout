using FootScout.WebAPI.DbManager;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Repositories.Interfaces;

namespace FootScout.WebAPI.Repositories.Classes
{
    public class AchievementsRepository : IAchievementsRepository
    {
        private readonly AppDbContext _dbContext;

        public AchievementsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAchievements(Achievements achievements)
        {
            await _dbContext.Achievements.AddAsync(achievements);
            await _dbContext.SaveChangesAsync();
        }
    }
}