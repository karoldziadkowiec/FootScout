using FootScout.WebAPI.DbManager;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootScout.WebAPI.Repositories.Classes
{
    public class ClubAdvertisementRepository : IClubAdvertisementRepository
    {
        private readonly AppDbContext _dbContext;

        public ClubAdvertisementRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ClubAdvertisement> GetClubAdvertisement(int clubAdvertisementId)
        {
            return await _dbContext.ClubAdvertisements
                .Include(ca => ca.PlayerPosition)
                .Include(ca => ca.SalaryRange)
                .Include(ca => ca.User)
                .FirstOrDefaultAsync(ca => ca.Id == clubAdvertisementId);
        }

        public async Task<IEnumerable<ClubAdvertisement>> GetAllClubAdvertisements()
        {
            return await _dbContext.ClubAdvertisements
                .Include(ca => ca.PlayerPosition)
                .Include(ca => ca.SalaryRange)
                .Include(ca => ca.User)
                .OrderByDescending(ca => ca.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClubAdvertisement>> GetActiveClubAdvertisements()
        {
            return await _dbContext.ClubAdvertisements
                .Include(ca => ca.PlayerPosition)
                .Include(ca => ca.SalaryRange)
                .Include(ca => ca.User)
                .Where(ca => ca.EndDate >= DateTime.Now)
                .OrderByDescending(ca => ca.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClubAdvertisement>> GetInactiveClubAdvertisements()
        {
            return await _dbContext.ClubAdvertisements
                .Include(ca => ca.PlayerPosition)
                .Include(ca => ca.SalaryRange)
                .Include(ca => ca.User)
                .Where(ca => ca.EndDate < DateTime.Now)
                .OrderByDescending(ca => ca.EndDate)
                .ToListAsync();
        }

        public async Task CreateClubAdvertisement(ClubAdvertisement clubAdvertisement)
        {
            clubAdvertisement.CreationDate = DateTime.Now;
            clubAdvertisement.EndDate = DateTime.Now.AddDays(30);

            await _dbContext.ClubAdvertisements.AddAsync(clubAdvertisement);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateClubAdvertisement(ClubAdvertisement clubAdvertisement)
        {
            _dbContext.ClubAdvertisements.Update(clubAdvertisement);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteClubAdvertisement(int clubAdvertisementId)
        {
            var clubAdvertisement = await _dbContext.ClubAdvertisements.FindAsync(clubAdvertisementId);
            if (clubAdvertisement == null)
                throw new ArgumentException($"No Club Advertisement found with ID {clubAdvertisementId}");

            if (clubAdvertisement.SalaryRangeId != null)
            {
                var salaryRange = await _dbContext.SalaryRanges.FindAsync(clubAdvertisement.SalaryRangeId);
                if (salaryRange != null)
                    _dbContext.SalaryRanges.Remove(salaryRange);
            }

            var favorites = await _dbContext.FavoriteClubAdvertisements
                .Where(ca => ca.ClubAdvertisementId == clubAdvertisementId)
                .ToListAsync();
            _dbContext.FavoriteClubAdvertisements.RemoveRange(favorites);

            var playerOffers = await _dbContext.PlayerOffers
                .Where(po => po.ClubAdvertisementId == clubAdvertisementId)
                .ToListAsync();
            _dbContext.PlayerOffers.RemoveRange(playerOffers);

            _dbContext.ClubAdvertisements.Remove(clubAdvertisement);
            await _dbContext.SaveChangesAsync();
        }
    }
}