using binks_forum_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace binks_forum_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswerInMessageController : ControllerBase
    {
        private readonly IAnswerInMessageService _answerInMessageService;

        public AnswerInMessageController(IAnswerInMessageService answerInMessageService)
        {
            _answerInMessageService = answerInMessageService;
        }
    }
}