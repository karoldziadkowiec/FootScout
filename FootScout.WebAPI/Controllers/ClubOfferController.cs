using AutoMapper;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Models.DTOs;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootScout.WebAPI.Controllers
{
    [Route("api/club-offers")]
    [Authorize(Policy = "AdminOrUserRights")]
    [ApiController]
    public class ClubOfferController : ControllerBase
    {
        private readonly IClubOfferRepository _clubOfferRepository;
        private readonly IMapper _mapper;

        public ClubOfferController(IClubOfferRepository clubOfferRepository, IMapper mapper)
        {
            _clubOfferRepository = clubOfferRepository;
            _mapper = mapper;
        }

        // GET: api/club-offers/:clubOfferId
        [HttpGet("{clubOfferId}")]
        public async Task<ActionResult<ClubOffer>> GetClubOffer(int clubOfferId)
        {
            var clubOffer = await _clubOfferRepository.GetClubOffer(clubOfferId);
            if (clubOffer == null)
                return NotFound();

            return Ok(clubOffer);
        }

        // GET: api/club-offers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClubOffer>>> GetClubOffers()
        {
            var clubOffers = await _clubOfferRepository.GetClubOffers();
            return Ok(clubOffers);
        }

        // GET: api/club-offers/active
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<ClubOffer>>> GetActiveClubOffers()
        {
            var activeClubOffers = await _clubOfferRepository.GetActiveClubOffers();
            return Ok(activeClubOffers);
        }

        // GET: api/club-offers/inactive
        [HttpGet("inactive")]
        public async Task<ActionResult<IEnumerable<ClubOffer>>> GetInactiveClubOffers()
        {
            var inactiveClubOffers = await _clubOfferRepository.GetInactiveClubOffers();
            return Ok(inactiveClubOffers);
        }

        // POST: api/club-offers
        [HttpPost]
        public async Task<ActionResult> CreateClubOffer([FromBody] ClubOfferCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid dto data.");

            var clubOffer = _mapper.Map<ClubOffer>(dto);
            await _clubOfferRepository.CreateClubOffer(clubOffer);

            return Ok(clubOffer);
        }

        // PUT: api/club-offers/:clubOfferId
        [HttpPut("{clubOfferId}")]
        public async Task<ActionResult> UpdateClubOffer(int clubOfferId, [FromBody] ClubOffer clubOffer)
        {
            if (clubOfferId != clubOffer.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _clubOfferRepository.UpdateClubOffer(clubOffer);
            return NoContent();
        }

        // GET: api/club-offers/status/:playerAdvertisementId/:userId
        [HttpGet("status/{playerAdvertisementId}/{userId}")]
        public async Task<IActionResult> GetClubOfferStatusId(int playerAdvertisementId, string userId)
        {
            var clubOfferStatusId = await _clubOfferRepository.GetClubOfferStatusId(playerAdvertisementId, userId);
            return Ok(clubOfferStatusId);
        }
    }
}