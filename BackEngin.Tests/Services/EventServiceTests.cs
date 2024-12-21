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

            // Create mock objects with proper relationships
            var mockDistrict = new Districts
            {
                Id = 1,
                Name = "Test District",
                PostCode = 12345,
                CityId = 1,
            };

            var mockCity = new Cities
            {
                Id = 1,
                Name = "Test City",
                CountryId = 1
            };

            var mockCountry = new Countries
            {
                Id = 1,
                Name = "Test Country"
            };

            mockDistrict.City = mockCity;
            mockCity.Country = mockCountry;

            var mockAddress = new Addresses
            {
                Id = 1,
                Name = "Test Address",
                Street = "Test Street",
                DistrictId = 1,
                District = mockDistrict
            };

            var mockCreator = new Users
            {
                Id = "creator1",
                UserName = "Creator Name"
            };

            var mockEvents = new List<Events>
            {
                new Events
                {
                    Id = 1,
                    Title = "Event 1",
                    CreatorId = "creator1",
                    Creator = mockCreator,
                    AddressId = 1,
                    Address = mockAddress,
                    Date = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                },
                new Events
                {
                    Id = 2,
                    Title = "Event 2",
                    CreatorId = "creator1",
                    Creator = mockCreator,
                    AddressId = 1,
                    Address = mockAddress,
                    Date = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                },
            };

            // Setup all necessary mocks
            _mockUnitOfWork.Setup(u => u.Events.GetPaginatedAsync(
                    It.Is<string>(s => s == "Creator,Address,Address.District"),
                    It.Is<Expression<Func<Events, bool>>>(f => f == null),
                    pageNumber,
                    pageSize
                ))
                .ReturnsAsync((mockEvents, mockEvents.Count));

            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
                .ReturnsAsync(new List<Users> { mockCreator });

            _mockUnitOfWork.Setup(u => u.Addresses.FindAsync(It.IsAny<Func<Addresses, bool>>()))
                .ReturnsAsync(new List<Addresses> { mockAddress });

            _mockUnitOfWork.Setup(u => u.Districts.FindAsync(It.IsAny<Func<Districts, bool>>()))
                .ReturnsAsync(new List<Districts> { mockDistrict });

            _mockUnitOfWork.Setup(u => u.Cities.FindAsync(It.IsAny<Func<Cities, bool>>()))
                .ReturnsAsync(new List<Cities> { mockCity });

            _mockUnitOfWork.Setup(u => u.Countries.FindAsync(It.IsAny<Func<Countries, bool>>()))
                .ReturnsAsync(new List<Countries> { mockCountry });

            _mockUnitOfWork.Setup(u => u.Events_Requirements.FindAsync(
                It.IsAny<Expression<Func<Events_Requirements, bool>>>(),
                It.IsAny<string>()))
                .ReturnsAsync(new List<Events_Requirements>());

            _mockUnitOfWork.Setup(u => u.User_Event_Participations.CountAsync(
                It.IsAny<Expression<Func<User_Event_Participations, bool>>>()))
                .ReturnsAsync(0);

            // Act
            var result = await _eventService.GetAllEventsAsync(pageNumber, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(2);
            result.TotalCount.Should().Be(2);
            result.Items.First().Title.Should().Be("Event 1");
            result.Items.First().CreatorUserName.Should().Be("Creator Name");
            result.Items.First().Address.Should().NotBeNull();
            result.Items.First().Address.District.Should().NotBeNull();
            result.Items.First().Address.District.City.Should().NotBeNull();
            result.Items.First().Address.District.City.Country.Should().NotBeNull();

            // Verify the call
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
                CreatorId = "creatorId",
                AddressId = 1,
                BodyText = "Sample body text",
                Date = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            var mockCreator = new Users
            {
                Id = "creatorId",
                UserName = "TestUser"
            };

            var mockDistrict = new Districts
            {
                Id = 1,
                Name = "Test District",
                PostCode = 12345,
                CityId = 1
            };

            var mockCity = new Cities
            {
                Id = 1,
                Name = "Test City",
                CountryId = 1
            };

            var mockCountry = new Countries
            {
                Id = 1,
                Name = "Test Country"
            };

            var mockAddress = new Addresses
            {
                Id = 1,
                Name = "Test Address",
                Street = "Test Street",
                DistrictId = 1,
                District = mockDistrict
            };

            mockDistrict.City = mockCity;
            mockCity.Country = mockCountry;

            // Setup all necessary mocks
            _mockUnitOfWork.Setup(u => u.Events.GetByIdAsync(eventId))
                .ReturnsAsync(mockEvent);

            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
                .ReturnsAsync(new List<Users> { mockCreator });

            _mockUnitOfWork.Setup(u => u.Addresses.FindAsync(It.IsAny<Func<Addresses, bool>>()))
                .ReturnsAsync(new List<Addresses> { mockAddress });

            _mockUnitOfWork.Setup(u => u.Districts.FindAsync(It.IsAny<Func<Districts, bool>>()))
                .ReturnsAsync(new List<Districts> { mockDistrict });

            _mockUnitOfWork.Setup(u => u.Cities.FindAsync(It.IsAny<Func<Cities, bool>>()))
                .ReturnsAsync(new List<Cities> { mockCity });

            _mockUnitOfWork.Setup(u => u.Countries.FindAsync(It.IsAny<Func<Countries, bool>>()))
                .ReturnsAsync(new List<Countries> { mockCountry });

            _mockUnitOfWork.Setup(u => u.Events_Requirements.FindAsync(
                It.IsAny<Expression<Func<Events_Requirements, bool>>>(),
                It.IsAny<string>()))
                .ReturnsAsync(new List<Events_Requirements>());

            _mockUnitOfWork.Setup(u => u.User_Event_Participations.CountAsync(
                It.IsAny<Expression<Func<User_Event_Participations, bool>>>()))
                .ReturnsAsync(0);

            // Act
            var result = await _eventService.GetEventByIdAsync(eventId);

            // Assert
            result.Should().NotBeNull();
            result.EventId.Should().Be(eventId);
            result.Title.Should().Be("Sample Event");
            result.CreatorUserName.Should().Be("TestUser");
            result.Address.Should().NotBeNull();
            result.Address.Name.Should().Be("Test Address");
            result.Address.District.Should().NotBeNull();
            result.Address.District.City.Should().NotBeNull();
            result.Address.District.City.Country.Should().NotBeNull();
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

            // Setup mock objects
            var mockDistrict = new Districts
            {
                Id = dto.DistrictId,
                Name = "Test District",
                CityId = 1
            };

            var mockCity = new Cities
            {
                Id = 1,
                Name = "Test City",
                CountryId = 1
            };

            var mockCountry = new Countries
            {
                Id = 1,
                Name = "Test Country"
            };

            mockDistrict.City = mockCity;
            mockCity.Country = mockCountry;

            // Mock creator
            var mockCreator = new Users
            {
                Id = creatorId,
                UserName = creatorName
            };

            // Setup all necessary mocks
            _mockUnitOfWork.Setup(u => u.Districts.GetByIdAsync(dto.DistrictId))
                .ReturnsAsync(mockDistrict);

            _mockUnitOfWork.Setup(u => u.Requirements.CountAsync(
                It.IsAny<Expression<Func<Requirements, bool>>>()))
                .ReturnsAsync(dto.RequirementIds.Count);

            _mockUnitOfWork.Setup(u => u.Addresses.SingleOrDefaultAsync(
                It.IsAny<Func<Addresses, bool>>()))
                .ReturnsAsync((Addresses)null);

            // Mock finding the creator
            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
                .ReturnsAsync(new List<Users> { mockCreator });

            // Mock finding the district
            _mockUnitOfWork.Setup(u => u.Districts.FindAsync(It.IsAny<Func<Districts, bool>>()))
                .ReturnsAsync(new List<Districts> { mockDistrict });

            // Mock finding the city
            _mockUnitOfWork.Setup(u => u.Cities.FindAsync(It.IsAny<Func<Cities, bool>>()))
                .ReturnsAsync(new List<Cities> { mockCity });

            // Mock finding the country
            _mockUnitOfWork.Setup(u => u.Countries.FindAsync(It.IsAny<Func<Countries, bool>>()))
                .ReturnsAsync(new List<Countries> { mockCountry });

            // Mock event requirements
            _mockUnitOfWork.Setup(u => u.Events_Requirements.FindAsync(
                It.IsAny<Expression<Func<Events_Requirements, bool>>>(),
                It.IsAny<string>()))
                .ReturnsAsync(new List<Events_Requirements>());

            // Mock participants count
            _mockUnitOfWork.Setup(u => u.User_Event_Participations.CountAsync(
                It.IsAny<Expression<Func<User_Event_Participations, bool>>>()))
                .ReturnsAsync(0);

            // Setup mock objects with proper relationships
            var mockEvent = new Events
            {
                Id = 1,
                Title = "New Event",
                BodyText = "Some body text",
                CreatorId = creatorId,
                Creator = mockCreator,
                AddressId = 1,
                Address = new Addresses
                {
                    Id = 1,
                    Name = "Event Location",
                    Street = "123 Main St",
                    DistrictId = dto.DistrictId,
                    District = mockDistrict
                }
            };

            // When Events.AddAsync is called, capture the event and return it in subsequent queries
            Events capturedEvent = null;
            _mockUnitOfWork.Setup(u => u.Events.AddAsync(It.IsAny<Events>()))
                .Callback<Events>(e =>
                {
                    capturedEvent = e;
                    capturedEvent.Creator = mockCreator;
                    capturedEvent.Address = mockEvent.Address;
                })
                .Returns(Task.CompletedTask);

            // Setup all the Find operations to return the proper objects
            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.Is<Func<Users, bool>>(x => true)))
                .ReturnsAsync(new List<Users> { mockCreator });

            _mockUnitOfWork.Setup(u => u.Addresses.FindAsync(It.Is<Func<Addresses, bool>>(x => true)))
                .ReturnsAsync(new List<Addresses> { mockEvent.Address });

            _mockUnitOfWork.Setup(u => u.Districts.FindAsync(It.Is<Func<Districts, bool>>(x => true)))
                .ReturnsAsync(new List<Districts> { mockDistrict });

            _mockUnitOfWork.Setup(u => u.Cities.FindAsync(It.Is<Func<Cities, bool>>(x => true)))
                .ReturnsAsync(new List<Cities> { mockCity });

            _mockUnitOfWork.Setup(u => u.Countries.FindAsync(It.Is<Func<Countries, bool>>(x => true)))
                .ReturnsAsync(new List<Countries> { mockCountry });

            // Keep existing mock setups but update them to use It.Is<Func<T, bool>> consistently
            _mockUnitOfWork.Setup(u => u.Events_Requirements.FindAsync(
                It.IsAny<Expression<Func<Events_Requirements, bool>>>(),
                It.IsAny<string>()))
                .ReturnsAsync(new List<Events_Requirements>());

            _mockUnitOfWork.Setup(u => u.User_Event_Participations.CountAsync(
                It.IsAny<Expression<Func<User_Event_Participations, bool>>>()))
                .ReturnsAsync(0);

            _mockUnitOfWork.Setup(u => u.Events.AddAsync(It.IsAny<Events>()))
                .Returns(Task.CompletedTask);

            _mockUnitOfWork.Setup(u => u.User_Event_Participations.AddAsync(It.IsAny<User_Event_Participations>()))
                .Returns(Task.CompletedTask);

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
            result.CreatorUserName.Should().Be(creatorName);
            result.CreatorId.Should().Be(creatorId);
            result.Address.Should().NotBeNull();
            result.Address.Name.Should().Be("Event Location");

            // Verify calls
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

            var mockDistrict = new Districts { Id = 10, Name = "Test District", CityId = 1 };
            var mockCity = new Cities { Id = 1, Name = "Test City", CountryId = 1 };
            var mockCountry = new Countries { Id = 1, Name = "Test Country" };
            mockDistrict.City = mockCity;
            mockCity.Country = mockCountry;

            var existingCreator = new Users { Id = "creatorId", UserName = "Creator Name" };
            var existingEvent = new Events
            {
                Id = eventId,
                Title = "Old Title",
                BodyText = "Old Body",
                CreatorId = "creatorId",
                Creator = existingCreator,
                Date = DateTime.UtcNow.AddDays(1),
                CreatedAt = DateTime.UtcNow,
                Address = new Addresses
                {
                    Id = 101,
                    DistrictId = 10,
                    District = mockDistrict,
                    Name = "Old Address",
                    Street = "Old Street"
                }
            };

            // Setup event lookup
            _mockUnitOfWork.Setup(u => u.Events.FindAsync(
                    It.IsAny<Expression<Func<Events, bool>>>(),
                    "Creator,Address"
                ))
                .ReturnsAsync(new List<Events> { existingEvent });

            // Setup creator lookup
            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
                .ReturnsAsync(new List<Users> { existingCreator });

            // Setup event owner lookup
            _mockUnitOfWork.Setup(u => u.Events.GetByIdAsync(eventId))
                .ReturnsAsync(existingEvent);

            // Setup District lookups
            _mockUnitOfWork.Setup(u => u.Districts.GetByIdAsync(dto.DistrictId))
                .ReturnsAsync(mockDistrict);

            _mockUnitOfWork.Setup(u => u.Districts.FindAsync(It.IsAny<Func<Districts, bool>>()))
                .ReturnsAsync(new List<Districts> { mockDistrict });

            // Setup City lookups
            _mockUnitOfWork.Setup(u => u.Cities.FindAsync(It.IsAny<Func<Cities, bool>>()))
                .ReturnsAsync(new List<Cities> { mockCity });

            // Setup Country lookups
            _mockUnitOfWork.Setup(u => u.Countries.FindAsync(It.IsAny<Func<Countries, bool>>()))
                .ReturnsAsync(new List<Countries> { mockCountry });

            // Requirements exist
            _mockUnitOfWork.Setup(u => u.Requirements.CountAsync(It.IsAny<Expression<Func<Requirements, bool>>>()))
                .ReturnsAsync(dto.RequirementIds.Count);

            // Current event-requirements setup
            _mockUnitOfWork.Setup(u => u.Events_Requirements.FindAsync(
                    It.IsAny<Func<Events_Requirements, bool>>()
                ))
                .ReturnsAsync(new List<Events_Requirements>());

            _mockUnitOfWork.Setup(u => u.Events_Requirements.FindAsync(
                    It.IsAny<Expression<Func<Events_Requirements, bool>>>(),
                    It.IsAny<string>()
                ))
                .ReturnsAsync(new List<Events_Requirements>());

            // Setup participation count
            _mockUnitOfWork.Setup(u => u.User_Event_Participations.CountAsync(
                    It.IsAny<Expression<Func<User_Event_Participations, bool>>>()
                ))
                .ReturnsAsync(0);

            _mockUnitOfWork.Setup(u => u.Addresses.Update(It.IsAny<Addresses>())).Verifiable();
            _mockUnitOfWork.Setup(u => u.Events.Update(It.IsAny<Events>())).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _eventService.UpdateEventAsync(eventId, dto);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("Updated Title");
            result.BodyText.Should().Be("Updated Body");
            result.CreatorUserName.Should().Be("Creator Name");

            _mockUnitOfWork.Verify(u => u.Events.Update(existingEvent), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.AtLeastOnce());
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
