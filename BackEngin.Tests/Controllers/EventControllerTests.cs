using BackEngin.Controllers;
using BackEngin.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Dynamic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BackEngin.Tests.Controllers
{
    public class EventControllerTests
    {
        private readonly Mock<IEventService> _mockEventService;
        private readonly EventController _controller;
        private readonly Mock<ClaimsPrincipal> _mockUser;

        public EventControllerTests()
        {
            _mockEventService = new Mock<IEventService>();

            // Mock user context
            _mockUser = new Mock<ClaimsPrincipal>();
            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier))
                     .Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });

            // Setup controller with mock user
            _controller = new EventController(_mockEventService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = _mockUser.Object }
                }
            };
        }

        // Test GetEvents endpoint
        [Fact]
        public async Task GetEvents_ShouldReturnBadRequest_WhenPageIsLessThanOrEqualToZero()
        {
            // Arrange
            int page = 0;
            int pageSize = 10;

            // Act
            var result = await _controller.GetEvents(page, pageSize);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Page and pageSize must be positive integers.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetEvents_ShouldReturnOk_WhenValidDataIsPassed()
        {
            // Arrange
            int page = 1;
            int pageSize = 10;
            var mockEvents = new PaginatedResponseDTO<EventDto>
            {
                Items = new[] { new EventDto() },
                TotalCount = 1
            };
            _mockEventService.Setup(s => s.GetAllEventsAsync(page, pageSize)).ReturnsAsync(mockEvents);

            // Act
            var result = await _controller.GetEvents(page, pageSize);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(mockEvents, okResult.Value);
        }

        // Test GetEventById endpoint
        [Fact]
        public async Task GetEventById_ShouldReturnNotFound_WhenEventDoesNotExist()
        {
            // Arrange
            int eventId = 1;
            _mockEventService.Setup(s => s.GetEventByIdAsync(eventId)).ReturnsAsync((EventDto)null);

            // Act
            var result = await _controller.GetEventById(eventId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.NotNull(notFoundResult.Value);

            var expectedJson = JsonConvert.SerializeObject(new { Message = "The event does not exist." });
            var actualJson = JsonConvert.SerializeObject(notFoundResult.Value);

            Assert.Equal(expectedJson, actualJson);
        }



        [Fact]
        public async Task GetEventById_ShouldReturnOk_WhenEventExists()
        {
            // Arrange
            int eventId = 1;
            var mockEvent = new EventDto();
            _mockEventService.Setup(s => s.GetEventByIdAsync(eventId)).ReturnsAsync(mockEvent);

            // Act
            var result = await _controller.GetEventById(eventId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(mockEvent, okResult.Value);
        }

        // Test UpdateEvent endpoint
        [Fact]
        public async Task UpdateEvent_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            int eventId = 1;
            var updateEventDto = new UpdateEventDto();
            _controller.ModelState.AddModelError("Error", "Invalid data");

            // Act
            var result = await _controller.UpdateEvent(eventId, updateEventDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateEvent_ShouldReturnUnauthorized_WhenUserIsNotOwner()
        {
            // Arrange
            int eventId = 1;
            var userId = 2; // Not the event owner
            var ownerId = 1; // The actual owner of the event
            var eventDto = new UpdateEventDto();

            // Mock the event service to return a different user ID as the event owner
            _mockEventService.Setup(s => s.GetEventOwnerId(eventId)).ReturnsAsync(ownerId.ToString());

            // Create a ClaimsPrincipal for a user who is not the owner of the event
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()), // Simulate user ID 2
                new Claim(ClaimTypes.Role, "User") // Add a dummy role to avoid errors related to missing role claims
            }, "mock", ClaimTypes.NameIdentifier, ClaimTypes.Role));

            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = user // Assign the user with the Role claim
            };

            var result = await _controller.UpdateEvent(eventId, eventDto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result);
        }



        [Fact]
        public async Task UpdateEvent_ShouldReturnOk_WhenEventUpdatedSuccessfully()
        {
            // Arrange
            int eventId = 1;
            var updateEventDto = new UpdateEventDto();
            var userId = 1; // Event owner
            var updatedEvent = new EventDto();

            // Mock the event service to return the event owner's ID and the updated event data
            _mockEventService.Setup(s => s.GetEventOwnerId(eventId)).ReturnsAsync(userId.ToString());
            _mockEventService.Setup(s => s.UpdateEventAsync(eventId, updateEventDto)).ReturnsAsync(updatedEvent);

            // Create a ClaimsPrincipal for the event owner (user with the same ID as the event owner)
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()), // Simulate user ID 1 (event owner)
                new Claim(ClaimTypes.Role, "User") // Add a role to avoid errors related to missing role claims
            }, "mock", ClaimTypes.NameIdentifier, ClaimTypes.Role));

            // Set the ControllerContext with the user
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = user // Assign the user with the Role claim
            };

            // Act
            var result = await _controller.UpdateEvent(eventId, updateEventDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(updatedEvent, okResult.Value);
        }


        // Test DeleteEvent endpoint
        [Fact]
        public async Task DeleteEvent_ShouldReturnUnauthorized_WhenUserIsNotOwner()
        {
            // Arrange
            int eventId = 1;
            var userId = 2; // Not the event owner
            var ownerId = 1; // The actual owner of the event

            // Mock the event service to return a different user ID as the event owner
            _mockEventService.Setup(s => s.GetEventOwnerId(eventId)).ReturnsAsync(ownerId.ToString());

            // Create a ClaimsPrincipal for a user who is not the owner of the event
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()), // Simulate user ID 2
                new Claim(ClaimTypes.Role, "User") // Add a dummy role to avoid errors related to missing role claims
            }, "mock", ClaimTypes.NameIdentifier, ClaimTypes.Role));

            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = user // Assign the user with the Role claim
            };

            // Act
            var result = await _controller.DeleteEvent(eventId);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result);
        }


        [Fact]
        public async Task DeleteEvent_ShouldReturnOk_WhenEventDeletedSuccessfully()
        {
            // Arrange
            int eventId = 1;
            var userId = 1; // Event owner
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()), // Simulate user ID 1
                new Claim(ClaimTypes.Role, "User") // Add a dummy role to avoid errors related to missing role claims
            }, "mock", ClaimTypes.NameIdentifier, ClaimTypes.Role));

            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = user
            };

            _mockEventService.Setup(s => s.GetEventOwnerId(eventId)).ReturnsAsync(userId.ToString());
            _mockEventService.Setup(s => s.DeleteEventAsync(eventId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteEvent(eventId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var expected = JsonConvert.SerializeObject(new { Message = "Event successfully deleted." });
            var actual = JsonConvert.SerializeObject(okResult.Value);

            Assert.Equal(expected, actual);

            //Assert.NotNull(okResult.Value);
            //Assert.Equal("Event successfully deleted.", okResult.Value);
        }




        [Fact]
        public async Task CreateEvent_ShouldReturnBadRequest_WhenEventDtoIsNull()
        {
            // Arrange
            CreateEventDto createEventDto = null;

            // Act
            var result = await _controller.CreateEvent(createEventDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);

            // Serialize expected and actual values to JSON for comparison
            var expectedJson = JsonConvert.SerializeObject(new { message = "Invalid event data." });
            var actualJson = JsonConvert.SerializeObject(badRequestResult.Value);

            Assert.Equal(expectedJson, actualJson);
        }





        [Fact]
        public async Task CreateEvent_ShouldReturnOk_WhenEventCreatedSuccessfully()
        {
            // Arrange
            var createEventDto = new CreateEventDto();
            var creatorId = 1;
            var createdEvent = new EventDto();

            // Mock an authenticated user (user with ID 1) and include the Role claim
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, creatorId.ToString()), // Simulate an authenticated user with ID 1
                new Claim(ClaimTypes.Role, "User") // Simulate the user's role (adjust to whatever role is required)
            }, "mock", ClaimTypes.NameIdentifier, ClaimTypes.Role));

            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = user
            };

            // Setup the mock for CreateEventAsync
            _mockEventService.Setup(s => s.CreateEventAsync(createEventDto, creatorId.ToString())).ReturnsAsync(createdEvent);

            // Act
            var result = await _controller.CreateEvent(createEventDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(createdEvent, okResult.Value);
        }



        // Test JoinToEvent endpoint
        [Fact]
        public async Task JoinToEvent_ShouldReturnBadRequest_WhenUserAlreadyJoined()
        {
            // Arrange
            int eventId = 1;
            int userId = 1;
            _mockEventService.Setup(s => s.JoinToEventAsync(eventId, userId.ToString())).ReturnsAsync(false);

            // Act
            var result = await _controller.JoinToEvent(eventId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to join the event. The user already joined this event.", badRequestResult.Value);
        }

        [Fact]
        public async Task JoinToEvent_ShouldReturnOk_WhenUserJoinsSuccessfully()
        {
            // Arrange
            var userId = 1;
            var eventId = 1;

            // Create a mock event with EventDto where the user is the event creator
            var eventDto = new EventDto
            {
                Title = "Test Event",
                BodyText = "Event details",
                Date = DateTime.Now.AddDays(1),
                CreatedAt = DateTime.Now,
                CreatorUserName = "user1",
                Address = new Addresses { /* Mock Address details here */ },
                Participants = new List<ParticipantDto>(), // Add participants if needed
                Requirements = new List<RequirementDto>() // Add requirements if needed
            };

            // Mock the service methods
            _mockEventService.Setup(s => s.GetEventByIdAsync(eventId)).ReturnsAsync(eventDto); // Mock event retrieval
            _mockEventService.Setup(s => s.JoinToEventAsync(eventId, userId.ToString())).ReturnsAsync(true); // Mock joining the event

            // Mock an authenticated user (user with ID 1) and include the Role claim
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()), // Simulate an authenticated user with ID 1
                new Claim(ClaimTypes.Role, "User") // Simulate the user's role (adjust to whatever role is required)
            }, "mock", ClaimTypes.NameIdentifier, ClaimTypes.Role));

            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = user
            };

            // Act
            var result = await _controller.JoinToEvent(eventId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);  // Ensure the result is OkObjectResult
            Assert.Equal("Successfully joined the event.", okResult.Value);  // Ensure the message matches
        }




        // Test LeaveEvent endpoint
        [Fact]
        public async Task LeaveEvent_ShouldReturnBadRequest_WhenUserIsNotAParticipant()
        {
            // Arrange
            int eventId = 1;
            int userId = 1;
            _mockEventService.Setup(s => s.LeaveEventAsync(eventId, userId.ToString())).ReturnsAsync(false);

            // Act
            var result = await _controller.LeaveEvent(eventId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to leave the event. The user is not a participant of this event.", badRequestResult.Value);
        }

        [Fact]
        public async Task LeaveEvent_ShouldReturnOk_WhenUserLeavesSuccessfully()
        {
            // Arrange
            var userId = 1;
            var eventId = 1;

            // Create a mock event with EventDto where the user is the event participant
            var eventDto = new EventDto
            {
                Title = "Test Event",
                BodyText = "Event details",
                Date = DateTime.Now.AddDays(1),
                CreatedAt = DateTime.Now,
                CreatorUserName = "user1",
                Address = new Addresses { /* Mock Address details here */ },
                Participants = new List<ParticipantDto>
                {
                    new ParticipantDto {FirstName = "John", LastName = "Doe", UserName = "user1", UserId = userId.ToString()} // Add the user as a participant
                },
                Requirements = new List<RequirementDto>() // Add requirements if needed
            };

            // Mock the service methods
            _mockEventService.Setup(s => s.GetEventByIdAsync(eventId)).ReturnsAsync(eventDto); // Mock event retrieval
            _mockEventService.Setup(s => s.LeaveEventAsync(eventId, userId.ToString())).ReturnsAsync(true); // Ensure user can leave
            _mockEventService.Setup(s => s.GetEventByIdAsync(eventId)) // Mock the event retrieval again after the user leaves
                .ReturnsAsync(() =>
                {
                    // Create a new event DTO where the user is no longer a participant
                    var updatedEventDto = new EventDto
                    {
                        Title = eventDto.Title,
                        BodyText = eventDto.BodyText,
                        Date = eventDto.Date,
                        CreatedAt = eventDto.CreatedAt,
                        CreatorUserName = eventDto.CreatorUserName,
                        Address = eventDto.Address,
                        Participants = new List<ParticipantDto>(), // User is removed from participants
                        Requirements = eventDto.Requirements
                    };
                    return updatedEventDto;
                });

            // Mock an authenticated user (user with ID 1) and include the Role claim
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()), // Simulate an authenticated user with ID 1
                new Claim(ClaimTypes.Role, "User") // Simulate the user's role (adjust to whatever role is required)
            }, "mock", ClaimTypes.NameIdentifier, ClaimTypes.Role));

            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = user
            };

            // Act
            var result = await _controller.LeaveEvent(eventId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Successfully left the event.", okResult.Value);

            // Ensure the user is removed from the participants list in the mock event
            var updatedEvent = (await _mockEventService.Object.GetEventByIdAsync(eventId)).Participants;
            Assert.DoesNotContain(updatedEvent, p => p.UserId == userId.ToString()); // User should no longer be in the list
        }




        // Test GetAllRequirements endpoint
        [Fact]
        public async Task GetAllRequirements_ShouldReturnOk_WhenValidDataIsReturned()
        {
            // Arrange
            var paginatedRequirements = new PaginatedResponseDTO<RequirementDto>
            {
                Items = new[] { new RequirementDto() },
                TotalCount = 1
            };
            _mockEventService.Setup(s => s.GetAllRequirementsAsync(1, 10)).ReturnsAsync(paginatedRequirements);

            // Act
            var result = await _controller.GetAllRequirements(1, 10);

            // Assert
            var actionResult = Assert.IsType<ActionResult<PaginatedResponseDTO<RequirementDto>>>(result);
            var okResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(paginatedRequirements, okResult.Value);
        }

    }
}
