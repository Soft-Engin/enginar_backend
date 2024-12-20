using BackEngin.Services;
using BackEngin.Tests.Utils;
using DataAccess.Repositories;
using FluentAssertions;
using Models;
using Models.DTO;
using Moq;
using MockQueryable.Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System.Linq.Expressions;

namespace BackEngin.Tests.Services
{
    public class EventServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly EventService _eventService;

        public EventServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _eventService = new EventService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task CreateEvent_ShouldAddNewEvent()
        {
            // Arrange
            var creatorId = "user123";
            var createEventDto = new CreateEventDto
            {
                Title = "Hackathon",
                BodyText = "Annual coding event",
                Date = DateTime.UtcNow.AddDays(10),
                CreatedAt = DateTime.UtcNow,
                DistrictId = 1,
                AddressName = "Tech Park",
                Street = "Innovation Ave",
                RequirementIds = new List<int> { 1, 2 }
            };

            // Mock the entities to be used in the service method
            var district = new Districts { Id = 1, Name = "Downtown", CityId = 1 }; // Mock district
            var address = new Addresses
            {
                Id = 1,
                Name = "Tech Park",
                Street = "Innovation Ave",
                DistrictId = 1,
                District = new Districts
                {
                    Id = 1,
                    Name = "Downtown",
                    City = new Cities
                    {
                        Id = 1,
                        Name = "Metropolis",
                        Country = new Countries
                        {
                            Id = 1,
                            Name = "Countryland"
                        }
                    }
                }
            };

            var user = new Users { Id = creatorId, UserName = "user123", FirstName = "John", LastName = "Doe" }; // Mock user
            var requirement = new Requirements { Id = 1, Name = "Requirement 1", Description = "Description" }; // Mock requirement
            var eventRequirements = new List<Events_Requirements>
            {
                new Events_Requirements { EventId = 1, RequirementId = 1 }
            };
            var userEventParticipations = new List<User_Event_Participations>
            {
                new User_Event_Participations { UserId = creatorId, EventId = 1 }
            };

            var createdEvent = new Events
            {
                Id = 1,
                Title = createEventDto.Title,
                BodyText = createEventDto.BodyText,
                Date = createEventDto.Date,
                CreatedAt = createEventDto.CreatedAt,
                AddressId = 1,
                CreatorId = creatorId,
                Creator = user // Ensure Creator is set
            };


            // Mock all the UnitOfWork methods
            _mockUnitOfWork.Setup(u => u.Districts.GetByIdAsync(createEventDto.DistrictId)).ReturnsAsync(district); // Mock district
            _mockUnitOfWork.Setup(u => u.Addresses.SingleOrDefaultAsync(It.IsAny<Func<Addresses, bool>>())).ReturnsAsync(address); // Mock address
            _mockUnitOfWork.Setup(u => u.Users.FindAllAsync(It.IsAny<Func<Users, bool>>()))
                            .ReturnsAsync(new List<Users> { user }); // Mock user
                                             // Mock creator
            _mockUnitOfWork.Setup(u => u.Requirements.GetRangeByIdsAsync(It.IsAny<List<int>>())).ReturnsAsync(new List<Requirements> { requirement }); // Mock requirements
            _mockUnitOfWork.Setup(u => u.User_Event_Participations.FindAllAsync(It.IsAny<Func<User_Event_Participations, bool>>()))
                .Returns(Task.FromResult(userEventParticipations.AsQueryable()));

            _mockUnitOfWork.Setup(u => u.Events_Requirements.FindAllAsync(It.IsAny<Func<Events_Requirements, bool>>())).ReturnsAsync(eventRequirements); // Mock event requirements
            _mockUnitOfWork.Setup(u => u.Events.AddAsync(It.IsAny<Events>())).Returns(Task.CompletedTask); // Mock AddAsync for event
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1); // Mock CompleteAsync

