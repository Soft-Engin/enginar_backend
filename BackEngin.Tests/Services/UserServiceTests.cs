using BackEngin.Services;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Moq;
using Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using BackEngin.Tests.Helpers;
using Models.DTO;
using System.Data;
using System.Diagnostics.Metrics;
using System.Net;

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
                new Users
                {
                    Id = "1",
                    FirstName = "John",
                    LastName = "Doe",
                    UserName = "johndoe",
                    Email = "john.doe@example.com",
                    Address = new Addresses
                    {
                        Name = "Home",
                        Street = "123 Main St",
                        District = new Districts
                        {
                            Name = "Downtown",
                            City = new Cities
                            {
                                Name = "Metropolis",
                                Country = new Countries
                                {
                                    Name = "Countryland"
                                }
                            }
                        }
                    },
                    Role = new Roles { Name = "Admin" }
                },
                new Users
                {
                    Id = "2",
                    FirstName = "Jane",
                    LastName = "Smith"
                }
            }.AsQueryable();

            var mockDbSet = users.BuildMockDbSet();
            _mockUserManager.Setup(m => m.Users).Returns(mockDbSet.Object);

            //var service = new UserService(_mockUserManager.Object, _mockUnitOfWork.Object);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            result.Should().NotBeNull();
            result.UserName.Should().Be("johndoe");
            result.Email.Should().Be("john.doe@example.com");
            result.FirstName.Should().Be("John");
            result.LastName.Should().Be("Doe");
            result.AddressName.Should().Be("Home");
            result.Street.Should().Be("123 Main St");
            result.District.Should().Be("Downtown");
            result.City.Should().Be("Metropolis");
            result.Country.Should().Be("Countryland");
            result.PostCode.Should().Be(0); // Default value, since no PostCode is provided in the setup
            result.RoleName.Should().Be("Admin");
        }



        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnDefaultValues_WhenOptionalFieldsAreNull()
        {
            // Arrange
            var userId = "2";
            var user = new Users
            {
                Id = userId,
                FirstName = "Jane",
                LastName = "Smith",
                Role = new Roles { Name = "Admin" }
            };

            var users = new List<Users> { user }.AsQueryable();

            var mockDbSet = MockHelper.BuildMockDbSet(users);
            _mockUserManager.Setup(m => m.Users).Returns(mockDbSet.Object);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            result.Should().NotBeNull();
            result.UserName.Should().Be("User Name");
            result.Email.Should().Be("E-Mail");
            result.AddressName.Should().Be("Address Name");
            result.Street.Should().Be("Street");
            result.District.Should().Be("District");
            result.City.Should().Be("City");
            result.Country.Should().Be("Country");
            result.PostCode.Should().Be(0); // Default value
            result.RoleName.Should().Be(user.Role.Name);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var users = new List<Users>().AsQueryable(); // Empty list to simulate no users

            var mockDbSet = MockHelper.BuildMockDbSet(users);
            _mockUserManager.Setup(m => m.Users).Returns(mockDbSet.Object);

            // Act
            var result = await _userService.GetUserByIdAsync("NonExistentId");

            // Assert
            result.Should().BeNull();
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
                Address = new Addresses
                {
                    Name = "Old Address",
                    Street = "Old Street",
                    District = new Districts
                    {
                        Name = "Old District",
                        City = new Cities
                        {
                            Name = "Old City",
                            Country = new Countries { Name = "Old Country" }
                        },
                        PostCode = 54321
                    }
                }
            };

            var updateUserDto = new UpdateUserDto
            {
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                Email = "updated.email@example.com",
                UserName = "updatedusername",
                AddressName = "Updated Address",
                Street = "Updated Street",
                District = "Updated District",
                City = "Updated City",
                Country = "Updated Country",
                PostCode = 12345
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
            result.Address.Name.Should().Be(updateUserDto.AddressName);
            result.Address.Street.Should().Be(updateUserDto.Street);
            result.Address.District.Name.Should().Be(updateUserDto.District);
            result.Address.District.City.Name.Should().Be(updateUserDto.City);
            result.Address.District.City.Country.Name.Should().Be(updateUserDto.Country);
            result.Address.District.PostCode.Should().Be(updateUserDto.PostCode);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "1";
            var updateUserDto = new UpdateUserDto
            {
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                Email = "updated.email@example.com",
                UserName = "updatedusername",
                AddressName = "Updated Address",
                Street = "Updated Street",
                District = "Updated District",
                City = "Updated City",
                Country = "Updated Country",
                PostCode = 12345
            };

            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync((Users)null);

            // Act
            var result = await _userService.UpdateUserAsync(userId, updateUserDto);

            // Assert
            result.Should().BeNull();
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

        [Fact]
        public async Task GetFollowersAsync_ShouldReturnFollowersDTO()
        {
            // Arrange
            var userId = "user1";
            var expectedDto = new FollowersDTO
            {
                Usernames = new List<string> { "follower1", "follower2" },
                TotalCount = 2
            };

            _mockUnitOfWork.Setup(u => u.Users.GetFollowersAsync(userId))
                .ReturnsAsync(expectedDto);

            // Act
            var result = await _userService.GetFollowersAsync(userId);

            // Assert
            result.Should().BeEquivalentTo(expectedDto);
            _mockUnitOfWork.Verify(u => u.Users.GetFollowersAsync(userId), Times.Once);
        }

        [Fact]
        public async Task FollowUserAsync_ShouldReturnTrue_WhenFollowIsSuccessful()
        {
            // Arrange
            var initiatorUserId = "user1";
            var targetUserId = "user2";

            _mockUnitOfWork.Setup(u => u.Users.FollowUserAsync(initiatorUserId, targetUserId))
                .ReturnsAsync(true);

            // Act
            var result = await _userService.FollowUserAsync(initiatorUserId, targetUserId);

            // Assert
            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.Users.FollowUserAsync(initiatorUserId, targetUserId), Times.Once);
        }

        [Fact]
        public async Task FollowUserAsync_ShouldThrowInvalidOperationException_WhenUserTriesToFollowSelf()
        {
            // Arrange
            var userId = "user1";

            // Act
            var act = async () => await _userService.FollowUserAsync(userId, userId);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Users cannot follow themselves.");
        }

        [Fact]
        public async Task UnfollowUserAsync_ShouldReturnFalse_WhenUnfollowFails()
        {
            // Arrange
            var initiatorUserId = "user1";
            var targetUserId = "user2";

            _mockUnitOfWork.Setup(u => u.Users.UnfollowUserAsync(initiatorUserId, targetUserId))
                .ReturnsAsync(false);

            // Act
            var result = await _userService.UnfollowUserAsync(initiatorUserId, targetUserId);

            // Assert
            result.Should().BeFalse();
            _mockUnitOfWork.Verify(u => u.Users.UnfollowUserAsync(initiatorUserId, targetUserId), Times.Once);
        }

    }
}
