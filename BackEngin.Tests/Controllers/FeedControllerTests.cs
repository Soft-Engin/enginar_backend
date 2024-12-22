using BackEngin.Controllers;
using BackEngin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Moq;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

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
        public async Task GetPaginatedRecipes_ShouldReturnOk_WithPaginatedRecipes()
        {
            // Arrange
            var seed = "testseed";
            var pageNumber = 1;
            var pageSize = 10;
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

            _mockFeedService.Setup(s => s.GetRecipeFeed(seed, pageNumber, pageSize))
                           .ReturnsAsync(paginatedResponse);

            // Act
            var result = await _feedController.GetPaginatedRecipes(seed, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(paginatedResponse);
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
            
            _mockFeedService.Setup(s => s.GetRecipeFeed(seed, expectedPageNumber, pageSize))
                           .ReturnsAsync(new PaginatedResponseDTO<RecipeDTO>());

            // Act
            await _feedController.GetPaginatedRecipes(seed, pageNumber, pageSize);

            // Assert
            _mockFeedService.Verify(s => s.GetRecipeFeed(seed, expectedPageNumber, pageSize), Times.Once());
        }

        [Fact]
        public async Task GetPaginatedRecipes_ShouldHandleException()
        {
            // Arrange
            _mockFeedService.Setup(s => s.GetRecipeFeed(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
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
    }
}