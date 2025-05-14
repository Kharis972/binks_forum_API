using System.Net.Mime;
using binks_forum_API.Models;
using binks_forum_API.Service.Interfaces;
using binks_forum_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace binks_forum_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBadgeService _badgeService;
        private readonly ITopicService _topicService;
        private readonly ITopicMessagesService _topicMessagesService;
        private readonly IModoService _modoService;
        private readonly IActivityService _activityService;
        private readonly INewsRankService _newsRankService;
        private readonly IRankService _rankService;
        private readonly IFactionService _factionService;
        private readonly IAnswerInMessageService _answerInMessageService;

        //Constructeur pour initialiser le dépôt d'utilisateurs via injection de dépendance.
        public AdminController
        (
            IUserService userService,
            IBadgeService badgeService,
            ITopicService topicService, 
            ITopicMessagesService topicMessagesService, 
            IModoService modoService, 
            IActivityService activityService, 
            INewsRankService newsRankService, 
            IRankService rankService,
            IFactionService factionService,
            IAnswerInMessageService answerInMessageService
        )
        {
            _userService = userService;
            _badgeService = badgeService;
            _topicService = topicService;
            _topicMessagesService = topicMessagesService;
            _modoService = modoService;
            _activityService = activityService;
            _newsRankService = newsRankService;
            _rankService = rankService;
            _factionService = factionService;
            _answerInMessageService = answerInMessageService;
        }

        [HttpGet("getAllUsers")]
        [Authorize(Roles = "Admin")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            try
            {
                IEnumerable<User> users = await _userService.GetAllAsync() ?? Enumerable.Empty<User>();
                return Ok(users);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while retrieving users.");
            }
        }

        [HttpGet("getAllTopics")]
        [Authorize(Roles = "Admin")]
        [Produces(MediaTypeNames.Application.Json)] 
        [ProducesResponseType(typeof(IEnumerable<Topic>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTopicsAsync()
        {
            try
            {
                IEnumerable<Topic> topics = await _topicService.GetAllAsync() ?? Enumerable.Empty<Topic>();
                return Ok(topics);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while retrieving topics.");
            }
        }

        [HttpGet("getAllTopicMessages")]
        [Authorize(Roles = "Admin")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<TopicMessages>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTopicMessagesAsync()
        {
            try
            {
                IEnumerable<TopicMessages> topicMessages = await _topicMessagesService.GetAllAsync() ?? Enumerable.Empty<TopicMessages>();
                return Ok(topicMessages);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while retrieving topic messages.");
            }
        }

        [HttpGet("getAllModos")]
        [Authorize(Roles = "Admin")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Modo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllModosAsync()
        {
            try
            {
                IEnumerable<Modo> modos = await _modoService.GetAllAsync() ?? Enumerable.Empty<Modo>();
                return Ok(modos);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while retrieving modos.");
            }
        }

        [HttpGet("getAllActivities")]
        [Authorize(Roles = "Admin")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Activity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllActivitiesAsync()
        {
            try
            {
                List<Activity> activities = (await _activityService.GetAllAsync() ?? Enumerable.Empty<Activity>()).ToList();
                return Ok(activities);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while retrieving activities.");
            }
        }

        [HttpGet("getAllNewsRanks")]
        [Authorize(Roles = "Admin")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<NewsRank>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllNewsRanksAsync()
        {
            try
            {
                List<NewsRank> newsRanks = (await _newsRankService.GetAllAsync() ?? Enumerable.Empty<NewsRank>()).ToList();
                return Ok(newsRanks);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while retrieving news ranks.");
            }
        }

        [HttpGet("getAllRanks")]
        [Authorize(Roles = "Admin")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Rank>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRanksAsync()
        {
            try
            {
                List<Rank> ranks = (await _rankService.GetAllAsync() ?? Enumerable.Empty<Rank>()).ToList();
                return Ok(ranks);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while retrieving ranks.");
            }
        }

        [HttpGet("getAllFactions")]
        [Authorize(Roles = "Admin")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Faction>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllFactionsAsync()
        {
            try
            {
                List<Faction> factions = (await _factionService.GetAllAsync() ?? Enumerable.Empty<Faction>()).ToList();
                return Ok(factions);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while retrieving factions.");
            }
        
        }

        [HttpGet("getAllAnswerInMessages")]
        [Authorize(Roles = "Admin")]
        [Produces (MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<AnswerInMessage>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllANswerInMessagesAsync()
        {
            try
            {
                List<AnswerInMessage> answerInMessage = (await _answerInMessageService.GetAllAsync() ?? Enumerable.Empty<AnswerInMessage>()).ToList();
                return Ok(answerInMessage);
            }
            catch(Exception)
            {
                return BadRequest("An error occurred while retrieving answer in messages.");
            }

        }
    }
}