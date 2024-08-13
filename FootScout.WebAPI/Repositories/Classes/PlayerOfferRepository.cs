using FootScout.WebAPI.DbManager;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootScout.WebAPI.Repositories.Classes
{
    public class PlayerOfferRepository : IPlayerOfferRepository
    {
        private readonly AppDbContext _dbContext;

        public PlayerOfferRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PlayerOffer> GetPlayerOffer(int playerOfferId)
        {
            return await _dbContext.PlayerOffers
                .Include(po => po.ClubAdvertisement)
                .Include(ca => ca.ClubAdvertisement.PlayerPosition)
                .Include(ca => ca.ClubAdvertisement.SalaryRange)
                .Include(ca => ca.ClubAdvertisement.User)
                .Include(po => po.OfferStatus)
                .Include(po => po.PlayerPosition)
                .Include(po => po.PlayerFoot)
                .Include(po => po.UserPlayer)
                .FirstOrDefaultAsync(po => po.Id == playerOfferId);
        }

        public async Task<IEnumerable<PlayerOffer>> GetPlayerOffers()
        {
            return await _dbContext.PlayerOffers
                .Include(po => po.ClubAdvertisement)
                .Include(ca => ca.ClubAdvertisement.PlayerPosition)
                .Include(ca => ca.ClubAdvertisement.SalaryRange)
                .Include(ca => ca.ClubAdvertisement.User)
                .Include(po => po.OfferStatus)
                .Include(po => po.PlayerPosition)
                .Include(po => po.PlayerFoot)
                .Include(po => po.UserPlayer)
                .OrderByDescending(po => po.CreationDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<PlayerOffer>> GetActivePlayerOffers()
        {
            return await _dbContext.PlayerOffers
                .Include(po => po.ClubAdvertisement)
                .Include(ca => ca.ClubAdvertisement.PlayerPosition)
                .Include(ca => ca.ClubAdvertisement.SalaryRange)
                .Include(ca => ca.ClubAdvertisement.User)
                .Include(po => po.OfferStatus)
                .Include(po => po.PlayerPosition)
                .Include(po => po.PlayerFoot)
                .Include(po => po.UserPlayer)
                .Where(po => po.ClubAdvertisement.EndDate >= DateTime.Now)
                .OrderByDescending(po => po.CreationDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<PlayerOffer>> GetInactivePlayerOffers()
        {
            return await _dbContext.PlayerOffers
                .Include(po => po.ClubAdvertisement)
                .Include(ca => ca.ClubAdvertisement.PlayerPosition)
                .Include(ca => ca.ClubAdvertisement.SalaryRange)
                .Include(ca => ca.ClubAdvertisement.User)
                .Include(po => po.OfferStatus)
                .Include(po => po.PlayerPosition)
                .Include(po => po.PlayerFoot)
                .Include(po => po.UserPlayer)
                .Where(po => po.ClubAdvertisement.EndDate < DateTime.Now)
                .OrderByDescending(po => po.CreationDate)
                .ToListAsync();
        }

        public async Task CreatePlayerOffer(PlayerOffer playerOffer)
        {
            playerOffer.CreationDate = DateTime.Now;

            var offeredStatus = await _dbContext.OfferStatuses
                .FirstOrDefaultAsync(a => a.StatusName == "Offered");
            playerOffer.OfferStatusId = offeredStatus.Id;
            playerOffer.OfferStatus = offeredStatus;

            await _dbContext.PlayerOffers.AddAsync(playerOffer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePlayerOffer(PlayerOffer playerOffer)
        {
            _dbContext.PlayerOffers.Update(playerOffer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AcceptPlayerOffer(PlayerOffer playerOffer)
        {
            var acceptedStatus = await _dbContext.OfferStatuses
                .FirstOrDefaultAsync(a => a.StatusName == "Accepted");
            playerOffer.OfferStatusId = acceptedStatus.Id;
            playerOffer.OfferStatus = acceptedStatus;

            _dbContext.PlayerOffers.Update(playerOffer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RejectPlayerOffer(PlayerOffer playerOffer)
        {
            var rejectedStatus = await _dbContext.OfferStatuses
                .FirstOrDefaultAsync(a => a.StatusName == "Rejected");
            playerOffer.OfferStatusId = rejectedStatus.Id;
            playerOffer.OfferStatus = rejectedStatus;

            _dbContext.PlayerOffers.Update(playerOffer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> GetPlayerOfferStatusId(int clubAdvertisementId, string userId)
        {
            var offerStatusId = await _dbContext.PlayerOffers
                .Where(co => co.ClubAdvertisementId == clubAdvertisementId && co.UserPlayerId == userId)
                .Select(co => co.OfferStatusId)
                .FirstOrDefaultAsync();

            return offerStatusId;
        }
    }
}