using BackEngin.Controllers;
using BackEngin.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using Models;

namespace BackEngin.Tests.Controllers
{
    public class EventControllerTests
    {
        private readonly Mock<IEventService> _mockEventService;
        private readonly Mock<ClaimsPrincipal> _mockUser;
        private readonly EventController _eventController;

        public EventControllerTests()
        {
            _mockEventService = new Mock<IEventService>();

            // Mock user context
            _mockUser = new Mock<ClaimsPrincipal>();
            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier))
                     .Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindAll(ClaimTypes.Name))
                     .Returns(new[] { new Claim(ClaimTypes.Name, "currentUserName") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role))
                     .Returns(new Claim(ClaimTypes.Role, "User"));

            // Setup controller with mock user
            _eventController = new EventController(_mockEventService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = _mockUser.Object }
                }
            };
        }

        [Fact]
        public async Task GetEvents_ShouldReturnOk_WithPaginatedEvents()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            var paginatedEvents = new PaginatedResponseDTO<EventDTO>
            {
                Items = new List<EventDTO>
                {
                    new EventDTO { EventId = 1, Title = "Event 1" },
                    new EventDTO { EventId = 2, Title = "Event 2" }
                },
                TotalCount = 2,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            _mockEventService.Setup(s => s.GetAllEventsAsync(pageNumber, pageSize))
                             .ReturnsAsync(paginatedEvents);

            // Act
            var result = await _eventController.GetEvents(pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(paginatedEvents);
        }

        [Fact]
        public async Task GetEvents_ShouldReturnBadRequest_WhenPageOrSizeIsInvalid()
        {
            // Arrange
            int pageNumber = 0; // invalid
            int pageSize = 10;

            // Act
            var result = await _eventController.GetEvents(pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequest = result as BadRequestObjectResult;
            badRequest.Value.Should().BeEquivalentTo(new { message = "Page and pageSize must be positive integers." });
        }

        [Fact]
        public async Task GetEvents_ShouldReturnInternalServerError_OnException()
        {
            // Arrange
            _mockEventService.Setup(s => s.GetAllEventsAsync(It.IsAny<int>(), It.IsAny<int>()))
                             .ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _eventController.GetEvents();

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            objectResult.Value.Should().BeEquivalentTo(new { message = "An unexpected error occurred.", details = "Unexpected error" });
        }

        [Fact]
        public async Task GetEventById_ShouldReturnOk_WhenEventExists()
        {
            // Arrange
            int eventId = 1;
            var eventDto = new EventDTO { EventId = eventId, Title = "Test Event" };
            _mockEventService.Setup(s => s.GetEventByIdAsync(eventId)).ReturnsAsync(eventDto);

            // Act
            var result = await _eventController.GetEventById(eventId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(eventDto);
        }

        [Fact]
        public async Task GetEventById_ShouldReturnNotFound_WhenEventDoesNotExist()
        {
            // Arrange
            int eventId = 99;
            _mockEventService.Setup(s => s.GetEventByIdAsync(eventId)).ReturnsAsync((EventDTO)null);

            // Act
            var result = await _eventController.GetEventById(eventId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().BeEquivalentTo(new { message = "The event does not exist." });
        }

        [Fact]
        public async Task GetEventById_ShouldReturnInternalServerError_OnException()
        {
            // Arrange
            int eventId = 1;
            _mockEventService.Setup(s => s.GetEventByIdAsync(eventId)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _eventController.GetEventById(eventId);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            objectResult.Value.Should().BeEquivalentTo(new { message = "An unexpected error occurred.", details = "Error" });
        }

        [Fact]
        public async Task CreateEvent_ShouldReturnOk_WhenCreationIsSuccessful()
        {
            // Arrange
            var createDto = new CreateEventDTO
            {
                Title = "New Event",
                BodyText = "This is a new event",
                Date = DateTime.Now.AddDays(1),
                DistrictId = 1,
                AddressName = "Some Place",
                Street = "123 Main St",
                RequirementIds = new List<int>()
            };

            var createdEventDto = new EventDTO { EventId = 1, Title = "New Event" };
            _mockEventService.Setup(s => s.CreateEventAsync(createDto, It.IsAny<string>(), It.IsAny<string>()))
                             .ReturnsAsync(createdEventDto);

            // Act
            var result = await _eventController.CreateEvent(createDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(createdEventDto);
        }

        [Fact]
        public async Task CreateEvent_ShouldReturnBadRequest_WhenCreateEventDtoIsNull()
        {
            // Act
            var result = await _eventController.CreateEvent(null);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badResult = result as BadRequestObjectResult;
            badResult.Value.Should().BeEquivalentTo(new { message = "Invalid event data." });
        }

        [Fact]
        public async Task CreateEvent_ShouldReturnBadRequest_WhenCreationFails()
        {
            // Arrange
            var createDto = new CreateEventDTO
            {
                Title = "New Event",
                BodyText = "This is a new event",
                Date = DateTime.Now.AddDays(1),
                DistrictId = 1,
                AddressName = "Some Place",
                Street = "123 Main St"
            };

            _mockEventService.Setup(s => s.CreateEventAsync(createDto, It.IsAny<string>(), It.IsAny<string>()))
                             .ReturnsAsync((EventDTO)null);

            // Act
            var result = await _eventController.CreateEvent(createDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badResult = result as BadRequestObjectResult;
            badResult.Value.Should().BeEquivalentTo(new { message = "Failed to create event." });
        }

        [Fact]
        public async Task CreateEvent_ShouldReturnInternalServerError_OnException()
        {
            // Arrange
            var createDto = new CreateEventDTO
            {
                Title = "New Event",
                BodyText = "This is a new event",
                Date = DateTime.Now.AddDays(1),
                DistrictId = 1,
                AddressName = "Some Place",
                Street = "123 Main St"
            };

            _mockEventService.Setup(s => s.CreateEventAsync(createDto, It.IsAny<string>(), It.IsAny<string>()))
                             .ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _eventController.CreateEvent(createDto);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            objectResult.Value.Should().BeEquivalentTo(new { message = "An unexpected error occurred.", details = "Error" });
        }

        [Fact]
        public async Task UpdateEvent_ShouldReturnOk_WhenUpdatedSuccessfully()
        {
            // Arrange
            int eventId = 1;
            var updateDto = new UpdateEventDTO
            {
                Title = "Updated Event",
                BodyText = "Updated content",
                Date = DateTime.Now.AddDays(2),
                DistrictId = 1,
                AddressName = "New Place",
                Street = "456 Other St",
                RequirementIds = new List<int>()
            };

            // We need to mock ownership check
            _mockEventService.Setup(s => s.GetEventOwnerId(eventId))
                             .ReturnsAsync("currentUserId");
            _mockEventService.Setup(s => s.UpdateEventAsync(eventId, updateDto))
                             .ReturnsAsync(new EventDTO { EventId = eventId, Title = "Updated Event" });

            // Act
            var result = await _eventController.UpdateEvent(eventId, updateDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var updatedEvent = okResult.Value as EventDTO;
            updatedEvent.Title.Should().Be("Updated Event");
        }

        [Fact]
        public async Task UpdateEvent_ShouldReturnNotFound_WhenEventDoesNotExist()
        {
            // Arrange
            int eventId = 99;
            var updateDto = new UpdateEventDTO
            {
                Title = "Updated Event"
            };

            _mockEventService.Setup(s => s.GetEventOwnerId(eventId))
                             .ReturnsAsync("currentUserId");
            _mockEventService.Setup(s => s.UpdateEventAsync(eventId, updateDto))
                             .ReturnsAsync((EventDTO)null);

            // Act
            var result = await _eventController.UpdateEvent(eventId, updateDto);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFound = result as NotFoundObjectResult;
            notFound.Value.Should().BeEquivalentTo(new { message = "The event could not be updated because it does not exist." });
        }

        [Fact]
        public async Task UpdateEvent_ShouldReturnUnauthorized_WhenUserDoesNotOwnEvent()
        {
            // Arrange
            int eventId = 1;
            var updateDto = new UpdateEventDTO { Title = "Updated Event" };

            _mockEventService.Setup(s => s.GetEventOwnerId(eventId))
                             .ReturnsAsync("someOtherUserId");

            // Act
            var result = await _eventController.UpdateEvent(eventId, updateDto);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();
            var unauthorized = result as UnauthorizedObjectResult;
            unauthorized.Value.Should().BeEquivalentTo(new { message = "You are not authorized to update this event." });
        }

        [Fact]
        public async Task UpdateEvent_ShouldReturnBadRequest_OnArgumentException()
        {
            // Arrange
            int eventId = 1;
            var updateDto = new UpdateEventDTO { Title = "Updated Event" };

            _mockEventService.Setup(s => s.GetEventOwnerId(eventId))
                             .ReturnsAsync("currentUserId");
            _mockEventService.Setup(s => s.UpdateEventAsync(eventId, updateDto))
                             .ThrowsAsync(new ArgumentException("Invalid argument"));

            // Act
            var result = await _eventController.UpdateEvent(eventId, updateDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequest = result as BadRequestObjectResult;
            badRequest.Value.Should().BeEquivalentTo(new { message = "Invalid argument" });
        }

        [Fact]
        public async Task DeleteEvent_ShouldReturnOk_WhenDeletedSuccessfully()
        {
            // Arrange
            int eventId = 1;
            _mockEventService.Setup(s => s.GetEventOwnerId(eventId))
                             .ReturnsAsync("currentUserId");
            _mockEventService.Setup(s => s.DeleteEventAsync(eventId))
                             .ReturnsAsync(true);

            // Act
            var result = await _eventController.DeleteEvent(eventId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { message = "The event was successfully deleted." });
        }

        [Fact]
        public async Task DeleteEvent_ShouldReturnNotFound_WhenEventDoesNotExist()
        {
            // Arrange
            int eventId = 99;
            _mockEventService.Setup(s => s.GetEventOwnerId(eventId))
                             .ReturnsAsync("currentUserId");
            _mockEventService.Setup(s => s.DeleteEventAsync(eventId))
                             .ReturnsAsync(false);

            // Act
            var result = await _eventController.DeleteEvent(eventId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFound = result as NotFoundObjectResult;
            notFound.Value.Should().BeEquivalentTo(new { message = "The event does not exist." });
        }

        [Fact]
        public async Task DeleteEvent_ShouldReturnUnauthorized_WhenUserDoesNotOwnEvent()
        {
            // Arrange
            int eventId = 1;
            _mockEventService.Setup(s => s.GetEventOwnerId(eventId))
                             .ReturnsAsync("anotherUserId");

            // Act
            var result = await _eventController.DeleteEvent(eventId);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();
            var unauthorized = result as UnauthorizedObjectResult;
            unauthorized.Value.Should().BeEquivalentTo(new { message = "You are not authorized to delete this event." });
        }

        [Fact]
        public async Task DeleteEvent_ShouldReturnInternalServerError_OnException()
        {
            // Arrange
            int eventId = 1;
            _mockEventService.Setup(s => s.GetEventOwnerId(eventId))
                             .ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _eventController.DeleteEvent(eventId);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            objectResult.Value.Should().BeEquivalentTo(new { message = "An unexpected error occurred.", details = "Error" });
        }

        [Fact]
        public async Task JoinToEvent_ShouldReturnJoinedMessage_WhenUserJoins()
        {
            // Arrange
            int eventId = 1;
            _mockEventService.Setup(s => s.ToggleAttendToEventAsync(eventId, "currentUserId"))
                             .ReturnsAsync(true);

            // Act
            var result = await _eventController.ToggleEventAttendance(eventId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { message = "Successfully joined the event." });
        }

        [Fact]
        public async Task JoinToEvent_ShouldReturnLeftMessage_WhenUserLeaves()
        {
            // Arrange
            int eventId = 1;
            _mockEventService.Setup(s => s.ToggleAttendToEventAsync(eventId, "currentUserId"))
                             .ReturnsAsync(false);

            // Act
            var result = await _eventController.ToggleEventAttendance(eventId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { message = "Successfully left the event." });
        }

        [Fact]
        public async Task JoinToEvent_ShouldReturnBadRequest_OnArgumentException()
        {
            // Arrange
            int eventId = 1;
            _mockEventService.Setup(s => s.ToggleAttendToEventAsync(eventId, "currentUserId"))
                             .ThrowsAsync(new ArgumentException("Invalid event ID"));

            // Act
            var result = await _eventController.ToggleEventAttendance(eventId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequest = result as BadRequestObjectResult;
            badRequest.Value.Should().BeEquivalentTo(new { message = "Invalid event ID" });
        }

        [Fact]
        public async Task JoinToEvent_ShouldReturnInternalServerError_OnException()
        {
            // Arrange
            int eventId = 1;
            _mockEventService.Setup(s => s.ToggleAttendToEventAsync(eventId, "currentUserId"))
                             .ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _eventController.ToggleEventAttendance(eventId);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objResult = result as ObjectResult;
            objResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            objResult.Value.Should().BeEquivalentTo(new { message = "An unexpected error occurred.", details = "Unexpected error" });
        }

        [Fact]
        public async Task GetAllRequirements_ShouldReturnOk_WithPaginatedRequirements()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            var requirementsResponse = new PaginatedResponseDTO<RequirementDTO>
            {
                Items = new List<RequirementDTO>
                {
                    new RequirementDTO { Id = 1, Name = "Requirement 1" },
                    new RequirementDTO { Id = 2, Name = "Requirement 2" }
                },
                TotalCount = 2,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            _mockEventService.Setup(s => s.GetAllRequirementsAsync(pageNumber, pageSize))
                             .ReturnsAsync(requirementsResponse);

            // Act
            var result = await _eventController.GetAllRequirements(pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(requirementsResponse);
        }

        [Fact]
        public async Task GetAllRequirements_ShouldReturnBadRequest_WhenPageOrSizeInvalid()
        {
            // Arrange
            int pageNumber = 0;
            int pageSize = 10;

            // Act
            var result = await _eventController.GetAllRequirements(pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequest = result as BadRequestObjectResult;
            badRequest.Value.Should().BeEquivalentTo(new { message = "Page and pageSize must be positive integers." });
        }

        [Fact]
        public async Task GetEventParticipants_ShouldReturnOk_WithParticipants()
        {
            // Arrange
            int eventId = 1;
            int pageNumber = 1;
            int pageSize = 10;
            var participantsResponse = new EventParticipantsResponseDTO
            {
                Participations = new PaginatedResponseDTO<UserCompactDTO>
                {
                    Items = new List<UserCompactDTO>
                    {
                        new UserCompactDTO { UserId = "user1", UserName = "User One" },
                        new UserCompactDTO { UserId = "user2", UserName = "User Two" }
                    },
                    TotalCount = 2,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                },
                FollowedParticipations = null // Not authenticated case
            };

            _mockEventService.Setup(s => s.GetPaginatedParticipantsAsync(eventId, null, pageNumber, pageSize))
                             .ReturnsAsync(participantsResponse);

            // Mock unauthenticated user
            _mockUser.Setup(u => u.Identity.IsAuthenticated).Returns(false);

            // Act
            var result = await _eventController.GetEventParticipants(eventId, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(participantsResponse);
        }

        [Fact]
        public async Task GetEventParticipants_ShouldReturnBadRequest_WhenInvalidPageOrSize()
        {
            // Arrange
            int eventId = 1;
            int pageNumber = 0;
            int pageSize = 10;

            // Act
            var result = await _eventController.GetEventParticipants(eventId, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequest = result as BadRequestObjectResult;
            badRequest.Value.Should().BeEquivalentTo(new { message = "Page and pageSize must be positive integers." });
        }

        [Fact]
        public async Task IsUserParticipant_ShouldReturnOk_WithTrue_WhenUserIsParticipant()
        {
            // Arrange
            var eventId = 1;
            var userId = "currentUserId";
            _mockEventService.Setup(es => es.IsUserParticipantAsync(eventId, userId)).ReturnsAsync(true);

            // Act
            var result = await _eventController.IsUserParticipant(eventId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            okResult.Value.Should().BeEquivalentTo(new { isParticipant = true });

            _mockEventService.Verify(es => es.IsUserParticipantAsync(eventId, userId), Times.Once);
  
        }

        [Fact]
        public async Task IsUserParticipant_ShouldReturnOk_WithFalse_WhenUserIsNotParticipant()
        {
            // Arrange
            var eventId = 1;
            var userId = "currentUserId";
            _mockEventService.Setup(es => es.IsUserParticipantAsync(eventId, userId)).ReturnsAsync(false);

            // Act
            var result = await _eventController.IsUserParticipant(eventId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            okResult.Value.Should().BeEquivalentTo(new { isParticipant = false });

            _mockEventService.Verify(es => es.IsUserParticipantAsync(eventId, userId), Times.Once);
        }

        [Fact]
        public async Task IsUserParticipant_ShouldReturnInternalServerError_OnException()
        {
            // Arrange
            var eventId = 1;
            _mockEventService.Setup(es => es.IsUserParticipantAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Something went wrong"));

            // Act
            var result = await _eventController.IsUserParticipant(eventId);

            // Assert
            var objectResult = result.Should().BeOfType<ObjectResult>().Which;
            objectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            objectResult.Value.Should().BeEquivalentTo(new
            {
                message = "An unexpected error occurred.",
                details = "Something went wrong"
            });

            _mockEventService.Verify(es => es.IsUserParticipantAsync(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }


        [Fact]
        public async Task GetDistrictsByCityId_ShouldReturnOk_WithDistricts()
        {
            // Arrange
            int cityId = 1;
            var districts = new List<DistrictDTO>
            {
                new DistrictDTO { Id = 1, Name = "District 1", PostCode = 1000 },
                new DistrictDTO { Id = 2, Name = "District 2", PostCode = 2000 }
            };

            _mockEventService.Setup(s => s.GetDistrictsByCityIdAsync(cityId))
                             .ReturnsAsync(districts);

            // Act
            var result = await _eventController.GetDistrictsByCityId(cityId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(districts);
        }

        [Fact]
        public async Task GetCityByDistrictId_ShouldReturnOk_WhenCityFound()
        {
            // Arrange
            int districtId = 1;
            var city = new CityDTO { Id = 10, Name = "City Name" };
            _mockEventService.Setup(s => s.GetCityByDistrictIdAsync(districtId))
                             .ReturnsAsync(city);

            // Act
            var result = await _eventController.GetCityByDistrictId(districtId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(city);
        }

        [Fact]
        public async Task GetCityByDistrictId_ShouldReturnNotFound_WhenCityNotFound()
        {
            // Arrange
            int districtId = 99;
            _mockEventService.Setup(s => s.GetCityByDistrictIdAsync(districtId))
                             .ReturnsAsync((CityDTO)null);

            // Act
            var result = await _eventController.GetCityByDistrictId(districtId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().BeEquivalentTo(new { message = "City not found for the given district." });
        }

        [Fact]
        public async Task GetCountryByCityId_ShouldReturnOk_WhenCountryFound()
        {
            // Arrange
            int cityId = 1;
            var country = new CountryDTO { Id = 100, Name = "Country Name" };
            _mockEventService.Setup(s => s.GetCountryByCityIdAsync(cityId))
                             .ReturnsAsync(country);

            // Act
            var result = await _eventController.GetCountryByCityId(cityId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(country);
        }

        [Fact]
        public async Task GetCountryByCityId_ShouldReturnNotFound_WhenCountryNotFound()
        {
            // Arrange
            int cityId = 99;
            _mockEventService.Setup(s => s.GetCountryByCityIdAsync(cityId))
                             .ReturnsAsync((CountryDTO)null);

            // Act
            var result = await _eventController.GetCountryByCityId(cityId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().BeEquivalentTo(new { message = "Country not found for the given city." });
        }

        [Fact]
        public async Task GetAllCountries_ShouldReturnOk_WhenCountriesFound()
        {
            // Arrange
            var countries = new List<CountryDTO>
            {
                new CountryDTO { Id = 1, Name = "Country1" },
                new CountryDTO { Id = 2, Name = "Country2" }
            };

            _mockEventService.Setup(s => s.GetAllCountriesAsync())
                             .ReturnsAsync(countries);

            // Act
            var result = await _eventController.GetAllCountries();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(countries);
        }

        [Fact]
        public async Task GetCitiesByCountryId_ShouldReturnOk_WhenCitiesFound()
        {
            // Arrange
            int countryId = 1;
            var cities = new List<CityDTO>
            {
                new CityDTO { Id = 10, Name = "City1" },
                new CityDTO { Id = 11, Name = "City2" }
            };

            _mockEventService.Setup(s => s.GetCitiesByCountryIdAsync(countryId))
                             .ReturnsAsync(cities);

            // Act
            var result = await _eventController.GetCitiesByCountryId(countryId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(cities);
        }

        [Fact]
        public async Task GetDistrictsByCityId_ShouldReturnInternalServerError_OnException()
        {
            // Arrange
            int cityId = 1;
            _mockEventService.Setup(s => s.GetDistrictsByCityIdAsync(cityId))
                             .ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _eventController.GetDistrictsByCityId(cityId);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            objectResult.Value.Should().BeEquivalentTo(new { message = "An unexpected error occurred.", details = "Error" });
        }

        [Fact]
        public async Task SearchEvents_ShouldReturnOk_WithSearchResults()
        {
            // Arrange
            var searchParams = new EventSearchParams
            {
                TitleContains = "Party",
                FromDate = DateTime.UtcNow,
                ToDate = DateTime.UtcNow.AddDays(7),
                CountryIds = new List<int> { 1 },
                CityIds = new List<int> { 1 },
                RequirementIds = new List<int> { 1 }
            };

            var paginatedEvents = new PaginatedResponseDTO<EventDTO>
            {
                Items = new List<EventDTO>
                {
                    new EventDTO
                    {
                        EventId = 1,
                        Title = "Birthday Party",
                        Date = DateTime.UtcNow.AddDays(1),
                        CreatorUserName = "host1",
                        Address = new Addresses
                        {
                            District = new Districts
                            {
                                City = new Cities
                                {
                                    Country = new Countries { Id = 1 }
                                }
                            }
                        }
                    }
                },
                TotalCount = 1,
                PageNumber = 1,
                PageSize = 10
            };

            _mockEventService.Setup(s => s.SearchEventsAsync(searchParams, 1, 10))
                .ReturnsAsync(paginatedEvents);

            // Act
            var result = await _eventController.SearchEvents(searchParams, 1, 10);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(paginatedEvents);
            okResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task SearchEvents_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _eventController.ModelState.AddModelError("error", "some error");

            // Act
            var result = await _eventController.SearchEvents(new EventSearchParams());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequest = result as BadRequestObjectResult;
            badRequest.Value.Should().BeEquivalentTo(new { message = "Invalid request data.", errors = _eventController.ModelState });
        }

        [Fact]
        public async Task SearchEvents_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            _mockEventService.Setup(s => s.SearchEventsAsync(
                It.IsAny<EventSearchParams>(),
                It.IsAny<int>(),
                It.IsAny<int>()
            )).ThrowsAsync(new Exception("Test error"));

            // Act
            var result = await _eventController.SearchEvents(new EventSearchParams());

            // Assert
            var statusCodeResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusCodeResult.StatusCode.Should().Be(500);
            statusCodeResult.Value.Should().BeEquivalentTo(
                new { message = "An unexpected error occurred.", details = "Test error" }
            );
        }

        [Fact]
        public async Task SearchEvents_ShouldHandleEmptyResults()
        {
            // Arrange
            var searchParams = new EventSearchParams
            {
                TitleContains = "NonexistentEvent"
            };

            var emptyResponse = new PaginatedResponseDTO<EventDTO>
            {
                Items = new List<EventDTO>(),
                TotalCount = 0,
                PageNumber = 1,
                PageSize = 10
            };

            _mockEventService.Setup(s => s.SearchEventsAsync(searchParams, 1, 10))
                .ReturnsAsync(emptyResponse);

            // Act
            var result = await _eventController.SearchEvents(searchParams);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value as PaginatedResponseDTO<EventDTO>;
            response.Should().NotBeNull();
            response.Items.Should().BeEmpty();
            response.TotalCount.Should().Be(0);
        }

        [Theory]
        [InlineData(null, null, "date", "asc")]
        [InlineData("test", "desc", "title", null)]
        [InlineData("", "", "", "")]
        public async Task SearchEvents_ShouldHandleVariousSearchParameters(
            string titleContains,
            string sortOrder,
            string sortBy,
            string creatorUserName)
        {
            // Arrange
            var searchParams = new EventSearchParams
            {
                TitleContains = titleContains,
                SortOrder = sortOrder,
                SortBy = sortBy,
                CreatorUserName = creatorUserName
            };

            var response = new PaginatedResponseDTO<EventDTO>
            {
                Items = new List<EventDTO>(),
                TotalCount = 0,
                PageNumber = 1,
                PageSize = 10
            };

            _mockEventService.Setup(s => s.SearchEventsAsync(
                It.Is<EventSearchParams>(sp => 
                    sp.TitleContains == titleContains &&
                    sp.SortOrder == sortOrder &&
                    sp.SortBy == sortBy &&
                    sp.CreatorUserName == creatorUserName),
                1, 10))
                .ReturnsAsync(response);

            // Act
            var result = await _eventController.SearchEvents(searchParams);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _mockEventService.Verify(s => s.SearchEventsAsync(
                It.Is<EventSearchParams>(sp =>
                    sp.TitleContains == titleContains &&
                    sp.SortOrder == sortOrder &&
                    sp.SortBy == sortBy &&
                    sp.CreatorUserName == creatorUserName),
                1, 10), Times.Once);
        }

        [Fact]
        public async Task GetEventParticipants_ShouldReturnBasicResponse_WhenUserNotAuthenticated()
        {
            // Arrange
            int eventId = 1;
            var expectedResponse = new EventParticipantsResponseDTO
            {
                Participations = new PaginatedResponseDTO<UserCompactDTO>
                {
                    Items = new List<UserCompactDTO> 
                    {
                        new UserCompactDTO { UserId = "1", UserName = "user1" },
                        new UserCompactDTO { UserId = "2", UserName = "user2" }
                    },
                    TotalCount = 2,
                    PageNumber = 1,
                    PageSize = 10
                },
                FollowedParticipations = null // Should be null for unauthenticated users
            };

            // Setup mock without user authentication
            _mockUser.Setup(u => u.Identity.IsAuthenticated).Returns(false);
            
            _mockEventService.Setup(s => s.GetPaginatedParticipantsAsync(eventId, null, 1, 10))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _eventController.GetEventParticipants(eventId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            var response = okResult.Value.Should().BeOfType<EventParticipantsResponseDTO>().Which;
            response.Participations.Should().NotBeNull();
            response.FollowedParticipations.Should().BeNull();
            response.Participations.Items.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetEventParticipants_ShouldReturnFullResponse_WhenUserAuthenticated()
        {
            // Arrange
            int eventId = 1;
            var expectedResponse = new EventParticipantsResponseDTO
            {
                Participations = new PaginatedResponseDTO<UserCompactDTO>
                {
                    Items = new List<UserCompactDTO> 
                    {
                        new UserCompactDTO { UserId = "1", UserName = "user1" },
                        new UserCompactDTO { UserId = "2", UserName = "user2" }
                    },
                    TotalCount = 2,
                    PageNumber = 1,
                    PageSize = 10
                },
                FollowedParticipations = new PaginatedResponseDTO<UserCompactDTO>
                {
                    Items = new List<UserCompactDTO> 
                    {
                        new UserCompactDTO { UserId = "1", UserName = "user1" }
                    },
                    TotalCount = 1,
                    PageNumber = 1,
                    PageSize = 10
                }
            };

            // Setup mock with user authentication
            _mockUser.Setup(u => u.Identity.IsAuthenticated).Returns(true);
            
            _mockEventService.Setup(s => s.GetPaginatedParticipantsAsync(eventId, "currentUserId", 1, 10))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _eventController.GetEventParticipants(eventId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            var response = okResult.Value.Should().BeOfType<EventParticipantsResponseDTO>().Which;
            response.Participations.Should().NotBeNull();
            response.FollowedParticipations.Should().NotBeNull();
            response.Participations.Items.Should().HaveCount(2);
            response.FollowedParticipations.Items.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetEventParticipants_ShouldHandleEmptyResults()
        {
            // Arrange
            int eventId = 1;
            var emptyResponse = new EventParticipantsResponseDTO
            {
                Participations = new PaginatedResponseDTO<UserCompactDTO>
                {
                    Items = new List<UserCompactDTO>(),
                    TotalCount = 0,
                    PageNumber = 1,
                    PageSize = 10
                }
            };

            _mockEventService.Setup(s => s.GetPaginatedParticipantsAsync(eventId, null, 1, 10))
                .ReturnsAsync(emptyResponse);

            // Act
            var result = await _eventController.GetEventParticipants(eventId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            var response = okResult.Value.Should().BeOfType<EventParticipantsResponseDTO>().Which;
            response.Participations.Items.Should().BeEmpty();
            response.Participations.TotalCount.Should().Be(0);
        }
    }
}
