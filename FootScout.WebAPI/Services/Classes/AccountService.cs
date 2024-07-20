using FootScout.WebAPI.DbManager;
using FootScout.WebAPI.Entities;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using FootScout.WebAPI.Services.Interfaces;
using FootScout.WebAPI.Models.DTOs;

namespace FootScout.WebAPI.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ICookieService _cookieService;

        public AccountService(AppDbContext dbContext, UserManager<User> userManager, ITokenService tokenService, ICookieService cookieService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _tokenService = tokenService;
            _cookieService = cookieService;
        }

        public async Task Register(RegisterDTO registerDTO)
        {
            var userByEmail = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (userByEmail != null)
            {
                throw new ArgumentException($"User with email {registerDTO.Email} already exists.");
            }
            if (!registerDTO.Password.Equals(registerDTO.ConfirmPassword))
            {
                throw new ArgumentException($"Confirmed password is different.");
            }

            var address = new Address { City = registerDTO.City, Street = registerDTO.Street };
            var user = new User
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                PhoneNumber = registerDTO.PhoneNumber,
                Address = address,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            await _userManager.AddToRoleAsync(user, Role.User);

            if (!result.Succeeded)
            {
                throw new ArgumentException($"Unable to register user {registerDTO.Email}, errors: {GetRegisterError(result.Errors)}");
            }
        }

        public async Task<string> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                throw new ArgumentException($"User {loginDTO.Email} does not exist.");
            }
            if (!await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                throw new ArgumentException($"Unable to authenticate user {loginDTO.Email} - wrong password.");
            }

            var token = await _tokenService.CreateTokenJWT(user);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            await _cookieService.SetCookies(token, tokenString);

            return tokenString;
        }

        private string GetRegisterError(IEnumerable<IdentityError> errors)
        {
            return string.Join(", ", errors.Select(error => error.Description).ToArray());
        }
    }
}