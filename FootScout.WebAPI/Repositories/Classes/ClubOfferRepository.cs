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
                .Include(pa => pa.PlayerAdvertisement.Player)
                .Include(co => co.OfferStatus)
                .Include(co => co.PlayerPosition)
                .Include(co => co.ClubMember)
                .FirstOrDefaultAsync(co => co.Id == clubOfferId);
        }

        public async Task<IEnumerable<ClubOffer>> GetClubOffers()
        {
            return await _dbContext.ClubOffers
                .Include(co => co.PlayerAdvertisement)
                .Include(pa => pa.PlayerAdvertisement.PlayerPosition)
                .Include(pa => pa.PlayerAdvertisement.PlayerFoot)
                .Include(pa => pa.PlayerAdvertisement.SalaryRange)
                .Include(pa => pa.PlayerAdvertisement.Player)
                .Include(co => co.OfferStatus)
                .Include(co => co.PlayerPosition)
                .Include(co => co.ClubMember)
                .OrderByDescending(co => co.CreationDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClubOffer>> GetActiveClubOffers()
        {
            return await _dbContext.ClubOffers
                .Include(co => co.PlayerAdvertisement)
                .Include(pa => pa.PlayerAdvertisement.PlayerPosition)
                .Include(pa => pa.PlayerAdvertisement.PlayerFoot)
                .Include(pa => pa.PlayerAdvertisement.SalaryRange)
                .Include(pa => pa.PlayerAdvertisement.Player)
                .Include(co => co.OfferStatus)
                .Include(co => co.PlayerPosition)
                .Include(co => co.ClubMember)
                .Where(co => co.PlayerAdvertisement.EndDate >= DateTime.Now)
                .OrderByDescending(co => co.CreationDate)
                .ToListAsync();
        }

        public async Task<int> GetActiveClubOfferCount()
        {
            return await _dbContext.ClubOffers
                .Include(co => co.PlayerAdvertisement)
                .Include(pa => pa.PlayerAdvertisement.PlayerPosition)
                .Include(pa => pa.PlayerAdvertisement.PlayerFoot)
                .Include(pa => pa.PlayerAdvertisement.SalaryRange)
                .Include(pa => pa.PlayerAdvertisement.Player)
                .Include(co => co.OfferStatus)
                .Include(co => co.PlayerPosition)
                .Include(co => co.ClubMember)
                .Where(co => co.PlayerAdvertisement.EndDate >= DateTime.Now)
                .CountAsync();
        }

        public async Task<IEnumerable<ClubOffer>> GetInactiveClubOffers()
        {
            return await _dbContext.ClubOffers
                .Include(co => co.PlayerAdvertisement)
                .Include(pa => pa.PlayerAdvertisement.PlayerPosition)
                .Include(pa => pa.PlayerAdvertisement.PlayerFoot)
                .Include(pa => pa.PlayerAdvertisement.SalaryRange)
                .Include(pa => pa.PlayerAdvertisement.Player)
                .Include(co => co.OfferStatus)
                .Include(co => co.PlayerPosition)
                .Include(co => co.ClubMember)
                .Where(co => co.PlayerAdvertisement.EndDate < DateTime.Now)
                .OrderByDescending(co => co.CreationDate)
                .ToListAsync();
        }

        public async Task CreateClubOffer(ClubOffer clubOffer)
        {
            clubOffer.CreationDate = DateTime.Now;

            var offeredStatus = await _dbContext.OfferStatuses
                .FirstOrDefaultAsync(a => a.StatusName == "Offered");
            clubOffer.OfferStatusId = offeredStatus.Id;
            clubOffer.OfferStatus = offeredStatus;

            await _dbContext.ClubOffers.AddAsync(clubOffer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateClubOffer(ClubOffer clubOffer)
        {
            _dbContext.ClubOffers.Update(clubOffer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteClubOffer(int clubOfferId)
        {
            var clubOffer = await _dbContext.ClubOffers.FindAsync(clubOfferId);
            if (clubOffer == null)
            {
                throw new Exception("Club offer not found");
            }

            _dbContext.ClubOffers.Remove(clubOffer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AcceptClubOffer(ClubOffer clubOffer)
        {
            var acceptedStatus = await _dbContext.OfferStatuses
                .FirstOrDefaultAsync(a => a.StatusName == "Accepted");
            clubOffer.OfferStatusId = acceptedStatus.Id;
            clubOffer.OfferStatus = acceptedStatus;

            _dbContext.ClubOffers.Update(clubOffer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RejectClubOffer(ClubOffer clubOffer)
        {
            var rejectedStatus = await _dbContext.OfferStatuses
                .FirstOrDefaultAsync(a => a.StatusName == "Rejected");
            clubOffer.OfferStatusId = rejectedStatus.Id;
            clubOffer.OfferStatus = rejectedStatus;

            _dbContext.ClubOffers.Update(clubOffer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> GetClubOfferStatusId(int playerAdvertisementId, string userId)
        {
            var offerStatusId = await _dbContext.ClubOffers
                .Where(co => co.PlayerAdvertisementId == playerAdvertisementId && co.ClubMemberId == userId)
                .Select(co => co.OfferStatusId)
                .FirstOrDefaultAsync();

            return offerStatusId;
        }
    }
}