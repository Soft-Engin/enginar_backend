﻿using BackEngin.Services;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Moq;
using Models;
using FluentAssertions;
using BackEngin.Tests.Helpers;
using Models.DTO;
using BackEngin.Tests.Utils;
using MockQueryable.Moq;
using System.Data;
using System.Diagnostics.Metrics;
using System.Net;
using System.Collections.Generic;
using BackEngin.Services.Interfaces;
using MockQueryable;
using Models.InteractionModels;
using System.Linq.Expressions;

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
                    Bio = "MyBio",
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
            result.Bio.Should().Be("MyBio");
            result.UserId.Should().Be("1");
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

            var mockDbSet = users.AsQueryable().BuildMockDbSet();
            _mockUserManager.Setup(m => m.Users).Returns(mockDbSet.Object);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            result.Should().NotBeNull();
            result.UserName.Should().Be("User Name");
            result.Email.Should().Be("E-Mail");
            result.AddressName.Should().Be("Address Name");
            result.Street.Should().Be("Street");
            result.UserId.Should().Be(userId);
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

            var mockDbSet = users.AsQueryable().BuildMockDbSet();
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
                Bio = "Old Bio",
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
                Bio = "New Me",
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
            result.UserId.Should().Be(userId);
            result.FirstName.Should().Be(updateUserDto.FirstName);
            result.LastName.Should().Be(updateUserDto.LastName);
            result.Email.Should().Be(updateUserDto.Email);
            result.Bio.Should().Be(updateUserDto.Bio);
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
            result.UserId.Should().Be(userId);
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

            var expectedDto = new PaginatedResponseDTO<UserCompactDTO>
            {
                Items = new List<UserCompactDTO>
        {
            new UserCompactDTO { UserId = "follower1", UserName = "Follower1" },
            new UserCompactDTO { UserId = "follower2", UserName = "Follower2" }
        },
                TotalCount = 2,
                PageNumber = 1,
                PageSize = 10
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
        public async Task GetFollowingAsync_ShouldReturnFollowingDTO()
        {
            // Arrange
            var userId = "user1";
            var user = new Users { Id = userId, UserName = "User1" }; // Mock user

            var expectedDto = new PaginatedResponseDTO<UserCompactDTO>
            {
                Items = new List<UserCompactDTO>
                {
                    new UserCompactDTO { UserId = "following1", UserName = "Following1" },
                    new UserCompactDTO { UserId = "following2", UserName = "Following2" }
                },
                TotalCount = 2,
                PageNumber = 1,
                PageSize = 10
            };

            var page = 1;
            var pageSize = 10;

            // Setup UserManager to find the user
            _mockUserManager.Setup(u => u.FindByIdAsync(userId))
                .ReturnsAsync(user);

            // Setup UnitOfWork to return the expected DTO
            _mockUnitOfWork.Setup(u => u.Users.GetFollowingAsync(userId, page, pageSize))
                .ReturnsAsync(expectedDto);

            // Act
            var result = await _userService.GetFollowingAsync(userId, page, pageSize);

            // Assert
            result.Should().BeEquivalentTo(expectedDto);
            _mockUnitOfWork.Verify(u => u.Users.GetFollowingAsync(userId, page, pageSize), Times.Once);
            _mockUserManager.Verify(u => u.FindByIdAsync(userId), Times.Once);
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
        public async Task GetBookmarkedBlogsAsync_ShouldReturnPaginatedResponse_WhenBlogsExist()
        {
            // Arrange
            var userId = "user123";
            var page = 1;
            var pageSize = 2;

                    var bookmarkedBlogIds = new List<Blog_Bookmarks>
            {
                new Blog_Bookmarks { BlogId = 1, UserId = userId },
                new Blog_Bookmarks { BlogId = 2, UserId = userId },
                new Blog_Bookmarks { BlogId = 3, UserId = userId }
            };

            var blogs = new List<Blogs>
            {
                new Blogs { Id = 1, Header = "Header1", BodyText = "Body1", UserId = "creator1", User = new Users { Id = "creator1", UserName = "User1" } },
                new Blogs { Id = 2, Header = "Header2", BodyText = "Body2", UserId = "creator2", User = new Users { Id = "creator2", UserName = "User2" } },
                new Blogs { Id = 3, Header = "Header3", BodyText = "Body3", UserId = "creator3", User = new Users { Id = "creator3", UserName = "User3" } }
            };

            _mockUnitOfWork.Setup(uow => uow.Blog_Bookmarks.FindAsync(It.IsAny<Func<Blog_Bookmarks, bool>>()))
                .ReturnsAsync(bookmarkedBlogIds.AsQueryable().BuildMock());

            // Update the mock for Blogs.FindAsync to include the "User" navigation property
            _mockUnitOfWork.Setup(uow => uow.Blogs.FindAsync(It.IsAny<Expression<Func<Blogs, bool>>>(), "User"))
                .ReturnsAsync(blogs.AsQueryable().BuildMock());

            // Act
            var result = await _userService.GetBookmarkedBlogsAsync(userId, page, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(pageSize);
            result.TotalCount.Should().Be(3);

            var firstItem = result.Items.First();
            firstItem.Header.Should().Be("Header1");
            firstItem.BlogId.Should().Be(1);
            firstItem.BodyText.Should().Be("Body1");
            firstItem.UserName.Should().Be("User1");
            firstItem.UserId.Should().Be("creator1"); // Verify UserId of the creator

            result.PageNumber.Should().Be(page);
            result.PageSize.Should().Be(pageSize);
        }



        [Fact]
        public async Task GetBookmarkedBlogsAsync_ShouldReturnEmptyResponse_WhenNoBookmarksExist()
        {
            // Arrange
            var userId = "user123";
            var page = 1;
            var pageSize = 2;

            var bookmarkedBlogIds = new List<Blog_Bookmarks>();
            var blogs = new List<Blogs>();

            _mockUnitOfWork.Setup(uow => uow.Blog_Bookmarks.FindAsync(It.IsAny<Func<Blog_Bookmarks, bool>>()))
                .ReturnsAsync(bookmarkedBlogIds.AsQueryable().BuildMock());

            _mockUnitOfWork.Setup(uow => uow.Blogs.FindAsync(It.IsAny<Func<Blogs, bool>>()))
                .ReturnsAsync(blogs.AsQueryable().BuildMock());

            // Act
            var result = await _userService.GetBookmarkedBlogsAsync(userId, page, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().BeEmpty();
            result.TotalCount.Should().Be(0);
            result.PageNumber.Should().Be(page);
            result.PageSize.Should().Be(pageSize);
        }


        [Fact]
        public async Task GetLikedBlogsAsync_ShouldReturnPaginatedResponse_WhenLikedBlogsExist()
        {
            // Arrange
            var userId = "user123";
            var page = 1;
            var pageSize = 2;

            var likedBlogIds = new List<Blog_Likes>
            {
                new Blog_Likes { BlogId = 1, UserId = userId },
                new Blog_Likes { BlogId = 2, UserId = userId },
                new Blog_Likes { BlogId = 3, UserId = userId }
            };

            var blogs = new List<Blogs>
            {
                new Blogs { Id = 1, Header = "Header1", BodyText = "Body1", UserId = "creator1", User = new Users { Id = "creator1", UserName = "User1" } },
                new Blogs { Id = 2, Header = "Header2", BodyText = "Body2", UserId = "creator2", User = new Users { Id = "creator2", UserName = "User2" } },
                new Blogs { Id = 3, Header = "Header3", BodyText = "Body3", UserId = "creator3", User = new Users { Id = "creator3", UserName = "User3" } }
            };

            // Mocking the Blog_Likes.FindAsync method
            _mockUnitOfWork.Setup(uow => uow.Blog_Likes.FindAsync(It.IsAny<Func<Blog_Likes, bool>>()))
                .ReturnsAsync(likedBlogIds.AsQueryable().BuildMock());

            // Mocking the Blogs.FindAsync method with eager loading of the "User" property
            _mockUnitOfWork.Setup(uow => uow.Blogs.FindAsync(It.IsAny<Expression<Func<Blogs, bool>>>(), "User"))
                .ReturnsAsync(blogs.AsQueryable().BuildMock());

            // Act
            var result = await _userService.GetLikedBlogsAsync(userId, page, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(pageSize);
            result.TotalCount.Should().Be(3);

            var firstItem = result.Items.First();
            firstItem.BlogId.Should().Be(1);
            firstItem.Header.Should().Be("Header1");
            firstItem.BodyText.Should().Be("Body1");
            firstItem.UserName.Should().Be("User1");
            firstItem.UserId.Should().Be("creator1"); // Verify UserId of the creator

            result.PageNumber.Should().Be(page);
            result.PageSize.Should().Be(pageSize);
        }



        [Fact]
        public async Task GetLikedBlogsAsync_ShouldReturnEmptyResponse_WhenNoLikesExist()
        {
            // Arrange
            var userId = "user123";
            var page = 1;
            var pageSize = 2;

            var likedBlogIds = new List<Blog_Likes>();
            var blogs = new List<Blogs>();

            _mockUnitOfWork.Setup(uow => uow.Blog_Likes.FindAsync(It.IsAny<Func<Blog_Likes, bool>>()))
                .ReturnsAsync(likedBlogIds.AsQueryable().BuildMock());

            _mockUnitOfWork.Setup(uow => uow.Blogs.FindAsync(It.IsAny<Func<Blogs, bool>>()))
                .ReturnsAsync(blogs.AsQueryable().BuildMock());

            // Act
            var result = await _userService.GetLikedBlogsAsync(userId, page, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().BeEmpty();
            result.TotalCount.Should().Be(0);
            result.PageNumber.Should().Be(page);
            result.PageSize.Should().Be(pageSize);
        }



        [Fact]
        public async Task GetLikedRecipesAsync_ShouldReturnPaginatedResponse_WhenRecipesExist()
        {
            // Arrange
            var userId = "user123";
            var page = 1;
            var pageSize = 2;

            var likedRecipeIds = new List<Recipe_Likes>
            {
                new Recipe_Likes { RecipeId = 1, UserId = userId },
                new Recipe_Likes { RecipeId = 2, UserId = userId },
                new Recipe_Likes { RecipeId = 3, UserId = userId }
            };

            var recipes = new List<Recipes>
            {
                new Recipes { Id = 1, Header = "Header1", BodyText = "Body1", UserId = "creator1", User = new Users { Id = "creator1", UserName = "User1" } },
                new Recipes { Id = 2, Header = "Header2", BodyText = "Body2", UserId = "creator2", User = new Users { Id = "creator2", UserName = "User2" } },
                new Recipes { Id = 3, Header = "Header3", BodyText = "Body3", UserId = "creator3", User = new Users { Id = "creator3", UserName = "User3" } }
            };

            _mockUnitOfWork.Setup(uow => uow.Recipe_Likes.FindAsync(It.IsAny<Func<Recipe_Likes, bool>>()))
                .ReturnsAsync(likedRecipeIds.AsQueryable().BuildMock());

            // Mock the Recipes.FindAsync method to include the "User" navigation property for lazy loading
            _mockUnitOfWork.Setup(uow => uow.Recipes.FindAsync(It.IsAny<Expression<Func<Recipes, bool>>>(), "User"))
                .ReturnsAsync(recipes.AsQueryable().BuildMock());

            // Act
            var result = await _userService.GetLikedRecipesAsync(userId, page, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(pageSize);
            result.TotalCount.Should().Be(3);

            var firstItem = result.Items.First();
            firstItem.RecipeId.Should().Be(1);
            firstItem.Header.Should().Be("Header1");
            firstItem.BodyText.Should().Be("Body1");
            firstItem.UserName.Should().Be("User1");
            firstItem.UserId.Should().Be("creator1"); // Verify UserId of the creator

            result.PageNumber.Should().Be(page);
            result.PageSize.Should().Be(pageSize);
        }



        [Fact]
        public async Task GetLikedRecipesAsync_ShouldReturnEmptyResponse_WhenNoLikesExist()
        {
            // Arrange
            var userId = "user123";
            var page = 1;
            var pageSize = 2;

            var bookmarkedRecipeIds = new List<Recipe_Likes>();
            var recipes = new List<Recipes>();

            _mockUnitOfWork.Setup(uow => uow.Recipe_Likes.FindAsync(It.IsAny<Func<Recipe_Likes, bool>>()))
                .ReturnsAsync(bookmarkedRecipeIds.AsQueryable().BuildMock());

            _mockUnitOfWork.Setup(uow => uow.Recipes.FindAsync(It.IsAny<Func<Recipes, bool>>()))
                .ReturnsAsync(recipes.AsQueryable().BuildMock());

            // Act
            var result = await _userService.GetLikedRecipesAsync(userId, page, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().BeEmpty();
            result.TotalCount.Should().Be(0);
            result.PageNumber.Should().Be(page);
            result.PageSize.Should().Be(pageSize);
        }





        [Fact]
        public async Task GetBookmarkedRecipesAsync_ShouldReturnPaginatedResponse_WhenRecipesExist()
        {
            // Arrange
            var userId = "user123";
            var page = 1;
            var pageSize = 2;

            var bookmarkedRecipeIds = new List<Recipe_Bookmarks>
            {
                new Recipe_Bookmarks { RecipeId = 1, UserId = userId },
                new Recipe_Bookmarks { RecipeId = 2, UserId = userId },
                new Recipe_Bookmarks { RecipeId = 3, UserId = userId }
            };

            var recipes = new List<Recipes>
            {
                new Recipes { Id = 1, Header = "Header1", BodyText = "Body1", UserId = "creator1", User = new Users { Id = "creator1", UserName = "User1" } },
                new Recipes { Id = 2, Header = "Header2", BodyText = "Body2", UserId = "creator2", User = new Users { Id = "creator2", UserName = "User2" } },
                new Recipes { Id = 3, Header = "Header3", BodyText = "Body3", UserId = "creator3", User = new Users { Id = "creator3", UserName = "User3" } }
            };

            _mockUnitOfWork.Setup(uow => uow.Recipe_Bookmarks.FindAsync(It.IsAny<Func<Recipe_Bookmarks, bool>>()))
                .ReturnsAsync(bookmarkedRecipeIds.AsQueryable().BuildMock());

            // Mock the Recipes.FindAsync method to include the "User" navigation property for lazy loading
            _mockUnitOfWork.Setup(uow => uow.Recipes.FindAsync(It.IsAny<Expression<Func<Recipes, bool>>>(), "User"))
                .ReturnsAsync(recipes.AsQueryable().BuildMock());

            // Act
            var result = await _userService.GetBookmarkedRecipesAsync(userId, page, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(pageSize);
            result.TotalCount.Should().Be(3);

            var firstItem = result.Items.First();
            firstItem.Header.Should().Be("Header1");
            firstItem.BodyText.Should().Be("Body1");
            firstItem.UserName.Should().Be("User1");
            firstItem.RecipeId.Should().Be(1);
            firstItem.UserId.Should().Be("creator1"); // Verify UserId of the creator

            result.PageNumber.Should().Be(page);
            result.PageSize.Should().Be(pageSize);
        }



        [Fact]
        public async Task GetBookmarkedRecipesAsync_ShouldReturnEmptyResponse_WhenNoBookmarksExist()
        {
            // Arrange
            var userId = "user123";
            var page = 1;
            var pageSize = 2;

            var bookmarkedRecipeIds = new List<Recipe_Bookmarks>();
            var recipes = new List<Recipes>();

            _mockUnitOfWork.Setup(uow => uow.Recipe_Bookmarks.FindAsync(It.IsAny<Func<Recipe_Bookmarks, bool>>()))
                .ReturnsAsync(bookmarkedRecipeIds.AsQueryable().BuildMock());

            _mockUnitOfWork.Setup(uow => uow.Recipes.FindAsync(It.IsAny<Func<Recipes, bool>>()))
                .ReturnsAsync(recipes.AsQueryable().BuildMock());

            // Act
            var result = await _userService.GetBookmarkedRecipesAsync(userId, page, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().BeEmpty();
            result.TotalCount.Should().Be(0);
            result.PageNumber.Should().Be(page);
            result.PageSize.Should().Be(pageSize);
        }



        [Fact]
        public async Task SearchUsers_ShouldReturnAll_WhenNoFilter()
        {
            var usersList = TestUtilities.CreateUsers(10);
            var users = usersList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Users.GetQueryable()).Returns(users.Object);

            var searchParams = new UserSearchParams();

            var result = await _userService.SearchUsersAsync(searchParams, 1, 20);

            result.TotalCount.Should().Be(10);
            result.Items.Should().HaveCount(10);
        }

        [Fact]
        public async Task SearchUsers_ShouldFilterByUserNameContains()
        {
            var usersList = TestUtilities.CreateUsers(5);
            usersList[0].UserName = "specialUser";
            var users = usersList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Users.GetQueryable()).Returns(users.Object);

            var searchParams = new UserSearchParams
            {
                UserNameContains = "special"
            };

            var result = await _userService.SearchUsersAsync(searchParams, 1, 10);
            result.TotalCount.Should().Be(1);
            result.Items.First().UserName.Should().Be("specialUser");
        }

        [Fact]
        public async Task SearchUsers_ShouldFilterByFirstOrLastNameContains()
        {
            var usersList = TestUtilities.CreateUsers(5);
            usersList[1].FirstName = "John";
            usersList[1].LastName = "SpecialLast";
            var users = usersList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Users.GetQueryable()).Returns(users.Object);

            var searchParams = new UserSearchParams
            {
                First_LastNameContains = "Special"
            };

            var result = await _userService.SearchUsersAsync(searchParams, 1, 10);
            result.TotalCount.Should().Be(1);
            result.Items.First().LastName.Should().Be("SpecialLast");
        }

        [Fact]
        public async Task SearchUsers_ShouldFilterByEmailContains()
        {
            var usersList = TestUtilities.CreateUsers(5);
            usersList[2].Email = "unique@example.com";
            var users = usersList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Users.GetQueryable()).Returns(users.Object);

            var searchParams = new UserSearchParams
            {
                EmailContains = "unique"
            };

            var result = await _userService.SearchUsersAsync(searchParams, 1, 10);
            result.TotalCount.Should().Be(1);
            result.Items.First().Email.Should().Be("unique@example.com");
        }

        [Fact]
        public async Task SearchUsers_ShouldApplySortingByUserNameAsc()
        {
            var usersList = TestUtilities.CreateUsers(3);
            usersList[0].UserName = "ZUser";
            usersList[1].UserName = "AUser";
            usersList[2].UserName = "MUser";

            var users = usersList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Users.GetQueryable()).Returns(users.Object);

            var searchParams = new UserSearchParams
            {
                SortBy = "UserName",
                SortOrder = "asc"
            };

            var result = await _userService.SearchUsersAsync(searchParams, 1, 10);
            result.Items.First().UserName.Should().Be("AUser");
        }

        [Fact]
        public async Task SearchUsers_ShouldApplySortingByNameDesc()
        {
            var usersList = TestUtilities.CreateUsers(3);
            // Sort by Name means FirstName+LastName
            usersList[0].FirstName = "Zed";
            usersList[0].LastName = "Zebra";

            usersList[1].FirstName = "Adam";
            usersList[1].LastName = "Apple";

            usersList[2].FirstName = "Mary";
            usersList[2].LastName = "Maple";

            var users = usersList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Users.GetQueryable()).Returns(users.Object);

            var searchParams = new UserSearchParams
            {
                SortBy = "Name",
                SortOrder = "desc"
            };

            var result = await _userService.SearchUsersAsync(searchParams, 1, 10);
            // ZedZebra should come first in desc
            result.Items.First().FirstName.Should().Be("Zed");
        }

        [Fact]
        public async Task SearchUsers_ShouldPaginate()
        {
            var usersList = TestUtilities.CreateUsers(25);
            var users = usersList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Users.GetQueryable()).Returns(users.Object);

            var searchParams = new UserSearchParams();
            int pageNumber = 3;
            int pageSize = 5;

            var result = await _userService.SearchUsersAsync(searchParams, pageNumber, pageSize);

            result.PageNumber.Should().Be(pageNumber);
            result.PageSize.Should().Be(pageSize);
            result.TotalCount.Should().Be(25);
            result.Items.Should().HaveCount(5);
        }

        [Fact]
        public async Task GetUserBannerImageAsync_ShouldReturnImage_WhenExists()
        {
            // Arrange
            var userId = "1";
            var expectedImage = new byte[] { 1, 2, 3, 4, 5 };
            var user = new Users { Id = userId, BannerImage = expectedImage };

            _mockUserManager.Setup(m => m.FindByIdAsync(userId))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserBannerImageAsync(userId);

            // Assert
            result.Should().BeEquivalentTo(expectedImage);
            _mockUserManager.Verify(m => m.FindByIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetUserBannerImageAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "1";
            _mockUserManager.Setup(m => m.FindByIdAsync(userId))
                .ReturnsAsync((Users)null);

            // Act
            var result = await _userService.GetUserBannerImageAsync(userId);

            // Assert
            result.Should().BeNull();
            _mockUserManager.Verify(m => m.FindByIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetUserProfileImageAsync_ShouldReturnImage_WhenExists()
        {
            // Arrange
            var userId = "1";
            var expectedImage = new byte[] { 1, 2, 3, 4, 5 };
            var user = new Users { Id = userId, ProfileImage = expectedImage };

            _mockUserManager.Setup(m => m.FindByIdAsync(userId))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserProfileImageAsync(userId);

            // Assert
            result.Should().BeEquivalentTo(expectedImage);
            _mockUserManager.Verify(m => m.FindByIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetUserProfileImageAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "1";
            _mockUserManager.Setup(m => m.FindByIdAsync(userId))
                .ReturnsAsync((Users)null);

            // Act
            var result = await _userService.GetUserProfileImageAsync(userId);

            // Assert
            result.Should().BeNull();
            _mockUserManager.Verify(m => m.FindByIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetUserAllergensAsync_ShouldReturnPaginatedAllergens_WhenUserHasAllergens()
        {
            // Arrange
            var userId = "1";
            var pageNumber = 1;
            var pageSize = 2;

            var userAllergens = new List<User_Allergens>
            {
                new User_Allergens { UserId = userId, PreferenceId = 1 },
                new User_Allergens { UserId = userId, PreferenceId = 2 },
                new User_Allergens { UserId = userId, PreferenceId = 3 }
            };

            var preferences = new List<Preferences>
            {
                new Preferences { Id = 1, Name = "Peanuts", Description = "Nut allergy" },
                new Preferences { Id = 2, Name = "Dairy", Description = "Lactose intolerance" },
                new Preferences { Id = 3, Name = "Shellfish", Description = "Seafood allergy" }
            };

            var mockUserAllergensDbSet = userAllergens.AsQueryable().BuildMockDbSet();
            var mockPreferencesDbSet = preferences.AsQueryable().BuildMockDbSet();

            _mockUnitOfWork.Setup(uow => uow.User_Allergens.FindAsync(It.IsAny<Func<User_Allergens, bool>>()))
                .ReturnsAsync((Func<User_Allergens, bool> predicate) => userAllergens.Where(predicate));

            _mockUnitOfWork.Setup(uow => uow.Preferences.FindAsync(It.IsAny<Func<Preferences, bool>>()))
                .ReturnsAsync((Func<Preferences, bool> predicate) => preferences.Where(predicate));

            // Act
            var result = await _userService.GetUserAllergensAsync(userId, pageNumber, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(pageSize);
            result.TotalCount.Should().Be(3);
            result.Items.Should().BeEquivalentTo(preferences.Take(pageSize).Select(p => new AllergenIdDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            }));

            _mockUnitOfWork.Verify(uow => uow.User_Allergens.FindAsync(It.IsAny<Func<User_Allergens, bool>>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Preferences.FindAsync(It.IsAny<Func<Preferences, bool>>()), Times.Once);
        }

        [Fact]
        public async Task GetUserAllergensAsync_ShouldReturnEmptyResult_WhenUserHasNoAllergens()
        {
            // Arrange
            var userId = "1";
            var pageNumber = 1;
            var pageSize = 10;

            _mockUnitOfWork.Setup(uow => uow.User_Allergens.FindAsync(It.IsAny<Func<User_Allergens, bool>>()))
                .ReturnsAsync(Enumerable.Empty<User_Allergens>());

            // Act
            var result = await _userService.GetUserAllergensAsync(userId, pageNumber, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().BeEmpty();
            result.TotalCount.Should().Be(0);
            result.PageNumber.Should().Be(pageNumber);
            result.PageSize.Should().Be(pageSize);

            _mockUnitOfWork.Verify(uow => uow.User_Allergens.FindAsync(It.IsAny<Func<User_Allergens, bool>>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Preferences.FindAsync(It.IsAny<Func<Preferences, bool>>()), Times.Never);
        }

        [Fact]
        public async Task SetUserAllergensAsync_ShouldUpdateAllergens_WhenValidDataIsProvided()
        {
            // Arrange
            var userId = "user123";
            var allergenIds = new List<int> { 1, 2, 3 };
            var user = new Users
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe",
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
                            Country = new Countries { Name = "Countryland" }
                        },
                        PostCode = 12345
                    }
                },
                Role = new Roles { Name = "Admin" }
            };

            var preferences = new List<Preferences>
            {
                new Preferences { Id = 1 },
                new Preferences { Id = 2 },
                new Preferences { Id = 3 }
            };

            var existingUserAllergens = new List<User_Allergens>
            {
                new User_Allergens { UserId = userId, PreferenceId = 1 },
                new User_Allergens { UserId = userId, PreferenceId = 2 }
            };

            // Mocking _userManager.Users
            var users = new List<Users> { user }.AsQueryable();
            var mockDbSet = users.BuildMockDbSet();
            _mockUserManager.Setup(m => m.Users).Returns(mockDbSet.Object);

            // Mocking _unitOfWork methods
            _mockUnitOfWork.Setup(u => u.Preferences.GetAllAsync())
                .ReturnsAsync(preferences);
            _mockUnitOfWork.Setup(u => u.User_Allergens.FindAsync(It.IsAny<Expression<Func<User_Allergens, bool>>>(), ""))
                .ReturnsAsync(existingUserAllergens);

            // Act
            await _userService.SetUserAllergensAsync(userId, allergenIds);

            // Assert
            _mockUnitOfWork.Verify(u => u.User_Allergens.DeleteRange(existingUserAllergens), Times.Once);
            _mockUnitOfWork.Verify(u => u.User_Allergens.AddRangeAsync(It.Is<List<User_Allergens>>(list =>
                list.Count == 3 &&
                list.Any(a => a.PreferenceId == 1) &&
                list.Any(a => a.PreferenceId == 2) &&
                list.Any(a => a.PreferenceId == 3)
            )), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task SetUserAllergensAsync_ShouldThrowArgumentException_WhenUserNotFound()
        {
            // Arrange
            var userId = "nonexistent_user";
            var allergenIds = new List<int> { 1, 2, 3 };

            // Mocking _userManager.Users to simulate an empty user set
            var users = new List<Users>().AsQueryable();
            var mockDbSet = users.BuildMockDbSet();
            _mockUserManager.Setup(m => m.Users).Returns(mockDbSet.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _userService.SetUserAllergensAsync(userId, allergenIds)
            );

            exception.Message.Should().Be("User not found. (Parameter 'userId')");
            exception.ParamName.Should().Be("userId");

            // Verify that no database calls were made to update or delete allergens
            _mockUnitOfWork.Verify(u => u.User_Allergens.DeleteRange(It.IsAny<IEnumerable<User_Allergens>>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.User_Allergens.AddRangeAsync(It.IsAny<IEnumerable<User_Allergens>>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Never);
        }


        [Fact]
        public async Task SetUserAllergensAsync_ShouldThrowArgumentException_WhenInvalidAllergenIdsAreProvided()
        {
            // Arrange
            var userId = "user123";
            var allergenIds = new List<int> { 1, 2, 999 }; // 999 is invalid
            var user = new Users
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe"
            };

            var preferences = new List<Preferences>
            {
                new Preferences { Id = 1 },
                new Preferences { Id = 2 },
                new Preferences { Id = 3 }
            };

            // Mocking _userManager.Users to simulate user retrieval
            var users = new List<Users> { user }.AsQueryable();
            var mockDbSet = users.BuildMockDbSet();
            _mockUserManager.Setup(m => m.Users).Returns(mockDbSet.Object);

            // Mocking _unitOfWork.Preferences.GetAllAsync to simulate valid preferences
            _mockUnitOfWork.Setup(u => u.Preferences.GetAllAsync())
                .ReturnsAsync(preferences);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _userService.SetUserAllergensAsync(userId, allergenIds)
            );

            exception.Message.Should().Contain("Invalid allergen IDs: 999");
            exception.ParamName.Should().Be("allergenIds");

            // Verify that no database calls were made to update or delete allergens
            _mockUnitOfWork.Verify(u => u.User_Allergens.DeleteRange(It.IsAny<IEnumerable<User_Allergens>>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.User_Allergens.AddRangeAsync(It.IsAny<IEnumerable<User_Allergens>>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Never);
        }


        [Fact]
        public async Task SetUserAllergensAsync_ShouldDeleteAndAddAllergens()
        {
            // Arrange
            var userId = "user123";
            var allergenIds = new List<int> { 1, 2, 4 };
            var user = new Users
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe"
            };

            var preferences = new List<Preferences>
            {
                new Preferences { Id = 1 },
                new Preferences { Id = 2 },
                new Preferences { Id = 3 },
                new Preferences { Id = 4 }
            };

            var existingUserAllergens = new List<User_Allergens>
            {
                new User_Allergens { UserId = userId, PreferenceId = 1 },
                new User_Allergens { UserId = userId, PreferenceId = 2 }
            };

            // Mocking _userManager.Users
            var users = new List<Users> { user }.AsQueryable();
            var mockDbSet = users.BuildMockDbSet();
            _mockUserManager.Setup(m => m.Users).Returns(mockDbSet.Object);

            // Mocking _unitOfWork methods
            _mockUnitOfWork.Setup(u => u.Preferences.GetAllAsync())
                .ReturnsAsync(preferences);
            _mockUnitOfWork.Setup(u => u.User_Allergens.FindAsync(It.IsAny<Expression<Func<User_Allergens, bool>>>(), ""))
                .ReturnsAsync(existingUserAllergens);

            // Act
            await _userService.SetUserAllergensAsync(userId, allergenIds);

            // Assert
            // Verify that old allergens were deleted
            _mockUnitOfWork.Verify(u => u.User_Allergens.DeleteRange(existingUserAllergens), Times.Once);

            // Verify that new allergens were added
            _mockUnitOfWork.Verify(u => u.User_Allergens.AddRangeAsync(It.Is<List<User_Allergens>>(list =>
                list.Count == 3 &&
                list.Any(a => a.PreferenceId == 1) &&
                list.Any(a => a.PreferenceId == 2) &&
                list.Any(a => a.PreferenceId == 4)
            )), Times.Once);

            // Verify that changes were saved
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }



    }
}