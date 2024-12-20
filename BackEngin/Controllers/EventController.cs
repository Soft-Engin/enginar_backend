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

        //Implement try catch and exception throwing in the service.


        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // Get all events
        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 10;

                var events = await _eventService.GetAllEventsAsync(pageNumber, pageSize);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // Get an event by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var evt = await _eventService.GetEventByIdAsync(id);

            if (evt == null)
            {
                return NotFound(new { Message = "The event does not exist." });
            }
            return Ok(evt);
        }

        // Update an existing event
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventDto eventDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user_id = await _eventService.GetEventOwnerId(id);

            if (!await CanUserAccess(user_id))
            {
                return Unauthorized();
            }

            try
            {
                var updatedEvent = await _eventService.UpdateEventAsync(id, eventDTO);
                if (updatedEvent == null)
                {
                    return NotFound(new { Message = "Update failed!" });
                }

                return Ok(updatedEvent);
            }

            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the exception details if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        // Delete an event
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var user_id = await _eventService.GetEventOwnerId(id);

            if (!await CanUserAccess(user_id))
            {
                return Unauthorized();
            }

            var result = await _eventService.DeleteEventAsync(id);
            if (!result)
            {
                return NotFound(new { Message = "Event not found." });
            }

            return Ok(new { Message = "Event successfully deleted." });
        }

        // Create an event
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto createEventDto)
        {
            if (createEventDto == null) return BadRequest(new { message = "Invalid event data." });

            var creatorId = await GetActiveUserId();

            try
            {
                var result = await _eventService.CreateEventAsync(createEventDto, creatorId);
                if (result == null)
                    return BadRequest("Failed to create event.");

                return Ok(result);
            }
            
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the exception details if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        [HttpPost("JoinToEvent/{eventId}")]
        [Authorize]
        public async Task<IActionResult> JoinToEvent(int eventId)
        {
            var userId = await GetActiveUserId();

            try
            {
                var result = await _eventService.JoinToEventAsync(eventId, userId);
                if (!result)
                    return BadRequest("Failed to join the event. The user already joined this event.");

                return Ok("Successfully joined the event.");
            }

            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the exception details if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        [HttpPost("LeaveEvent/{eventId}")]
        [Authorize]
        public async Task<IActionResult> LeaveEvent(int eventId)
        {
            var userId = await GetActiveUserId();

            try
            {
                var result = await _eventService.LeaveEventAsync(eventId, userId);
                if (!result)
                    return BadRequest("Failed to leave the event. The user is not a participant of this event.");

                return Ok("Successfully left the event.");
            }

            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the exception details if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        [HttpGet("requirements")]
        [Authorize]
        public async Task<ActionResult<PaginatedResponseDTO<RequirementDto>>> GetAllRequirements(int pageNumber = 1, int pageSize = 10)
        {
            var requirements = await _eventService.GetAllRequirementsAsync(pageNumber, pageSize);
            return Ok(requirements);
        }



    }
}
