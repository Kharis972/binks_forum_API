#pragma warning disable

using binks_forum_API.Service.Interfaces;
using binks_forum_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using binks_forum_API.Models;

namespace binks_forum_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RankController : ControllerBase
    {        
        private readonly IRankService _rankService;

        //Constructeur pour initialiser le dépôt d'utilisateurs via injection de dépendance.
        public RankController(IRankService rankService)
        {
            _rankService = rankService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var ranks = await _rankService.GetAllAsync();
            return ranks is null ? Problem("Rangs indisponibles.", statusCode: StatusCodes.Status503ServiceUnavailable) : Ok(ranks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0) return BadRequest();
            Rank? rank = await _rankService.GetByIdAsync(id);
            return rank is null ? NotFound() : Ok(rank);
        }
    }
}
