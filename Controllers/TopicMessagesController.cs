using System.Security.Claims;
using binks_forum_API.Data;
using binks_forum_API.DTOs.TopicMessages;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace binks_forum_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TopicMessagesController(
    ITopicMessagesService topicMessagesService,
    ApplicationDataBaseContext database) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetForTopicAsync([FromQuery] int topicId)
    {
        if (topicId <= 0) return BadRequest(new { message = "Identifiant de discussion invalide." });
        if (!await database.Topics.AnyAsync(topic => topic.TopicId == topicId)) return NotFound();
        var messages = await database.TopicMessages.AsNoTracking()
            .Where(message => message.TopicId == topicId)
            .OrderBy(message => message.Date)
            .Select(message => new
            {
                message.Id,
                message.TopicId,
                message.UserId,
                message.Date,
                message.Score,
                message.Content
            })
            .ToListAsync();
        return Ok(messages);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync([FromQuery] int topicId, [FromBody] AddNewTopicMessages request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();
        try
        {
            var message = await topicMessagesService.AddNewTopicMessagesAsync(topicId, userId, request);
            return StatusCode(StatusCodes.Status201Created, new
            {
                message.Id,
                message.TopicId,
                message.UserId,
                message.Date,
                message.Score,
                message.Content
            });
        }
        catch (TopicNotFoundException) { return NotFound(new { message = "Discussion introuvable." }); }
        catch (UserNotFoundException) { return Unauthorized(); }
        catch (UnauthorizedException) { return Forbid(); }
        catch (Exception) { return Problem("Impossible de publier la réponse.", statusCode: StatusCodes.Status503ServiceUnavailable); }
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> EditAsync(int id, [FromQuery] int topicId, [FromBody] EditTopicMessages request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();
        try
        {
            var message = await topicMessagesService.EditTopicMessagesAsync(userId, topicId, request, id);
            return Ok(new { message.Id, message.TopicId, message.UserId, message.Date, message.Score, message.Content });
        }
        catch (TopicMessagesNotFoundException) { return NotFound(); }
        catch (TopicNotFoundException) { return NotFound(); }
        catch (UnauthorizedException) { return Forbid(); }
        catch (NoChangesException) { return Conflict(new { message = "Aucun changement à enregistrer." }); }
        catch (Exception) { return Problem("Impossible de modifier la réponse.", statusCode: StatusCodes.Status503ServiceUnavailable); }
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteAsync(int id, [FromQuery] int topicId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();
        try
        {
            await topicMessagesService.DeleteTopicMessagesAsync(userId, topicId, id);
            return NoContent();
        }
        catch (TopicMessagesNotFoundException) { return NotFound(); }
        catch (TopicNotFoundException) { return NotFound(); }
        catch (UnauthorizedException) { return Forbid(); }
        catch (Exception) { return Problem("Impossible de supprimer la réponse.", statusCode: StatusCodes.Status503ServiceUnavailable); }
    }
}
