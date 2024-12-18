using BackEngin.Services;
using BackEngin.Tests.Utils;
using DataAccess.Repositories;
using DataAccess.Repositories.IRepositories;
using FluentAssertions;
using Models;
using Models.DTO;
using Moq;
using MockQueryable.Moq;

namespace BackEngin.Tests.Services
{
    public class IngredientsServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IIngredients_PreferencesRepository> _mockIngredientsPreferencesRepository;
        private readonly Mock<IIngredientsRepository> _mockIngredientsRepository;
        private readonly Mock<IPreferencesRepository> _mockPreferencesRepository;
        private readonly IngredientsService _ingredientsService;

        public IngredientsServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            // Initialize mock for Ingredients_Preferences repository
            _mockIngredientsPreferencesRepository = new Mock<IIngredients_PreferencesRepository>();

            // Setup the Ingredients_Preferences repository in the UnitOfWork mock
            _mockUnitOfWork.Setup(u => u.Ingredients_Preferences)
                           .Returns(_mockIngredientsPreferencesRepository.Object);

            // Initialize mock for Ingredients repository
            _mockIngredientsRepository = new Mock<IIngredientsRepository>();
            _mockUnitOfWork.Setup(u => u.Ingredients)
                           .Returns(_mockIngredientsRepository.Object);

            // Initialize mock for Preferences repository
            _mockPreferencesRepository = new Mock<IPreferencesRepository>();
            _mockUnitOfWork.Setup(u => u.Preferences)
                           .Returns(_mockPreferencesRepository.Object);

