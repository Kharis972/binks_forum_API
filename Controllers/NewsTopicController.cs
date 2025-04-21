#pragma warning disable

using binks_forum_API.Service.Interfaces;
using binks_forum_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace binks_forum_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsTopicController : ControllerBase
    {        
        private readonly INewsTopicService _newsTopicService;

        //Constructeur pour initialiser le dépôt d'utilisateurs via injection de dépendance.
        public NewsTopicController(INewsTopicService newsTopicService)
        {
            _newsTopicService = newsTopicService;
        }
    }
}