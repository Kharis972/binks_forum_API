using binks_forum_API.DTOs.AnswerInMessage;
using binks_forum_API.Models;
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
        [ProducesResponseType(typeof(AnswerInMessage), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewAnswerInMessageAsync([FromBody] NewAnswerInMessage newAnswerInMessage, [FromQuery] string userId)
        {
            try
            {
                AnswerInMessage? answerInMessage = await _answerInMessageService.AddNewAnswerInMessageAsync(newAnswerInMessage, userId);
                return Ok(newAnswerInMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("editAnswerInMessage/{id}")]
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