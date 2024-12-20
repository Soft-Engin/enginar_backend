using BackEngin.Data;
using BackEngin.Services.Interfaces;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
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
        private readonly IUserService _userService;

        public EventService(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
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

            // Retrieve the participants for each event using GetQueryable to optimize the query
            var eventIds = items.Select(e => e.Id).ToList();
            var participantsQueryable = _unitOfWork.User_Event_Participations
                .GetQueryable()
                .Where(uep => eventIds.Contains(uep.EventId))
                .Include(uep => uep.User) // Eager load the User to avoid N+1 query issue
                .ThenInclude(user => user.Role); // Eager load Role from the User table

            var participants = await participantsQueryable.ToListAsync();

            // Group participants by EventId and project to ParticipantDto
            var participantsGrouped = participants
                .GroupBy(uep => uep.EventId)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(uep => new ParticipantDto
                    {
                        FirstName = uep.User.FirstName,
                        LastName = uep.User.LastName,
                        UserId = uep.User.Id.ToString(),
                        Role = uep.User.Role.Name, // Access Role from the User
                        RoleId = uep.User.RoleId, // Access RoleId from the User
                        UserName = uep.User.UserName
                    }).ToList()
                );

            // Get the total participants count for each event
            var totalParticipantsCountGrouped = participantsGrouped
                .ToDictionary(k => k.Key, v => v.Value.Count);

            // Fetch the requirements for each event using GetQueryable
            var eventRequirementsQueryable = _unitOfWork.Events_Requirements
                .GetQueryable()
                .Where(er => eventIds.Contains(er.EventId))
                .Include(er => er.Requirement); // Eager load the Requirement to avoid N+1 query issue

            var eventRequirements = await eventRequirementsQueryable.ToListAsync();

            // Group requirements by EventId
            var requirementsGrouped = eventRequirements
                .GroupBy(er => er.EventId)
                .ToDictionary(g => g.Key, g => g.Select(er => er.Requirement).ToList());

            // Transform events to DTOs and include participants, requirements, and total participant count
            var events = items.Select(e => new EventDto
            {
                Title = e.Title,
                Date = e.Date,
                EventId = e.Id,
                BodyText = e.BodyText,
                CreatorUserName = e.Creator.UserName,
                Address = e.Address,
                CreatedAt = e.CreatedAt,
                Participants = participantsGrouped.ContainsKey(e.Id) ? participantsGrouped[e.Id] : new List<ParticipantDto>(),
                Requirements = requirementsGrouped.ContainsKey(e.Id)
                    ? requirementsGrouped[e.Id].Select(r => new RequirementDto
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Description = r.Description
                    }).ToList()
                    : new List<RequirementDto>(),
                TotalParticipantsCount = totalParticipantsCountGrouped.ContainsKey(e.Id) ? totalParticipantsCountGrouped[e.Id] : 0 // Add the total participants count
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
            // Fetch the event entity by ID, including Creator and Address
            var eventEntity = await _unitOfWork.Events.GetByIdAsync(eventId);

            if (eventEntity == null)
                return null;

            // Fetch participants for the event, limiting to the required fields using GetQueryable
            var participantsQueryable = _unitOfWork.User_Event_Participations
                .GetQueryable()
                .Where(uep => uep.EventId == eventId)
                .Include(uep => uep.User) // Include the User for access to firstName, lastName, role, etc.
                .ThenInclude(user => user.Role); // Eager load the Role from the User table

            var participants = await participantsQueryable.ToListAsync();

            // Group participants and project to ParticipantDto
            var participantDtos = participants.Select(uep => new ParticipantDto
            {
                FirstName = uep.User.FirstName,
                LastName = uep.User.LastName,
                UserId = uep.User.Id.ToString(),
                Role = uep.User.Role.Name, // Access Role from the User
                RoleId = uep.User.RoleId, // Access RoleId from the User
                UserName = uep.User.UserName
            }).ToList();

            // Get the total count of participants
            var totalParticipantsCount = participantDtos.Count;

            // Fetch the event requirements for the specific event
            var eventRequirementsQueryable = _unitOfWork.Events_Requirements
                .GetQueryable()
                .Where(er => er.EventId == eventId)
                .Include(er => er.Requirement); // Eager load the Requirement to avoid N+1 query issue

            var eventRequirements = await eventRequirementsQueryable.ToListAsync();

            // Project the event requirements to RequirementDto
            var requirementsDtos = eventRequirements.Select(er => new RequirementDto
            {
                Id = er.Requirement.Id,
                Name = er.Requirement.Name,
                Description = er.Requirement.Description
            }).ToList();

            // Return the event DTO with populated participants and requirements
            return new EventDto
            {
                Title = eventEntity.Title,
                Date = eventEntity.Date,
                CreatedAt = eventEntity.CreatedAt,
                EventId = eventId,
                BodyText = eventEntity.BodyText,
                CreatorUserName = eventEntity.Creator.UserName,
                Address = eventEntity.Address,
                Participants = participantDtos, // Populate the Participants list with ParticipantDto
                Requirements = requirementsDtos, // Populate the Requirements list with RequirementDto
                TotalParticipantsCount = totalParticipantsCount // Add the total count of participants
            };
        }

        public async Task<EventDto?> CreateEventAsync(CreateEventDto createEventDto, string creatorId)
        {
            // Validate the district exists
            var district = await _unitOfWork.Districts.GetByIdAsync(createEventDto.DistrictId);
            if (district == null)
            {
                throw new ArgumentException("The specified district does not exist.");
            }

            // Fetch the creator's details


            // Fetch the creator's details
            var creator = await _userService.GetUserByIdAsync(creatorId);
            if (creator == null)
            {
                throw new ArgumentException("The specified creator does not exist.");
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
            await _unitOfWork.CompleteAsync(); // Ensure event is saved

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
                await _unitOfWork.CompleteAsync(); // Ensure event requirements are saved
            }

            // Fetch participants
            var userEventParticipations = await _unitOfWork.User_Event_Participations
                .FindAllAsync(uep => uep.EventId == eventEntity.Id);

            var userIds = userEventParticipations.Select(uep => uep.UserId).ToList();

            var participants = await _unitOfWork.Users
                .GetQueryable()
                .Where(user => userIds.Contains(user.Id))
                .Select(user => new ParticipantDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserId = user.Id.ToString(),
                    Role = user.Role.Name,
                    RoleId = user.RoleId,
                    UserName = creator.UserName
                }).ToListAsync();

            // Calculate the total number of participants
            var totalParticipantsCount = participants.Count;

            // Fetch requirements
            var eventRequirements = await _unitOfWork.Events_Requirements
                .FindAllAsync(er => er.EventId == eventEntity.Id);

            var requirementIds = eventRequirements.Select(er => er.RequirementId).ToList();

            var requirements = await _unitOfWork.Requirements
                .FindAllAsync(r => requirementIds.Contains(r.Id));

            // Return the event DTO
            return new EventDto
            {
                Title = eventEntity.Title,
                Date = eventEntity.Date,
                CreatedAt = eventEntity.CreatedAt,
                EventId = eventEntity.Id,
                BodyText = eventEntity.BodyText,
                CreatorUserName = creator.UserName,
                Address = eventEntity.Address,
                Participants = participants,
                Requirements = requirements.Select(r => new RequirementDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description
                }).ToList(),
                TotalParticipantsCount = totalParticipantsCount
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

            // Fetch the creator's details
            var creatorId = await GetEventOwnerId(eventId);
            if (creatorId == null)
            {
                throw new ArgumentException("The specified creator does not exist.");
            }

            var creatorUser = await _userService.GetUserByIdAsync(creatorId);

            if (creatorUser == null)
            {
                throw new ArgumentException("The specified creator does not exist.");
            }

            // Check if the address exists in the database
            var address = await _unitOfWork.Addresses.SingleOrDefaultAsync(a =>
                a.Name.ToLower() == updateEventDto.AddressName.ToLower() &&
                a.Street.ToLower() == updateEventDto.Street.ToLower() &&
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


            // Fetch participants
            var userEventParticipations = await _unitOfWork.User_Event_Participations
                .FindAllAsync(uep => uep.EventId == eventEntity.Id);

            var userIds = userEventParticipations.Select(uep => uep.UserId).ToList();

            var participants = await _unitOfWork.Users
                .GetQueryable()
                .Where(user => userIds.Contains(user.Id))
                .Select(user => new ParticipantDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserId = user.Id.ToString(),
                    Role = user.Role.Name,
                    RoleId = user.RoleId,
                    UserName = user.UserName
                }).ToListAsync();

            // Calculate the total number of participants
            var totalParticipantsCount = participants.Count;

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

            // Extract Requirement IDs from current requirements
            var currentRequirementIds = currentEventRequirements
                .Select(er => er.RequirementId)
                .ToList();

            // Find the requirements that need to be added (new requirements that are not in the current list)
            var requirementIdsToAdd = updateEventDto.RequirementIds
                .Where(id => !currentRequirementIds.Contains(id))
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

            // Fetch requirements
            var eventRequirements = await _unitOfWork.Events_Requirements
                .FindAllAsync(er => er.EventId == eventEntity.Id);

            var requirementIds = eventRequirements.Select(er => er.RequirementId).ToList();

            var requirements = await _unitOfWork.Requirements
                .FindAllAsync(r => requirementIds.Contains(r.Id));

            // Return the updated event as a DTO
            return new EventDto
            {
                Title = eventEntity.Title,
                Date = eventEntity.Date,
                CreatedAt = eventEntity.CreatedAt,
                EventId = eventEntity.Id,
                BodyText = eventEntity.BodyText,
                CreatorUserName = creatorUser.UserName,
                Address = eventEntity.Address,
                Participants = participants,
                Requirements = requirements.Select(r => new RequirementDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description
                }).ToList(),
                TotalParticipantsCount = totalParticipantsCount
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

        public async Task<string?> GetEventOwnerId(int eventId)
        {
            var eventEntity = await _unitOfWork.Events.GetByIdAsync(eventId);

            if (eventEntity == null)
                return null;

            if (eventEntity.CreatorId == null)
                throw new InvalidOperationException("The Creator is not loaded or does not exist.");

            return eventEntity.CreatorId;
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
                .GetQueryable()
                .AsNoTracking()  // Prevent tracking to avoid caching issues
                .FirstOrDefaultAsync(uep => uep.EventId == eventId && uep.UserId == userId);

            if (existingParticipation != null)
            {
                // The user is already participating in this event
                return false; // The user cannot join the event again
            }

            // Add the user to the event
            var userEventParticipation = new User_Event_Participations
            {
                EventId = eventId,
                UserId = userId,
                Event = eventEntity,
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
                .GetQueryable()
                .AsNoTracking()  // Prevent tracking to avoid caching issues
                .FirstOrDefaultAsync(uep => uep.EventId == eventId && uep.UserId == userId);

            if (existingParticipation == null)
            {
                // The user is not a participant in this event
                return false; // The user cannot leave an event they are not part of
            }

            // Remove the user from the event
            _unitOfWork.User_Event_Participations.Delete(existingParticipation);

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
