using binks_forum_API.Service.Interfaces;
using binks_forum_API.Services.Interfaces;
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
        private readonly IActivityService _activityService;
        private readonly INewsRankService _newsRankService;
        private readonly IRankService _rankService;

        //Constructeur pour initialiser le dépôt d'utilisateurs via injection de dépendance.
        public AdminController
        (
            IUserService userService,
            ITopicService topicService, 
            ITopicMessagesService topicMessagesService, 
            IModoService modoService, 
            IActivityService activityService, 
            INewsRankService newsRankService, 
            IRankService rankService
        )
        {
            _userService = userService;
            _topicService = topicService;
            _topicMessagesService = topicMessagesService;
            _modoService = modoService;
            _activityService = activityService;
            _newsRankService = newsRankService;
            _rankService = rankService;
        }

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAllTopics")]
        public async Task<IActionResult> GetAllTopicsAsync()
        {
            try
            {
                var topics = await _topicService.GetAllAsync();
                return Ok(topics);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAllTopicMessages")]
        public async Task<IActionResult> GetAllTopicMessagesAsync()
        {
            try
            {
                var topicMessages = await _topicMessagesService.GetAllAsync();
                return Ok(topicMessages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAllModos")]
        public async Task<IActionResult> GetAllModosAsync()
        {
            try
            {
                var modos = await _modoService.GetAllAsync();
                return Ok(modos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAllActivities")]
        public async Task<IActionResult> GetAllActivitiesAsync()
        {
            try
            {
                var activities = await _activityService.GetAllAsync();
                return Ok(activities);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAllNewsRanks")]
        public async Task<IActionResult> GetAllNewsRanksAsync()
        {
            try
            {
                var newsRanks = await _newsRankService.GetAllAsync();
                return Ok(newsRanks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAllRanks")]
        public async Task<IActionResult> GetAllRanksAsync()
        {
            try
            {
                var ranks = await _rankService.GetAllAsync();
                return Ok(ranks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}