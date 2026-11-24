using binks_forum_API.Models;
using binks_forum_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace binks_forum_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrivateDiscussionController : ControllerBase
    {
        private readonly IPrivateDiscussionService _privateDiscussionService;

        public PrivateDiscussionController(IPrivateDiscussionService privateDiscussionService)
        {
            _privateDiscussionService = privateDiscussionService;
        }

        [HttpGet("getAllPrivateDiscussions")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllPrivateDiscussionsAsync()
        {
            try
            {
                var privateDiscussions = await _privateDiscussionService.GetAllAsync();
                if (privateDiscussions == null || !privateDiscussions.Any())
                {
                    return NotFound("No private discussions found.");
                }
                return Ok(privateDiscussions);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
