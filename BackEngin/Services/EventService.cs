using BackEngin.Data;
using BackEngin.Services.Interfaces;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Models;
using Models.DTO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEngin.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Get all events (paginated)
        public async Task<PaginatedResponseDTO<EventDTO>> GetAllEventsAsync(int page, int pageSize)
        {
            // Get paginated events, including Creator and Address
            var (items, totalCount) = await _unitOfWork.Events.GetPaginatedAsync(
                includeProperties: "Creator,Address,Address.District",
                pageNumber: page,
                pageSize: pageSize
            );

            var events = items.Select(MapEventToDto);

            // Return the paginated response
            return new PaginatedResponseDTO<EventDTO>
            {
                Items = events,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };
        }

        public EventDTO MapEventToDto(Events e)
        {
            //Make sure the DTO is populated with all the required data
            if (e.Creator == null)
            {
                e.Creator = _unitOfWork.Users.FindAsync(u => u.Id == e.CreatorId).Result.First();
            }
            if (e.Address == null)
            {
                e.Address = _unitOfWork.Addresses.FindAsync(u => u.Id == e.AddressId).Result.First();
            }
            if (e.Address.District == null)
            {
                e.Address.District = _unitOfWork.Districts.FindAsync(u => u.Id == e.Address.DistrictId).Result.First();
            }
            if (e.Address.District.City == null)
            {
                e.Address.District.City = _unitOfWork.Cities.FindAsync(u => u.Id == e.Address.District.CityId).Result.First();
            }
            if (e.Address.District.City.Country == null)
            {
                e.Address.District.City.Country = _unitOfWork.Countries.FindAsync(u => u.Id == e.Address.District.City.CountryId).Result.First();
            }

            return new EventDTO
            {
                Title = e.Title,
                Date = e.Date,
                EventId = e.Id,
                BodyText = e.BodyText,
                CreatorUserName = e.Creator.UserName,
                CreatorId = e.CreatorId,
                Address = e.Address,
                CreatedAt = e.CreatedAt,
                Requirements = _unitOfWork.Events_Requirements.FindAsync(r => r.EventId == e.Id, includeProperties: "Requirement").Result
                                .Select(er => new RequirementDTO
                                {
                                    Id = er.Requirement.Id,
                                    Name = er.Requirement.Name,
                                    Description = er.Requirement.Description
                                }).ToList(),
                TotalParticipantsCount = _unitOfWork.User_Event_Participations.CountAsync(r => r.EventId == e.Id).Result
            };
        }


        // Get an event by ID
        public async Task<EventDTO?> GetEventByIdAsync(int eventId)
        {
            // Fetch the event entity by ID, including Creator and Address
            var eventEntity = await _unitOfWork.Events.GetByIdAsync(eventId);

            if (eventEntity == null)
                return null;

            // Return the event DTO with populated participants and requirements
            return MapEventToDto(eventEntity);
        }

        public async Task<EventDTO?> CreateEventAsync(CreateEventDTO createEventDto, string creatorId, string creatorName)
        {
            // Validate the district exists
            var district = await _unitOfWork.Districts.GetByIdAsync(createEventDto.DistrictId);
            if (district == null)
            {
                throw new ArgumentException("The specified district does not exist.");
            }

            if (createEventDto.Date < DateTime.UtcNow)
            {
                throw new ArgumentException("The event can not be set for the past.");
            }

            if (createEventDto.RequirementIds != null && createEventDto.RequirementIds.Any())
            {
                if (await _unitOfWork.Requirements.CountAsync(r => createEventDto.RequirementIds.Contains(r.Id)) != createEventDto.RequirementIds.Count())
                {
                    throw new ArgumentException("One or more requirements do not exist.");
                }
            }


            // Check if the address already exists
            var address = await _unitOfWork.Addresses.SingleOrDefaultAsync(a =>
                a.Name.ToLower() == createEventDto.AddressName.ToLower() &&
                a.Street.ToLower() == createEventDto.Street.ToLower() &&
                a.DistrictId == createEventDto.DistrictId);

            // If the address doesn't exist, create a new one
            if (address == null)
            {
                address = new Addresses
                {
                    Name = createEventDto.AddressName,
                    Street = createEventDto.Street,
                    DistrictId = createEventDto.DistrictId
                };

                await _unitOfWork.Addresses.AddAsync(address);
                await _unitOfWork.CompleteAsync(); // Ensure address is saved
            }

            // Create the event
            var eventEntity = new Events
            {
                Title = createEventDto.Title,
                BodyText = createEventDto.BodyText,
                Date = createEventDto.Date.ToUniversalTime(),
                CreatorId = creatorId,
                AddressId = address.Id,
                CreatedAt = DateTime.UtcNow // Include CreatedAt
            };

            await _unitOfWork.Events.AddAsync(eventEntity);

            await _unitOfWork.CompleteAsync();
            // Add the creator as a participant
            var userEventParticipation = new User_Event_Participations
            {
                UserId = creatorId,
                EventId = eventEntity.Id
            };
            await _unitOfWork.User_Event_Participations.AddAsync(userEventParticipation);

            await _unitOfWork.CompleteAsync();
            // If there are requirements to associate with the event
            if (createEventDto.RequirementIds != null && createEventDto.RequirementIds.Any())
            {
                var req = await _unitOfWork.Requirements
                    .FindAsync(r => createEventDto.RequirementIds.Contains(r.Id));

                var evtReqs = req.Select(requirement => new Events_Requirements
                {
                    EventId = eventEntity.Id,
                    RequirementId = requirement.Id
                }).ToList();

                await _unitOfWork.Events_Requirements.AddRangeAsync(evtReqs);
            }
            await _unitOfWork.CompleteAsync();


            // Return the event DTO
            return MapEventToDto(eventEntity);
        }



        // Update an existing event
        public async Task<EventDTO?> UpdateEventAsync(int eventId, UpdateEventDTO updateEventDto)
        {
            // Fetch the event by its ID
            var eventQuery = await _unitOfWork.Events.FindAsync(r => r.Id == eventId, includeProperties: "Creator,Address");

            if (eventQuery.Count() == 0)
            {
                throw new Exception("The specified event does not exist.");
            }

            var eventEntity = eventQuery.First();

            // Validate the district exists (use DistrictId from the DTO)
            var district = await _unitOfWork.Districts.GetByIdAsync(updateEventDto.DistrictId);
            if (district == null)
            {
                throw new ArgumentException("The specified district does not exist.");
            }

            // Fetch the creator's details
            var creatorId = await GetEventOwnerId(eventId);
            if (creatorId == null)
            {
                throw new ArgumentException("The specified creator does not exist.");
            }

            if (updateEventDto.Date < DateTime.UtcNow)
            {
                throw new ArgumentException("The event can not be set for the past.");
            }

            if (updateEventDto.RequirementIds != null && updateEventDto.RequirementIds.Any())
            {
                if (await _unitOfWork.Requirements.CountAsync(r => updateEventDto.RequirementIds.Contains(r.Id)) != updateEventDto.RequirementIds.Count())
                {
                    throw new ArgumentException("One or more requirements do not exist.");
                }
            }


            // Check if the address exists in the database
            var address = eventEntity.Address;

            address.Name = updateEventDto.AddressName;
            address.Street = updateEventDto.Street;
            address.DistrictId = updateEventDto.DistrictId;


            _unitOfWork.Addresses.Update(address);


            // Update the event entity with the new details
            eventEntity.Title = updateEventDto.Title;
            eventEntity.BodyText = updateEventDto.BodyText;
            eventEntity.Date = updateEventDto.Date.ToUniversalTime();


            var currentEventRequirements = _unitOfWork.Events_Requirements
                .FindAsync(er => er.EventId == eventId).Result.Select(r => r.RequirementId).ToList();

            // Add new requirements
            foreach (var requirementId in updateEventDto.RequirementIds)
            {
                if (!currentEventRequirements.Contains(requirementId))
                {
                    await _unitOfWork.Events_Requirements.AddAsync(new Events_Requirements
                    {
                        EventId = eventId,
                        RequirementId = requirementId
                    });

                    currentEventRequirements.Remove(requirementId);
                }
            }

            // Remove requirements that are not in the updated list
            foreach (var requirementId in currentEventRequirements)
            {
                var er = await _unitOfWork.Events_Requirements.FindAsync(r => r.EventId == eventId && r.RequirementId == requirementId);
                _unitOfWork.Events_Requirements.Delete(er.First());
            }


            // Update the event in the repository
            _unitOfWork.Events.Update(eventEntity);
            await _unitOfWork.CompleteAsync(); // Save changes

            // Return the updated event as a DTO
            return MapEventToDto(eventEntity);
        }


        // Delete an event
        public async Task<bool> DeleteEventAsync(int eventId)
        {
            var eventEntity = await _unitOfWork.Events.GetByIdAsync(eventId);

            if (eventEntity == null)
                return false;

            _unitOfWork.Events.Delete(eventEntity);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<string?> GetEventOwnerId(int eventId)
        {
            var eventEntity = await _unitOfWork.Events.GetByIdAsync(eventId);

            if (eventEntity == null)
                return null;

            if (eventEntity.CreatorId == null)
                throw new InvalidOperationException("The Creator is not loaded or does not exist.");

            return eventEntity.CreatorId;
        }


        public async Task<bool> ToggleAttendToEventAsync(int eventId, string userId)
        {
            // Check if the event exists
            var eventEntity = await _unitOfWork.Events.GetByIdAsync(eventId);

            if (eventEntity == null)
            {
                throw new ArgumentException("Event not found.");
            }

            if (eventEntity.CreatorId == userId)
            {
                throw new ArgumentException("The creator cannot leave their own event.");
            }

            if (eventEntity.Date < DateTime.UtcNow)
            {
                throw new ArgumentException("The event has already passed.");
            }

            // Check if the user has already joined the event
            var existingParticipation = _unitOfWork.User_Event_Participations.FindAsync(uep => uep.EventId == eventId && uep.UserId == userId).Result.FirstOrDefault();

            if (existingParticipation != default)
            {
                // The user is already participating in this event
                _unitOfWork.User_Event_Participations.Delete(existingParticipation);
                await _unitOfWork.CompleteAsync();

                return false; // Return false if the user successfully left the event
            }
            else
            {
                // Add the user to the event
                var userEventParticipation = new User_Event_Participations
                {
                    EventId = eventId,
                    UserId = userId
                };

                await _unitOfWork.User_Event_Participations.AddAsync(userEventParticipation);
                await _unitOfWork.CompleteAsync();

                return true; // Return true if the user successfully joined the event
            }
        }

        public async Task<PaginatedResponseDTO<ParticipantDTO>> GetPaginatedParticipantsAsync(int eventId, int page, int pageSize)
        {
            // Get paginated participants for the event
            var (items, totalCount) = await _unitOfWork.User_Event_Participations.GetPaginatedAsync(
                includeProperties: "User",
                uep => uep.EventId == eventId,
                pageNumber: page,
                pageSize: pageSize
                );

            var participants = items.Select(uep => new ParticipantDTO
            {
                UserId = uep.UserId,
                UserName = uep.User.UserName
            });

            // Return the paginated response
            return new PaginatedResponseDTO<ParticipantDTO>
            {
                Items = participants,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };
        }


        public async Task<PaginatedResponseDTO<RequirementDTO>> GetAllRequirementsAsync(int pageNumber, int pageSize)
        {
            // Validate page number and page size
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            // Fetch the total count of requirements for pagination
            var totalRequirements = await _unitOfWork.Requirements.CountAsync(null); // Assuming CountAsync is available in the repository

            // Fetch the requirements for the current page
            var requirements = await _unitOfWork.Requirements
                .FindAllAsync(r => true,
                    query => query.Skip((pageNumber - 1) * pageSize).Take(pageSize)); // Skip and Take for pagination

            // Map the requirements to RequirementDto
            var requirementDtos = requirements.Select(r => new RequirementDTO
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description
            }).ToList();

            // Return the paginated result
            return new PaginatedResponseDTO<RequirementDTO>
            {
                Items = requirementDtos,
                TotalCount = totalRequirements,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<IEnumerable<DistrictDTO>> GetDistrictsByCityIdAsync(int cityId)
        {
            var districts = await _unitOfWork.Districts.FindAsync(d => d.CityId == cityId, "City");
            return districts.Select(d => new DistrictDTO
            {
                Id = d.Id,
                Name = d.Name,
                PostCode = d.PostCode
            });
        }

        public async Task<IEnumerable<CityDTO>> GetCitiesByCountryIdAsync(int countryId)
        {
            var cities = await _unitOfWork.Cities.FindAsync(c => c.CountryId == countryId, "Country");
            return cities.Select(c => new CityDTO
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<IEnumerable<CountryDTO>> GetAllCountriesAsync()
        {
            var countries = await _unitOfWork.Countries.GetAllAsync();
            return countries.Select(c => new CountryDTO
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<CityDTO> GetCityByDistrictIdAsync(int districtId)
        {
            var district = (await _unitOfWork.Districts.FindAsync(d => d.Id == districtId, "City")).FirstOrDefault();
            if (district?.City == null)
            {
                return null;
            }
            return new CityDTO
            {
                Id = district.City.Id,
                Name = district.City.Name
            };
        }

        public async Task<CountryDTO> GetCountryByCityIdAsync(int cityId)
        {
            var city = (await _unitOfWork.Cities.FindAsync(c => c.Id == cityId, "Country")).FirstOrDefault();
            if (city?.Country == null)
            {
                return null;
            }
            return new CountryDTO
            {
                Id = city.Country.Id,
                Name = city.Country.Name
            };
        }
    }
}
