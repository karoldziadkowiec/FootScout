using FootScout.WebAPI.DbManager;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootScout.WebAPI.Repositories.Classes
{
    public class ClubOfferRepository : IClubOfferRepository
    {
        private readonly AppDbContext _dbContext;

        public ClubOfferRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ClubOffer> GetClubOffer(int clubOfferId)
        {
            return await _dbContext.ClubOffers
                .Include(co => co.PlayerAdvertisement)
                .Include(pa => pa.PlayerAdvertisement.PlayerPosition)
                .Include(pa => pa.PlayerAdvertisement.PlayerFoot)
                .Include(pa => pa.PlayerAdvertisement.SalaryRange)
                .Include(pa => pa.PlayerAdvertisement.User)
                .Include(co => co.AdvertisementStatus)
                .Include(co => co.PlayerPosition)
                .Include(co => co.UserClub)
                .FirstOrDefaultAsync(co => co.Id == clubOfferId);
        }

        public async Task<IEnumerable<ClubOffer>> GetClubOffers()
        {
            return await _dbContext.ClubOffers
                .Include(co => co.PlayerAdvertisement)
                .Include(pa => pa.PlayerAdvertisement.PlayerPosition)
                .Include(pa => pa.PlayerAdvertisement.PlayerFoot)
                .Include(pa => pa.PlayerAdvertisement.SalaryRange)
                .Include(pa => pa.PlayerAdvertisement.User)
                .Include(co => co.AdvertisementStatus)
                .Include(co => co.PlayerPosition)
                .Include(co => co.UserClub)
                .OrderByDescending(co => co.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClubOffer>> GetActiveClubOffers()
        {
            return await _dbContext.ClubOffers
                .Include(co => co.PlayerAdvertisement)
                .Include(pa => pa.PlayerAdvertisement.PlayerPosition)
                .Include(pa => pa.PlayerAdvertisement.PlayerFoot)
                .Include(pa => pa.PlayerAdvertisement.SalaryRange)
                .Include(pa => pa.PlayerAdvertisement.User)
                .Include(co => co.AdvertisementStatus)
                .Include(co => co.PlayerPosition)
                .Include(co => co.UserClub)
                .Where(co => co.EndDate >= DateTime.Now)
                .OrderByDescending(co => co.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClubOffer>> GetInactiveClubOffers()
        {
            return await _dbContext.ClubOffers
                .Include(co => co.PlayerAdvertisement)
                .Include(pa => pa.PlayerAdvertisement.PlayerPosition)
                .Include(pa => pa.PlayerAdvertisement.PlayerFoot)
                .Include(pa => pa.PlayerAdvertisement.SalaryRange)
                .Include(pa => pa.PlayerAdvertisement.User)
                .Include(co => co.AdvertisementStatus)
                .Include(co => co.PlayerPosition)
                .Include(co => co.UserClub)
                .Where(co => co.EndDate < DateTime.Now)
                .OrderByDescending(co => co.EndDate)
                .ToListAsync();
        }

        public async Task CreateClubOffer(ClubOffer clubOffer)
        {
            clubOffer.CreationDate = DateTime.Now;

            var offeredStatus = await _dbContext.AdvertisementStatuses
                .FirstOrDefaultAsync(a => a.StatusName == "Offered");
            clubOffer.AdvertisementStatus = offeredStatus;

            await _dbContext.ClubOffers.AddAsync(clubOffer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateClubOffer(ClubOffer clubOffer)
        {
            _dbContext.ClubOffers.Update(clubOffer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CheckClubOfferIsSubmitted(int playerAdvertisementId, string userId)
        {
            var offeredStatusId = await _dbContext.AdvertisementStatuses
                .Where(a => a.StatusName == "Offered")
                .Select(a => a.Id)
                .FirstOrDefaultAsync();

            var clubOffer = await _dbContext.ClubOffers
                .Where(co => co.AdvertisementStatusId == offeredStatusId && co.PlayerAdvertisementId == playerAdvertisementId && co.UserClubId == userId)
                .FirstOrDefaultAsync();

            return clubOffer != null ? clubOffer.Id : 0;
        }
    }
}