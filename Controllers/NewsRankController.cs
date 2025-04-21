#pragma warning disable

using binks_forum_API.Service.Interfaces;
using binks_forum_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace binks_forum_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsRankController : ControllerBase
    {        
        private readonly INewsRankService _newsRankService;

        //Constructeur pour initialiser le dépôt d'utilisateurs via injection de dépendance.
        public NewsRankController(INewsRankService newsRankService)
        {
            _newsRankService = newsRankService;
        }
    }
}