            // Act
            var result = await _eventService.CreateEventAsync(createEventDto, creatorId);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("Hackathon");
            result.BodyText.Should().Be("Annual coding event");
            _mockUnitOfWork.Verify(u => u.Events.AddAsync(It.IsAny<Events>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }



        [Fact]
        public async Task UpdateEvent_ShouldModifyExistingEvent()
        {
            // Arrange
            var eventId = 1;
            var updateEventDto = new UpdateEventDto
            {
                Title = "Updated Hackathon",
                BodyText = "Updated description",
                Date = DateTime.UtcNow.AddDays(20),
                DistrictId = 2,
                AddressName = "New Tech Park",
                Street = "Future Ave",
                RequirementIds = new List<int> { 3, 4 }
            };

            var existingEvent = new Events
            {
                Id = eventId,
                Title = "Hackathon",
                BodyText = "Annual coding event",
                Date = DateTime.UtcNow.AddDays(10),
                CreatedAt = DateTime.UtcNow,
                AddressId = 1,
                CreatorId = "user123"
            };

            _mockUnitOfWork.Setup(u => u.Events.GetByIdAsync(eventId)).ReturnsAsync(existingEvent);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _eventService.UpdateEventAsync(eventId, updateEventDto);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("Updated Hackathon");
            result.BodyText.Should().Be("Updated description");
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteEvent_ShouldRemoveEvent()
        {
            // Arrange
            var eventId = 1;
            var existingEvent = new Events
            {
                Id = eventId,
                Title = "Hackathon",
                BodyText = "Annual coding event",
                Date = DateTime.UtcNow.AddDays(10),
                CreatedAt = DateTime.UtcNow,
                AddressId = 1,
                CreatorId = "user123"
            };

            _mockUnitOfWork.Setup(u => u.Events.GetByIdAsync(eventId)).ReturnsAsync(existingEvent);
            _mockUnitOfWork.Setup(u => u.Events.Delete(existingEvent)).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _eventService.DeleteEventAsync(eventId);

            // Assert
            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.Events.Delete(existingEvent), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        /*

        [Fact]
        public async Task GetEventById_ShouldReturnEventDetails()
        {
            // Arrange
            var eventId = 1;
            var existingEvent = new EventDto
            {
                Title = "Hackathon",
                BodyText = "Annual coding event",
                Date = DateTime.UtcNow.AddDays(10),
                CreatedAt = DateTime.UtcNow,
                CreatorUserName = "user123",
                Address = new Addresses
                {
                    Name = "Home",
                    Street = "123 Main St",
                    District = new Districts
                    {
                        Name = "Downtown",
                        City = new Cities
                        {
                            Name = "Metropolis",
                            Country = new Countries
                            {
                                Name = "Countryland"
                            }
                        }
                    }
                },
            };

            _mockUnitOfWork.Setup(u => u.Events.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(existingEvent);

            // Act
            var result = await _eventService.GetEventByIdAsync(eventId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(eventId);
            result.Title.Should().Be("Hackathon");
        }
        */

        [Fact]
        public async Task GetEventById_ShouldReturnEventDetails()
        {
            // Arrange
            var eventId = 1;
            var existingEvent = new Events
            {
                Id = eventId,
                Title = "Hackathon",
                BodyText = "Annual coding event",
                Date = DateTime.UtcNow.AddDays(10),
                CreatedAt = DateTime.UtcNow,
                AddressId = 1,
                CreatorId = "user123",
                Creator = new Users // Mock Creator
                {
                    Id = "user123",
                    UserName = "user123",
                    FirstName = "John",
                    LastName = "Doe"
                },
                Address = new Addresses
                {
                    Name = "Home",
                    Street = "123 Main St",
                    District = new Districts
                    {
                        Name = "Downtown",
                        City = new Cities
                        {
                            Name = "Metropolis",
                            Country = new Countries
                            {
                                Name = "Countryland"
                            }
                        }
                    }
                },
            };

            var userEventParticipations = new List<User_Event_Participations>
            {
                new User_Event_Participations { UserId = "user123", EventId = eventId }
            };

            var participants = new List<Users>
            {
                new Users
                {
                    Id = "user123",
                    UserName = "user123",
                    FirstName = "John",
                    LastName = "Doe"
                }
            };

            // Mock required methods
            _mockUnitOfWork.Setup(u => u.Events.GetByIdAsync(eventId)).ReturnsAsync(existingEvent);

            _mockUnitOfWork.Setup(u => u.User_Event_Participations.FindAllAsync(It.IsAny<Func<User_Event_Participations, bool>>()))
                .ReturnsAsync(userEventParticipations.AsQueryable());


            _mockUnitOfWork.Setup(u => u.Users.FindAllAsync(It.IsAny<Func<Users, bool>>()))
                .ReturnsAsync(participants);


            // Mock the Requirements (if any)
            var eventRequirements = new List<Events_Requirements>
            {
                new Events_Requirements { EventId = eventId, RequirementId = 1 }
            };

            var requirements = new List<Requirements>
            {
                new Requirements { Id = 1, Name = "Skill 1", Description = "Coding expertise" }
            };

            _mockUnitOfWork.Setup(u => u.Events_Requirements.FindAllAsync(It.IsAny<Func<Events_Requirements, bool>>()))
                .ReturnsAsync(eventRequirements);


            // Mocking Requirements FindAllAsync with Expression
            var requirementPredicate = (Expression<Func<Requirements, bool>>)(r => r.Id == 1);  // No need for casting
            _mockUnitOfWork.Setup(u => u.Requirements.FindAllAsync(requirementPredicate, It.IsAny<Func<IQueryable<Requirements>, IQueryable<Requirements>>>()))
                .ReturnsAsync(requirements);


            // Act
            var result = await _eventService.GetEventByIdAsync(eventId);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("Hackathon");
            result.BodyText.Should().Be("Annual coding event");
            result.CreatorUserName.Should().Be("user123");
            result.Address.Name.Should().Be("Home");  // Fixed to match the mock Address Name
            result.Participants.Should().HaveCount(1);
            result.Requirements.Should().HaveCount(1);
            result.Requirements.First().Name.Should().Be("Skill 1");
        }



        [Fact]
        public async Task JoinToEvent_ShouldAddUserToEvent()
        {
            // Arrange
            var eventId = 1;
            var userId = "user123";
            var existingEvent = new Events
            {
                Id = eventId,
                Title = "Hackathon",
                BodyText = "Annual coding event",
                Date = DateTime.UtcNow.AddDays(10),
                CreatedAt = DateTime.UtcNow,
                AddressId = 1,
                CreatorId = "creator456"
            };

            _mockUnitOfWork.Setup(u => u.Events.GetByIdAsync(eventId)).ReturnsAsync(existingEvent);
            _mockUnitOfWork.Setup(u => u.User_Event_Participations.AddAsync(It.IsAny<User_Event_Participations>())).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _eventService.JoinToEventAsync(eventId, userId);

            // Assert
            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.User_Event_Participations.AddAsync(It.IsAny<User_Event_Participations>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task LeaveEvent_ShouldRemoveUserFromEvent()
        {
            // Arrange
            var eventId = 1;
            var userId = "user123";
            var participation = new User_Event_Participations
            {
                EventId = eventId,
                UserId = userId
            };

            _mockUnitOfWork.Setup(u => u.User_Event_Participations.FindAsync(It.IsAny<Func<User_Event_Participations, bool>>()))
                           .ReturnsAsync(new List<User_Event_Participations> { participation });
            _mockUnitOfWork.Setup(u => u.User_Event_Participations.Delete(participation)).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _eventService.LeaveEventAsync(eventId, userId);

            // Assert
            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.User_Event_Participations.Delete(participation), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }
    }
}

        
