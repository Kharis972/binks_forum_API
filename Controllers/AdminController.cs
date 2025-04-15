using binks_forum_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace binks_forum_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITopicService _topicService;
        private readonly ITopicMessagesService _topicMessagesService;
        private readonly IModoService _modoService;

        //Constructeur pour initialiser le dépôt d'utilisateurs via injection de dépendance.
        public AdminController(IUserService userService, ITopicService topicService, ITopicMessagesService topicMessagesService, IModoService modoService)
        {
            _userService = userService;
            _topicService = topicService;
            _topicMessagesService = topicMessagesService;
            _modoService = modoService;
        }
    }
}