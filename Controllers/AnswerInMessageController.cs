using binks_forum_API.DTOs.AnswerInMessage;
using binks_forum_API.Models;
using binks_forum_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet("getAllAnswersInMessage")]
        [ProducesResponseType(typeof(IEnumerable<AnswerInMessage>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAnswersInMessageAsync()
        {
            try
            {
                IEnumerable<AnswerInMessage> answersInMessage = await _answerInMessageService.GetAllAsync() ?? Enumerable.Empty<AnswerInMessage>();
                return Ok(answersInMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAnswerInMessageById/{id}")]
        [ProducesResponseType(typeof(AnswerInMessage), StatusCodes.Status200OK)]    
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAnswerInMessageByIdAsync(int id)
        {
            try
            {
                AnswerInMessage? answerInMessage = await _answerInMessageService.GetByIdAsync(id);
                if (answerInMessage == null)
                {
                    return NotFound();
                }
                return Ok(answerInMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addNewAnswerInMessage")]
        [Authorize]
        [ProducesResponseType(typeof(AnswerInMessage), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewAnswerInMessageAsync([FromBody] NewAnswerInMessage newAnswerInMessage, [FromQuery] string userId)
        {
            try
            {
                AnswerInMessage? answerInMessage = await _answerInMessageService.AddNewAnswerInMessageAsync(newAnswerInMessage, userId);
                return answerInMessage is null
                    ? Problem("Impossible de créer la réponse.", statusCode: StatusCodes.Status500InternalServerError)
                    : CreatedAtAction(nameof(GetAnswerInMessageByIdAsync), new { id = answerInMessage.AnswerInMessageId }, answerInMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("editAnswerInMessage/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(AnswerInMessage), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditAnswerInMessageAsync(int id, [FromBody] EditAnswerInMessage editAnswerInMessage, [FromQuery] string userId)
        {
            try
            {
                AnswerInMessage? answerInMessage = await _answerInMessageService.EditAnswerInMessageAsync(id, userId, editAnswerInMessage);
                return Ok(answerInMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteAnswerInMessage/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAnswerInMessageAsync(int id, [FromQuery] string userId)
        {
            try
            {
                await _answerInMessageService.DeleteAnswerInMessageAsync(id, userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
