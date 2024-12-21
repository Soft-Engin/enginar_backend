// Service Interface
using Models.DTO;
using System.Threading.Tasks;

namespace BackEngin.Services.Interfaces
{
    public interface IEventService
    {
        Task<PaginatedResponseDTO<EventDto>> GetAllEventsAsync(int page, int pageSize);
        Task<EventDto?> GetEventByIdAsync(int eventId);
        Task<EventDto?> CreateEventAsync(CreateEventDto createEventDto, string creatorId, string creatorName);
        Task<EventDto?> UpdateEventAsync(int eventId, UpdateEventDto updateEventDto);
        Task<bool> DeleteEventAsync(int eventId);
        Task<string> GetEventOwnerId(int eventId);
        Task<bool> ToggleAttendToEventAsync(int eventId, string userId);
        Task<PaginatedResponseDTO<ParticipantDto>> GetPaginatedParticipantsAsync(int eventId, int page, int pageSize);
        Task<PaginatedResponseDTO<RequirementDto>> GetAllRequirementsAsync(int pageNumber, int pageSize);
        Task<IEnumerable<DistrictDto>> GetDistrictsByCityIdAsync(int cityId);
        Task<IEnumerable<CityDto>> GetCitiesByCountryIdAsync(int countryId);
        Task<IEnumerable<CountryDto>> GetAllCountriesAsync();
        Task<CityDto> GetCityByDistrictIdAsync(int districtId);
        Task<CountryDto> GetCountryByCityIdAsync(int cityId);

    }
}