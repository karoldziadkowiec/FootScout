﻿using FootScout.WebAPI.DbManager;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootScout.WebAPI.Repositories.Classes
{
    public class PlayerFootRepository : IPlayerFootRepository
    {
        private readonly AppDbContext _dbContext;

        public PlayerFootRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PlayerFoot>> GetPlayerFeet()
            => await _dbContext.PlayerFeet.ToListAsync();

        public async Task<string> GetPlayerFootName(int footId)
            => await _dbContext.PlayerFeet.Where(p => p.Id == footId).Select(p => p.FootName).FirstOrDefaultAsync();
    }
}