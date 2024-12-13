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
using System.Collections.Generic;
using BackEngin.Services.Interfaces;

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
                PhoneNumber = "1234567890",
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
                PhoneNumber = "9876543210",
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
            result.PhoneNumber.Should().Be(updateUserDto.PhoneNumber);
            result.AddressName.Should().Be(updateUserDto.AddressName);
            result.Street.Should().Be(updateUserDto.Street);
            result.District.Should().Be(updateUserDto.District);
            result.City.Should().Be(updateUserDto.City);
            result.Country.Should().Be(updateUserDto.Country);
            result.PostCode.Should().Be(updateUserDto.PostCode);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldAddNewAddress_WhenUserDoesNotHaveAddress()
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
                PhoneNumber = "123456789",
                Address = null // User does not have an address
            };

            var updateUserDto = new UpdateUserDto
            {
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                Email = "updated.email@example.com",
                UserName = "updatedusername",
                PhoneNumber = "987654321",
                AddressName = "New Address",
                Street = "New Street",
                District = "New District",
                City = "New City",
                Country = "New Country",
                PostCode = 54321
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
            result.PhoneNumber.Should().Be(updateUserDto.PhoneNumber);

            // Address assertions
            result.AddressName.Should().Be(updateUserDto.AddressName);
            result.Street.Should().Be(updateUserDto.Street);
            result.District.Should().Be(updateUserDto.District);
            result.City.Should().Be(updateUserDto.City);
            result.Country.Should().Be(updateUserDto.Country);
            result.PostCode.Should().Be(updateUserDto.PostCode);

            // Verify that the address was added
            _mockUserManager.Verify(m => m.UpdateAsync(It.Is<Users>(u =>
                u.Address != null &&
                u.Address.Name == updateUserDto.AddressName &&
                u.Address.Street == updateUserDto.Street &&
                u.Address.District.Name == updateUserDto.District &&
                u.Address.District.City.Name == updateUserDto.City &&
                u.Address.District.City.Country.Name == updateUserDto.Country &&
                u.Address.District.PostCode == updateUserDto.PostCode
            )), Times.Once);
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
                PhoneNumber = "9876543210",
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
            var user = new Users { Id = userId, UserName = "User1" }; // Mock user
            var expectedDto = new PaginatedResponseDTO<string>
            {
                Items = new List<string> { "follower1", "follower2" },
                TotalCount = 2
            };
            var page = 1;
            var pageSize = 10;

            // Setup UserManager to find the user
            _mockUserManager.Setup(u => u.FindByIdAsync(userId))
                .ReturnsAsync(user);

            // Setup UnitOfWork to return the expected DTO
            _mockUnitOfWork.Setup(u => u.Users.GetFollowersAsync(userId, page, pageSize))
                .ReturnsAsync(expectedDto);

            // Act
            var result = await _userService.GetFollowersAsync(userId, page, pageSize);

            // Assert
            result.Should().BeEquivalentTo(expectedDto);
            _mockUnitOfWork.Verify(u => u.Users.GetFollowersAsync(userId, page, pageSize), Times.Once);
            _mockUserManager.Verify(u => u.FindByIdAsync(userId), Times.Once);
        }


        [Fact]
        public async Task FollowUserAsync_ShouldReturnTrue_WhenFollowIsSuccessful()
        {
            // Arrange
            var initiatorUserId = "user1";
            var targetUserId = "user2";

            // Create mock users for initiator and target
            var initiatorUser = new Users { Id = initiatorUserId, UserName = "InitiatorUser" };
            var targetUser = new Users { Id = targetUserId, UserName = "TargetUser" };

            // Mock the FindByIdAsync method to return the appropriate user based on the userId
            _mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string userId) =>
                {
                    if (userId == initiatorUserId)
                        return initiatorUser;
                    else if (userId == targetUserId)
                        return targetUser;
                    else
                        return null;
                });

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

        [Fact]
        public async Task GetBookmarkedRecipesAsync_ShouldReturnMappedBookmarkRecipesDTO_WhenBookmarksExist()
        {
            // Arrange
            var userId = "1";
            var user = new Users { Id = userId, UserName = "john_doe" }; // Mock user

            // Mock the user to be returned by _userManager
            var mockUser = new Users { Id = userId, UserName = "TestUser" };
            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(mockUser);

            // Create bookmarked recipes with the correct UserId
            var bookmarkedRecipes = new List<BookmarkRecipesItemDTO>
            {
                new BookmarkRecipesItemDTO { UserName = "john_doe", Header = "Recipe1", BodyText = "Delicious!" },
                new BookmarkRecipesItemDTO { UserName = "john_doe", Header = "Recipe2", BodyText = "Tasty!" }
            };

            // Create the BookmarkRecipesDTO to be returned
            var bookmarkRecipesDTO = new PaginatedResponseDTO<BookmarkRecipesItemDTO>
            {
                Items = bookmarkedRecipes,
                TotalCount = 2
            };
            var page = 1;
            var pageSize = 10;

            // Mock the repository call to return the BookmarkRecipesDTO
            _mockUnitOfWork.Setup(u => u.Users_Recipes_Interactions.GetBookmarkedRecipesAsync(userId, page, pageSize))
                .ReturnsAsync(bookmarkRecipesDTO);

            // Act
            var result = await _userService.GetBookmarkedRecipesAsync(userId, page, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(bookmarkRecipesDTO);
            _mockUnitOfWork.Verify(u => u.Users_Recipes_Interactions.GetBookmarkedRecipesAsync(userId, page, pageSize), Times.Once);
        }


        [Fact]
        public async Task GetBookmarkedRecipesAsync_ShouldReturnEmptyList_WhenNoBookmarksExist()
        {
            // Arrange
            var userId = "1";

            // Mock the user to be returned by _userManager
            var mockUser = new Users { Id = userId, UserName = "TestUser" };
            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(mockUser);

            // Mock the service to return an empty BookmarkRecipesDTO
            var emptyBookmarkRecipesDTO = new PaginatedResponseDTO<BookmarkRecipesItemDTO>
            {
                Items = new List<BookmarkRecipesItemDTO>(), // Empty list of recipes
                TotalCount = 0 // Total count is 0
            };
            var page = 1;
            var pageSize = 10;

            // Mock the repository method
            _mockUnitOfWork.Setup(u => u.Users_Recipes_Interactions.GetBookmarkedRecipesAsync(userId, page, pageSize))
                .ReturnsAsync(emptyBookmarkRecipesDTO);

            // Act
            var result = await _userService.GetBookmarkedRecipesAsync(userId, page, pageSize);

            // Assert
            result.Should().NotBeNull(); // Ensure the result is not null
            result.Items.Should().BeEmpty(); // Ensure the Recipes list is empty
            result.TotalCount.Should().Be(0); // Ensure the TotalCount is 0
            _mockUnitOfWork.Verify(u => u.Users_Recipes_Interactions.GetBookmarkedRecipesAsync(userId, page, pageSize), Times.Once);
        }

        [Fact]
        public async Task GetBookmarkedBlogsAsync_ShouldReturnBookmarkBlogsDTO_WhenBookmarksExist()
        {
            // Arrange
            var userId = "1";

            // Mock the user to be returned by _userManager
            var mockUser = new Users { Id = userId, UserName = "TestUser" };
            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(mockUser);

            var mockedBlogs = new List<BookmarkBlogsItemDTO>
            {
                new BookmarkBlogsItemDTO { UserName = "User1", Header = "Blog 1", BodyText = "Content of Blog 1" },
                new BookmarkBlogsItemDTO { UserName = "User2", Header = "Blog 2", BodyText = "Content of Blog 2" }
            };

            var bookmarkBlogsDTO = new PaginatedResponseDTO<BookmarkBlogsItemDTO>
            {
                Items = mockedBlogs,
                TotalCount = mockedBlogs.Count
            };
            var page = 1;
            var pageSize = 10;

            _mockUnitOfWork.Setup(u => u.Users_Blogs_Interactions.GetBookmarkedBlogsAsync(userId, page, pageSize))
                .ReturnsAsync(bookmarkBlogsDTO);

            // Act
            var result = await _userService.GetBookmarkedBlogsAsync(userId, page, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(2);
            result.TotalCount.Should().Be(2);
            result.Items.Should().ContainSingle(b => b.Header == "Blog 1" && b.UserName == "User1");
            _mockUnitOfWork.Verify(u => u.Users_Blogs_Interactions.GetBookmarkedBlogsAsync(userId, page, pageSize), Times.Once);
        }

        [Fact]
        public async Task GetBookmarkedBlogsAsync_ShouldReturnEmptyBookmarkBlogsDTO_WhenNoBookmarksExist()
        {
            // Arrange
            var userId = "1";

            // Mock the user to be returned by _userManager
            var mockUser = new Users { Id = userId, UserName = "TestUser" };
            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(mockUser);

            var emptyBookmarkBlogsDTO = new PaginatedResponseDTO<BookmarkBlogsItemDTO>
            {
                Items = new List<BookmarkBlogsItemDTO>(), // Empty list
                TotalCount = 0
            };
            var page = 1;
            var pageSize = 10;

            _mockUnitOfWork.Setup(u => u.Users_Blogs_Interactions.GetBookmarkedBlogsAsync(userId, page, pageSize))
                .ReturnsAsync(emptyBookmarkBlogsDTO);

            // Act
            var result = await _userService.GetBookmarkedBlogsAsync(userId, page, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().BeEmpty();
            result.TotalCount.Should().Be(0);
            _mockUnitOfWork.Verify(u => u.Users_Blogs_Interactions.GetBookmarkedBlogsAsync(userId, page, pageSize), Times.Once);
        }

    }
}