﻿using FootScout.WebAPI.DbManager;
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
                .Include(co => co.OfferStatus)
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
                .Include(co => co.OfferStatus)
                .Include(co => co.PlayerPosition)
                .Include(co => co.UserClub)
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
                .Include(pa => pa.PlayerAdvertisement.User)
                .Include(co => co.OfferStatus)
                .Include(co => co.PlayerPosition)
                .Include(co => co.UserClub)
                .Where(co => co.PlayerAdvertisement.EndDate >= DateTime.Now)
                .OrderByDescending(co => co.CreationDate)
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
                .Include(co => co.OfferStatus)
                .Include(co => co.PlayerPosition)
                .Include(co => co.UserClub)
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

        public async Task<int> GetClubOfferStatusId(int playerAdvertisementId, string userId)
        {
            var offerStatusId = await _dbContext.ClubOffers
                .Where(co => co.PlayerAdvertisementId == playerAdvertisementId && co.UserClubId == userId)
                .Select(co => co.OfferStatusId)
                .FirstOrDefaultAsync();

            return offerStatusId;
        }
    }
}