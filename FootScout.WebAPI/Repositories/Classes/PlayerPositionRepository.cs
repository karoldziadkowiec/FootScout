using FootScout.WebAPI.DbManager;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootScout.WebAPI.Repositories.Classes
{
    public class PlayerPositionRepository : IPlayerPositionRepository
    {
        private readonly AppDbContext _dbContext;

        public PlayerPositionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PlayerPosition>> GetPlayerPositions()
            => await _dbContext.PlayerPositions.ToListAsync();

        public async Task<int> GetPlayerPositionCount()
            => await _dbContext.PlayerPositions.CountAsync();

        public async Task<string> GetPlayerPositionName(int positionId)
            => await _dbContext.PlayerPositions.Where(p => p.Id == positionId).Select(p => p.PositionName).FirstOrDefaultAsync();
    }
}