using BackEngin.Controllers;
using BackEngin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Moq;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BackEngin.Tests.Controllers
{
    public class FeedControllerTests
    {
        private readonly Mock<IFeedService> _mockFeedService;
        private readonly FeedController _feedController;

        public FeedControllerTests()
        {
            _mockFeedService = new Mock<IFeedService>();
            _feedController = new FeedController(_mockFeedService.Object);
        }


        [Fact]
        public async Task GetPaginatedBlogs_ShouldReturnOk_WithPaginatedBlogs()
        {
            // Arrange
            var seed = "testseed";
            var pageNumber = 1;
            var pageSize = 10;
            var paginatedResponse = new PaginatedResponseDTO<BlogDTO>
            {
                Items = new List<BlogDTO>
                {
                    new BlogDTO
                    {
                        Id = 1,
                        Header = "Test Blog",
                        BodyText = "Test Content",
                        UserId = "user1",
                        UserName = "TestUser",
                        RecipeId = 1,
                        CreatedAt = DateTime.Now
                    }
                },
                TotalCount = 1,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            _mockFeedService.Setup(s => s.GetBlogsFeed(seed, pageNumber, pageSize))
                           .ReturnsAsync(paginatedResponse);

            // Act
            var result = await _feedController.GetPaginatedBlogs(seed, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(paginatedResponse);
        }

        [Fact]
        public async Task GetPaginatedEvents_ShouldReturnOk_WithPaginatedEvents()
        {
            // Arrange
            var seed = "testseed";
            var pageNumber = 1;
            var pageSize = 10;
            var paginatedResponse = new PaginatedResponseDTO<EventDTO>
            {
                Items = new List<EventDTO>
                {
                    new EventDTO
                    {
                        EventId = 1,
                        Title = "Test Event",
                        BodyText = "Test Description",
                        CreatorId = "user1",
                        Date = DateTime.Now.AddDays(1),
                        CreatedAt = DateTime.Now
                    }
                },
                TotalCount = 1,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            _mockFeedService.Setup(s => s.GetEventFeed(seed, pageNumber, pageSize))
                           .ReturnsAsync(paginatedResponse);

            // Act
            var result = await _feedController.GetPaginatedEvents(seed, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(paginatedResponse);
        }

        [Fact]
        public async Task GetPaginatedRecipes_ShouldHandleNegativePageNumber()
        {
            // Arrange
            var seed = "testseed";
            var pageNumber = -1;
            var pageSize = 10;
            var expectedPageNumber = 1;
            string? userId = null;
            
            // Setup controller context with no authentication
            _feedController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            _mockFeedService.Setup(s => s.GetRecipeFeed(seed, expectedPageNumber, pageSize, userId))
                           .ReturnsAsync(new PaginatedResponseDTO<RecipeDTO>());

            // Act
            await _feedController.GetPaginatedRecipes(seed, pageNumber, pageSize);

            // Assert
            _mockFeedService.Verify(s => s.GetRecipeFeed(seed, expectedPageNumber, pageSize, userId), Times.Once());
        }

        [Fact]
        public async Task GetPaginatedRecipes_ShouldHandleException()
        {
            // Arrange
            _mockFeedService.Setup(s => s.GetRecipeFeed(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                           .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _feedController.GetPaginatedRecipes("seed", 1, 10);

            // Assert
            result.Should().BeOfType<ObjectResult>()
                  .Which.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task GetPaginatedBlogs_ShouldHandleException()
        {
            // Arrange
            _mockFeedService.Setup(s => s.GetBlogsFeed(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                           .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _feedController.GetPaginatedBlogs("seed", 1, 10);

            // Assert
            result.Should().BeOfType<ObjectResult>()
                  .Which.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task GetPaginatedEvents_ShouldHandleException()
        {
            // Arrange
            _mockFeedService.Setup(s => s.GetEventFeed(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                           .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _feedController.GetPaginatedEvents("seed", 1, 10);

            // Assert
            result.Should().BeOfType<ObjectResult>()
                  .Which.StatusCode.Should().Be(500);
        }

                private void SetupUserAuth(string userId)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);
            _feedController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };
        }
        
        [Fact]
        public async Task GetPaginatedRecentFollowedRecipes_ShouldReturnOk()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var userId = "testUserId";
            var paginatedResponse = new PaginatedResponseDTO<RecipeDTO>
            {
                Items = new List<RecipeDTO> { new RecipeDTO { Id = 1, UserId = "user2", UserName = "TestUser" } },
                TotalCount = 1,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        
            SetupUserAuth(userId);
            _mockFeedService.Setup(s => s.GetFollowedRecentRecipeFeed(pageNumber, pageSize, userId))
                .ReturnsAsync(paginatedResponse);
        
            // Act
            var result = await _feedController.GetPaginatedRecentFollowedRecipes(pageNumber, pageSize);
        
            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnValue = okResult.Value.Should().BeOfType<PaginatedResponseDTO<RecipeDTO>>().Subject;
            returnValue.Should().BeEquivalentTo(paginatedResponse);
        }
        
        [Fact]
        public async Task GetPaginatedRecentFollowedBlogs_ShouldReturnOk()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var userId = "testUserId";
            var paginatedResponse = new PaginatedResponseDTO<BlogDTO>
            {
                Items = new List<BlogDTO> { new BlogDTO { Id = 1, UserId = "user2", UserName = "TestUser" } },
                TotalCount = 1,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        
            SetupUserAuth(userId);
            _mockFeedService.Setup(s => s.GetFollowedRecentBlogsFeed(pageNumber, pageSize, userId))
                .ReturnsAsync(paginatedResponse);
        
            // Act
            var result = await _feedController.GetPaginatedRecentFollowedBlogs(pageNumber, pageSize);
        
            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnValue = okResult.Value.Should().BeOfType<PaginatedResponseDTO<BlogDTO>>().Subject;
            returnValue.Should().BeEquivalentTo(paginatedResponse);
        }
        
        [Fact]
        public async Task GetPaginatedRecentFollowedEvents_ShouldReturnOk()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var userId = "testUserId";
            var paginatedResponse = new PaginatedResponseDTO<EventDTO>
            {
                Items = new List<EventDTO> { new EventDTO { EventId = 1, CreatorId = "user2", CreatorUserName = "TestUser" } },
                TotalCount = 1,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        
            SetupUserAuth(userId);
            _mockFeedService.Setup(s => s.GetFollowedRecentEventFeed(pageNumber, pageSize, userId))
                .ReturnsAsync(paginatedResponse);
        
            // Act
            var result = await _feedController.GetPaginatedRecentFollowedEvents(pageNumber, pageSize);
        
            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnValue = okResult.Value.Should().BeOfType<PaginatedResponseDTO<EventDTO>>().Subject;
            returnValue.Should().BeEquivalentTo(paginatedResponse);
        }
        
        [Fact]
        public async Task GetPaginatedUpcomingFollowedEvents_ShouldReturnOk()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var userId = "testUserId";
            var paginatedResponse = new PaginatedResponseDTO<EventDTO>
            {
                Items = new List<EventDTO> { new EventDTO { EventId = 1, CreatorId = "user2", Date = DateTime.Now.AddDays(7) } },
                TotalCount = 1,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        
            SetupUserAuth(userId);
            _mockFeedService.Setup(s => s.GetFollowedUpcomingEventFeed(pageNumber, pageSize, userId))
                .ReturnsAsync(paginatedResponse);
        
            // Act
            var result = await _feedController.GetPaginatedUpcomingFollowedEvents(pageNumber, pageSize);
        
            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnValue = okResult.Value.Should().BeOfType<PaginatedResponseDTO<EventDTO>>().Subject;
            returnValue.Should().BeEquivalentTo(paginatedResponse);
        }

        [Fact]
        public async Task GetPaginatedRecentFollowedBlogs_ShouldHandleException()
        {
            // Arrange
            var userId = "testUserId";
            SetupUserAuth(userId);
            _mockFeedService.Setup(s => s.GetFollowedRecentBlogsFeed(It.IsAny<int>(), It.IsAny<int>(), userId))
                .ThrowsAsync(new Exception("Test exception"));
        
            // Act
            var result = await _feedController.GetPaginatedRecentFollowedBlogs(1, 10);
        
            // Assert
            result.Should().BeOfType<ObjectResult>()
                  .Which.StatusCode.Should().Be(500);
        }
        
        [Fact]
        public async Task GetPaginatedUpcomingFollowedEvents_ShouldHandleException()
        {
            // Arrange
            var userId = "testUserId";
            SetupUserAuth(userId);
            _mockFeedService.Setup(s => s.GetFollowedUpcomingEventFeed(It.IsAny<int>(), It.IsAny<int>(), userId))
                .ThrowsAsync(new Exception("Test exception"));
        
            // Act
            var result = await _feedController.GetPaginatedUpcomingFollowedEvents(1, 10);
        
            // Assert
            result.Should().BeOfType<ObjectResult>()
                  .Which.StatusCode.Should().Be(500);
        }
        
        [Theory]
        [InlineData(0, 10)]
        [InlineData(1, 0)]
        public async Task GetPaginatedRecentFollowedFeeds_ShouldAdjustInvalidParameters(int pageNumber, int pageSize)
        {
            // Arrange
            var userId = "testUserId";
            SetupUserAuth(userId);
            var paginatedResponse = new PaginatedResponseDTO<RecipeDTO>
            {
                Items = new List<RecipeDTO>(),
                TotalCount = 0,
                PageNumber = 1,
                PageSize = 10
            };
        
            _mockFeedService.Setup(s => s.GetFollowedRecentRecipeFeed(1, 10, userId))
                .ReturnsAsync(paginatedResponse);
        
            // Act
            var result = await _feedController.GetPaginatedRecentFollowedRecipes(pageNumber, pageSize);
        
            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnValue = okResult.Value.Should().BeOfType<PaginatedResponseDTO<RecipeDTO>>().Subject;
            returnValue.PageNumber.Should().Be(1);
            returnValue.PageSize.Should().Be(10);
        }
        
        [Fact]
        public async Task GetPaginatedRecentFollowedEvents_ShouldReturnEmptyList_WhenNoEvents()
        {
            // Arrange
            var userId = "testUserId";
            SetupUserAuth(userId);
            var emptyResponse = new PaginatedResponseDTO<EventDTO>
            {
                Items = new List<EventDTO>(),
                TotalCount = 0,
                PageNumber = 1,
                PageSize = 10
            };
        
            _mockFeedService.Setup(s => s.GetFollowedRecentEventFeed(1, 10, userId))
                .ReturnsAsync(emptyResponse);
        
            // Act
            var result = await _feedController.GetPaginatedRecentFollowedEvents(1, 10);
        
            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnValue = okResult.Value.Should().BeOfType<PaginatedResponseDTO<EventDTO>>().Subject;
            returnValue.Items.Should().BeEmpty();
            returnValue.TotalCount.Should().Be(0);
        }

        [Fact]
        public async Task GetPaginatedRecipes_ShouldReturnOk_WithPaginatedRecipes()
        {
            // Arrange
            var seed = "testseed";
            var pageNumber = 1;
            var pageSize = 10;
            string? userId = null;
            var paginatedResponse = new PaginatedResponseDTO<RecipeDTO>
            {
                Items = new List<RecipeDTO>
                {
                    new RecipeDTO
                    {
                        Id = 1,
                        Header = "Test Recipe",
                        BodyText = "Test Description",
                        UserId = "user1",
                        UserName = "TestUser",
                        ServingSize = 4,
                        PreparationTime = 30,
                        CreatedAt = DateTime.Now
                    }
                },
                TotalCount = 1,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            // Setup controller context with no authentication
            _feedController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            _mockFeedService.Setup(s => s.GetRecipeFeed(seed, pageNumber, pageSize, userId))
                            .ReturnsAsync(paginatedResponse);

            // Act
            var result = await _feedController.GetPaginatedRecipes(seed, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(paginatedResponse);
        }

        [Fact]
        public async Task GetPaginatedRecipes_WithAuth_ShouldReturnOk_WithPaginatedRecipes()
        {
            // Arrange
            var seed = "testseed";
            var pageNumber = 1;
            var pageSize = 10;
            var userId = "testUserId";
            var paginatedResponse = new PaginatedResponseDTO<RecipeDTO>
            {
                Items = new List<RecipeDTO>
                {
                    new RecipeDTO
                    {
                        Id = 1,
                        Header = "Test Recipe",
                        BodyText = "Test Description",
                        UserId = "user1",
                        UserName = "TestUser",
                        ServingSize = 4,
                        PreparationTime = 30,
                        CreatedAt = DateTime.Now
                    }
                },
                TotalCount = 1,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
            var identity = new ClaimsIdentity(claims, "Test");
            var user = new ClaimsPrincipal(identity);
            _feedController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            _mockFeedService.Setup(s => s.GetRecipeFeed(seed, pageNumber, pageSize, userId))
                            .ReturnsAsync(paginatedResponse);

            // Act
            var result = await _feedController.GetPaginatedRecipes(seed, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(paginatedResponse);
        }
    }
}