using FootScout.WebAPI.Models.Constants;
using FootScout.WebAPI.Models.DTOs;
using FootScout.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost("admin-action")]
        public IActionResult AdminAction()
        {
            return Ok("This is an admin action");
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("user-action")]
        public IActionResult UserAction()
        {
            return Ok("This is a user action");
        }

        [Authorize(Policy = "AdminOrUserRights")]
        [HttpPost("admin-user-action")]
        public IActionResult AdminUserAction()
        {
            return Ok("This is a admin or user action");
        }
    }
}