using AutoMapper;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Models.DTOs;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FootScout.WebAPI.Controllers
{
    [Route("api/users")]
    [Authorize(Policy = "AdminOrUserRights")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // GET: api/users/:userId
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var userDTO = await _userRepository.GetUser(userId);
            if (userDTO == null)
                return NotFound();

            return Ok(userDTO);
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var userDTOs = await _userRepository.GetUsers();
            return Ok(userDTOs);
        }

        // PUT: api/users/:userId
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody]UserUpdateDTO userUpdateDto)
        {
            if (userId != userUpdateDto.Id)
                return BadRequest();

            try
            {
                await _userRepository.UpdateUser(userUpdateDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _userRepository.GetUser(userId) == null)
                    return NotFound($"User {userId} not found");
                else
                    throw;
            }
            return NoContent();
        }

        // DELETE: api/users/:userId
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                if (await _userRepository.GetUser(userId) == null)
                    return NotFound($"User {userId} not found");

                await _userRepository.DeleteUser(userId);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NoContent();
        }

        // GET: api/users/:userId/club-history
        [HttpGet("{userId}/club-history")]
        public async Task<ActionResult<IEnumerable<ClubHistory>>> GetUserClubHistory(string userId)
        {
            var userClubHistories = await _userRepository.GetUserClubHistory(userId);
            return Ok(userClubHistories);
        }

        // GET: api/users/:userId/player-advertisements
        [HttpGet("{userId}/player-advertisements")]
        public async Task<ActionResult<IEnumerable<PlayerAdvertisement>>> GetUserPlayerAdvertisements(string userId)
        {
            var userPlayerAdvertisements = await _userRepository.GetUserPlayerAdvertisements(userId);
            return Ok(userPlayerAdvertisements);
        }
    }
}