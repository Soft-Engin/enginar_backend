// Service Interface
using Models;
using Models.DTO;
using System.Threading.Tasks;

namespace BackEngin.Services.Interfaces
{
    public interface IEventService
    {
        Task<PaginatedResponseDTO<EventDTO>> GetAllEventsAsync(int page, int pageSize);
        Task<EventDTO?> GetEventByIdAsync(int eventId);
        Task<EventDTO?> CreateEventAsync(CreateEventDTO createEventDto, string creatorId, string creatorName);
        Task<EventDTO?> UpdateEventAsync(int eventId, UpdateEventDTO updateEventDto);
        Task<bool> DeleteEventAsync(int eventId);
        Task<string> GetEventOwnerId(int eventId);
        Task<bool> ToggleAttendToEventAsync(int eventId, string userId);
        Task<PaginatedResponseDTO<ParticipantDTO>> GetPaginatedParticipantsAsync(int eventId, int page, int pageSize);
        Task<PaginatedResponseDTO<RequirementDTO>> GetAllRequirementsAsync(int pageNumber, int pageSize);
        Task<IEnumerable<DistrictDTO>> GetDistrictsByCityIdAsync(int cityId);
        Task<IEnumerable<CityDTO>> GetCitiesByCountryIdAsync(int countryId);
        Task<IEnumerable<CountryDTO>> GetAllCountriesAsync();
        Task<CityDTO> GetCityByDistrictIdAsync(int districtId);
        Task<CountryDTO> GetCountryByCityIdAsync(int cityId);

    }
}