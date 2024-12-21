using BackEngin.Services;
using BackEngin.Services.Interfaces;
using DataAccess.Repositories;
using FluentAssertions;
using Moq;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

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

        #region GetAllEventsAsync

        [Fact]
        public async Task GetAllEventsAsync_ShouldReturnPaginatedEvents()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;

            var mockEvents = new List<Events>
            {
                new Events { Id = 1, Title = "Event 1" },
                new Events { Id = 2, Title = "Event 2" },
            };

            // The EventService calls:
            // _unitOfWork.Events.GetPaginatedAsync(
            //     "Creator,Address,Address.District",
            //     null,
            //     pageNumber, 
            //     pageSize
            // );
            _mockUnitOfWork.Setup(u => u.Events.GetPaginatedAsync(
                    It.Is<string>(s => s == "Creator,Address,Address.District"),
                    It.Is<Expression<Func<Events, bool>>>(f => f == null),
                    pageNumber,
                    pageSize
                ))
                .ReturnsAsync((mockEvents, mockEvents.Count));

            // Act
            var result = await _eventService.GetAllEventsAsync(pageNumber, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(2);
            result.TotalCount.Should().Be(2);
            result.Items.First().Title.Should().Be("Event 1");

            // Optional: Verify the call
            _mockUnitOfWork.Verify(u => u.Events.GetPaginatedAsync(
                "Creator,Address,Address.District",
                null,
                pageNumber,
                pageSize
            ), Times.Once);
        }

        [Fact]
        public async Task GetAllEventsAsync_ShouldReturnEmpty_WhenNoEvents()
        {
            // Arrange
            _mockUnitOfWork.Setup(u => u.Events.GetPaginatedAsync(
                    "Creator,Address,Address.District",
                    null,
                    It.IsAny<int>(),
                    It.IsAny<int>()
                ))
                .ReturnsAsync((Enumerable.Empty<Events>(), 0));

            // Act
            var result = await _eventService.GetAllEventsAsync(1, 10);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().BeEmpty();
            result.TotalCount.Should().Be(0);
        }

        #endregion

        #region GetEventByIdAsync

        [Fact]
        public async Task GetEventByIdAsync_ShouldReturnEvent_WhenFound()
        {
            // Arrange
            int eventId = 1;
            var mockEvent = new Events
            {
                Id = eventId,
                Title = "Sample Event",
                CreatorId = "creatorId"
            };

            _mockUnitOfWork.Setup(u => u.Events.GetByIdAsync(eventId))
                .ReturnsAsync(mockEvent);

            // Act
            var result = await _eventService.GetEventByIdAsync(eventId);

            // Assert
            result.Should().NotBeNull();
            result.EventId.Should().Be(eventId);
            result.Title.Should().Be("Sample Event");
        }

        [Fact]
        public async Task GetEventByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            int eventId = 999;
            _mockUnitOfWork.Setup(u => u.Events.GetByIdAsync(eventId))
                .ReturnsAsync((Events)null);

            // Act
            var result = await _eventService.GetEventByIdAsync(eventId);

            // Assert
            result.Should().BeNull();
        }

        #endregion

        #region CreateEventAsync

        [Fact]
        public async Task CreateEventAsync_ShouldCreateEvent_WhenValid()
        {
            // Arrange
            var dto = new CreateEventDTO
            {
                Title = "New Event",
                BodyText = "Some body text",
                Date = DateTime.UtcNow.AddDays(1),
                CreatedAt = DateTime.UtcNow,
                DistrictId = 10,
                AddressName = "Event Location",
                Street = "123 Main St",
                RequirementIds = new List<int> { 1, 2 }
            };
            var creatorId = "creator1";
            var creatorName = "Creator Name";

            // District is found
            _mockUnitOfWork.Setup(u => u.Districts.GetByIdAsync(dto.DistrictId))
                .ReturnsAsync(new Districts { Id = dto.DistrictId });

            // Requirement count matches
            _mockUnitOfWork.Setup(u => u.Requirements.CountAsync(
                It.IsAny<Expression<Func<Requirements, bool>>>()))
                .ReturnsAsync(dto.RequirementIds.Count);

            // No existing address found => returns null
            _mockUnitOfWork.Setup(u => u.Addresses.SingleOrDefaultAsync(
                It.IsAny<Func<Addresses, bool>>()))
                .ReturnsAsync((Addresses)null);

            // Mock adding the new event
            _mockUnitOfWork.Setup(u => u.Events.AddAsync(It.IsAny<Events>()))
                .Returns(Task.CompletedTask);

            // Mock adding event-participation
            _mockUnitOfWork.Setup(u => u.User_Event_Participations.AddAsync(It.IsAny<User_Event_Participations>()))
                .Returns(Task.CompletedTask);

            // Mock adding the event-requirements
            _mockUnitOfWork.Setup(u => u.Events_Requirements.AddRangeAsync(
                It.IsAny<IEnumerable<Events_Requirements>>()))
                .Returns(Task.CompletedTask);



            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _eventService.CreateEventAsync(dto, creatorId, creatorName);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("New Event");
            result.BodyText.Should().Be("Some body text");

            // Verify calls:
            _mockUnitOfWork.Verify(u => u.Districts.GetByIdAsync(10), Times.Once);
            _mockUnitOfWork.Verify(u => u.Events.AddAsync(It.IsAny<Events>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.AtLeast(1));
        }

        [Fact]
        public async Task CreateEventAsync_ShouldThrow_WhenDistrictNotFound()
        {
            // Arrange
            var dto = new CreateEventDTO
            {
                DistrictId = 999,
                Date = DateTime.UtcNow.AddDays(1)
            };
            _mockUnitOfWork.Setup(u => u.Districts.GetByIdAsync(dto.DistrictId))
                .ReturnsAsync((Districts)null);

            // Act
            Func<Task> act = () => _eventService.CreateEventAsync(dto, "creatorId", "creatorName");

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("The specified district does not exist.");
        }

        [Fact]
        public async Task CreateEventAsync_ShouldThrow_WhenDateIsInPast()
        {
            // Arrange
            var dto = new CreateEventDTO
            {
                DistrictId = 10,
                Date = DateTime.UtcNow.AddDays(-1)
            };

            _mockUnitOfWork.Setup(u => u.Districts.GetByIdAsync(dto.DistrictId))
                .ReturnsAsync(new Districts { Id = dto.DistrictId });

            // Act
            Func<Task> act = () => _eventService.CreateEventAsync(dto, "creatorId", "creatorName");

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("The event can not be set for the past.");
        }

        #endregion

        #region UpdateEventAsync

        [Fact]
        public async Task UpdateEventAsync_ShouldUpdateEvent_WhenValid()
        {
            // Arrange
            int eventId = 1;
            var dto = new UpdateEventDTO
            {
                Title = "Updated Title",
                BodyText = "Updated Body",
                Date = DateTime.UtcNow.AddDays(2),
                DistrictId = 10,
                AddressName = "New Address",
                Street = "New Street",
                RequirementIds = new List<int> { 1, 2 }
            };

            var existingEvent = new Events
            {
                Id = eventId,
                CreatorId = "creatorId",
                Address = new Addresses { Id = 101, DistrictId = 10 }
            };

            _mockUnitOfWork.Setup(u => u.Events.FindAsync(
                    It.IsAny<Expression<Func<Events, bool>>>(),
                    "Creator,Address"
                ))
                .ReturnsAsync(new List<Events> { existingEvent });

            // District found
            _mockUnitOfWork.Setup(u => u.Districts.GetByIdAsync(dto.DistrictId))
                .ReturnsAsync(new Districts { Id = dto.DistrictId });

            // Requirements exist
            _mockUnitOfWork.Setup(u => u.Requirements.CountAsync(It.IsAny<Expression<Func<Requirements, bool>>>()))
                .ReturnsAsync(dto.RequirementIds.Count);

            // Current event-requirements
            _mockUnitOfWork.Setup(u => u.Events_Requirements.FindAsync(
                    It.IsAny<Func<Events_Requirements, bool>>()
                ))
                .ReturnsAsync(new List<Events_Requirements>()); // no existing => all new

            _mockUnitOfWork.Setup(u => u.Events.Update(It.IsAny<Events>())).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _eventService.UpdateEventAsync(eventId, dto);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("Updated Title");

            _mockUnitOfWork.Verify(u => u.Events.Update(existingEvent), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Exactly(2));
            // (One for the address + event changes, another for the requirement updates, etc.)
        }

        [Fact]
        public async Task UpdateEventAsync_ShouldThrow_WhenEventNotFound()
        {
            // Arrange
            int eventId = 999;
            _mockUnitOfWork.Setup(u => u.Events.FindAsync(
                    It.IsAny<Expression<Func<Events, bool>>>(),
                    "Creator,Address"
                ))
                .ReturnsAsync(new List<Events>()); // empty => not found

            var dto = new UpdateEventDTO { Title = "Doesn't matter" };

            // Act
            Func<Task> act = () => _eventService.UpdateEventAsync(eventId, dto);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("The specified event does not exist.");
        }

        #endregion

        #region DeleteEventAsync

        [Fact]
        public async Task DeleteEventAsync_ShouldReturnTrue_WhenEventExists()
        {
            // Arrange
            int eventId = 123;
            var existingEvent = new Events { Id = eventId };

            _mockUnitOfWork.Setup(u => u.Events.GetByIdAsync(eventId))
                .ReturnsAsync(existingEvent);
            _mockUnitOfWork.Setup(u => u.Events.Delete(existingEvent)).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _eventService.DeleteEventAsync(eventId);

            // Assert
            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.Events.Delete(existingEvent), Times.Once);
        }

        [Fact]
        public async Task DeleteEventAsync_ShouldReturnFalse_WhenEventNotFound()
        {
            // Arrange
            int eventId = 999;
            _mockUnitOfWork.Setup(u => u.Events.GetByIdAsync(eventId))
                .ReturnsAsync((Events)null);

            // Act
            var result = await _eventService.DeleteEventAsync(eventId);

            // Assert
            result.Should().BeFalse();
            _mockUnitOfWork.Verify(u => u.Events.Delete(It.IsAny<Events>()), Times.Never);
        }

        #endregion

        #region ToggleAttendToEventAsync

        [Fact]
        public async Task ToggleAttendToEventAsync_ShouldJoin_WhenNotParticipating()
        {
            // Arrange
            int eventId = 5;
            string userId = "userABC";
            var existingEvent = new Events
            {
                Id = eventId,
                CreatorId = "someoneElse",
                Date = DateTime.UtcNow.AddDays(1)
            };
            _mockUnitOfWork.Setup(u => u.Events.GetByIdAsync(eventId))
                .ReturnsAsync(existingEvent);

            // No existing participation => empty list
            _mockUnitOfWork.Setup(u => u.User_Event_Participations.FindAsync(
                It.IsAny<Func<User_Event_Participations, bool>>()))
                .ReturnsAsync(new List<User_Event_Participations>());

            _mockUnitOfWork.Setup(u => u.User_Event_Participations.AddAsync(It.IsAny<User_Event_Participations>()))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _eventService.ToggleAttendToEventAsync(eventId, userId);

            // Assert
            result.Should().BeTrue(); // joined
            _mockUnitOfWork.Verify(u => u.User_Event_Participations.AddAsync(It.IsAny<User_Event_Participations>()), Times.Once);
        }

        [Fact]
        public async Task ToggleAttendToEventAsync_ShouldLeave_WhenAlreadyParticipating()
        {
            // Arrange
            int eventId = 5;
            string userId = "userABC";
            var existingEvent = new Events
            {
                Id = eventId,
                CreatorId = "someoneElse",
                Date = DateTime.UtcNow.AddDays(1)
            };
            var existingParticipation = new User_Event_Participations { EventId = eventId, UserId = userId };

            _mockUnitOfWork.Setup(u => u.Events.GetByIdAsync(eventId))
                .ReturnsAsync(existingEvent);

            _mockUnitOfWork.Setup(u => u.User_Event_Participations.FindAsync(
                It.IsAny<Func<User_Event_Participations, bool>>()))
                .ReturnsAsync(new List<User_Event_Participations> { existingParticipation });

            _mockUnitOfWork.Setup(u => u.User_Event_Participations.Delete(existingParticipation));
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _eventService.ToggleAttendToEventAsync(eventId, userId);

            // Assert
            result.Should().BeFalse(); // left
            _mockUnitOfWork.Verify(u => u.User_Event_Participations.Delete(existingParticipation), Times.Once);
        }

        #endregion

        #region GetPaginatedParticipantsAsync

        [Fact]
        public async Task GetPaginatedParticipantsAsync_ShouldReturnPaginatedParticipants()
        {
            // Arrange
            int eventId = 77;
            int page = 1;
            int pageSize = 10;

            var participants = new List<User_Event_Participations>
            {
                new User_Event_Participations
                {
                    EventId = eventId,
                    UserId = "u1",
                    User = new Users { Id = "u1", UserName = "UserOne" }
                },
                new User_Event_Participations
                {
                    EventId = eventId,
                    UserId = "u2",
                    User = new Users { Id = "u2", UserName = "UserTwo" }
                }
            };

            // The service calls:
            // _unitOfWork.User_Event_Participations.GetPaginatedAsync(
            //     "User", 
            //     uep => uep.EventId == eventId,
            //     page,
            //     pageSize
            // );
            _mockUnitOfWork.Setup(u => u.User_Event_Participations.GetPaginatedAsync(
                    It.Is<string>(s => s == "User"),
                    It.IsAny<Expression<Func<User_Event_Participations, bool>>>(),
                    page,
                    pageSize
                ))
                .ReturnsAsync((participants, participants.Count));

            // Act
            var result = await _eventService.GetPaginatedParticipantsAsync(eventId, page, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(2);
            result.TotalCount.Should().Be(2);
            result.Items.First().UserName.Should().Be("UserOne");
        }

        #endregion

        // ... Additional tests for GetAllRequirementsAsync, GetDistrictsByCityIdAsync, etc. ...
    }
}
