using binks_forum_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace binks_forum_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrivateMessageController : ControllerBase
    {
        private readonly IPrivateMessageService _privateMessageService;

        public PrivateMessageController(IPrivateMessageService privateMessageService)
        {
            _privateMessageService = privateMessageService;
        }
    }
}