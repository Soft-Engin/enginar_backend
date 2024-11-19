using BackEngin.Services;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Moq;
using Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using BackEngin.Tests.Helpers;
using Models.DTO;

namespace BackEngin.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<UserManager<Users>> _mockUserManager;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserManager = MockHelper.MockUserManager<Users>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _userService = new UserService(_mockUserManager.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnUsers()
        {
            // Arrange
            var users = new List<Users>
            {
                new Users { Id = "1", FirstName = "John", LastName = "Doe" },
                new Users { Id = "2", FirstName = "Jane", LastName = "Smith" }
            }.AsQueryable();

            var mockDbSet = users.BuildMockDbSet();
            _mockUserManager.Setup(m => m.Users).Returns(mockDbSet.Object);

            var service = new UserService(_mockUserManager.Object, _mockUnitOfWork.Object);

            // Act
            var result = await service.GetAllUsersAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(u => u.FirstName == "John" && u.LastName == "Doe");
            result.Should().Contain(u => u.FirstName == "Jane" && u.LastName == "Smith");
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = "1";
            var users = new List<Users>
            {
                new Users { Id = "1", FirstName = "John", LastName = "Doe" },
                new Users { Id = "2", FirstName = "Jane", LastName = "Smith" }
            }.AsQueryable();

            var mockDbSet = users.BuildMockDbSet();
            _mockUserManager.Setup(m => m.Users).Returns(mockDbSet.Object);

            var service = new UserService(_mockUserManager.Object, _mockUnitOfWork.Object);

            // Act
            var result = await service.GetUserByIdAsync(userId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(userId);
            result.FirstName.Should().Be("John");
            result.LastName.Should().Be("Doe");
        }


        [Fact]
        public async Task UpdateUserAsync_ShouldReturnUpdatedUser_WhenUpdateIsSuccessful()
        {
            // Arrange
            var userId = "1";
            var existingUser = new Users
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                UserName = "johndoe",
                RoleId = 2,
                AddressId = 10
            };

            var updateUserDto = new UpdateUserDto
            {
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                Email = "updated.email@example.com",
                UserName = "updatedusername",
                RoleId = 3,
                AddressId = 20
            };

            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(existingUser);

            // Mock validation to succeed
            var userValidator = new Mock<IUserValidator<Users>>();
            userValidator
                .Setup(v => v.ValidateAsync(_mockUserManager.Object, It.IsAny<Users>()))
                .ReturnsAsync(IdentityResult.Success);

            _mockUserManager.Object.UserValidators.Add(userValidator.Object);

            // Mock UpdateAsync to succeed
            _mockUserManager.Setup(m => m.UpdateAsync(It.IsAny<Users>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userService.UpdateUserAsync(userId, updateUserDto);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be(updateUserDto.FirstName);
            result.LastName.Should().Be(updateUserDto.LastName);
            result.Email.Should().Be(updateUserDto.Email);
            result.UserName.Should().Be(updateUserDto.UserName);
            result.RoleId.Should().Be(updateUserDto.RoleId);
            result.AddressId.Should().Be(updateUserDto.AddressId);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldReturnTrue_WhenDeletionIsSuccessful()
        {
            var user = new Users { Id = "1" };
            _mockUserManager.Setup(m => m.FindByIdAsync("1")).ReturnsAsync(user);
            _mockUserManager.Setup(m => m.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

            var result = await _userService.DeleteUserAsync("1");

            result.Should().BeTrue();
        }
    }
}
