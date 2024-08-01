using AutoMapper;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Models.DTOs;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootScout.WebAPI.Controllers
{
    [Route("api")]
    [Authorize(Policy = "AdminOrUserRights")]
    [ApiController]
    public class ClubHistoryController : ControllerBase
    {
        private readonly IClubHistoryRepository _clubHistoryRepository;
        private readonly IAchievementsRepository _achievementsRepository;
        private readonly IMapper _mapper;

        public ClubHistoryController(IClubHistoryRepository clubHistoryRepository, IAchievementsRepository achievementsRepository, IMapper mapper)
        {
            _clubHistoryRepository = clubHistoryRepository;
            _achievementsRepository = achievementsRepository;
            _mapper = mapper;
        }

        // GET: api/club-history/:clubHistoryId
        [HttpGet("club-history/{clubHistoryId}")]
        public async Task<ActionResult<ClubHistory>> GetClubHistory(int clubHistoryId)
        {
            var clubHistory = await _clubHistoryRepository.GetClubHistory(clubHistoryId);
            if (clubHistory == null)
                return NotFound();

            return Ok(clubHistory);
        }

        // GET: api/club-history
        [HttpGet("club-history")]
        public async Task<ActionResult<IEnumerable<ClubHistory>>> GetAllClubHistory()
        {
            var clubHistories = await _clubHistoryRepository.GetAllClubHistory();
            return Ok(clubHistories);
        }

        // GET: api/users/:userId/club-history
        [HttpGet("users/{userId}/club-history")]
        public async Task<ActionResult<IEnumerable<ClubHistory>>> GetUserClubHistory(string userId)
        {
            var userClubHistories = await _clubHistoryRepository.GetUserClubHistory(userId);
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
            await _clubHistoryRepository.CreateClubHistory(clubHistory);

            return Ok(clubHistory);
        }

        // PUT: api/club-history/:clubHistoryId
        [HttpPut("club-history/{clubHistoryId}")]
        public async Task<ActionResult> UpdateClubHistory(int clubHistoryId, [FromBody] ClubHistory clubHistory)
        {
            if (clubHistoryId != clubHistory.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _clubHistoryRepository.UpdateClubHistory(clubHistory);
            return NoContent();
        }

        // DELETEs: api/club-history/:clubHistoryId
        [HttpDelete("club-history/{clubHistoryId}")]
        public async Task<ActionResult> DeleteClubHistory(int clubHistoryId)
        {
            var clubHistory = await _clubHistoryRepository.GetClubHistory(clubHistoryId);
            if (clubHistory == null)
                return NotFound();

            await _clubHistoryRepository.DeleteClubHistory(clubHistoryId);
            return NoContent();
        }
    }
}