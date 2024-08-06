using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootScout.WebAPI.Controllers
{
    [Route("api/player-positions")]
    [Authorize(Policy = "AdminOrUserRights")]
    [ApiController]
    public class PlayerPositionController : ControllerBase
    {
        private readonly IPlayerPositionRepository _playerPositionRepository;

        public PlayerPositionController(IPlayerPositionRepository playerPositionRepository)
        {
            _playerPositionRepository = playerPositionRepository;
        }

        // GET: api/player-positions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerPosition>>> GetPlayerPositions()
        {
            var playerPositions = await _playerPositionRepository.GetPlayerPositions();
            return Ok(playerPositions);
        }

        // GET: api/player-positions/:positionId
        [HttpGet("{positionId}")]
        public async Task<IActionResult> GetPlayerPositionName(int positionId)
        {
            var positionName = await _playerPositionRepository.GetPlayerPositionName(positionId);
            if (positionName == null)
                return NotFound();

            return Ok(positionName);
        }
    }
}