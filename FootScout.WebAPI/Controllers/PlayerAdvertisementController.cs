using AutoMapper;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Models.DTOs;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootScout.WebAPI.Controllers
{
    [Route("api/player-advertisements")]
    [Authorize(Policy = "AdminOrUserRights")]
    [ApiController]
    public class PlayerAdvertisementController : ControllerBase
    {
        private readonly IPlayerAdvertisementRepository _playerAdvertisementRepository;
        private readonly ISalaryRangeRepository _salaryRangeRepository;
        private readonly IMapper _mapper;

        public PlayerAdvertisementController(IPlayerAdvertisementRepository playerAdvertisementRepository, ISalaryRangeRepository salaryRangeRepository, IMapper mapper)
        {
            _playerAdvertisementRepository = playerAdvertisementRepository;
            _salaryRangeRepository = salaryRangeRepository;
            _mapper = mapper;
        }

        // GET: api/player-advertisements/:playerAdvertisementId
        [HttpGet("{playerAdvertisementId}")]
        public async Task<ActionResult<PlayerAdvertisement>> GetPlayerAdvertisement(int playerAdvertisementId)
        {
            var playerAdvertisement = await _playerAdvertisementRepository.GetPlayerAdvertisement(playerAdvertisementId);
            if (playerAdvertisement == null)
                return NotFound();

            return Ok(playerAdvertisement);
        }

        // GET: api/player-advertisements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerAdvertisement>>> GetPlayerAdvertisements()
        {
            var playerAdvertisements = await _playerAdvertisementRepository.GetPlayerAdvertisements();
            return Ok(playerAdvertisements);
        }

        // POST: api/player-advertisements
        [HttpPost]
        public async Task<ActionResult> CreatePlayerAdvertisement([FromBody] PlayerAdvertisementCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid dto data.");

            var salaryRange = _mapper.Map<SalaryRange>(dto.SalaryRangeDTO);
            await _salaryRangeRepository.CreateSalaryRange(salaryRange);

            var playerAdvertisement = _mapper.Map<PlayerAdvertisement>(dto);
            playerAdvertisement.SalaryRangeId = salaryRange.Id;
            await _playerAdvertisementRepository.CreatePlayerAdvertisement(playerAdvertisement);

            return Ok(playerAdvertisement);
        }

        // PUT: api/player-advertisements/:playerAdvertisementId
        [HttpPut("{playerAdvertisementId}")]
        public async Task<ActionResult> UpdatePlayerAdvertisement(int playerAdvertisementId, [FromBody] PlayerAdvertisement playerAdvertisement)
        {
            if (playerAdvertisementId != playerAdvertisement.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _playerAdvertisementRepository.UpdatePlayerAdvertisement(playerAdvertisement);
            return NoContent();
        }

        // DELETE: api/player-advertisements/:playerAdvertisementId
        [HttpDelete("{playerAdvertisementId}")]
        public async Task<ActionResult> DeletePlayerAdvertisement(int playerAdvertisementId)
        {
            var playerAdvertisement = await _playerAdvertisementRepository.GetPlayerAdvertisement(playerAdvertisementId);
            if (playerAdvertisement == null)
                return NotFound();

            await _playerAdvertisementRepository.DeletePlayerAdvertisement(playerAdvertisementId);
            return NoContent();
        }
    }
}