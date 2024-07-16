using FootScout.WebAPI.DbContext;
using FootScout.WebAPI.Entities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FootScout.WebAPI.Services.Interfaces;
using FootScout.WebAPI.Models.DTOs;

namespace FootScout.WebAPI.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public AccountService(AppDbContext dbContext, IConfiguration configuration, UserManager<User> userManager, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<bool> Register(RegisterDTO request)
        {
            var userByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userByEmail != null)
            {
                throw new ArgumentException($"User with email {request.Email} already exists.");
            }

            Address address = new Address { City = request.City, Street = request.Street };
            User user = new User
            {
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = address,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new ArgumentException($"Unable to register user {request.Email} errors: {GetRegisterError(result.Errors)}");
            }

            // Dodaj użytkownika do roli po poprawnym utworzeniu
            await _userManager.AddToRoleAsync(user, Role.User);

            return true;
        }

        public async Task<string> Login(LoginDTO request)
        {
            var user = await _userManager.FindByNameAsync(request.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new ArgumentException($"Unable to authenticate user {request.Email}");
            }

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.FirstName),
                new(ClaimTypes.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

            var token = _tokenService.GetTokenJWT(authClaims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GetRegisterError(IEnumerable<IdentityError> errors)
        {
            return string.Join(", ", errors.Select(error => error.Description).ToArray());
        }
    }
}