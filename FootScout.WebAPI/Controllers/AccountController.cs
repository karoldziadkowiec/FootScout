using FootScout.WebAPI.Models.Constants;
using FootScout.WebAPI.Models.DTOs;
using FootScout.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FootScout.WebAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // POST: api/account/register
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (registerDTO == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _accountService.Register(registerDTO);
                return Ok(registerDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error during the registration of account: {ex.Message}");
            }
        }

        // POST: api/account/login
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var token = await _accountService.Login(loginDTO);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error during the login: {ex.Message}");
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("roles")]
        public async Task<ActionResult<IEnumerable<string>>> GetRoles()
        {
            var roles = await _accountService.GetRoles();
            return Ok(roles);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("roles/make-admin/{userId}")]
        public async Task<IActionResult> MakeAnAdmin(string userId)
        {
            await _accountService.MakeAnAdmin(userId);
            return NoContent();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("roles/make-user/{userId}")]
        public async Task<IActionResult> DemoteFromAdmin(string userId)
        {
            await _accountService.MakeAnUser(userId);
            return NoContent();
        }
    }
}