using Moq;
using FluentAssertions;
using BackEngin.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.DTO;
using BackEngin.Tests.Helpers;
using DataAccess.Repositories;
using System.Data;
using System.Linq.Expressions;

namespace BackEngin.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<UserManager<Users>> _mockUserManager;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly AuthService _authService;

        /*
         * Just keeping mocking role repository here 
         * in case it is needed in the future.
         */

        //private readonly Mock<IRolesRepository> _mockRolesRepository;

        public AuthServiceTests()
        {
            _mockUserManager = MockHelper.MockUserManager<Users>();
            _mockConfiguration = MockHelper.MockConfiguration();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _authService = new AuthService(_mockUserManager.Object, _mockConfiguration.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task RegisterUser_ShouldReturnSuccess_WhenUserCreated()
        {
            var model = new RegisterRequestDTO { UserName = "testuser", Email = "test@example.com", Password = "Password123" };
            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<Users>(), model.Password))
                .ReturnsAsync(IdentityResult.Success);
            var role = new Roles { Id = 1, Name = "User", Description = "Default user role" };
            _mockUnitOfWork
                .Setup(uow => uow.Roles.FindAsync(It.IsAny<Func<Roles, bool>>()))
                .ReturnsAsync(new List<Roles> { role });

            var result = await _authService.RegisterUser(model);

            result.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task RegisterUser_ShouldReturnFailure_WhenUserCreationFails()
        {
            var model = new RegisterRequestDTO { UserName = "testuser", Email = "test@example.com", Password = "Password123" };
            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<Users>(), model.Password))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error creating user" }));
            var role = new Roles { Id = 1, Name = "User", Description = "Default user role" };
            _mockUnitOfWork
                .Setup(uow => uow.Roles.FindAsync(It.IsAny<Func<Roles, bool>>()))
                .ReturnsAsync(new List<Roles> { role });

            var result = await _authService.RegisterUser(model);

            result.Succeeded.Should().BeFalse();
        }

        [Fact]
        public async Task LoginUser_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Ensure a valid secret key is set
            Environment.SetEnvironmentVariable("JWTSecretKey", "test_secret_key_12312312312312312312312");

            var model = new LoginRequestDTO { Identifier = "testuser", Password = "Password123" };
            var user = new Users { UserName = model.Identifier, Id = "1" };
            var role = new Roles { Id = 2, Name = "User", Description = "Regular user" };

            _mockUserManager.Setup(um => um.FindByEmailAsync(model.Identifier)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.FindByNameAsync(model.Identifier)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.CheckPasswordAsync(user, model.Password)).ReturnsAsync(true);

            _mockUnitOfWork.Setup(uow => uow.Roles.GetByIdAsync(user.RoleId)).ReturnsAsync(role);

            var token = await _authService.LoginUser(model);

            token.Should().NotBeNull();
        }

        [Fact]
        public async Task LoginUser_ShouldReturnNull_WhenCredentialsAreInvalid()
        {
            var model = new LoginRequestDTO { Identifier = "testuser", Password = "wrongpassword" };
            var user = new Users { UserName = model.Identifier, Id = "1" };

            _mockUserManager.Setup(um => um.FindByEmailAsync(model.Identifier)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.CheckPasswordAsync(user, model.Password)).ReturnsAsync(false);

            var token = await _authService.LoginUser(model);

            token.Should().BeNull();
        }

        [Fact]
        public async Task GenerateJwtToken_ShouldReturnToken_WhenUserIsValid()
        {
            // Ensure a valid secret key is set
            Environment.SetEnvironmentVariable("JWTSecretKey", "test_secret_key_123123123123123123123123");

            var user = new Users { UserName = "testuser", Id = "1", RoleId = 2 };
            var role = new Roles { Id = 2, Name = "User", Description = "Regular user" };

            _mockUnitOfWork.Setup(uow => uow.Roles.GetByIdAsync(user.RoleId)).ReturnsAsync(role);

            var token = await _authService.GenerateJwtToken(user);

            token.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task SendPasswordResetTokenAsync_ShouldReturnToken_WhenEmailExists()
        {
            var email = "test@example.com";
            var user = new Users { UserName = "testuser", Email = email, Id = "1" };

            _mockUserManager.Setup(um => um.FindByEmailAsync(email)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.GeneratePasswordResetTokenAsync(user)).ReturnsAsync("reset-token");

            var token = await _authService.SendPasswordResetTokenAsync(email);

            token.Should().Be("reset-token");
        }

        [Fact]
        public async Task SendPasswordResetTokenAsync_ShouldReturnNull_WhenEmailDoesNotExist()
        {
            var email = "nonexistent@example.com";

            _mockUserManager.Setup(um => um.FindByEmailAsync(email)).ReturnsAsync((Users)null);

            var token = await _authService.SendPasswordResetTokenAsync(email);

            token.Should().BeNull();
        }

        [Fact]
        public async Task ResetPasswordAsync_ShouldReturnSuccess_WhenTokenAndPasswordsAreValid()
        {
            var model = new ResetPasswordDTO { Email = "test@example.com", Token = "valid-token", NewPassword = "NewPassword123", ConfirmPassword = "NewPassword123" };
            var user = new Users { UserName = "testuser", Email = model.Email, Id = "1" };

            _mockUserManager.Setup(um => um.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.ResetPasswordAsync(user, model.Token, model.NewPassword))
                .ReturnsAsync(IdentityResult.Success);

            var result = await _authService.ResetPasswordAsync(model);

            result.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task ResetPasswordAsync_ShouldReturnFailure_WhenTokenIsInvalid()
        {
            var model = new ResetPasswordDTO { Email = "test@example.com", Token = "invalid-token", NewPassword = "NewPassword123", ConfirmPassword = "NewPassword123" };
            var user = new Users { UserName = "testuser", Email = model.Email, Id = "1" };

            _mockUserManager.Setup(um => um.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.ResetPasswordAsync(user, model.Token, model.NewPassword))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Invalid token" }));

            var result = await _authService.ResetPasswordAsync(model);

            result.Succeeded.Should().BeFalse();
        }

        [Fact]
        public async Task ResetPasswordAsync_ShouldReturnFailure_WhenPasswordsDoNotMatch()
        {
            var model = new ResetPasswordDTO { Email = "test@example.com", Token = "valid-token", NewPassword = "NewPassword123", ConfirmPassword = "DifferentPassword" };
            var user = new Users { UserName = "testuser", Email = model.Email, Id = "1" };

            _mockUserManager.Setup(um => um.FindByEmailAsync(model.Email)).ReturnsAsync(user);

            var result = await _authService.ResetPasswordAsync(model);

            result.Succeeded.Should().BeFalse();
        }

        [Fact]
        public async Task RegisterUser_ShouldFail_WhenUsernameAlreadyExists()
        {
            // Arrange
            var model = new RegisterRequestDTO
            {
                UserName = "existinguser",
                Email = "newemail@example.com",
                Password = "Password123"
            };

            var existingUser = new Users { UserName = "existinguser", Email = "existing@example.com" };

            // Setup UserManager to find an existing user by username
            _mockUserManager.Setup(um => um.FindByNameAsync(model.UserName))
                .ReturnsAsync(existingUser);

            // Alternatively, depending on your AuthService implementation,
            // UserManager.CreateAsync might automatically fail if username exists.
            // Here, we simulate CreateAsync failing due to duplicate username.
            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<Users>(), model.Password))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Username already exists." }));

            var role = new Roles { Id = 1, Name = "User", Description = "Default user role" };
            _mockUnitOfWork
                .Setup(uow => uow.Roles.FindAsync(It.IsAny<Func<Roles, bool>>()))
                .ReturnsAsync(new List<Roles> { role });

            // Act
            var result = await _authService.RegisterUser(model);

            // Assert
            result.Succeeded.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Description == "Username already exists.");
        }

        [Fact]
        public async Task RegisterUser_ShouldFail_WhenEmailAlreadyExists()
        {
            // Arrange
            var model = new RegisterRequestDTO
            {
                UserName = "newuser",
                Email = "existing@example.com",
                Password = "Password123"
            };

            var existingUser = new Users { UserName = "existinguser", Email = "existing@example.com" };

            // Setup UserManager to find an existing user by email
            _mockUserManager.Setup(um => um.FindByEmailAsync(model.Email))
                .ReturnsAsync(existingUser);

            // Alternatively, depending on your AuthService implementation,
            // UserManager.CreateAsync might automatically fail if email exists.
            // Here, we simulate CreateAsync failing due to duplicate email.
            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<Users>(), model.Password))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Email already exists." }));

            var role = new Roles { Id = 1, Name = "User", Description = "Default user role" };
            _mockUnitOfWork
                .Setup(uow => uow.Roles.FindAsync(It.IsAny<Func<Roles, bool>>()))
                .ReturnsAsync(new List<Roles> { role });

            // Act
            var result = await _authService.RegisterUser(model);

            // Assert
            result.Succeeded.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Description == "Email already exists.");
        }

        [Fact]
        public async Task MakeUserAdminAsync_UserExists_ReturnsSuccess()
        {
            // Arrange
            var userName = "testuser";
            var user = new Users { UserName = userName, RoleId = 1 };
            _mockUserManager.Setup(um => um.FindByNameAsync(userName)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);
            var adminRole = new Roles { Id = 2, Name = "Admin", Description = "Admin role" };
            _mockUnitOfWork
                .Setup(uow => uow.Roles.FindAsync(It.IsAny<Func<Roles, bool>>()))
                .ReturnsAsync(new List<Roles> { adminRole });

            // Act
            var result = await _authService.MakeUserAdminAsync(userName);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Equal(2, user.RoleId);
            _mockUserManager.Verify(um => um.UpdateAsync(user), Times.Once);
        }

        [Fact]
        public async Task MakeUserAdminAsync_UserDoesNotExist_ReturnsFailedResult()
        {
            // Arrange
            var userName = "nonexistentuser";
            _mockUserManager.Setup(um => um.FindByNameAsync(userName)).ReturnsAsync((Users)null);

            // Act
            var result = await _authService.MakeUserAdminAsync(userName);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains(result.Errors, e => e.Description == "User not found.");
        }

        [Fact]
        public async Task MakeUserAdminAsync_UpdateFails_ReturnsFailedResult()
        {
            // Arrange
            var userName = "testuser";
            var user = new Users { UserName = userName, RoleId = 1 };
            var adminRole = new Roles { Id = 2, Name = "Admin", Description = "Admin role" };

            _mockUserManager.Setup(um => um.FindByNameAsync(userName)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.UpdateAsync(It.Is<Users>(u => u.RoleId == adminRole.Id)))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Update failed." }));
            _mockUnitOfWork
                .Setup(uow => uow.Roles.FindAsync(It.IsAny<Func<Roles, bool>>()))
                .ReturnsAsync(new List<Roles> { adminRole });



            // Act
            var result = await _authService.MakeUserAdminAsync(userName);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains(result.Errors, e => e.Description == "Update failed.");
            _mockUserManager.Verify(um => um.UpdateAsync(It.Is<Users>(u => u.RoleId == adminRole.Id)), Times.Once);
        }

    }
}
