using binks_forum_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace binks_forum_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicMessagesController : ControllerBase
    {
        private readonly ITopicMessagesService _topicMessagesService;
        public TopicMessagesController(ITopicMessagesService topicMessagesService)
        {
            _topicMessagesService = topicMessagesService;
        }
    }
}