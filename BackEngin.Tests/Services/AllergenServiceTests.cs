using BackEngin.Services;
using DataAccess.Repositories;
using Models;
using Models.DTO;
using Moq;
using FluentAssertions;
using BackEngin.Tests.Utils;
using MockQueryable.Moq;

namespace BackEngin.Tests.Services
{
    public class AllergenServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly AllergenService _allergenService;

        public AllergenServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _allergenService = new AllergenService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetAllAllergensAsync_ShouldReturnListOfAllergenIdDTO()
        {
            // Arrange
            var allergens = new List<Preferences>
            {
                new Preferences { Id = 1, Name = "Gluten", Description = "Found in wheat" },
                new Preferences { Id = 2, Name = "Dairy", Description = "Milk and milk products" }
            };
            _mockUnitOfWork.Setup(u => u.Preferences.GetAllAsync()).ReturnsAsync(allergens);

            // Act
            var result = await _allergenService.GetAllAllergensAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(new List<AllergenIdDTO>
            {
                new AllergenIdDTO { Id = 1, Name = "Gluten", Description = "Found in wheat" },
                new AllergenIdDTO { Id = 2, Name = "Dairy", Description = "Milk and milk products" }
            });
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

        [Fact]
        public async Task SearchAllergens_ShouldReturnAll_WhenNoFilterApplied()
        {
            // Arrange
            var allergens = TestUtilities.CreateAllergens(5).AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Preferences.GetQueryable()).Returns(allergens.Object);
            var searchParams = new AllergenSearchParams(); // No filters, should return all

            // Act
            var result = await _allergenService.SearchAllergens(searchParams, 1, 10);

            // Assert
            result.TotalCount.Should().Be(5);
            result.Items.Should().HaveCount(5);
        }

        [Fact]
        public async Task SearchAllergens_ShouldFilterByName()
        {
            // Arrange
            var allergens = TestUtilities.CreateAllergens(5);
            allergens.First().Name = "SpecialAllergen";
            var mockAllergens = allergens.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Preferences.GetQueryable()).Returns(mockAllergens.Object);

            var searchParams = new AllergenSearchParams
            {
                NameContains = "Special"
            };

            // Act
            var result = await _allergenService.SearchAllergens(searchParams, 1, 10);

            // Assert
            result.TotalCount.Should().Be(1);
            result.Items.Should().HaveCount(1);
            result.Items.First().Name.Should().Be("SpecialAllergen");
        }

        [Fact]
        public async Task SearchAllergens_ShouldFilterByDescription()
        {
            // Arrange
            var allergens = TestUtilities.CreateAllergens(5);
            allergens.Last().Description = "unique description";
            var mockAllergens = allergens.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Preferences.GetQueryable()).Returns(mockAllergens.Object);

            var searchParams = new AllergenSearchParams
            {
                DescriptionContains = "unique"
            };

            // Act
            var result = await _allergenService.SearchAllergens(searchParams, 1, 10);

            // Assert
            result.TotalCount.Should().Be(1);
            result.Items.Should().HaveCount(1);
            result.Items.First().Description.Should().Be("unique description");
        }

        [Fact]
        public async Task SearchAllergens_ShouldApplySorting()
        {
            // Arrange
            var allergens = TestUtilities.CreateAllergens(5);
            allergens[0].Name = "Zeta";
            allergens[1].Name = "Alpha";
            var mockAllergens = allergens.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Preferences.GetQueryable()).Returns(mockAllergens.Object);

            var searchParams = new AllergenSearchParams
            {
                SortBy = "Name",
                SortOrder = "desc"
            };

            // Act
            var result = await _allergenService.SearchAllergens(searchParams, 1, 10);

            // Assert (desc should put "Zeta" first if it exists)
            result.Items.First().Name.Should().Be("Zeta");
        }

        [Fact]
        public async Task SearchAllergens_ShouldPaginateResults()
        {
            // Arrange
            var allergens = TestUtilities.CreateAllergens(30).AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Preferences.GetQueryable()).Returns(allergens.Object);

            var searchParams = new AllergenSearchParams();
            int pageNumber = 2;
            int pageSize = 10;

            // Act
            var result = await _allergenService.SearchAllergens(searchParams, pageNumber, pageSize);

            // Assert
            result.PageNumber.Should().Be(pageNumber);
            result.PageSize.Should().Be(pageSize);
            result.TotalCount.Should().Be(30);
            result.Items.Should().HaveCount(10);
        }


    }
}
