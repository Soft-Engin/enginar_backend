using BackEngin.Controllers;
using BackEngin.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Moq;
using FluentAssertions;


namespace BackEngin.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _authController = new AuthController(_mockAuthService.Object);
        }

        [Fact]
        public async Task Register_ShouldReturnOk_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var model = new RegisterRequestDTO { UserName = "testuser", Email = "test@example.com", Password = "Password123" };
            _mockAuthService.Setup(a => a.RegisterUser(model)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authController.Register(model);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenRegistrationFails()
        {
            // Arrange
            var model = new RegisterRequestDTO { UserName = "testuser", Email = "test@example.com", Password = "Password123" };
            _mockAuthService.Setup(a => a.RegisterUser(model)).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

            // Act
            var result = await _authController.Register(model);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenLoginIsSuccessful()
        {
            // Arrange
            var model = new LoginRequestDTO { Identifier = "testuser", Password = "Password123" };
            string token = "sample-jwt-token";
            _mockAuthService.Setup(a => a.LoginUser(model)).ReturnsAsync(token);

            // Act
            var result = await _authController.Login(model) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(new { Token = token, message = "User logged in successfully!" });
        }


        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenLoginFails()
        {
            // Arrange
            var model = new LoginRequestDTO { Identifier = "testuser", Password = "wrongpassword" };
            _mockAuthService.Setup(a => a.LoginUser(model)).ReturnsAsync((string)null);

            // Act
            var result = await _authController.Login(model);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();
        }

        [Fact]
        public async Task ForgotPassword_ShouldReturnOk_WithToken_WhenEmailExists()
        {
            // Arrange
            var model = new ForgotPasswordDTO { Email = "test@example.com" };
            string resetToken = "sample-reset-token";
            _mockAuthService.Setup(a => a.SendPasswordResetTokenAsync(model.Email)).ReturnsAsync(resetToken);

            // Act
            var result = await _authController.ForgotPassword(model) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(new { Token = resetToken, message = "Password reset token generated successfully." });
        }

        [Fact]
        public async Task ForgotPassword_ShouldReturnOk_WhenEmailDoesNotExist()
        {
            // Arrange
            var model = new ForgotPasswordDTO { Email = "nonexistent@example.com" };
            _mockAuthService.Setup(a => a.SendPasswordResetTokenAsync(model.Email)).ReturnsAsync((string)null);

            // Act
            var result = await _authController.ForgotPassword(model) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(new { message = "If an account with that email exists, a password reset token has been generated." });
        }

        [Fact]
        public async Task ResetPassword_ShouldReturnOk_WhenResetIsSuccessful()
        {
            // Arrange
            var model = new ResetPasswordDTO { Email = "test@example.com", Token = "valid-token", NewPassword = "NewPassword123", ConfirmPassword = "NewPassword123" };
            _mockAuthService.Setup(a => a.ResetPasswordAsync(model)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authController.ResetPassword(model);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task ResetPassword_ShouldReturnBadRequest_WhenResetFails()
        {
            // Arrange
            var model = new ResetPasswordDTO { Email = "test@example.com", Token = "invalid-token", NewPassword = "NewPassword123", ConfirmPassword = "NewPassword123" };
            _mockAuthService.Setup(a => a.ResetPasswordAsync(model)).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Invalid token" }));

            // Act
            var result = await _authController.ResetPassword(model);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenUserNameIsMissing()
        {
            // Arrange
            var model = new RegisterRequestDTO { Email = "test@example.com", Password = "Password123" };
            _authController.ModelState.AddModelError("UserName", "The UserName field is required.");

            // Act
            var result = await _authController.Register(model);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequest = result as BadRequestObjectResult;
            badRequest.Value.Should().BeOfType<SerializableError>();
            ((SerializableError)badRequest.Value).ContainsKey("UserName").Should().BeTrue();
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenEmailIsMissing()
        {
            // Arrange
            var model = new RegisterRequestDTO { UserName = "testuser", Password = "Password123" };
            _authController.ModelState.AddModelError("Email", "The Email field is required.");

            // Act
            var result = await _authController.Register(model);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequest = result as BadRequestObjectResult;
            badRequest.Value.Should().BeOfType<SerializableError>();
            ((SerializableError)badRequest.Value).ContainsKey("Email").Should().BeTrue();
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenPasswordIsMissing()
        {
            // Arrange
            var model = new RegisterRequestDTO { UserName = "testuser", Email = "test@example.com" };
            _authController.ModelState.AddModelError("Password", "The Password field is required.");

            // Act
            var result = await _authController.Register(model);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequest = result as BadRequestObjectResult;
            badRequest.Value.Should().BeOfType<SerializableError>();
            ((SerializableError)badRequest.Value).ContainsKey("Password").Should().BeTrue();
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenEmailIsInvalid()
        {
            // Arrange
            var model = new RegisterRequestDTO { UserName = "testuser", Email = "invalid-email", Password = "Password123" };
            _authController.ModelState.AddModelError("Email", "The Email field is not a valid e-mail address.");

            // Act
            var result = await _authController.Register(model);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequest = result as BadRequestObjectResult;
            badRequest.Value.Should().BeOfType<SerializableError>();
            ((SerializableError)badRequest.Value).ContainsKey("Email").Should().BeTrue();
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenPasswordDoesNotMeetComplexity()
        {
            // Arrange
            var model = new RegisterRequestDTO { UserName = "testuser", Email = "test@example.com", Password = "123" };
            _authController.ModelState.AddModelError("Password", "The Password does not meet complexity requirements.");

            // Act
            var result = await _authController.Register(model);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequest = result as BadRequestObjectResult;
            badRequest.Value.Should().BeOfType<SerializableError>();
            ((SerializableError)badRequest.Value).ContainsKey("Password").Should().BeTrue();
        }


    }

}
