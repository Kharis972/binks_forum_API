#pragma warning disable

using binks_forum_API.Service.Interfaces;
using binks_forum_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}