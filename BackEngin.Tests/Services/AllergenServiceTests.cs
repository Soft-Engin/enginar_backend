using BackEngin.Services;
using BackEngin.Services.Interfaces;
using DataAccess.Repositories;
using Models;
using Models.DTO;
using Moq;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Repositories.IRepositories;
using System.Linq.Expressions;

namespace BackEngin.Tests.Services
{
    public class AllergenServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly AllergenService _allergenService;
        private readonly Mock<IPreferencesRepository> _mockPreferencesRepository;


        public AllergenServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _allergenService = new AllergenService(_mockUnitOfWork.Object);

            _mockPreferencesRepository = new Mock<IPreferencesRepository>();

            _mockUnitOfWork.Setup(u => u.Preferences)
                           .Returns(_mockPreferencesRepository.Object);
        }


        [Fact]
        public async Task GetPaginatedAllergensAsync_ShouldReturnPaginatedAllergens()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 2;
            var allergens = new List<Preferences>
            {
                new Preferences { Id = 1, Name = "Gluten", Description = "Found in wheat" },
                new Preferences { Id = 2, Name = "Dairy", Description = "Milk and milk products" }
            };

            _mockPreferencesRepository
                 .Setup(u => u.GetPaginatedAsync(It.IsAny<Expression<Func<Preferences, bool>>>(), pageNumber, pageSize))
                 .ReturnsAsync((allergens, 10));

            // Act
            var result = await _allergenService.GetPaginatedAsync(pageNumber, pageSize);

            // Verify
            _mockPreferencesRepository.Verify(u => u.GetPaginatedAsync(It.IsAny<Expression<Func<Preferences, bool>>>(), pageNumber, pageSize), Times.Once);
            
            // Assert
            result.Should().NotBeNull();
            result.Items.Count().Should().Be(pageSize);
            result.TotalCount.Should().Be(10);
            result.PageNumber.Should().Be(pageNumber);
            result.PageSize.Should().Be(pageSize);
            result.Items.First().Name.Should().Be("Gluten");
            result.Items.Last().Description.Should().Be("Milk and milk products");

        }


        [Fact]
        public async Task CreateAllergenAsync_ShouldReturnAllergenId_WhenCreationIsSuccessful()
        {
            // Arrange
            var allergenDTO = new AllergenDTO { Name = "Soy", Description = "Found in soy products" };
            var allergen = new Preferences { Id = 1, Name = "Soy", Description = "Found in soy products" };
            _mockUnitOfWork.Setup(u => u.Preferences.AddAsync(It.IsAny<Preferences>()))
                .Callback<Preferences>(a => a.Id = allergen.Id)
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _allergenService.CreateAllergenAsync(allergenDTO);

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(1);
        }

        [Fact]
        public async Task CreateAllergenAsync_ShouldReturnNull_WhenAnExceptionOccurs()
        {
            // Arrange
            var allergenDTO = new AllergenDTO { Name = "Soy", Description = "Found in soy products" };
            _mockUnitOfWork.Setup(u => u.Preferences.AddAsync(It.IsAny<Preferences>())).ThrowsAsync(new Exception());

            // Act
            var result = await _allergenService.CreateAllergenAsync(allergenDTO);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAllergenAsync_ShouldReturnTrue_WhenDeletionIsSuccessful()
        {
            // Arrange
            var allergen = new Preferences { Id = 1, Name = "Gluten", Description = "Found in wheat" };
            _mockUnitOfWork.Setup(u => u.Preferences.GetByIdAsync(allergen.Id)).ReturnsAsync(allergen);
            _mockUnitOfWork.Setup(u => u.Preferences.Delete(allergen));
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _allergenService.DeleteAllergenAsync(allergen.Id);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAllergenAsync_ShouldReturnNull_WhenAllergenDoesNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(u => u.Preferences.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Preferences)null);

            // Act
            var result = await _allergenService.DeleteAllergenAsync(1);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAllergenAsync_ShouldReturnFalse_WhenAnExceptionOccurs()
        {
            // Arrange
            var allergen = new Preferences { Id = 1, Name = "Gluten", Description = "Found in wheat" };
            _mockUnitOfWork.Setup(u => u.Preferences.GetByIdAsync(allergen.Id)).ReturnsAsync(allergen);
            _mockUnitOfWork.Setup(u => u.Preferences.Delete(allergen)).Throws(new Exception());

            // Act
            var result = await _allergenService.DeleteAllergenAsync(allergen.Id);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateAllergenAsync_ShouldReturnTrue_WhenUpdateIsSuccessful()
        {
            // Arrange
            var allergenId = 1;
            var allergenDTO = new AllergenDTO { Name = "Updated Name", Description = "Updated Description" };
            var allergen = new Preferences { Id = allergenId, Name = "Old Name", Description = "Old Description" };
            _mockUnitOfWork.Setup(u => u.Preferences.GetByIdAsync(allergenId)).ReturnsAsync(allergen);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _allergenService.UpdateAllergenAsync(allergenId, allergenDTO);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateAllergenAsync_ShouldReturnNull_WhenAllergenDoesNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(u => u.Preferences.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Preferences)null);

            // Act
            var result = await _allergenService.UpdateAllergenAsync(1, new AllergenDTO());

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAllergenAsync_ShouldReturnFalse_WhenAnExceptionOccurs()
        {
            // Arrange
            var allergenId = 1;
            var allergenDTO = new AllergenDTO { Name = "Updated Name", Description = "Updated Description" };
            var allergen = new Preferences { Id = allergenId, Name = "Old Name", Description = "Old Description" };
            _mockUnitOfWork.Setup(u => u.Preferences.GetByIdAsync(allergenId)).ReturnsAsync(allergen);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).Throws(new Exception());

            // Act
            var result = await _allergenService.UpdateAllergenAsync(allergenId, allergenDTO);

            // Assert
            result.Should().BeFalse();
        }
    }
}
