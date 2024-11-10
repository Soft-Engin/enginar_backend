using Moq;
using FluentAssertions;
using BackEngin.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.DTO;
using BackEngin.Tests.Helpers;

namespace BackEngin.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<UserManager<Users>> _mockUserManager;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _mockUserManager = MockHelper.MockUserManager<Users>();
            _mockConfiguration = MockHelper.MockConfiguration();
            _authService = new AuthService(_mockUserManager.Object, _mockConfiguration.Object);
        }

        [Fact]
        public async Task RegisterUser_ShouldReturnSuccess_WhenUserCreated()
        {
            var model = new RegisterRequestDTO { UserName = "testuser", Email = "test@example.com", Password = "Password123" };
            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<Users>(), model.Password))
                .ReturnsAsync(IdentityResult.Success);

            var result = await _authService.RegisterUser(model);

            result.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task RegisterUser_ShouldReturnFailure_WhenUserCreationFails()
        {
            var model = new RegisterRequestDTO { UserName = "testuser", Email = "test@example.com", Password = "Password123" };
            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<Users>(), model.Password))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error creating user" }));

            var result = await _authService.RegisterUser(model);

            result.Succeeded.Should().BeFalse();
        }

        [Fact]
        public async Task LoginUser_ShouldReturnToken_WhenCredentialsAreValid()
        {
            var model = new LoginRequestDTO { Identifier = "testuser", Password = "Password123" };
            var user = new Users { UserName = model.Identifier, Id = "1" };

            _mockUserManager.Setup(um => um.FindByEmailAsync(model.Identifier)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.FindByNameAsync(model.Identifier)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.CheckPasswordAsync(user, model.Password)).ReturnsAsync(true);

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
        public void GenerateJwtToken_ShouldReturnToken_WhenUserIsValid()
        {
            var user = new Users { UserName = "testuser", Id = "1" };

            var token = _authService.GenerateJwtToken(user);

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
    }
}
