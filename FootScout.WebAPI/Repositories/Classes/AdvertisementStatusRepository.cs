using FootScout.WebAPI.DbManager;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootScout.WebAPI.Repositories.Classes
{
    public class AdvertisementStatusRepository : IAdvertisementStatusRepository
    {
        private readonly AppDbContext _dbContext;

        public AdvertisementStatusRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<AdvertisementStatus>> GetAdvertisementStatuses()
            => await _dbContext.AdvertisementStatuses.ToListAsync();

        public async Task<string> GetAdvertisementStatusName(int statusId)
            => await _dbContext.AdvertisementStatuses.Where(a => a.Id == statusId).Select(a => a.StatusName).FirstOrDefaultAsync();

        public async Task<int> GetAdvertisementStatusId(string statusName)
            => await _dbContext.AdvertisementStatuses.Where(a => a.StatusName == statusName).Select(a => a.Id).FirstOrDefaultAsync();
    }
}