#pragma warning disable

using binks_forum_API.Service.Interfaces;
using binks_forum_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}