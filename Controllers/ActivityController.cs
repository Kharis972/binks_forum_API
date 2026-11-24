#pragma warning disable

using binks_forum_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using binks_forum_API.Models;

namespace binks_forum_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {        
        private readonly IActivityService _activityService;

        //Constructeur pour initialiser le dépôt d'utilisateurs via injection de dépendance.
        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var activities = await _activityService.GetAllAsync();
            return activities is null ? Problem("Evenements indisponibles.", statusCode: StatusCodes.Status503ServiceUnavailable) : Ok(activities);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0) return BadRequest();
            Activity? activity = await _activityService.GetByIdAsync(id);
            return activity is null ? NotFound() : Ok(activity);
        }
    }
}
