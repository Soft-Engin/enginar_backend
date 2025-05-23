﻿using BackEngin.Controllers;
using BackEngin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Moq;
using FluentAssertions;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BackEngin.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UserController _userController;
        private readonly Mock<ClaimsPrincipal> _mockUser;

        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();

            // Mock user context
            _mockUser = new Mock<ClaimsPrincipal>();
            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier))
                     .Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role))
                     .Returns(new Claim(ClaimTypes.Role, "User"));

            // Setup controller with mock user
            _userController = new UserController(_mockUserService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = _mockUser.Object }
                }
            };
        }

        [Fact]
        public async Task GetUsers_ShouldReturnOk_WithUserListDTO()
        {
            // Arrange
            var userListDto = new PaginatedResponseDTO<UserDTO>
            {
                TotalCount = 2,
                Items = new List<UserDTO>
                {
                    new UserDTO
                    {
                        UserId = "1",
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
                        UserId = "2",
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

            _mockUserService.Setup(us => us.GetAllUsersAsync(1, 10)).ReturnsAsync(userListDto);

            // Act
            var result = await _userController.GetUsers(1, 10);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            okResult.Value.Should().BeEquivalentTo(userListDto);

            _mockUserService.Verify(us => us.GetAllUsersAsync(1, 10), Times.Once);
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
            var userId = "currentUserId";
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

            var updateResultUserDto = new UpdateUserResultDto
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
                PostCode = 12345,
                UserId = userId
            };

            _mockUserService.Setup(us => us.UpdateUserAsync(userId, updateUserDto)).ReturnsAsync(updateResultUserDto);

            // Act
            var result = await _userController.UpdateUser(userId, updateUserDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(updateResultUserDto);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "currentUserId";
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

            _mockUserService.Setup(us => us.UpdateUserAsync(userId, updateUserDto)).ReturnsAsync((UpdateUserResultDto)null);

            // Act
            var result = await _userController.UpdateUser(userId, updateUserDto);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnOk_WhenUserIsAuthorizedAndDeletionIsSuccessful()
        {
            // Arrange
            var userId = "currentUserId"; // The user to be deleted

            // Mock the user context to simulate the current user's claims
            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier)).Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role)).Returns(new Claim(ClaimTypes.Role, "User"));

            // Mock the deletion method to simulate a successful deletion
            _mockUserService.Setup(us => us.DeleteUserAsync(userId)).ReturnsAsync(true);

            // Act
            var result = await _userController.DeleteUser(userId);

            // Assert
            result.Should().BeOfType<OkObjectResult>(); // Expecting successful deletion with OkObjectResult

            // Verify that DeleteUserAsync was called once
            _mockUserService.Verify(us => us.DeleteUserAsync(userId), Times.Once);
        }



        [Fact]
        public async Task DeleteUser_ShouldReturnUnauthorized_WhenUserIsNotAuthorizedAndDeletionIsNotSuccessful()
        {
            // Arrange
            var userId = "otherUserId"; // The user to be deleted

            // Mock the user context to simulate the current user's claims
            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier)).Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role)).Returns(new Claim(ClaimTypes.Role, "User"));

            // Mock the deletion method to simulate a successful deletion
            _mockUserService.Setup(us => us.DeleteUserAsync(userId)).ReturnsAsync(false);

            // Act
            var result = await _userController.DeleteUser(userId);

            // Assert
            result.Should().BeOfType<UnauthorizedResult>(); // Expecting successful deletion with OkObjectResult

            // Verify that DeleteUserAsync was called once
            //_mockUserService.Verify(us => us.DeleteUserAsync(userId), Times.Once);
        }




        [Fact]
        public async Task DeleteUser_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "currentUserId"; // The user to be deleted

            // Mock the user context to simulate the current user's claims
            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier)).Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role)).Returns(new Claim(ClaimTypes.Role, "User"));

            _mockUserService.Setup(us => us.DeleteUserAsync(userId)).ReturnsAsync(false);

            // Act
            var result = await _userController.DeleteUser(userId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetFollowers_ShouldReturnOk_WithFollowersDTO()
        {
            // Arrange
            var userId = "user1";
            var followersDto = new PaginatedResponseDTO<UserCompactDTO>
            {
                Items = new List<UserCompactDTO>
                {
                    new UserCompactDTO { UserId = "following1", UserName = "Following1" },
                    new UserCompactDTO { UserId = "following2", UserName = "Following2" }
                },
                TotalCount = 2
            };

            _mockUserService.Setup(s => s.GetFollowersAsync(userId, 1, 10))
                .ReturnsAsync(followersDto);

            // Act
            var result = await _userController.GetFollowers(userId, 1, 10);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().BeEquivalentTo(followersDto);
        }

        [Fact]
        public async Task FollowUser_ShouldReturnOk_WhenFollowSucceeds()
        {
            // Arrange
            var userId = "currentUserId";
            var targetUserId = "user2";

            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier)).Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role)).Returns(new Claim(ClaimTypes.Role, "User"));

            _mockUserService.Setup(s => s.FollowUserAsync(userId, targetUserId))
                .ReturnsAsync(true);

            // Act
            var result = await _userController.FollowUser(targetUserId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().BeEquivalentTo(new { message = "Successfully followed user." });
        }

        [Fact]
        public async Task UnfollowUser_ShouldReturnOk_WhenUnfollowSucceeds()
        {
            // Arrange
            var userId = "currentUserId";
            var targetUserId = "user2";

            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier)).Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role)).Returns(new Claim(ClaimTypes.Role, "User"));

            _mockUserService.Setup(s => s.UnfollowUserAsync(userId, targetUserId))
                .ReturnsAsync(true);

            // Act
            var result = await _userController.UnfollowUser(targetUserId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().BeEquivalentTo(new { message = "Successfully unfollowed user." });
        }


        [Fact]
        public async Task UnfollowUser_ShouldReturnBadRequest_WhenUnfollowIsNotSuccessful()
        {
            // Arrange
            var userId = "otherUserId";
            var targetUserId = "user2";

            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier)).Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role)).Returns(new Claim(ClaimTypes.Role, "User"));

            _mockUserService.Setup(s => s.UnfollowUserAsync(userId, targetUserId))
                .ReturnsAsync(false);

            // Act
            var result = await _userController.UnfollowUser(targetUserId);

            // Assert
            var okResult = Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public async Task GetBookmarkedRecipes_ShouldReturnOk_WhenBookmarksExist()
        {
            // Arrange
            var userId = "currentUserId";
            var bookmarkedRecipes = new PaginatedResponseDTO<BookmarkRecipesItemDTO>
            {
                Items = new List<BookmarkRecipesItemDTO>
                {
                    new BookmarkRecipesItemDTO { UserName = "john_doe", Header = "Recipe1", BodyText = "Delicious!" },
                    new BookmarkRecipesItemDTO { UserName = "john_doe", Header = "Recipe2", BodyText = "Tasty!" }
                },
                TotalCount = 2
            };

            _mockUserService.Setup(s => s.GetBookmarkedRecipesAsync(userId, 1, 10))
                .ReturnsAsync(bookmarkedRecipes);

            // Act
            var result = await _userController.GetBookmarkedRecipes(userId, 1, 10);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(bookmarkedRecipes);
        }

        [Fact]
        public async Task GetBookmarkedRecipes_ShouldReturnNotFound_WhenNoBookmarksExist()
        {
            // Arrange
            var userId = "currentUserId";

            _mockUserService.Setup(s => s.GetBookmarkedRecipesAsync(userId, 1, 10))
                .ReturnsAsync((PaginatedResponseDTO<BookmarkRecipesItemDTO>)null);

            // Act
            var result = await _userController.GetBookmarkedRecipes(userId, 1, 10);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task SearchUsers_ShouldReturnOk_WithFilteredUsers()
        {
            // Arrange
            var searchParams = new UserSearchParams
            {
                UserNameContains = "johndoe",
                First_LastNameContains = "John",
                EmailContains = "example.com",
                SortBy = "Name",
                SortOrder = "asc"
            };

            var paginatedUsers = new PaginatedResponseDTO<UserDTO>
            {
                Items = new List<UserDTO>
                    {
                        new UserDTO
                        {
                            UserId = "1",
                            FirstName = "John",
                            LastName = "Doe",
                            Email = "johndoe@example.com",
                            UserName = "johndoe",
                            Role = new Roles { Name = "Admin" }
                        }
                    },
                TotalCount = 1,
                PageNumber = 1,
                PageSize = 10
            };

            _mockUserService.Setup(us => us.SearchUsersAsync(searchParams, 1, 10))
                .ReturnsAsync(paginatedUsers);

            // Act
            var result = await _userController.SearchUsers(
                searchParams,
                pageNumber: 1,
                pageSize: 10
                ) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(paginatedUsers);
        }

        [Fact]
        public async Task GetCurrentUser_ShouldReturnOk_WithCurrentUserDetails()
        {
            // Arrange
            var userId = "currentUserId";
            var currentUserDto = new GetUserByIdDTO
            {
                UserName = "current_user",
                Email = "current.user@example.com",
                FirstName = "Current",
                LastName = "User",
                AddressName = "Current Address",
                Street = "123 Current St",
                District = "Current District",
                City = "Current City",
                Country = "Current Country",
                PostCode = 12345,
                RoleName = "User"
            };

            // Mock the user context to simulate the current user's claims
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.NameIdentifier))
                     .Returns(new Claim(ClaimTypes.NameIdentifier, userId));

            _mockUserService.Setup(us => us.GetUserByIdAsync(userId))
                            .ReturnsAsync(currentUserDto);

            // Act
            var result = await _userController.GetCurrentUser();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            okResult.Value.Should().BeEquivalentTo(currentUserDto);

            _mockUserService.Verify(us => us.GetUserByIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetUserBanner_ShouldReturnFileResult_WhenImageExists()
        {
            // Arrange
            var userId = "1";
            var imageData = new byte[] { 1, 2, 3, 4, 5 };

            _mockUserService.Setup(s => s.GetUserBannerImageAsync(userId))
                .ReturnsAsync(imageData);

            // Act
            var result = await _userController.GetUserBanner(userId);

            // Assert
            var fileResult = result.Should().BeOfType<FileContentResult>().Which;
            fileResult.FileContents.Should().BeEquivalentTo(imageData);
            fileResult.ContentType.Should().Be("image/jpeg");
        }

        [Fact]
        public async Task GetUserBanner_ShouldReturnNotFound_WhenImageDoesNotExist()
        {
            // Arrange
            var userId = "1";
            _mockUserService.Setup(s => s.GetUserBannerImageAsync(userId))
                .ReturnsAsync((byte[])null);

            // Act
            var result = await _userController.GetUserBanner(userId);

            // Assert
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Which;
            notFoundResult.Value.Should().BeEquivalentTo(new { message = "No banner image found for this user." });
        }

        [Fact]
        public async Task GetUserProfile_ShouldReturnFileResult_WhenImageExists()
        {
            // Arrange
            var userId = "1";
            var imageData = new byte[] { 1, 2, 3, 4, 5 };

            _mockUserService.Setup(s => s.GetUserProfileImageAsync(userId))
                .ReturnsAsync(imageData);

            // Act
            var result = await _userController.GetUserProfilePicture(userId);

            // Assert
            var fileResult = result.Should().BeOfType<FileContentResult>().Which;
            fileResult.FileContents.Should().BeEquivalentTo(imageData);
            fileResult.ContentType.Should().Be("image/jpeg");
        }

        [Fact]
        public async Task GetUserProfile_ShouldReturnNotFound_WhenImageDoesNotExist()
        {
            // Arrange
            var userId = "1";
            _mockUserService.Setup(s => s.GetUserProfileImageAsync(userId))
                .ReturnsAsync((byte[])null);

            // Act
            var result = await _userController.GetUserProfilePicture(userId);

            // Assert
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Which;
            notFoundResult.Value.Should().BeEquivalentTo(new { message = "No profile image found for this user." });
        }

        [Fact]
        public async Task GetUserAllergens_ShouldReturnOk_WithPaginatedAllergens()
        {
            // Arrange
            var paginatedAllergens = new PaginatedResponseDTO<AllergenIdDTO>
            {
                TotalCount = 3,
                Items = new List<AllergenIdDTO>
                {
                    new AllergenIdDTO { Id = 1, Name = "Peanuts" },
                    new AllergenIdDTO { Id = 2, Name = "Dairy" },
                    new AllergenIdDTO { Id = 3, Name = "Shellfish" }
                }
            };

            _mockUserService.Setup(us => us.GetUserAllergensAsync("currentUserId", 1, 10))
                .ReturnsAsync(paginatedAllergens);

            // Act
            var result = await _userController.GetUserAllergens(1, 10);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            okResult.Value.Should().BeEquivalentTo(paginatedAllergens);

            _mockUserService.Verify(us => us.GetUserAllergensAsync("currentUserId", 1, 10), Times.Once);
        }

        [Fact]
        public async Task GetUserAllergens_ShouldReturnBadRequest_WhenPageOrPageSizeIsInvalid()
        {
            // Act
            var result = await _userController.GetUserAllergens(0, 10);

            // Assert
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Which;
            badRequestResult.Value.Should().BeEquivalentTo(new { message = "Page and pageSize must be positive integers." });

            _mockUserService.Verify(us => us.GetUserAllergensAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task GetUserAllergens_ShouldReturnNotFound_WhenNoAllergensExist()
        {
            // Arrange
            _mockUserService.Setup(us => us.GetUserAllergensAsync("currentUserId", 1, 10))
                .ReturnsAsync(new PaginatedResponseDTO<AllergenIdDTO> { Items = new List<AllergenIdDTO>() });

            // Act
            var result = await _userController.GetUserAllergens(1, 10);

            // Assert
            var notFoundResult = result.Should().BeOfType<OkObjectResult>().Which;
            notFoundResult.Value.Should().BeEquivalentTo(new { message = "No allergens found for the given user." });

            _mockUserService.Verify(us => us.GetUserAllergensAsync("currentUserId", 1, 10), Times.Once);
        }

        [Fact]
        public async Task GetUserAllergens_ShouldReturnInternalServerError_OnUnexpectedError()
        {
            // Arrange
            _mockUserService.Setup(us => us.GetUserAllergensAsync("currentUserId", 1, 10))
                .ThrowsAsync(new Exception("Something went wrong"));

            // Act
            var result = await _userController.GetUserAllergens(1, 10);

            // Assert
            var statusCodeResult = result.Should().BeOfType<ObjectResult>().Which;
            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            statusCodeResult.Value.Should().BeEquivalentTo(new { message = "An unexpected error occurred.", details = "Something went wrong" });

            _mockUserService.Verify(us => us.GetUserAllergensAsync("currentUserId", 1, 10), Times.Once);
        }

        [Fact]
        public async Task SetUserAllergens_ShouldReturnOk_WhenRequestIsValid()
        {
            // Arrange
            var userId = "currentUserId";
            var request = new SetUserAllergensRequestDTO
            {
                AllergenIds = new List<int> { 1, 2, 3 }
            };

            _mockUser.Setup(u => u.FindFirst(ClaimTypes.NameIdentifier))
                     .Returns(new Claim(ClaimTypes.NameIdentifier, userId));

            _mockUserService.Setup(us => us.SetUserAllergensAsync(userId, request.AllergenIds))
                            .Returns(Task.CompletedTask);

            // Act
            var result = await _userController.SetUserAllergens(request);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            okResult.Value.Should().BeEquivalentTo(new { message = "User allergens updated successfully." });

            _mockUserService.Verify(us => us.SetUserAllergensAsync(userId, request.AllergenIds), Times.Once);
        }

        [Fact]
        public async Task SetUserAllergens_ShouldReturnBadRequest_WhenRequestIsInvalid()
        {
            // Arrange
            SetUserAllergensRequestDTO request = null;

            // Act
            var result = await _userController.SetUserAllergens(request);

            // Assert
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Which;
            badRequestResult.Value.Should().BeEquivalentTo(new { message = "Invalid request payload." });

            _mockUserService.Verify(us => us.SetUserAllergensAsync(It.IsAny<string>(), It.IsAny<List<int>>()), Times.Never);
        }

        [Fact]
        public async Task SetUserAllergens_ShouldReturnBadRequest_WhenArgumentExceptionIsThrown()
        {
            // Arrange
            var userId = "currentUserId";
            var request = new SetUserAllergensRequestDTO
            {
                AllergenIds = new List<int> { 99, 100 }
            };

            _mockUser.Setup(u => u.FindFirst(ClaimTypes.NameIdentifier))
                     .Returns(new Claim(ClaimTypes.NameIdentifier, userId));

            _mockUserService.Setup(us => us.SetUserAllergensAsync(userId, request.AllergenIds))
                            .ThrowsAsync(new ArgumentException("Invalid allergen IDs."));

            // Act
            var result = await _userController.SetUserAllergens(request);

            // Assert
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Which;
            badRequestResult.Value.Should().BeEquivalentTo(new { message = "Invalid allergen IDs." });

            _mockUserService.Verify(us => us.SetUserAllergensAsync(userId, request.AllergenIds), Times.Once);
        }

        [Fact]
        public async Task SetUserAllergens_ShouldReturnInternalServerError_WhenUnhandledExceptionOccurs()
        {
            // Arrange
            var userId = "currentUserId";
            var request = new SetUserAllergensRequestDTO
            {
                AllergenIds = new List<int> { 1, 2, 3 }
            };

            _mockUser.Setup(u => u.FindFirst(ClaimTypes.NameIdentifier))
                     .Returns(new Claim(ClaimTypes.NameIdentifier, userId));

            _mockUserService.Setup(us => us.SetUserAllergensAsync(userId, request.AllergenIds))
                            .ThrowsAsync(new Exception("Unexpected error."));

            // Act
            var result = await _userController.SetUserAllergens(request);

            // Assert
            var internalServerErrorResult = result.Should().BeOfType<ObjectResult>().Which;
            internalServerErrorResult.StatusCode.Should().Be(500);
            internalServerErrorResult.Value.Should().BeEquivalentTo(new { message = "An unexpected error occurred.", details = "Unexpected error." });

            _mockUserService.Verify(us => us.SetUserAllergensAsync(userId, request.AllergenIds), Times.Once);
        }


    }
}
