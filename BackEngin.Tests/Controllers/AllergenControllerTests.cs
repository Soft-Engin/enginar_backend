using BackEngin.Controllers;
using BackEngin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Moq;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEngin.Tests.Controllers
{
    public class AllergenControllerTests
    {
        private readonly Mock<IAllergenService> _mockAllergenService;
        private readonly AllergenController _allergenController;

        public AllergenControllerTests()
        {
            _mockAllergenService = new Mock<IAllergenService>();
            _allergenController = new AllergenController(_mockAllergenService.Object);
        }

        [Fact]
        public async Task GetAllAllergens_ShouldReturnOk_WithAllAllergens()
        {
            // Arrange
            var allergens = new List<AllergenIdDTO>
            {
                new AllergenIdDTO { Id = 1, Name = "Gluten", Description = "Found in wheat" },
                new AllergenIdDTO { Id = 2, Name = "Dairy", Description = "Milk and milk products" }
            };
            _mockAllergenService.Setup(a => a.GetAllAllergensAsync()).ReturnsAsync(allergens);

            // Act
            var result = await _allergenController.GetAllAllergens() as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(allergens);
        }

        [Fact]
        public async Task CreateAllergen_ShouldReturnOk_WhenCreationIsSuccessful()
        {
            // Arrange
            var allergen = new AllergenDTO { Name = "Soy", Description = "Found in soy products" };
            var allergenId = 1;
            _mockAllergenService.Setup(a => a.CreateAllergenAsync(allergen)).ReturnsAsync(allergenId);

            // Act
            var result = await _allergenController.CreateAllergen(allergen) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(new { Id = allergenId, message = "Allergen created successfully!" });
        }

        [Fact]
        public async Task CreateAllergen_ShouldReturnBadRequest_WhenCreationFails()
        {
            // Arrange
            var allergen = new AllergenDTO { Name = "Soy", Description = "Found in soy products" };
            _mockAllergenService.Setup(a => a.CreateAllergenAsync(allergen)).ReturnsAsync((int?)null);

            // Act
            var result = await _allergenController.CreateAllergen(allergen);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult!.Value.Should().BeEquivalentTo(new { message = "Allergen could not be created." });
        }

        [Fact]
        public async Task UpdateAllergen_ShouldReturnOk_WhenUpdateIsSuccessful()
        {
            // Arrange
            var allergenId = 1;
            var allergen = new AllergenDTO { Name = "Nuts", Description = "Tree nuts" };
            _mockAllergenService.Setup(a => a.UpdateAllergenAsync(allergenId, allergen)).ReturnsAsync(true);

            // Act
            var result = await _allergenController.UpdateAllergen(allergenId, allergen) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(new { message = "Allergen updated successfully!" });
        }

        [Fact]
        public async Task UpdateAllergen_ShouldReturnBadRequest_WhenAllergenDoesNotExist()
        {
            // Arrange
            var allergenId = 1;
            var allergen = new AllergenDTO { Name = "Nuts", Description = "Tree nuts" };
            _mockAllergenService.Setup(a => a.UpdateAllergenAsync(allergenId, allergen)).ReturnsAsync((bool?)null);

            // Act
            var result = await _allergenController.UpdateAllergen(allergenId, allergen);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult!.Value.Should().BeEquivalentTo(new { message = "Allergen does not exist." });
        }

        [Fact]
        public async Task DeleteAllergen_ShouldReturnOk_WhenDeletionIsSuccessful()
        {
            // Arrange
            var allergenId = 1;
            _mockAllergenService.Setup(a => a.DeleteAllergenAsync(allergenId)).ReturnsAsync(true);

            // Act
            var result = await _allergenController.DeleteAllergen(allergenId) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(new { message = "Allergen deleted successfully!" });
        }

        [Fact]
        public async Task DeleteAllergen_ShouldReturnBadRequest_WhenAllergenDoesNotExist()
        {
            // Arrange
            var allergenId = 1;
            _mockAllergenService.Setup(a => a.DeleteAllergenAsync(allergenId)).ReturnsAsync((bool?)null);

            // Act
            var result = await _allergenController.DeleteAllergen(allergenId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult!.Value.Should().BeEquivalentTo(new { message = "Allergen does not exist." });
        }
    }
}
