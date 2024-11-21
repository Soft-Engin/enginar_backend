using BackEngin.Controllers;
using BackEngin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using FluentAssertions;
using Models.DTO;

namespace BackEngin.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UserController _userController;

        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _userController = new UserController(_mockUserService.Object);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnOk_WithUserListDTO()
        {
            // Arrange
            var userListDto = new UserListDTO
            {
                TotalCount = 2,
                Users = new List<UserDTO>
                {
                    new UserDTO
                    {
                        Id = "1",
                        FirstName = "John",
                        LastName = "Doe",
                        UserName = "johndoe",
                        Email = "johndoe@example.com",
                        PhoneNumber = "1234567890",
                        Address = new Addresses
                        {
                            Name = "Home",
                            Street = "123 Main St"
                        },
                        Role = new Roles
                        {
                            Name = "Admin"
                        }
                    },
                    new UserDTO
                    {
                        Id = "2",
                        FirstName = "Jane",
                        LastName = "Smith",
                        UserName = "janesmith",
                        Email = "janesmith@example.com",
                        PhoneNumber = "9876543210",
                        Address = new Addresses
                        {
                            Name = "Office",
                            Street = "456 Elm St"
                        },
                        Role = new Roles
                        {
                            Name = "User"
                        }
                    }
                }
            };

            _mockUserService.Setup(us => us.GetAllUsersAsync()).ReturnsAsync(userListDto);

            // Act
            var result = await _userController.GetUsers();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            okResult.Value.Should().BeEquivalentTo(userListDto);

            _mockUserService.Verify(us => us.GetAllUsersAsync(), Times.Once);
        }


        [Fact]
        public async Task GetUserById_ShouldReturnOk_WithUser()
        {
            // Arrange
            var userDto = new GetUserByIdDTO
            {
                UserName = "JohnDoe",
                Email = "johndoe@example.com",
                FirstName = "John",
                LastName = "Doe",
                AddressName = "Home",
                Street = "123 Main St",
                District = "Downtown",
                City = "Metropolis",
                Country = "CountryLand",
                PostCode = 12345,
                RoleName = "User"
            };

            _mockUserService.Setup(us => us.GetUserByIdAsync("1")).ReturnsAsync(userDto);

            // Act
            var result = await _userController.GetUserById("1");

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(userDto);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            _mockUserService.Setup(us => us.GetUserByIdAsync("1")).ReturnsAsync((GetUserByIdDTO)null);

            // Act
            var result = await _userController.GetUserById("1");

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnOk_WhenUpdateIsSuccessful()
        {
            // Arrange
            var userId = "1";
            var updateUserDto = new UpdateUserDto
            {
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                Email = "updated.email@example.com",
                UserName = "updatedusername",
                PhoneNumber = "9876543210",
                AddressName = "Updated Address",
                Street = "Updated Street",
                District = "Updated District",
                City = "Updated City",
                Country = "Updated Country",
                PostCode = 12345
            };

            _mockUserService.Setup(us => us.UpdateUserAsync(userId, updateUserDto)).ReturnsAsync(updateUserDto);

            // Act
            var result = await _userController.UpdateUser(userId, updateUserDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(updateUserDto);
        }


        [Fact]
        public async Task UpdateUser_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "1";
            var updateUserDto = new UpdateUserDto
            {
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                Email = "updated.email@example.com",
                UserName = "updatedusername",
                PhoneNumber = "9876543210",
                AddressName = "Updated Address",
                Street = "Updated Street",
                District = "Updated District",
                City = "Updated City",
                Country = "Updated Country",
                PostCode = 12345
            };

            _mockUserService.Setup(us => us.UpdateUserAsync(userId, updateUserDto)).ReturnsAsync((UpdateUserDto)null);

            // Act
            var result = await _userController.UpdateUser(userId, updateUserDto);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }



        [Fact]
        public async Task DeleteUser_ShouldReturnNoContent_WhenDeletionIsSuccessful()
        {
            // Arrange
            _mockUserService.Setup(us => us.DeleteUserAsync("1")).ReturnsAsync(true);

            // Act
            var result = await _userController.DeleteUser("1");

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            _mockUserService.Setup(us => us.DeleteUserAsync("1")).ReturnsAsync(false);

            // Act
            var result = await _userController.DeleteUser("1");

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetFollowers_ShouldReturnOk_WithFollowersDTO()
        {
            // Arrange
            var userId = "user1";
            var followersDto = new FollowersDTO
            {
                Usernames = new List<string> { "follower1", "follower2" },
                TotalCount = 2
            };

            _mockUserService.Setup(s => s.GetFollowersAsync(userId))
                .ReturnsAsync(followersDto);

            // Act
            var result = await _userController.GetFollowers(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().BeEquivalentTo(followersDto);
        }

        [Fact]
        public async Task FollowUser_ShouldReturnOk_WhenFollowSucceeds()
        {
            // Arrange
            var userId = "user1";
            var targetUserId = "user2";

            _mockUserService.Setup(s => s.FollowUserAsync(userId, targetUserId))
                .ReturnsAsync(true);

            // Act
            var result = await _userController.FollowUser(userId, targetUserId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().BeEquivalentTo(new { Message = "Successfully followed user." });
        }

        [Fact]
        public async Task FollowUser_ShouldReturnBadRequest_WhenFollowFails()
        {
            // Arrange
            var userId = "user1";
            var targetUserId = "user2";

            _mockUserService.Setup(s => s.FollowUserAsync(userId, targetUserId))
                .ReturnsAsync(false);

            // Act
            var result = await _userController.FollowUser(userId, targetUserId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            badRequestResult.Value.Should().Be("Unable to follow user. Perhaps you already follow them.");
        }

        [Fact]
        public async Task UnfollowUser_ShouldReturnOk_WhenUnfollowSucceeds()
        {
            // Arrange
            var userId = "user1";
            var targetUserId = "user2";

            _mockUserService.Setup(s => s.UnfollowUserAsync(userId, targetUserId))
                .ReturnsAsync(true);

            // Act
            var result = await _userController.UnfollowUser(userId, targetUserId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().BeEquivalentTo(new { Message = "Successfully unfollowed user." });
        }

        [Fact]
        public async Task GetBookmarkedRecipes_ShouldReturnOk_WhenBookmarksExist()
        {
            // Arrange
            var userId = "1";
            var user = new Users { Id = userId, UserName = "john_doe" }; // Mock user

            // Create mocked bookmark recipes data
            var bookmarkedRecipes = new List<BookmarkRecipesItemDTO>
            {
                new BookmarkRecipesItemDTO { UserName = "john_doe", Header = "Recipe1", BodyText = "Delicious!" },
                new BookmarkRecipesItemDTO { UserName = "john_doe", Header = "Recipe2", BodyText = "Tasty!" }
            };

            var bookmarkRecipesDTO = new BookmarkRecipesDTO
            {
                Recipes = bookmarkedRecipes,
                TotalCount = 2
            };

            // Mock the service to return the BookmarkRecipesDTO
            _mockUserService.Setup(s => s.GetBookmarkedRecipesAsync(userId))
                .ReturnsAsync(bookmarkRecipesDTO);

            // Act
            var result = await _userController.GetBookmarkedRecipes(userId);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(bookmarkRecipesDTO);
        }

        [Fact]
        public async Task GetBookmarkedRecipes_ShouldReturnNotFound_WhenNoBookmarksExist()
        {
            // Arrange
            var userId = "1";

            // Mock the service to return null (no bookmarked recipes)
            _mockUserService.Setup(s => s.GetBookmarkedRecipesAsync(userId))
                .ReturnsAsync((BookmarkRecipesDTO)null);

            // Act
            var result = await _userController.GetBookmarkedRecipes(userId);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
            notFoundResult.Value.Should().Be("No bookmarked recipes found for this user.");
        }

        [Fact]
        public async Task GetBookmarkedBlogs_ShouldReturnOkResultWithBlogs_WhenBookmarksExist()
        {
            // Arrange
            var userId = "1";

            var mockedBlogs = new List<BookmarkBlogsItemDTO>
            {
                new BookmarkBlogsItemDTO { UserName = "User1", Header = "Blog 1", BodyText = "Content of Blog 1" },
                new BookmarkBlogsItemDTO { UserName = "User2", Header = "Blog 2", BodyText = "Content of Blog 2" }
            };

            var bookmarkBlogsDTO = new BookmarkBlogsDTO
            {
                Blogs = mockedBlogs,
                TotalCount = mockedBlogs.Count
            };

            _mockUserService.Setup(s => s.GetBookmarkedBlogsAsync(userId))
                .ReturnsAsync(bookmarkBlogsDTO);

            // Act
            var result = await _userController.GetBookmarkedBlogs(userId);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

            var returnedValue = okResult.Value as BookmarkBlogsDTO;
            returnedValue.Should().NotBeNull();
            returnedValue.Blogs.Should().HaveCount(2);
            returnedValue.TotalCount.Should().Be(2);
            _mockUserService.Verify(s => s.GetBookmarkedBlogsAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetBookmarkedBlogs_ShouldReturnNotFound_WhenNoBookmarksExist()
        {
            // Arrange
            var userId = "1";

            _mockUserService.Setup(s => s.GetBookmarkedBlogsAsync(userId))
                .ReturnsAsync((BookmarkBlogsDTO)null);

            // Act
            var result = await _userController.GetBookmarkedBlogs(userId);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
            notFoundResult.Value.Should().Be("No bookmarked blogs found for this user.");
            _mockUserService.Verify(s => s.GetBookmarkedBlogsAsync(userId), Times.Once);
        }


    }
}
