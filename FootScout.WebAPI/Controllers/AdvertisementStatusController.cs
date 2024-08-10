using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootScout.WebAPI.Controllers
{
    [Route("api/advertisement-statuses")]
    [Authorize(Policy = "AdminOrUserRights")]
    [ApiController]
    public class AdvertisementStatusController : ControllerBase
    {
        private readonly IAdvertisementStatusRepository _advertisementStatusRepository;

        public AdvertisementStatusController(IAdvertisementStatusRepository advertisementStatusRepository)
        {
            _advertisementStatusRepository = advertisementStatusRepository;
        }

        // GET: api/advertisement-statuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdvertisementStatus>>> GetAdvertisementStatuses()
        {
            var advertisementStatuses = await _advertisementStatusRepository.GetAdvertisementStatuses();
            return Ok(advertisementStatuses);
        }

        // GET: api/advertisement-statuses/:statusId
        [HttpGet("{statusId}")]
        public async Task<IActionResult> GetAdvertisementStatusName(int statusId)
        {
            var statusName = await _advertisementStatusRepository.GetAdvertisementStatusName(statusId);
            if (statusName == null)
                return NotFound();

            return Ok(statusName);
        }

        // GET: api/advertisement-statuses/name/:statusName
        [HttpGet("name/{statusName}")]
        public async Task<IActionResult> GetAdvertisementStatusId(string statusName)
        {
            var statusId = await _advertisementStatusRepository.GetAdvertisementStatusId(statusName);
            if (statusId == null)
                return NotFound();

            return Ok(statusId);
        }
    }
}