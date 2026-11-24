#pragma warning disable

using binks_forum_API.Service.Interfaces;
using binks_forum_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using binks_forum_API.Models;

namespace binks_forum_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {        
        private readonly INewsService _newsService;

        //Constructeur pour initialiser le dépôt d'utilisateurs via injection de dépendance.
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var news = await _newsService.GetAllAsync();
            return news is null ? Problem("Actualites indisponibles.", statusCode: StatusCodes.Status503ServiceUnavailable) : Ok(news);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0) return BadRequest();
            News? news = await _newsService.GetByIdAsync(id);
            return news is null ? NotFound() : Ok(news);
        }
    }
}
