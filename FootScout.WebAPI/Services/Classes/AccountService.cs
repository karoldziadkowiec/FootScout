using FootScout.WebAPI.DbManager;
using FootScout.WebAPI.Entities;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using FootScout.WebAPI.Services.Interfaces;
using FootScout.WebAPI.Models.DTOs;
using AutoMapper;
using FootScout.WebAPI.Models.Constants;
using Microsoft.EntityFrameworkCore;

namespace FootScout.WebAPI.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly ICookieService _cookieService;

        public AccountService(AppDbContext dbContext, UserManager<User> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager, ITokenService tokenService, ICookieService cookieService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _cookieService = cookieService;
        }

        public async Task Register(RegisterDTO registerDTO)
        {
            var userByEmail = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (userByEmail != null)
                throw new ArgumentException($"User with email {registerDTO.Email} already exists.");

            if (!registerDTO.Password.Equals(registerDTO.ConfirmPassword))
                throw new ArgumentException($"Confirmed password is different.");

            var user = _mapper.Map<User>(registerDTO);
            
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

        public async Task<IEnumerable<string>> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles.Select(role => role.Name);
        }

        public async Task MakeAnAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new ArgumentException($"User {userId} does not exist.");

            await _userManager.AddToRoleAsync(user, Role.Admin);
        }

        private string GetRegisterError(IEnumerable<IdentityError> errors)
        {
            return string.Join(", ", errors.Select(error => error.Description).ToArray());
        }
    }
}