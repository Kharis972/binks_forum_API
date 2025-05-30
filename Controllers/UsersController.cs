#pragma warning disable

using binks_forum_API.DTOs.User;
using binks_forum_API.Models;
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

        [HttpPost("signup")]
        public async Task<IActionResult> SignupAsync([FromBody] NewUserRequest newUserRequest)
        {
            try
            {
                User user = await _userService.SignupAsync(newUserRequest);
                return Ok(user);
            }
            catch
            {
                return Conflict("L'utilisateur existe déjà.");
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest loginRequest)
        {
            try
            {
                User user = await _userService.LoginAsync(loginRequest);
                return Ok(user);
            }
            catch
            {
                return Unauthorized("Identifiants invalides.");
            }
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                List<User> users = (await _userService.GetAllAsync()).ToList();
                return Ok(users);
            }
            catch
            {
                return NotFound("Aucun utilisateur trouvé.");
            }
        }
        
    }
}