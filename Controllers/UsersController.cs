#pragma warning disable

using binks_forum_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace binks_forum_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {        
        private readonly IUserService _userService;

        //Constructeur pour initialiser le dépôt d'utilisateurs via injection de dépendance.
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
    }
}