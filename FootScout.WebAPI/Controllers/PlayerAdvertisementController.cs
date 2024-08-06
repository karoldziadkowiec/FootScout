using AutoMapper;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Models.DTOs;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootScout.WebAPI.Controllers
{
    [Route("api2")]
    [Authorize(Policy = "AdminOrUserRights")]
    [ApiController]
    public class PlayerAdvertisementController : ControllerBase
    {
        private readonly IPlayerAdvertisementRepository _playerAdvertisementRepository;
        private readonly IAchievementsRepository _achievementsRepository;
        private readonly IMapper _mapper;

        public PlayerAdvertisementController(IPlayerAdvertisementRepository playerAdvertisementRepository, IAchievementsRepository achievementsRepository, IMapper mapper)
        {
            _playerAdvertisementRepository = playerAdvertisementRepository;
            _achievementsRepository = achievementsRepository;
            _mapper = mapper;
        }

        // GET: api/club-history/:clubHistoryId
        [HttpGet("club-history/{playerAdvertisementId}")]
        public async Task<ActionResult<ClubHistory>> GetClubHistory(int playerAdvertisementId)
        {
            var clubHistory = await _playerAdvertisementRepository.GetPlayerAdvertisement(playerAdvertisementId);
            if (clubHistory == null)
                return NotFound();

            return Ok(clubHistory);
        }

        // GET: api/club-history
        [HttpGet("club-history")]
        public async Task<ActionResult<IEnumerable<ClubHistory>>> GetAllClubHistory()
        {
            var clubHistories = await _playerAdvertisementRepository.GetAllPlayerAdvertisements();
            return Ok(clubHistories);
        }

        // GET: api/users/:userId/club-history
        [HttpGet("users/{userId}/club-history")]
        public async Task<ActionResult<IEnumerable<ClubHistory>>> GetUserClubHistory(string userId)
        {
            var userClubHistories = await _playerAdvertisementRepository.GetUserPlayerAdvertisement(userId);
            return Ok(userClubHistories);
        }

        // POST: api/club-history
        [HttpPost("club-history")]
        public async Task<ActionResult> CreateClubHistory([FromBody] ClubHistoryCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var achievements = _mapper.Map<Achievements>(dto.Achievements);
            await _achievementsRepository.CreateAchievements(achievements);

            var clubHistory = _mapper.Map<ClubHistory>(dto);
            clubHistory.AchievementsId = achievements.Id;
            await _playerAdvertisementRepository.CreatePlayerAdvertisement(clubHistory);

            return Ok(clubHistory);
        }

        // PUT: api/club-history/:playerAdvertisementId
        [HttpPut("club-history/{playerAdvertisementId}")]
        public async Task<ActionResult> UpdateClubHistory(int playerAdvertisementId, [FromBody] ClubHistory clubHistory)
        {
            if (playerAdvertisementId != clubHistory.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _playerAdvertisementRepository.UpdatePlayerAdvertisement(clubHistory);
            return NoContent();
        }

        // DELETEs: api/club-history/:playerAdvertisementId
        [HttpDelete("club-history/{playerAdvertisementId}")]
        public async Task<ActionResult> DeleteClubHistory(int playerAdvertisementId)
        {
            var clubHistory = await _playerAdvertisementRepository.GetPlayerAdvertisement(playerAdvertisementId);
            if (clubHistory == null)
                return NotFound();

            await _playerAdvertisementRepository.DeletePlayerAdvertisement(playerAdvertisementId);
            return NoContent();
        }
    }
}