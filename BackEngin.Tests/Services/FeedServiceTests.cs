using BackEngin.Services;
using BackEngin.Services.Interfaces;
using DataAccess.Repositories;
using FluentAssertions;
using Models;
using Models.DTO;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace BackEngin.Tests.Services
{
    public class FeedServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IEventService> _mockEventService;
        private readonly FeedService _feedService;

        public FeedServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockEventService = new Mock<IEventService>();
            _feedService = new FeedService(_mockUnitOfWork.Object, _mockEventService.Object);
        }

        [Fact]
        public async Task GetEventFeed_ShouldReturnPaginatedEvents()
        {
            // Arrange
            var seed = "test";
            var page = 1;
            var pageSize = 10;
            var events = new List<Events>
            {
                new Events
                {
                    Id = 1,
                    Title = "Test Event",
                    BodyText = "Test Description",
                    CreatorId = "user1",
                    Creator = new Users { UserName = "TestUser" },
                    Date = DateTime.Now.AddDays(1),
                    CreatedAt = DateTime.Now,
                    Address = new Addresses
                    {
                        District = new Districts
                        {
                            City = new Cities
                            {
                                Country = new Countries()
                            }
                        }
                    }
                }
            };

            var eventDTO = new EventDTO
            {
                EventId = 1,
                Title = "Test Event",
                BodyText = "Test Description",
                CreatorUserName = "TestUser"
            };

            _mockUnitOfWork.Setup(u => u.Events.GetPaginatedBySeedAsync(
                It.IsAny<uint>(),
                It.IsAny<uint>(),
                It.IsAny<uint>(),
                It.IsAny<Expression<Func<Events, bool>>>(),
                page,
                pageSize,
                "Creator,Address,Address.District,Address.District.City,Address.District.City.Country"))
                .ReturnsAsync((events, 1));

            _mockEventService.Setup(e => e.MapEventToDto(It.IsAny<Events>()))
                .Returns(eventDTO);

            // Act
            var result = await _feedService.GetEventFeed(seed, page, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(1);
            result.TotalCount.Should().Be(1);
            result.PageNumber.Should().Be(page);
            result.PageSize.Should().Be(pageSize);
            result.Items.First().Should().BeEquivalentTo(eventDTO);
        }

        [Fact]
        public async Task GetBlogsFeed_ShouldReturnPaginatedBlogs()
        {
            // Arrange
            var seed = "test";
            var page = 1;
            var pageSize = 10;
            var blogs = new List<Blogs>
            {
                new Blogs
                {
                    Id = 1,
                    Header = "Test Blog",
                    BodyText = "Test Content",
                    UserId = "user1",
                    User = new Users { UserName = "TestUser" },
                    CreatedAt = DateTime.Now
                }
            };

            _mockUnitOfWork.Setup(u => u.Blogs.GetPaginatedBySeedAsync(
                It.IsAny<uint>(),
                It.IsAny<uint>(),
                It.IsAny<uint>(),
                page,
                pageSize,
                "User"))
                .ReturnsAsync((blogs, 1));

            // Act
            var result = await _feedService.GetBlogsFeed(seed, page, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(1);
            result.TotalCount.Should().Be(1);
            result.PageNumber.Should().Be(page);
            result.PageSize.Should().Be(pageSize);
            result.Items.First().Header.Should().Be("Test Blog");
            result.Items.First().UserName.Should().Be("TestUser");
        }

        [Fact]
        public async Task GetRecipeFeed_ShouldReturnPaginatedRecipes()
        {
            // Arrange
            var seed = "test";
            var page = 1;
            var pageSize = 10;
            var userId = "testUser";
            var recipes = new List<Recipes>
            {
                new Recipes
                {
                    Id = 1,
                    Header = "Test Recipe",
                    BodyText = "Test Content",
                    UserId = "user1",
                    User = new Users { UserName = "TestUser" },
                    ServingSize = 4,
                    PreparationTime = 30,
                    CreatedAt = DateTime.Now
                }
            };

            _mockUnitOfWork.Setup(u => u.User_Allergens.FindAsync(It.IsAny<Func<User_Allergens, bool>>()))
                .ReturnsAsync(new List<User_Allergens>());

            _mockUnitOfWork.Setup(u => u.Ingredients_Preferences.FindAsync(It.IsAny<Func<Ingredients_Preferences, bool>>()))
                .ReturnsAsync(new List<Ingredients_Preferences>());

            _mockUnitOfWork.Setup(u => u.Recipes.GetPaginatedBySeedAsync(
                It.IsAny<uint>(),
                It.IsAny<uint>(),
                It.IsAny<uint>(),
                It.IsAny<Expression<Func<Recipes, bool>>>(),
                page,
                pageSize,
                "User,Recipes_Ingredients"))
                .ReturnsAsync((recipes, 1));

            // Act
            var result = await _feedService.GetRecipeFeed(seed, page, pageSize, userId);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(1);
            result.TotalCount.Should().Be(1);
            result.PageNumber.Should().Be(page);
            result.PageSize.Should().Be(pageSize);
            result.Items.First().Header.Should().Be("Test Recipe");
            result.Items.First().UserName.Should().Be("TestUser");
            result.Items.First().ServingSize.Should().Be(4);
            result.Items.First().PreparationTime.Should().Be(30);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("different")]
        public void GetSeedValue_ShouldReturnConsistentValues(string seed)
        {
            // Act
            var result1 = _feedService.GetType()
                .GetMethod("GetSeedValue", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(_feedService, new object[] { seed });

            var result2 = _feedService.GetType()
                .GetMethod("GetSeedValue", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(_feedService, new object[] { seed });

            // Assert
            result1.Should().Be(result2);
        }

        [Theory]
        [InlineData((uint)1000)]
        [InlineData((uint)5000)]
        public void GetMultiplier_ShouldReturnValueInValidRange(uint seedValue)
        {
            // Act
            var result = (uint)_feedService.GetType()
                .GetMethod("GetMultiplier", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(_feedService, new object[] { seedValue });

            // Assert
            result.Should().BeGreaterOrEqualTo(5531);
            result.Should().BeLessThan(11662); // 6131 + 5531
        }

        [Fact]
        public async Task GetFollowedRecentRecipeFeed_ShouldReturnPaginatedRecipes_FromFollowedUsers()
        {
            // Arrange
            var userId = "user1";
            var page = 1;
            var pageSize = 10;
            var following = new List<UserCompactDTO>
            {
                new UserCompactDTO { UserId = "user2", UserName = "TestUser" },
                new UserCompactDTO { UserId = "user3", UserName = "TestUser2" }
            };

            var recipes = new List<Recipes>
            {
                new Recipes
                {
                    Id = 1,
                    Header = "Recent Recipe",
                    UserId = "user2",
                    User = new Users { UserName = "TestUser" },
                    CreatedAt = DateTime.Now.AddHours(-1)
                }
            };

            _mockUnitOfWork.Setup(u => u.Users.GetAllFollowingAsync(userId))
                .ReturnsAsync(following);

            _mockUnitOfWork.Setup(u => u.User_Allergens.FindAsync(It.IsAny<Func<User_Allergens, bool>>()))
                .ReturnsAsync(new List<User_Allergens>());

            _mockUnitOfWork.Setup(u => u.Ingredients_Preferences.FindAsync(It.IsAny<Func<Ingredients_Preferences, bool>>()))
                .ReturnsAsync(new List<Ingredients_Preferences>());

            _mockUnitOfWork.Setup(u => u.Recipes.GetPaginatedByFollowedAsync(
                It.IsAny<Expression<Func<Recipes, bool>>>(),
                page,
                pageSize,
                "User,Recipes_Ingredients"))
                .ReturnsAsync((recipes, 1));

            // Act
            var result = await _feedService.GetFollowedRecentRecipeFeed(page, pageSize, userId);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(1);
            result.TotalCount.Should().Be(1);
        }

        [Fact]
        public async Task GetFollowedRecentBlogsFeed_ShouldReturnPaginatedBlogs_FromFollowedUsers()
        {
            // Arrange
            var userId = "user1";
            var page = 1;
            var pageSize = 10;
            var following = new List<UserCompactDTO>
            {
                new UserCompactDTO { UserId = "user2", UserName = "TestUser" }
            };

            var blogs = new List<Blogs>
            {
                new Blogs
                {
                    Id = 1,
                    Header = "Recent Blog",
                    UserId = "user2",
                    User = new Users { UserName = "TestUser" },
                    CreatedAt = DateTime.Now.AddHours(-1)
                }
            };

            _mockUnitOfWork.Setup(u => u.Users.GetAllFollowingAsync(userId))
                .ReturnsAsync(following);

            _mockUnitOfWork.Setup(u => u.Blogs.GetPaginatedByFollowedAsync(
                It.IsAny<Expression<Func<Blogs, bool>>>(),
                page,
                pageSize,
                "User"))
                .ReturnsAsync((blogs, 1));

            // Act
            var result = await _feedService.GetFollowedRecentBlogsFeed(page, pageSize, userId);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(1);
            result.TotalCount.Should().Be(1);
            result.Items.First().UserName.Should().Be("TestUser");
        }

        [Fact]
        public async Task GetFollowedUpcomingEventFeed_ShouldReturnPaginatedEvents_FromFollowedUsers_OrderedByDate()
        {
            // Arrange
            var userId = "user1";
            var page = 1;
            var pageSize = 10;
            var following = new List<UserCompactDTO>
            {
                new UserCompactDTO { UserId = "user2", UserName = "TestUser" }
            };

            var futureDate = DateTime.Now.AddDays(7);
            var events = new List<Events>
            {
                new Events
                {
                    Id = 1,
                    Title = "Upcoming Event",
                    CreatorId = "user2",
                    Date = futureDate,
                    Creator = new Users { UserName = "TestUser" },
                    Address = new Addresses
                    {
                        District = new Districts
                        {
                            City = new Cities
                            {
                                Country = new Countries()
                            }
                        }
                    }
                }
            };

            var eventDTO = new EventDTO
            {
                EventId = 1,
                Title = "Upcoming Event",
                Date = futureDate
            };

            _mockUnitOfWork.Setup(u => u.Users.GetAllFollowingAsync(userId))
                .ReturnsAsync(following);

            _mockUnitOfWork.Setup(u => u.Events.GetPaginatedByFollowedAsync(
                It.IsAny<Expression<Func<Events, bool>>>(),
                It.IsAny<Expression<Func<Events, DateTime>>>(),
                page,
                pageSize,
                "Creator,Address,Address.District,Address.District.City,Address.District.City.Country"))
                .ReturnsAsync((events, 1));

            _mockEventService.Setup(e => e.MapEventToDto(It.IsAny<Events>()))
                .Returns(eventDTO);

            // Act
            var result = await _feedService.GetFollowedUpcomingEventFeed(page, pageSize, userId);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(1);
            result.TotalCount.Should().Be(1);
            result.Items.First().Date.Should().Be(futureDate);
        }

        [Fact]
        public async Task GetFollowedRecentFeed_ShouldReturnEmptyList_WhenUserHasNoFollowing()
        {
            // Arrange
            var userId = "testUser";
            var page = 1;
            var pageSize = 10;
            var following = new List<UserCompactDTO>();

            _mockUnitOfWork.Setup(u => u.Users.GetAllFollowingAsync(userId))
                .ReturnsAsync(following);

            _mockUnitOfWork.Setup(u => u.User_Allergens.FindAsync(It.IsAny<Func<User_Allergens, bool>>()))
                .ReturnsAsync(new List<User_Allergens>());

            _mockUnitOfWork.Setup(u => u.Ingredients_Preferences.FindAsync(It.IsAny<Func<Ingredients_Preferences, bool>>()))
                .ReturnsAsync(new List<Ingredients_Preferences>());

            _mockUnitOfWork.Setup(u => u.Recipes.GetPaginatedByFollowedAsync(
                It.IsAny<Expression<Func<Recipes, bool>>>(),
                page,
                pageSize,
                "User,Recipes_Ingredients"))
                .ReturnsAsync((new List<Recipes>(), 0));

            // Act
            var result = await _feedService.GetFollowedRecentRecipeFeed(page, pageSize, userId);

            // Assert
            result.Items.Should().BeEmpty();
            result.TotalCount.Should().Be(0);
        }

        [Fact]
        public async Task GetFollowedRecentRecipeFeed_ShouldHandleDatabaseException()
        {
            // Arrange
            var userId = "user1";
            _mockUnitOfWork.Setup(u => u.Users.GetAllFollowingAsync(userId))
                .ThrowsAsync(new Exception("Database error"));
        
            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => 
                _feedService.GetFollowedRecentRecipeFeed(1, 10, userId));
        }
        
        [Fact]
        public async Task GetFollowedRecentRecipeFeed_ShouldHandleLargePageNumbers()
        {
            // Arrange
            var userId = "user1";
            var page = 1000;
            var pageSize = 10;
            var following = new List<UserCompactDTO> { new UserCompactDTO { UserId = "user2" } };

            _mockUnitOfWork.Setup(u => u.Users.GetAllFollowingAsync(userId))
                .ReturnsAsync(following);

            _mockUnitOfWork.Setup(u => u.User_Allergens.FindAsync(It.IsAny<Func<User_Allergens, bool>>()))
                .ReturnsAsync(new List<User_Allergens>());

            _mockUnitOfWork.Setup(u => u.Ingredients_Preferences.FindAsync(It.IsAny<Func<Ingredients_Preferences, bool>>()))
                .ReturnsAsync(new List<Ingredients_Preferences>());

            _mockUnitOfWork.Setup(u => u.Recipes.GetPaginatedByFollowedAsync(
                It.IsAny<Expression<Func<Recipes, bool>>>(),
                page,
                pageSize,
                "User,Recipes_Ingredients"))
                .ReturnsAsync((new List<Recipes>(), 0));

            // Act
            var result = await _feedService.GetFollowedRecentRecipeFeed(page, pageSize, userId);

            // Assert
            result.Items.Should().BeEmpty();
            result.PageNumber.Should().Be(page);
        }
        
        [Fact]
        public async Task GetEventFeed_ShouldReturnConsistentResults_ForSameSeed()
        {
            // Arrange
            var seed = "test";
            var page = 1;
            var pageSize = 10;
            var events = new List<Events> 
            { 
                new Events { Id = 1, Creator = new Users { UserName = "Test" } }
            };
        
            _mockUnitOfWork.Setup(u => u.Events.GetPaginatedBySeedAsync(
                It.IsAny<uint>(),
                It.IsAny<uint>(),
                It.IsAny<uint>(),
                page,
                pageSize,
                "Creator,Address,Address.District,Address.District.City,Address.District.City.Country"))
                .ReturnsAsync((events, 1));
        
            // Act
            var result1 = await _feedService.GetEventFeed(seed, page, pageSize);
            var result2 = await _feedService.GetEventFeed(seed, page, pageSize);
        
            // Assert
            result1.Should().BeEquivalentTo(result2);
        }
    }
}