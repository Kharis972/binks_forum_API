using binks_forum_API.Models;
using binks_forum_API.DTOs.Topic;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace binks_forum_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TopicController(ITopicService topicService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Topic>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var topics = await topicService.GetAllAsync();
        return topics is null
            ? Problem("Impossible de lire les discussions. Vérifie la connexion à la base.", statusCode: StatusCodes.Status503ServiceUnavailable)
            : Ok(topics);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Topic), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        if (id <= 0) return BadRequest();
        var topic = await topicService.GetByIdAsync(id);
        return topic is null ? NotFound() : Ok(topic);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync([FromBody] NewTopic request)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId is null) return Unauthorized();
        try
        {
            var topic = await topicService.AddNewTopicAsync(userId, request);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = topic.TopicId }, topic);
        }
        catch (TopicAlreadyExistsException)
        {
            return Conflict(new { message = "Une discussion porte déjà ce titre." });
        }
        catch (UnauthorizedException)
        {
            return Forbid();
        }
        catch (UserNotFoundException)
        {
            return Unauthorized();
        }
        catch (Exception)
        {
            return Problem("Impossible de publier la discussion.", statusCode: StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> EditAsync(int id, [FromBody] EditTopic request)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId is null) return Unauthorized();
        try
        {
            return Ok(await topicService.EditTopicAsync(userId, id, request));
        }
        catch (UnauthorizedException) { return Forbid(); }
        catch (TopicNotFoundException) { return NotFound(); }
        catch (Exception) { return Problem("Impossible de modifier la discussion.", statusCode: StatusCodes.Status503ServiceUnavailable); }
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId is null) return Unauthorized();
        try
        {
            await topicService.DeleteTopicAsync(userId, id);
            return NoContent();
        }
        catch (UnauthorizedException) { return Forbid(); }
        catch (TopicNotFoundException) { return NotFound(); }
        catch (Exception) { return Problem("Impossible de supprimer la discussion.", statusCode: StatusCodes.Status503ServiceUnavailable); }
    }
}
