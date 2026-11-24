using binks_forum_API.DTOs.User;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Service.Interfaces;
using binks_forum_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace binks_forum_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsersController(IUserService userService, TokenService tokenService) : ControllerBase
{
    [HttpPost("signup")]
    [EnableRateLimiting("auth")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> SignupAsync([FromBody] NewUserRequest request)
    {
        try
        {
            var user = await userService.SignupAsync(request);
            return StatusCode(StatusCodes.Status201Created, CreateResponse(user));
        }
        catch (UserAlreadyExistsException)
        {
            return Conflict(new { message = "Cette adresse e-mail ou ce pseudo est déjà utilisé." });
        }
        catch (DatabaseGlobalException)
        {
            return Problem("La base de données est indisponible.", statusCode: StatusCodes.Status503ServiceUnavailable);
        }
        catch (Exception)
        {
            return Problem("Impossible de créer le compte.", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("login")]
    [EnableRateLimiting("auth")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
    {
        try
        {
            var user = await userService.LoginAsync(request);
            return Ok(CreateResponse(user));
        }
        catch (UserNotFoundException)
        {
            return Unauthorized(new { message = "Adresse e-mail ou mot de passe invalide." });
        }
        catch (IdentifiersMissMatchException)
        {
            return Unauthorized(new { message = "Adresse e-mail ou mot de passe invalide." });
        }
        catch (DatabaseGlobalException)
        {
            return Problem("La base de données est indisponible.", statusCode: StatusCodes.Status503ServiceUnavailable);
        }
        catch (Exception)
        {
            return Problem("Impossible de vous connecter.", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    private AuthResponse CreateResponse(User user)
    {
        var (token, expiresAt) = tokenService.Create(user);
        return new AuthResponse(token, expiresAt, user);
    }
}

public sealed record AuthResponse(string AccessToken, DateTime ExpiresAt, User User);
