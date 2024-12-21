using Asp.Versioning;
using BackEngin.Services;
using BackEngin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace BackEngin.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/event")]
    [ApiController]
    public class EventController : ApiControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "Page and pageSize must be positive integers." });
            }

            try
            {
                var events = await _eventService.GetAllEventsAsync(pageNumber, pageSize);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            try
            {
                var evt = await _eventService.GetEventByIdAsync(id);

                if (evt == null)
                {
                    return NotFound(new { message = "The event does not exist." });
                }
                return Ok(evt);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventDto eventDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data.", errors = ModelState });
            }

            try
            {
                var userId = await _eventService.GetEventOwnerId(id);

                if (!await CanUserAccess(userId))
                {
                    return Unauthorized(new { message = "You are not authorized to update this event." });
                }

                var updatedEvent = await _eventService.UpdateEventAsync(id, eventDTO);
                if (updatedEvent == null)
                {
                    return NotFound(new { message = "The event could not be updated because it does not exist." });
                }

                return Ok(updatedEvent);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            try
            {
                var userId = await _eventService.GetEventOwnerId(id);

                if (!await CanUserAccess(userId))
                {
                    return Unauthorized(new { message = "You are not authorized to delete this event." });
                }

                var result = await _eventService.DeleteEventAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "The event does not exist." });
                }

                return Ok(new { message = "The event was successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto createEventDto)
        {
            if (createEventDto == null)
            {
                return BadRequest(new { message = "Invalid event data." });
            }

            try
            {
                var result = await _eventService.CreateEventAsync(createEventDto, await GetActiveUserId(), await GetActiveUserName());
                if (result == null)
                {
                    return BadRequest(new { message = "Failed to create event." });
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPost("AttendToEvent/{eventId}")]
        [Authorize]
        public async Task<IActionResult> JoinToEvent(int eventId)
        {
            try
            {
                var userId = await GetActiveUserId();

                var result = await _eventService.ToggleAttendToEventAsync(eventId, userId);
                if (!result)
                {
                    return Ok(new { message = "Successfully left the event." });
                }

                return Ok(new { message = "Successfully joined the event." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("requirements")]
        public async Task<ActionResult<PaginatedResponseDTO<RequirementDto>>> GetAllRequirements(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "Page and pageSize must be positive integers." });
            }

            try
            {
                var requirements = await _eventService.GetAllRequirementsAsync(pageNumber, pageSize);
                return Ok(requirements);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("{eventId}/participants")]
        public async Task<ActionResult<PaginatedResponseDTO<ParticipantDto>>> GetEventParticipants(int eventId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "Page and pageSize must be positive integers." });
            }

            try
            {
                var participants = await _eventService.GetPaginatedParticipantsAsync(eventId, pageNumber, pageSize);
                return Ok(participants);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
