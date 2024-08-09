using AutoMapper;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Models.DTOs;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootScout.WebAPI.Controllers
{
    [Route("api/player-advertisements/favorites")]
    [Authorize(Policy = "UserRights")]
    [ApiController]
    public class PlayerAdvertisementFavoriteController : ControllerBase
    {
        private readonly IPlayerAdvertisementFavoriteRepository _playerAdvertisementFavoriteRepository;
        private readonly IMapper _mapper;

        public PlayerAdvertisementFavoriteController(IPlayerAdvertisementFavoriteRepository playerAdvertisementFavoriteRepository, IMapper mapper)
        {
            _playerAdvertisementFavoriteRepository = playerAdvertisementFavoriteRepository;
            _mapper = mapper;
        }

        // POST: api/player-advertisements/favorites
        [HttpPost]
        public async Task<ActionResult> AddToFavorites([FromBody] PlayerAdvertisementFavoriteCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid dto data.");

            var playerAdvertisementFavorite = _mapper.Map<PlayerAdvertisementFavorite>(dto);
            await _playerAdvertisementFavoriteRepository.AddToFavorites(playerAdvertisementFavorite);

            return Ok(playerAdvertisementFavorite);
        }

        // DELETE: api/player-advertisements/favorites/:playerAdvertisementFavoriteId
        [HttpDelete("{playerAdvertisementFavoriteId}")]
        public async Task<ActionResult> DeleteFromFavorites(int playerAdvertisementFavoriteId)
        {
            await _playerAdvertisementFavoriteRepository.DeleteFromFavorites(playerAdvertisementFavoriteId);
            return NoContent();
        }

        // GET: api/player-advertisements/favorites/check/:playerAdvertisementId/:userId
        [HttpGet("check/{playerAdvertisementId}/{userId}")]
        public async Task<IActionResult> CheckPlayerAdvertisementIsFavorite(int playerAdvertisementId, string userId)
        
        {
            var favoriteId = await _playerAdvertisementFavoriteRepository.CheckPlayerAdvertisementIsFavorite(playerAdvertisementId, userId);
            return Ok(favoriteId);
        }
    }
}