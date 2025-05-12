namespace binks_forum_API.Controllers
{
    using binks_forum_API.DTOs.Faction;
    using binks_forum_API.Models;
    using binks_forum_API.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Net.Mime;

    [Route("api/[controller]")]
    [ApiController]
    public class FactionController : ControllerBase
    {
        private readonly IFactionService _factionService;

        public FactionController(IFactionService factionService)
        {
            _factionService = factionService;
        }

        public object GetById { get; private set; }

        /// <summary>
        /// Adds a new faction to the database.
        /// </summary>
        /// <param name="newFaction">The new faction to add.</param>
        /// <param name="adminId">The ID of the admin adding the faction.</param>
        /// <returns>The created faction.</returns>
        /// <response code="201">Returns the created faction</response>
        /// <response code="400">If the faction already exists</response>
        /// <response code="500">If there is a server error</response>
        /// <remarks>
        /// Sample request:
        ///     
        /// {
        ///     "name": "New Faction",
        ///    "description": "This is a new faction.",
        ///     "adminId": "admin123"
        /// }               
        /// </remarks>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Faction), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewFaction([FromBody] NewFaction newFaction, [FromQuery] string adminId)
        {
            try
            {
                Faction? faction = await _factionService.AddNewFactionAsync(newFaction, adminId);
                return CreatedAtAction(nameof(GetById), new { id = faction.FactionId }, faction);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Faction), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditFaction([FromBody] EditFaction editFaction, [FromQuery] int factionId, [FromQuery] string adminId)
        {
            try
            {
                Faction? faction = await _factionService.EditFactionAsync(editFaction, factionId, adminId);
                return Ok(faction);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Faction), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteFaction([FromQuery] int factionId, [FromQuery] string adminId)
        {
            try
            {
                await _factionService.DeleteFactionAsync(factionId, adminId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}