            // Initialize IngredientsService with the mocked UnitOfWork
            _ingredientsService = new IngredientsService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetIngredientsPaginatedAsync_ShouldReturnPaginatedIngredients()
        {
            // Arrange
            var ingredients = new List<Ingredients>
            {
                new Ingredients { Id = 1, Name = "Flour", Type = new IngredientTypes { Id = 1, Name = "Grain" }, Ingredients_Preferences = new List<Ingredients_Preferences>() },
                new Ingredients { Id = 2, Name = "Milk", Type = new IngredientTypes { Id = 2, Name = "Dairy" }, Ingredients_Preferences = new List<Ingredients_Preferences>() }
            };
            var pageNumber = 1;
            var pageSize = 2;

            _mockIngredientsRepository.Setup(u => u.GetPaginatedAsync(It.IsAny<string>(), null, pageNumber, pageSize))
                                     .ReturnsAsync((ingredients, 5));

            // Act
            var result = await _ingredientsService.GetIngredientsPaginatedAsync(pageNumber, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Count().Should().Be(pageSize);
            result.TotalCount.Should().Be(5);
            result.PageNumber.Should().Be(pageNumber);
            result.PageSize.Should().Be(pageSize);
            result.Items.First().Name.Should().Be("Flour");
        }

        [Fact]
        public async Task GetIngredientByIdAsync_ShouldReturnIngredient_WhenExists()
        {
            // Arrange
            var ingredientId = 1;
            var ingredient = new Ingredients
            {
                Id = ingredientId,
                Name = "Flour",
                Type = new IngredientTypes { Id = 1, Name = "Grain" },
                Ingredients_Preferences = new List<Ingredients_Preferences>()
            };

            _mockIngredientsRepository.Setup(u => u.GetAllAsync(It.IsAny<string>()))
                                     .ReturnsAsync(new List<Ingredients> { ingredient });

            // Act
            var result = await _ingredientsService.GetIngredientByIdAsync(ingredientId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(ingredientId);
            result.Name.Should().Be("Flour");
        }

        [Fact]
        public async Task GetIngredientByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            var ingredientId = 99;
            _mockIngredientsRepository.Setup(u => u.GetAllAsync(It.IsAny<string>()))
                                     .ReturnsAsync(new List<Ingredients>());

            // Act
            var result = await _ingredientsService.GetIngredientByIdAsync(ingredientId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateIngredientAsync_ShouldReturnIngredientId_WhenSuccessful()
        {
            // Arrange
            var ingredientDto = new IngredientDTO
            {
                Name = "Sugar",
                TypeId = 1,
                AllergenIds = new List<int> { 2 }
            };

            var existingAllergens = new List<Preferences>
                {
                    new Preferences { Id = 2, Name = "Lactose" }
                };

            // Mock the Preferences repository to return existing allergens
            _mockPreferencesRepository.Setup(u => u.GetAllAsync())
                                     .ReturnsAsync(existingAllergens);

            // Mock the Ingredients repository's AddAsync method
            _mockIngredientsRepository.Setup(u => u.AddAsync(It.IsAny<Ingredients>()))
                                     .Callback<Ingredients>(ing => ing.Id = 3) // Simulate DB-generated Id
                                     .Returns(Task.CompletedTask)
                                     .Verifiable();

            // Mock the Ingredients_Preferences repository's AddAsync method
            _mockIngredientsPreferencesRepository.Setup(u => u.AddAsync(It.IsAny<Ingredients_Preferences>()))
                                                .Returns(Task.CompletedTask)
                                                .Verifiable();

            // Mock the UnitOfWork's CompleteAsync method to return 1 (indicating success)
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                           .ReturnsAsync(1)
                           .Verifiable();

            // Act
            var result = await _ingredientsService.CreateIngredientAsync(ingredientDto);

            // Assert
            result.Should().Be(3); // Expected to be the assigned Id

            // Verify that AddAsync was called once for Ingredients
            _mockIngredientsRepository.Verify(u => u.AddAsync(It.IsAny<Ingredients>()), Times.Once);

            // Verify that AddAsync was called once for Ingredients_Preferences
            _mockIngredientsPreferencesRepository.Verify(u => u.AddAsync(It.IsAny<Ingredients_Preferences>()), Times.Once);

            // Verify that CompleteAsync was called twice (once after adding ingredient, once after adding preferences)
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Exactly(2));
        }


        [Fact]
        public async Task CreateIngredientAsync_ShouldReturnNull_WhenExceptionOccurs()
        {
            // Arrange
            var ingredientDto = new IngredientDTO
            {
                Name = "Sugar",
                TypeId = 1,
                AllergenIds = new List<int> { 2 }
            };

            _mockPreferencesRepository.Setup(u => u.GetAllAsync())
                                     .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _ingredientsService.CreateIngredientAsync(ingredientDto);

            // Assert
            result.Should().BeNull();
        }


        [Fact]
        public async Task UpdateIngredientAsync_ShouldReturnNull_WhenIngredientNotFound()
        {
            // Arrange
            var ingredientId = 99;
            var ingredientDto = new IngredientDTO
            {
                Name = "Brown Sugar",
                TypeId = 1,
                AllergenIds = new List<int>()
            };

            _mockIngredientsRepository.Setup(u => u.GetByIdAsync(ingredientId))
                                     .ReturnsAsync((Ingredients)null);

            // Act
            var result = await _ingredientsService.UpdateIngredientAsync(ingredientId, ingredientDto);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateIngredientAsync_ShouldReturnFalse_WhenExceptionOccurs()
        {
            // Arrange
            var ingredientId = 1;
            var ingredientDto = new IngredientDTO
            {
                Name = "Brown Sugar",
                TypeId = 1,
                AllergenIds = new List<int> { 1 } // Non-empty list to trigger GetAllAsync
            };

            var existingIngredient = new Ingredients
            {
                Id = ingredientId,
                Name = "Sugar",
                TypeId = 1
            };

            // Mock the Ingredients repository's GetByIdAsync method
            _mockIngredientsRepository.Setup(u => u.GetByIdAsync(ingredientId))
                                     .ReturnsAsync(existingIngredient);

            // Mock the Preferences repository's GetAllAsync method to throw an exception
            _mockPreferencesRepository.Setup(u => u.GetAllAsync())
                                     .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _ingredientsService.UpdateIngredientAsync(ingredientId, ingredientDto);

            // Assert
            result.Should().BeFalse();
        }


        [Fact]
        public async Task DeleteIngredientAsync_ShouldReturnTrue_WhenSuccessful()
        {
            // Arrange
            var ingredientId = 1;
            var existingIngredient = new Ingredients
            {
                Id = ingredientId,
                Name = "Sugar",
                TypeId = 1
            };

            // Mock the Ingredients repository's GetByIdAsync method
            _mockIngredientsRepository.Setup(u => u.GetByIdAsync(ingredientId))
                                     .ReturnsAsync(existingIngredient);

            // Mock the Ingredients_Preferences repository's FindAsync method to return associated preferences
            var associatedPreferences = new List<Ingredients_Preferences>
            {
                new Ingredients_Preferences { IngredientId = ingredientId, PreferenceId = 2 },
                new Ingredients_Preferences { IngredientId = ingredientId, PreferenceId = 3 }
            };
            _mockIngredientsPreferencesRepository.Setup(u => u.FindAsync(It.IsAny<Func<Ingredients_Preferences, bool>>()))
                                                .ReturnsAsync(associatedPreferences);

            // Mock DeleteRange method
            _mockIngredientsPreferencesRepository.Setup(u => u.DeleteRange(associatedPreferences))
                                                .Verifiable();

            // Mock the Ingredients repository's Delete method
            _mockIngredientsRepository.Setup(u => u.Delete(existingIngredient))
                                     .Verifiable();

            // Mock CompleteAsync
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                           .ReturnsAsync(1)
                           .Verifiable();

            // Act
            var result = await _ingredientsService.DeleteIngredientAsync(ingredientId);

            // Assert
            result.Should().BeTrue();
            _mockIngredientsPreferencesRepository.Verify(u => u.FindAsync(It.IsAny<Func<Ingredients_Preferences, bool>>()), Times.Once);
            _mockIngredientsPreferencesRepository.Verify(u => u.DeleteRange(associatedPreferences), Times.Once);
            _mockIngredientsRepository.Verify(u => u.Delete(existingIngredient), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteIngredientAsync_ShouldReturnTrue_WhenNoAssociatedPreferences()
        {
            // Arrange
            var ingredientId = 2;
            var existingIngredient = new Ingredients
            {
                Id = ingredientId,
                Name = "Salt",
                TypeId = 1
            };

            // Mock the Ingredients repository's GetByIdAsync method
            _mockIngredientsRepository.Setup(u => u.GetByIdAsync(ingredientId))
                                     .ReturnsAsync(existingIngredient);

            // Mock the Ingredients_Preferences repository's FindAsync method to return no associated preferences
            _mockIngredientsPreferencesRepository.Setup(u => u.FindAsync(It.IsAny<Func<Ingredients_Preferences, bool>>()))
                                                .ReturnsAsync(new List<Ingredients_Preferences>());

            // Mock the Ingredients repository's Delete method
            _mockIngredientsRepository.Setup(u => u.Delete(existingIngredient))
                                     .Verifiable();

            // Mock CompleteAsync
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                           .ReturnsAsync(1)
                           .Verifiable();

            // Act
            var result = await _ingredientsService.DeleteIngredientAsync(ingredientId);

            // Assert
            result.Should().BeTrue();
            _mockIngredientsPreferencesRepository.Verify(u => u.FindAsync(It.IsAny<Func<Ingredients_Preferences, bool>>()), Times.Once);
            _mockIngredientsPreferencesRepository.Verify(u => u.DeleteRange(It.IsAny<List<Ingredients_Preferences>>()), Times.Never);
            _mockIngredientsRepository.Verify(u => u.Delete(existingIngredient), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteIngredientAsync_ShouldReturnFalse_WhenExceptionOccurs()
        {
            // Arrange
            var ingredientId = 3;
            var existingIngredient = new Ingredients
            {
                Id = ingredientId,
                Name = "Honey",
                TypeId = 1
            };

            // Mock the Ingredients repository's GetByIdAsync method
            _mockIngredientsRepository.Setup(u => u.GetByIdAsync(ingredientId))
                                     .ReturnsAsync(existingIngredient);

            // Mock the Ingredients_Preferences repository's FindAsync method to throw an exception
            _mockIngredientsPreferencesRepository.Setup(u => u.FindAsync(It.IsAny<Func<Ingredients_Preferences, bool>>()))
                                                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _ingredientsService.DeleteIngredientAsync(ingredientId);

            // Assert
            result.Should().BeFalse();
            _mockIngredientsPreferencesRepository.Verify(u => u.FindAsync(It.IsAny<Func<Ingredients_Preferences, bool>>()), Times.Once);
            _mockIngredientsPreferencesRepository.Verify(u => u.DeleteRange(It.IsAny<List<Ingredients_Preferences>>()), Times.Never);
            _mockIngredientsRepository.Verify(u => u.Delete(It.IsAny<Ingredients>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Never);
        }

        [Fact]
        public async Task SearchIngredients_ShouldReturnAll_WhenNoFilter()
        {
            var ingredientTypes = TestUtilities.CreateIngredientTypes(2);
            var allAllergens = TestUtilities.CreateAllergens(3);
            var ingredients = TestUtilities.CreateIngredients(10, ingredientTypes, true, allAllergens).AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Ingredients.GetQueryable()).Returns(ingredients.Object);

            var searchParams = new IngredientSearchParams();

            var result = await _ingredientsService.SearchIngredients(searchParams, 1, 20);

            result.TotalCount.Should().Be(10);
            result.Items.Should().HaveCount(10);
        }

        [Fact]
        public async Task SearchIngredients_ShouldFilterByName()
        {
            var types = TestUtilities.CreateIngredientTypes(1);
            var allergens = TestUtilities.CreateAllergens(2);
            var ingredientList = TestUtilities.CreateIngredients(5, types, true, allergens);
            ingredientList[0].Name = "SpecialIngredient";

            var mockIngredients = ingredientList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Ingredients.GetQueryable()).Returns(mockIngredients.Object);

            var searchParams = new IngredientSearchParams
            {
                NameContains = "Special"
            };

            var result = await _ingredientsService.SearchIngredients(searchParams, 1, 10);

            result.TotalCount.Should().Be(1);
            result.Items.First().Name.Should().Be("SpecialIngredient");
        }

        [Fact]
        public async Task SearchIngredients_ShouldFilterByIngredientTypeIds()
        {
            var types = TestUtilities.CreateIngredientTypes(3);
            var allergens = TestUtilities.CreateAllergens(2);
            var ingredientList = TestUtilities.CreateIngredients(10, types, true, allergens);

            // Let's pick only typeId = 1 and 2
            var chosenTypes = new List<int> { 1, 2 };
            // Make sure some ingredients have typeId=3
            ingredientList[8].TypeId = 3;
            ingredientList[9].TypeId = 3;

            var mockIngredients = ingredientList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Ingredients.GetQueryable()).Returns(mockIngredients.Object);

            var searchParams = new IngredientSearchParams
            {
                IngredientTypeIds = chosenTypes
            };

            var result = await _ingredientsService.SearchIngredients(searchParams, 1, 20);

            result.Items.Should().AllSatisfy(i => chosenTypes.Contains(i.Type.Id));
            result.TotalCount.Should().Be(ingredientList.Count(i => chosenTypes.Contains(i.TypeId)));
        }

        [Fact]
        public async Task SearchIngredients_ShouldExcludeAllergens()
        {
            var types = TestUtilities.CreateIngredientTypes(1);
            var allergens = TestUtilities.CreateAllergens(2);
            var ingredientList = TestUtilities.CreateIngredients(5, types, true, allergens);

            // IngredientId=1 has no allergen=2
            ingredientList[0].Ingredients_Preferences.Clear();
            // IngredientId=2 has allergen=2
            ingredientList[1].Ingredients_Preferences.Clear();
            ingredientList[1].Ingredients_Preferences.Add(new Ingredients_Preferences
            {
                IngredientId = 2,
                PreferenceId = 2,
                Preference = allergens[1]
            });

            var mockIngredients = ingredientList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Ingredients.GetQueryable()).Returns(mockIngredients.Object);

            var searchParams = new IngredientSearchParams
            {
                AllergenIds = new List<int> { 2 }
            };

            // Only ingredients without allergen=2 or with no allergens should remain
            var result = await _ingredientsService.SearchIngredients(searchParams, 1, 10);

            result.Items.Should().NotContain(i => i.Allergens.Any(a => a.Id == 2));
        }

        [Fact]
        public async Task SearchIngredients_ShouldApplySorting()
        {
            var types = TestUtilities.CreateIngredientTypes(1);
            var allergens = TestUtilities.CreateAllergens(1);
            var ingredientList = TestUtilities.CreateIngredients(3, types, false, allergens);

            ingredientList[0].Name = "Z-Ingredient";
            ingredientList[1].Name = "A-Ingredient";
            ingredientList[2].Name = "M-Ingredient";

            var mockIngredients = ingredientList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Ingredients.GetQueryable()).Returns(mockIngredients.Object);

            var searchParams = new IngredientSearchParams
            {
                SortBy = "Name",
                SortOrder = "asc"
            };

            var result = await _ingredientsService.SearchIngredients(searchParams, 1, 10);
            result.Items.First().Name.Should().Be("A-Ingredient");
        }

        [Fact]
        public async Task SearchIngredients_ShouldPaginate()
        {
            var types = TestUtilities.CreateIngredientTypes(1);
            var allergens = TestUtilities.CreateAllergens(1);
            var ingredientList = TestUtilities.CreateIngredients(25, types, false, allergens);

            var mockIngredients = ingredientList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Ingredients.GetQueryable()).Returns(mockIngredients.Object);

            var searchParams = new IngredientSearchParams();
            var pageNumber = 2;
            var pageSize = 5;

            var result = await _ingredientsService.SearchIngredients(searchParams, pageNumber, pageSize);

            result.PageNumber.Should().Be(pageNumber);
            result.PageSize.Should().Be(pageSize);
            result.TotalCount.Should().Be(25);
            result.Items.Should().HaveCount(5);
        }

    }
}
