// Service Interface
using Models.DTO;
using System.Threading.Tasks;

namespace BackEngin.Services.Interfaces
{
    public interface IEventService
    {
        Task<PaginatedResponseDTO<EventDto>> GetAllEventsAsync(int page, int pageSize);
        Task<EventDto?> GetEventByIdAsync(int eventId);
        Task<EventDto?> CreateEventAsync(CreateEventDto createEventDto, string creatorId);
        Task<EventDto?> UpdateEventAsync(int eventId, UpdateEventDto updateEventDto);
        Task<bool> DeleteEventAsync(int eventId);
        Task<string> GetEventOwnerId(int eventId);
        Task<bool> JoinToEventAsync(int eventId, string userId);
        Task<bool> LeaveEventAsync(int eventId, string userId);
        Task<PaginatedResponseDTO<RequirementDto>> GetAllRequirementsAsync(int pageNumber, int pageSize);
    }
}