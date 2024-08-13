using AutoMapper;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Models.DTOs;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootScout.WebAPI.Controllers
{
    [Route("api/club-advertisements/favorites")]
    [Authorize(Policy = "UserRights")]
    [ApiController]
    public class ClubAdvertisementFavoriteController : ControllerBase
    {
        private readonly IClubAdvertisementFavoriteRepository _clubAdvertisementFavoriteRepository;
        private readonly IMapper _mapper;

        public ClubAdvertisementFavoriteController(IClubAdvertisementFavoriteRepository clubAdvertisementFavoriteRepository, IMapper mapper)
        {
            _clubAdvertisementFavoriteRepository = clubAdvertisementFavoriteRepository;
            _mapper = mapper;
        }

        // POST: api/club-advertisements/favorites
        [HttpPost]
        public async Task<ActionResult> AddToFavorites([FromBody] ClubAdvertisementFavoriteCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid dto data.");

            var clubAdvertisementFavorite = _mapper.Map<ClubAdvertisementFavorite>(dto);
            await _clubAdvertisementFavoriteRepository.AddToFavorites(clubAdvertisementFavorite);

            return Ok(clubAdvertisementFavorite);
        }

        // DELETE: api/club-advertisements/favorites/:clubAdvertisementFavoriteId
        [HttpDelete("{clubAdvertisementFavoriteId}")]
        public async Task<ActionResult> DeleteFromFavorites(int clubAdvertisementFavoriteId)
        {
            await _clubAdvertisementFavoriteRepository.DeleteFromFavorites(clubAdvertisementFavoriteId);
            return NoContent();
        }

        // GET: api/club-advertisements/favorites/check/:clubAdvertisementId/:userId
        [HttpGet("check/{clubAdvertisementId}/{userId}")]
        public async Task<IActionResult> CheckClubAdvertisementIsFavorite(int clubAdvertisementId, string userId)
        {
            var favoriteId = await _clubAdvertisementFavoriteRepository.CheckClubAdvertisementIsFavorite(clubAdvertisementId, userId);
            return Ok(favoriteId);
        }
    }
}