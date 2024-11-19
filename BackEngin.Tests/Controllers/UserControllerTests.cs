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
        public async Task GetUsers_ShouldReturnOk_WithListOfUsers()
        {
            // Arrange
            var users = new List<Users> { new Users { Id = "1", FirstName = "John" } };
            _mockUserService.Setup(us => us.GetAllUsersAsync()).ReturnsAsync(users);

            // Act
            var result = await _userController.GetUsers();

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(users);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnOk_WithUser()
        {
            // Arrange
            var user = new Users { Id = "1", FirstName = "John" };
            _mockUserService.Setup(us => us.GetUserByIdAsync("1")).ReturnsAsync(user);

            // Act
            var result = await _userController.GetUserById("1");

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(user);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            _mockUserService.Setup(us => us.GetUserByIdAsync("1")).ReturnsAsync((Users)null);

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
                RoleId = 3,
                AddressId = 20
            };

            var updatedUser = new Users
            {
                Id = userId,
                FirstName = updateUserDto.FirstName,
                LastName = updateUserDto.LastName,
                Email = updateUserDto.Email,
                UserName = updateUserDto.UserName,
                RoleId = updateUserDto.RoleId,
                AddressId = updateUserDto.AddressId
            };

            _mockUserService.Setup(us => us.UpdateUserAsync(userId, updateUserDto)).ReturnsAsync(updatedUser);

            // Act
            var result = await _userController.UpdateUser(userId, updateUserDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(updatedUser);
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
                RoleId = 3,
                AddressId = 20
            };

            _mockUserService.Setup(us => us.UpdateUserAsync(userId, updateUserDto)).ReturnsAsync((Users)null);

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
    }
}
