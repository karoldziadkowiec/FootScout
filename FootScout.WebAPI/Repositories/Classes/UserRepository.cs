using AutoMapper;
using FootScout.WebAPI.DbManager;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Models.DTOs;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FootScout.WebAPI.Repositories.Classes
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserRepository(AppDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDTO> GetUser(string userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var users = await _dbContext.Users.ToListAsync();
            var userDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);
            return userDTOs;
        }

        public async Task UpdateUser(string userId, UserUpdateDTO dto)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user != null)
            {
                _mapper.Map(dto, user);
                _dbContext.Entry(user).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task ResetUserPassword(string userId, UserResetPasswordDTO dto)
        {
            if (!dto.PasswordHash.Equals(dto.ConfirmPasswordHash))
                throw new ArgumentException($"Confirmed password is different.");

            var user = await _dbContext.Users.FindAsync(userId);
            if (user != null)
            {
                _mapper.Map(dto, user);

                if (!string.IsNullOrEmpty(dto.PasswordHash))
                    user.PasswordHash = _passwordHasher.HashPassword(user, dto.PasswordHash);

                _dbContext.Entry(user).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteUser(string userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClubHistory>> GetUserClubHistory(string userId)
        {
            return await _dbContext.ClubHistories
                .Include(ch => ch.Achievements)
                .Include(ch => ch.PlayerPosition)
                .Include(ch => ch.User)
                .Where(ch => ch.UserId == userId)
                .OrderByDescending(ch => ch.StartDate)
                .ThenByDescending(ch => ch.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<PlayerAdvertisement>> GetUserPlayerAdvertisements(string userId)
        {
            return await _dbContext.PlayerAdvertisements
                .Include(pa => pa.PlayerPosition)
                .Include(pa => pa.PlayerFoot)
                .Include(pa => pa.SalaryRange)
                .Include(pa => pa.User)
                .Where(pa => pa.UserId == userId)
                .OrderByDescending(pa => pa.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<PlayerAdvertisement>> GetUserActivePlayerAdvertisements(string userId)
        {
            return await _dbContext.PlayerAdvertisements
                .Include(pa => pa.PlayerPosition)
                .Include(pa => pa.PlayerFoot)
                .Include(pa => pa.SalaryRange)
                .Include(pa => pa.User)
                .Where(pa => pa.UserId == userId && pa.EndDate >= DateTime.Now)
                .OrderBy(pa => pa.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<PlayerAdvertisement>> GetUserInactivePlayerAdvertisements(string userId)
        {
            return await _dbContext.PlayerAdvertisements
                .Include(pa => pa.PlayerPosition)
                .Include(pa => pa.PlayerFoot)
                .Include(pa => pa.SalaryRange)
                .Include(pa => pa.User)
                .Where(pa => pa.UserId == userId && pa.EndDate < DateTime.Now)
                .OrderByDescending(pa => pa.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<FavoritePlayerAdvertisement>> GetUserFavoritePlayerAdvertisements(string userId)
        {
            return await _dbContext.FavoritePlayerAdvertisements
                .Include(pa => pa.PlayerAdvertisement)
                .Include(pa => pa.PlayerAdvertisement.PlayerPosition)
                .Include(pa => pa.PlayerAdvertisement.PlayerFoot)
                .Include(pa => pa.PlayerAdvertisement.SalaryRange)
                .Include(pa => pa.PlayerAdvertisement.User)
                .Include(pa => pa.User)
                .Where(pa => pa.UserId == userId)
                .OrderByDescending(pa => pa.PlayerAdvertisement.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<FavoritePlayerAdvertisement>> GetUserActiveFavoritePlayerAdvertisements(string userId)
        {
            return await _dbContext.FavoritePlayerAdvertisements
                .Include(pa => pa.PlayerAdvertisement)
                .Include(pa => pa.PlayerAdvertisement.PlayerPosition)
                .Include(pa => pa.PlayerAdvertisement.PlayerFoot)
                .Include(pa => pa.PlayerAdvertisement.SalaryRange)
                .Include(pa => pa.PlayerAdvertisement.User)
                .Include(pa => pa.User)
                .Where(pa => pa.UserId == userId && pa.PlayerAdvertisement.EndDate >= DateTime.Now)
                .OrderBy(pa => pa.PlayerAdvertisement.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<FavoritePlayerAdvertisement>> GetUserInactiveFavoritePlayerAdvertisements(string userId)
        {
            return await _dbContext.FavoritePlayerAdvertisements
                .Include(pa => pa.PlayerAdvertisement)
                .Include(pa => pa.PlayerAdvertisement.PlayerPosition)
                .Include(pa => pa.PlayerAdvertisement.PlayerFoot)
                .Include(pa => pa.PlayerAdvertisement.SalaryRange)
                .Include(pa => pa.PlayerAdvertisement.User)
                .Include(pa => pa.User)
                .Where(pa => pa.UserId == userId && pa.PlayerAdvertisement.EndDate < DateTime.Now)
                .OrderByDescending(pa => pa.PlayerAdvertisement.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClubOffer>> GetReceivedClubOffers(string userId)
        {
            return await _dbContext.ClubOffers
                .Include(co => co.PlayerAdvertisement)
                .Include(pa => pa.PlayerAdvertisement.PlayerPosition)
                .Include(pa => pa.PlayerAdvertisement.PlayerFoot)
                .Include(pa => pa.PlayerAdvertisement.SalaryRange)
                .Include(pa => pa.PlayerAdvertisement.User)
                .Include(co => co.OfferStatus)
                .Include(co => co.PlayerPosition)
                .Include(co => co.ClubMember)
                .Where(pa => pa.PlayerAdvertisement.UserId == userId)
                .OrderByDescending(pa => pa.PlayerAdvertisement.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClubOffer>> GetSentClubOffers(string userId)
        {
            return await _dbContext.ClubOffers
                .Include(co => co.PlayerAdvertisement)
                .Include(pa => pa.PlayerAdvertisement.PlayerPosition)
                .Include(pa => pa.PlayerAdvertisement.PlayerFoot)
                .Include(pa => pa.PlayerAdvertisement.SalaryRange)
                .Include(pa => pa.PlayerAdvertisement.User)
                .Include(co => co.OfferStatus)
                .Include(co => co.PlayerPosition)
                .Include(co => co.ClubMember)
                .Where(pa => pa.ClubMemberId == userId)
                .OrderByDescending(pa => pa.PlayerAdvertisement.CreationDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClubAdvertisement>> GetUserClubAdvertisements(string userId)
        {
            return await _dbContext.ClubAdvertisements
                .Include(ca => ca.PlayerPosition)
                .Include(ca => ca.SalaryRange)
                .Include(ca => ca.User)
                .Where(ca => ca.UserId == userId)
                .OrderByDescending(ca => ca.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClubAdvertisement>> GetUserActiveClubAdvertisements(string userId)
        {
            return await _dbContext.ClubAdvertisements
                .Include(ca => ca.PlayerPosition)
                .Include(ca => ca.SalaryRange)
                .Include(ca => ca.User)
                .Where(ca => ca.UserId == userId && ca.EndDate >= DateTime.Now)
                .OrderBy(ca => ca.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClubAdvertisement>> GetUserInactiveClubAdvertisements(string userId)
        {
            return await _dbContext.ClubAdvertisements
                .Include(ca => ca.PlayerPosition)
                .Include(ca => ca.SalaryRange)
                .Include(ca => ca.User)
                .Where(ca => ca.UserId == userId && ca.EndDate < DateTime.Now)
                .OrderByDescending(ca => ca.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<FavoriteClubAdvertisement>> GetUserFavoriteClubAdvertisements(string userId)
        {
            return await _dbContext.FavoriteClubAdvertisements
                .Include(ca => ca.ClubAdvertisement)
                .Include(ca => ca.ClubAdvertisement.PlayerPosition)
                .Include(ca => ca.ClubAdvertisement.SalaryRange)
                .Include(ca => ca.ClubAdvertisement.User)
                .Include(ca => ca.User)
                .Where(ca => ca.UserId == userId)
                .OrderByDescending(ca => ca.ClubAdvertisement.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<FavoriteClubAdvertisement>> GetUserActiveFavoriteClubAdvertisements(string userId)
        {
            return await _dbContext.FavoriteClubAdvertisements
                .Include(ca => ca.ClubAdvertisement)
                .Include(ca => ca.ClubAdvertisement.PlayerPosition)
                .Include(ca => ca.ClubAdvertisement.SalaryRange)
                .Include(ca => ca.ClubAdvertisement.User)
                .Include(ca => ca.User)
                .Where(ca => ca.UserId == userId && ca.ClubAdvertisement.EndDate >= DateTime.Now)
                .OrderBy(ca => ca.ClubAdvertisement.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<FavoriteClubAdvertisement>> GetUserInactiveFavoriteClubAdvertisements(string userId)
        {
            return await _dbContext.FavoriteClubAdvertisements
                .Include(ca => ca.ClubAdvertisement)
                .Include(ca => ca.ClubAdvertisement.PlayerPosition)
                .Include(ca => ca.ClubAdvertisement.SalaryRange)
                .Include(ca => ca.ClubAdvertisement.User)
                .Include(ca => ca.User)
                .Where(ca => ca.UserId == userId && ca.ClubAdvertisement.EndDate < DateTime.Now)
                .OrderByDescending(ca => ca.ClubAdvertisement.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<PlayerOffer>> GetReceivedPlayerOffers(string userId)
        {
            return await _dbContext.PlayerOffers
                .Include(po => po.ClubAdvertisement)
                .Include(ca => ca.ClubAdvertisement.PlayerPosition)
                .Include(ca => ca.ClubAdvertisement.SalaryRange)
                .Include(ca => ca.ClubAdvertisement.User)
                .Include(po => po.OfferStatus)
                .Include(po => po.PlayerPosition)
                .Include(po => po.PlayerFoot)
                .Include(po => po.Player)
                .Where(pa => pa.ClubAdvertisement.UserId == userId)
                .OrderByDescending(pa => pa.ClubAdvertisement.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<PlayerOffer>> GetSentPlayerOffers(string userId)
        {
            return await _dbContext.PlayerOffers
                .Include(po => po.ClubAdvertisement)
                .Include(ca => ca.ClubAdvertisement.PlayerPosition)
                .Include(ca => ca.ClubAdvertisement.SalaryRange)
                .Include(ca => ca.ClubAdvertisement.User)
                .Include(po => po.OfferStatus)
                .Include(po => po.PlayerPosition)
                .Include(po => po.PlayerFoot)
                .Include(po => po.Player)
                .Where(pa => pa.PlayerId == userId)
                .OrderByDescending(pa => pa.ClubAdvertisement.CreationDate)
                .ToListAsync();
        }
    }
}