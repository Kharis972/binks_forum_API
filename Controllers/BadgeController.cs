using binks_forum_API.Services;
using binks_forum_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using binks_forum_API.Models;

namespace binks_forum_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BadgeController : ControllerBase
    {
        private readonly IBadgeService _badgeService;

        public BadgeController(IBadgeService badgeService)
        {
            _badgeService = badgeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var badges = await _badgeService.GetAllAsync();
            return badges is null ? Problem("Badges indisponibles.", statusCode: StatusCodes.Status503ServiceUnavailable) : Ok(badges);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0) return BadRequest();
            Badge? badge = await _badgeService.GetByIdAsync(id);
            return badge is null ? NotFound() : Ok(badge);
        }
    }
}
