﻿using FootScout.WebAPI.DbManager;
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

        public async Task<PlayerAdvertisement> GetPlayerAdvertisement(int playerAdvertisementId)
        {
            return await _dbContext.PlayerAdvertisements
                .Include(pa => pa.PlayerPosition)
                .Include(pa => pa.PlayerFoot)
                .Include(pa => pa.SalaryRange)
                .Include(pa => pa.User)
                .FirstOrDefaultAsync(pa => pa.Id == playerAdvertisementId);
        }

        public async Task<IEnumerable<PlayerAdvertisement>> GetPlayerAdvertisements()
        {
            return await _dbContext.PlayerAdvertisements
                .Include(pa => pa.PlayerPosition)
                .Include(pa => pa.PlayerFoot)
                .Include(pa => pa.SalaryRange)
                .Include(pa => pa.User)
                .OrderByDescending(pa => pa.EndDate)
                .ToListAsync();
        }

        public async Task CreatePlayerAdvertisement(PlayerAdvertisement playerAdvertisement)
        {
            playerAdvertisement.CreationDate = DateTime.Now;
            playerAdvertisement.StartDate = DateTime.Now;
            playerAdvertisement.EndDate = DateTime.Now.AddDays(30);

            await _dbContext.PlayerAdvertisements.AddAsync(playerAdvertisement);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePlayerAdvertisement(PlayerAdvertisement playerAdvertisement)
        {
            _dbContext.PlayerAdvertisements.Update(playerAdvertisement);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePlayerAdvertisement(int playerAdvertisementId)
        {
            var playerAdvertisement = await _dbContext.PlayerAdvertisements.FindAsync(playerAdvertisementId);
            if (playerAdvertisement == null)
                throw new ArgumentException($"No Player Advertisement found with ID {playerAdvertisementId}");

            if (playerAdvertisement.SalaryRangeId != null)
            {
                var salaryRange = await _dbContext.SalaryRanges.FindAsync(playerAdvertisement.SalaryRangeId);
                if (salaryRange != null)
                    _dbContext.SalaryRanges.Remove(salaryRange);
            }

            _dbContext.PlayerAdvertisements.Remove(playerAdvertisement);
            await _dbContext.SaveChangesAsync();
        }
    }
}