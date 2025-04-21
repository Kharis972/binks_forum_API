#pragma warning disable

using binks_forum_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}