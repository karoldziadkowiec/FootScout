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

        public async Task UpdateUser(UserUpdateDTO userUpdateDTO)
        {
            if (!userUpdateDTO.PasswordHash.Equals(userUpdateDTO.ConfirmPasswordHash))
                throw new ArgumentException($"Confirmed password is different.");

            var user = await _dbContext.Users.FindAsync(userUpdateDTO.Id);
            if (user != null)
            {
                _mapper.Map(userUpdateDTO, user);

                if (!string.IsNullOrEmpty(userUpdateDTO.PasswordHash))
                    user.PasswordHash = _passwordHasher.HashPassword(user, userUpdateDTO.PasswordHash);

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
                .Where(pa => pa.UserId == userId & pa.EndDate >= DateTime.Now)
                .OrderByDescending(pa => pa.EndDate)
                .ToListAsync();
        }
    }
}