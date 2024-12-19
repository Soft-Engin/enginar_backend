using BackEngin.Data;
using BackEngin.Services.Interfaces;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
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
        public async Task<PaginatedResponseDTO<EventDto>> GetAllEventsAsync(int page, int pageSize)
        {
            // Get paginated events, including Creator and Address
            var (items, totalCount) = await _unitOfWork.Events.GetPaginatedAsync(
                includeProperties: "Creator,Address",
                pageNumber: page,
                pageSize: pageSize
            );

            // Retrieve the participants for each event using the modified FindAllAsync
            var eventIds = items.Select(e => e.Id).ToList();
            var participantsQueryable = await _unitOfWork.User_Event_Participations
                .FindAllAsync(uep => eventIds.Contains(uep.EventId));

            // Group participants by EventId
            var participants = participantsQueryable
                .GroupBy(uep => uep.EventId)
                .ToDictionary(g => g.Key, g => g.Select(uep => uep.User).ToList());

            // Fetch the requirements for each event
            var eventRequirements = await _unitOfWork.Events_Requirements
                .FindAllAsync(er => eventIds.Contains(er.EventId));

            // Group requirements by EventId
            var requirementsGrouped = eventRequirements
                .GroupBy(er => er.EventId)
                .ToDictionary(g => g.Key, g => g.Select(er => er.Requirement).ToList());

            // Transform events to DTOs and include participants
            var events = items.Select(e => new EventDto
            {
                Title = e.Title,
                Date = e.Date,
                BodyText = e.BodyText,
                CreatorUserName = e.Creator.UserName,
                Address = e.Address,
                CreatedAt = e.CreatedAt,
                Participants = participants.ContainsKey(e.Id) ? participants[e.Id] : new List<Users>(),
                Requirements = requirementsGrouped.ContainsKey(e.Id)
                    ? requirementsGrouped[e.Id].Select(r => new RequirementDto
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Description = r.Description
                    }).ToList()
                    : new List<RequirementDto>()
            }).ToList();

            // Return the paginated response
            return new PaginatedResponseDTO<EventDto>
            {
                Items = events,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };
        }



        // Get an event by ID
        public async Task<EventDto?> GetEventByIdAsync(int eventId)
        {
            // Fetch the event entity by ID
            var eventEntity = await _unitOfWork.Events.GetByIdAsync(eventId);

            if (eventEntity == null)
                return null;

            // Fetch the participants for the event from User_Event_Participations table
            var userEventParticipations = await _unitOfWork.User_Event_Participations
                .FindAllAsync(uep => uep.EventId == eventId);

            var userIds = userEventParticipations.Select(uep => uep.UserId).ToList();


            // Retrieve the users based on the userIds from the User_Event_Participations table
            var participants = await _unitOfWork.Users
                .FindAllAsync(user => userIds.Contains(user.Id));

            // Fetch the event requirements from the Events_Requirements table
            var eventRequirements = await _unitOfWork.Events_Requirements
                .FindAllAsync(er => er.EventId == eventId);

            var requirementIds = eventRequirements.Select(er => er.RequirementId).ToList();

            // Retrieve the requirements based on the requirementIds
            var requirements = await _unitOfWork.Requirements
                .FindAllAsync(r => requirementIds.Contains(r.Id));


            // Return the event DTO with populated participants
            return new EventDto
            {
                Title = eventEntity.Title,
                Date = eventEntity.Date,
                CreatedAt = eventEntity.CreatedAt,
                BodyText = eventEntity.BodyText,
                CreatorUserName = eventEntity.Creator.UserName,
                Address = eventEntity.Address,
                Participants = participants, // Populate the Participants list
                Requirements = requirements.Select(r => new RequirementDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description
                }).ToList() // Populate the Requirements list
            };
        }

        // Create a new event
        public async Task<EventDto?> CreateEventAsync(CreateEventDto createEventDto, string creatorId)
        {
            // Validate the district exists
            var district = await _unitOfWork.Districts.GetByIdAsync(createEventDto.DistrictId);
            if (district == null)
            {
                throw new ArgumentException("The specified district does not exist.");
            }

            // Check if the address already exists
            var address = await _unitOfWork.Addresses.SingleOrDefaultAsync(a =>
                a.Name == createEventDto.AddressName &&
                a.Street == createEventDto.Street &&
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
                await _unitOfWork.CompleteAsync();
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

            // If there are requirements to associate with the event
            if (createEventDto.RequirementIds != null && createEventDto.RequirementIds.Any())
            {
                var rqs = await _unitOfWork.Requirements.GetRangeByIdsAsync(createEventDto.RequirementIds);

                var er = rqs.Select(requirement => new Events_Requirements
                {
                    EventId = eventEntity.Id,
                    RequirementId = requirement.Id
                }).ToList();

                await _unitOfWork.Events_Requirements.AddRangeAsync(er);
                await _unitOfWork.CompleteAsync();
            }

            // Fetch the participants for the event from User_Event_Participations table
            var userEventParticipations = await _unitOfWork.User_Event_Participations
                .FindAllAsync(uep => uep.EventId == eventEntity.Id);

            var userIds = userEventParticipations.Select(uep => uep.UserId).ToList();

            // Retrieve the users based on the userIds from the User_Event_Participations table
            var participants = await _unitOfWork.Users
                .FindAllAsync(user => userIds.Contains(user.Id));

            // Fetch the event requirements from the Events_Requirements table
            var eventRequirements = await _unitOfWork.Events_Requirements
                .FindAllAsync(er => er.EventId == eventEntity.Id);

            var requirementIds = eventRequirements.Select(er => er.RequirementId).ToList();

            // Retrieve the requirements based on the requirementIds
            var requirements = await _unitOfWork.Requirements
                .FindAllAsync(r => requirementIds.Contains(r.Id));

            // Return the event DTO with populated participants and requirements
            return new EventDto
            {
                Title = eventEntity.Title,
                Date = eventEntity.Date,
                CreatedAt = eventEntity.CreatedAt,
                BodyText = eventEntity.BodyText,
                CreatorUserName = eventEntity.Creator.UserName,
                Address = eventEntity.Address,
                Participants = participants, // Populate the Participants list
                Requirements = requirements.Select(r => new RequirementDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description
                }).ToList() // Populate the Requirements list
            };
        }



        // Update an existing event
        public async Task<EventDto?> UpdateEventAsync(int eventId, UpdateEventDto updateEventDto)
        {
            // Fetch the event by its ID
            var eventEntity = await _unitOfWork.Events.GetByIdAsync(eventId);

            if (eventEntity == null)
            {
                // Return null if the event does not exist
                return null;
            }

            // Validate the district exists (use DistrictId from the DTO)
            var district = await _unitOfWork.Districts.GetByIdAsync(updateEventDto.DistrictId);
            if (district == null)
            {
                throw new ArgumentException("The specified district does not exist.");
            }

            // Check if the address exists in the database
            var address = await _unitOfWork.Addresses.SingleOrDefaultAsync(a =>
                a.Name == updateEventDto.AddressName &&
                a.Street == updateEventDto.Street &&
                a.DistrictId == updateEventDto.DistrictId);

            // If the address does not exist, create a new one
            if (address == null)
            {
                address = new Addresses
                {
                    Name = updateEventDto.AddressName,
                    Street = updateEventDto.Street,
                    DistrictId = updateEventDto.DistrictId
                };

                await _unitOfWork.Addresses.AddAsync(address);
                await _unitOfWork.CompleteAsync(); // Save the new address
            }

            // Update the event entity with the new details
            eventEntity.Title = updateEventDto.Title;
            eventEntity.BodyText = updateEventDto.BodyText;
            eventEntity.Date = updateEventDto.Date.ToUniversalTime();
            eventEntity.AddressId = address.Id; // Set the address for the event
            eventEntity.Address = address; // Optionally update the address reference

            // Handle updating the event's requirements
            // Fetch current event requirements
            var currentEventRequirements = await _unitOfWork.Events_Requirements
                .FindAllAsync(er => er.EventId == eventId);

            // Find the requirements that need to be removed (existing requirements not in the provided list)
            var requirementsToRemove = currentEventRequirements
                .Where(er => !updateEventDto.RequirementIds.Contains(er.RequirementId))
                .ToList();

            // Remove the outdated requirements
            _unitOfWork.Events_Requirements.DeleteRange(requirementsToRemove);

            // Find the requirements that need to be added (new requirements that are not in the current list)
            var requirementIdsToAdd = updateEventDto.RequirementIds
                .Where(id => !currentEventRequirements.Any(er => er.RequirementId == id))
                .ToList();

            // Add new requirements
            foreach (var requirementId in requirementIdsToAdd)
            {
                var requirement = await _unitOfWork.Requirements.GetByIdAsync(requirementId);
                if (requirement != null)
                {
                    await _unitOfWork.Events_Requirements.AddAsync(new Events_Requirements
                    {
                        EventId = eventId,
                        RequirementId = requirementId
                    });
                }
            }


            // Update the event in the repository
            _unitOfWork.Events.Update(eventEntity);
            await _unitOfWork.CompleteAsync(); // Save changes

            // Return the updated event as a DTO
            return new EventDto
            {
                Title = eventEntity.Title,
                Date = eventEntity.Date,
                BodyText = eventEntity.BodyText,
                CreatorUserName = eventEntity.Creator.UserName,
                Address = eventEntity.Address, // Return the updated address
                Requirements = (await _unitOfWork.Events_Requirements
                    .FindAllAsync(er => er.EventId == eventId))
                    .Select(er => new RequirementDto
                    {
                        Id = er.Requirement.Id,
                        Name = er.Requirement.Name,
                        Description = er.Requirement.Description
                    }).ToList()
            };
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

        public async Task<string> GetEventOwnerId(int eventId)
        {
            var eventEntity = await _unitOfWork.Events.GetByIdAsync(eventId);

            if (eventEntity == null)
                return null;

            return eventEntity.Creator.Id;
        }

        public async Task<bool> JoinToEventAsync(int eventId, string userId)
        {
            // Check if the event exists
            var eventEntity = await _unitOfWork.Events.GetByIdAsync(eventId);
            if (eventEntity == null)
            {
                throw new ArgumentException("Event not found.");
            }

            // Check if the user has already joined the event
            var existingParticipation = await _unitOfWork.User_Event_Participations
                .FindAsync(uep => uep.EventId == eventId && uep.UserId == userId);

            if (existingParticipation != null)
            {
                // The user is already participating in this event
                return false; // The user cannot join the event again
            }

            // Add the user to the event
            var userEventParticipation = new User_Event_Participations
            {
                EventId = eventId,
                UserId = userId
            };

            await _unitOfWork.User_Event_Participations.AddAsync(userEventParticipation);

            // Save changes to the database
            await _unitOfWork.CompleteAsync();

            return true; // Return true if the user successfully joined the event
        }

        public async Task<bool> LeaveEventAsync(int eventId, string userId)
        {
            // Check if the event exists
            var eventEntity = await _unitOfWork.Events.GetByIdAsync(eventId);
            if (eventEntity == null)
            {
                throw new ArgumentException("Event not found.");
            }

            // Check if the user is already a participant in the event
            var existingParticipation = await _unitOfWork.User_Event_Participations
                .FindAsync(uep => uep.EventId == eventId && uep.UserId == userId);

            if (existingParticipation == null)
            {
                // The user is not a participant in this event
                return false; // The user cannot leave an event they are not part of
            }

            // Remove the user from the event
            _unitOfWork.User_Event_Participations.DeleteRange(existingParticipation);

            // Save changes to the database
            await _unitOfWork.CompleteAsync();

            return true; // Return true if the user successfully left the event
        }

        public async Task<PaginatedResponseDTO<RequirementDto>> GetAllRequirementsAsync(int pageNumber, int pageSize)
        {
            // Validate page number and page size
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            // Fetch the total count of requirements for pagination
            var totalRequirements = await _unitOfWork.Requirements.CountAsync(); // Assuming CountAsync is available in the repository

            // Fetch the requirements for the current page
            var requirements = await _unitOfWork.Requirements
                .FindAllAsync(r => true,
                    query => query.Skip((pageNumber - 1) * pageSize).Take(pageSize)); // Skip and Take for pagination

            // Map the requirements to RequirementDto
            var requirementDtos = requirements.Select(r => new RequirementDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description
            }).ToList();

            // Return the paginated result
            return new PaginatedResponseDTO<RequirementDto>
            {
                Items = requirementDtos,
                TotalCount = totalRequirements,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

    }
}
