using BackEngin.Services;
using BackEngin.Services.Interfaces;
using DataAccess.Repositories;
using FluentAssertions;
using Models;
using Models.DTO;
using Moq;

namespace BackEngin.Tests.Services
{
    public class RecipeServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly RecipeService _recipeService;

        public RecipeServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _recipeService = new RecipeService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetRecipes_ShouldReturnPaginatedRecipes()
        {
            // Arrange
            var recipes = new List<Recipes>
            {
                new Recipes { Id = 1, Header = "Pancakes", BodyText = "Delicious pancakes" },
                new Recipes { Id = 2, Header = "Omelette", BodyText = "Fluffy omelette" },
                new Recipes { Id = 3, Header = "Waffles", BodyText = "Crispy waffles" }
            };

            var pageNumber = 1;
            var pageSize = 2;

            _mockUnitOfWork.Setup(u => u.Recipes.GetPaginatedAsync(null, pageNumber, pageSize))
                           .ReturnsAsync((recipes.Take(pageSize), recipes.Count));

            // Act
            var result = await _recipeService.GetRecipes(pageNumber, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Count().Should().Be(pageSize); // Check the page size
            result.TotalCount.Should().Be(3); // Verify total count of recipes
            result.PageNumber.Should().Be(pageNumber); // Verify current page
            result.PageSize.Should().Be(pageSize); // Verify page size
            result.Items.First().Header.Should().Be("Pancakes"); // Verify first recipe
        }


        [Fact]
        public async Task CreateRecipe_ShouldThrowException_WhenNoIngredientsProvided()
        {
            // Arrange
            var createRecipeDto = new CreateRecipeDTO
            {
                Header = "Pancakes",
                BodyText = "Delicious pancakes recipe",
                UserId = "123",
                Ingredients = new List<RecipeIngredientRequestDTO>() // Empty ingredients
            };

            // Act
            Func<Task> act = async () => await _recipeService.CreateRecipe(createRecipeDto);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("A recipe must have at least one ingredient.");
        }

        [Fact]
        public async Task CreateRecipe_ShouldThrowException_WhenIngredientIdsAreInvalid()
        {
            // Arrange
            var createRecipeDto = new CreateRecipeDTO
            {
                Header = "Pancakes",
                BodyText = "Delicious pancakes recipe",
                UserId = "123",
                Ingredients = new List<RecipeIngredientRequestDTO>
                {
                    new RecipeIngredientRequestDTO { IngredientId = 999, Quantity = 2, Unit = "cups" } // Invalid ID
                }
            };

            // Mock to simulate invalid ingredient IDs
            _mockUnitOfWork.Setup(u => u.Ingredients.FindAsync(It.IsAny<Func<Ingredients, bool>>()))
                .ReturnsAsync(new List<Ingredients>()); // Empty result

            // Act
            Func<Task> act = async () => await _recipeService.CreateRecipe(createRecipeDto);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("One or more ingredient IDs are invalid.");
        }

        [Fact]
        public async Task CreateRecipe_ShouldReturnCreatedRecipe_WhenValidIngredientsProvided()
        {
            // Arrange
            var createRecipeDto = new CreateRecipeDTO
            {
                Header = "Pancakes",
                BodyText = "Delicious pancakes recipe",
                UserId = "123",
                Ingredients = new List<RecipeIngredientRequestDTO>
                {
                    new RecipeIngredientRequestDTO { IngredientId = 1, Quantity = 2, Unit = "cups" }
                }
            };

            var createdRecipe = new Recipes
            {
                Id = 1,
                Header = createRecipeDto.Header,
                BodyText = createRecipeDto.BodyText,
                UserId = createRecipeDto.UserId
            };

            var validIngredients = new List<Ingredients>
            {
                new Ingredients { Id = 1, Name = "Flour" }
            };

            // Mock valid ingredients
            _mockUnitOfWork.Setup(u => u.Ingredients.FindAsync(It.IsAny<Func<Ingredients, bool>>()))
                .ReturnsAsync(validIngredients);

            _mockUnitOfWork.Setup(u => u.Recipes.AddAsync(It.IsAny<Recipes>())).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);
            _mockUnitOfWork.Setup(u => u.Recipes_Ingredients.AddAsync(It.IsAny<Recipes_Ingredients>())).Verifiable();

            // Act
            var result = await _recipeService.CreateRecipe(createRecipeDto);

            // Assert
            result.Should().NotBeNull();
            result.Header.Should().Be("Pancakes");
            result.BodyText.Should().Be("Delicious pancakes recipe");
            result.Ingredients.Count.Should().Be(1);
            result.Ingredients.First().IngredientId.Should().Be(1);

            _mockUnitOfWork.Verify(u => u.Recipes.AddAsync(It.IsAny<Recipes>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.AtLeastOnce);
            _mockUnitOfWork.Verify(u => u.Recipes_Ingredients.AddAsync(It.IsAny<Recipes_Ingredients>()), Times.Once);
        }



        [Fact]
        public async Task GetRecipeDetails_ShouldReturnRecipeWithIngredients()
        {
            // Arrange
            var recipeId = 1;
            var recipe = new Recipes { Id = recipeId, Header = "Pancakes", BodyText = "Delicious pancakes", UserId = "123" };
            var ingredients = new List<Recipes_Ingredients>
            {
                new Recipes_Ingredients { RecipeId = recipeId, IngredientId = 1, Quantity = 2, Unit = "cups" }
            };
            var ingredientDetails = new List<Ingredients>
            {
                new Ingredients { Id = 1, Name = "Flour" }
            };

            _mockUnitOfWork.Setup(u => u.Recipes.GetByIdAsync(recipeId)).ReturnsAsync(recipe);
            _mockUnitOfWork.Setup(u => u.Recipes_Ingredients.FindAsync(It.IsAny<Func<Recipes_Ingredients, bool>>())).ReturnsAsync(ingredients);
            _mockUnitOfWork.Setup(u => u.Ingredients.FindAsync(It.IsAny<Func<Ingredients, bool>>())).ReturnsAsync(ingredientDetails);

            // Act
            var result = await _recipeService.GetRecipeDetails(recipeId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(recipeId);
            result.Ingredients.Should().HaveCount(1);
        }

        [Fact]
        public async Task UpdateRecipe_ShouldReturnUpdatedRecipe()
        {
            // Arrange
            var recipeId = 1;
            var updateRecipeDto = new RecipeRequestDTO
            {
                Header = "Updated Pancakes",
                BodyText = "Updated delicious pancakes recipe",
                Ingredients = new List<RecipeIngredientRequestDTO>
                {
                    new RecipeIngredientRequestDTO { IngredientId = 1, Quantity = 3, Unit = "cups" }
                }
            };

            var existingRecipe = new Recipes
            {
                Id = recipeId,
                Header = "Old Pancakes",
                BodyText = "Old pancakes recipe",
                UserId = "123"
            };

            var updatedRecipeIngredients = updateRecipeDto.Ingredients
                .Select(i => new RecipeIngredientDetailsDTO
                {
                    IngredientId = i.IngredientId,
                    Quantity = i.Quantity,
                    Unit = i.Unit,
                    IngredientName = "Flour" // Simulated from ingredient lookup
                }).ToList();

            var updatedRecipe = new RecipeDetailsDTO
            {
                Id = recipeId,
                Header = updateRecipeDto.Header,
                BodyText = updateRecipeDto.BodyText,
                Ingredients = updatedRecipeIngredients
            };

            _mockUnitOfWork.Setup(u => u.Recipes.GetByIdAsync(recipeId)).ReturnsAsync(existingRecipe);
            _mockUnitOfWork.Setup(u => u.Recipes_Ingredients.FindAsync(It.IsAny<Func<Recipes_Ingredients, bool>>()))
                           .ReturnsAsync(new List<Recipes_Ingredients>
                           {
                       new Recipes_Ingredients { RecipeId = recipeId, IngredientId = 1, Quantity = 2, Unit = "cups" }
                           });
            _mockUnitOfWork.Setup(u => u.Ingredients.FindAsync(It.IsAny<Func<Ingredients, bool>>()))
                           .ReturnsAsync(new List<Ingredients>
                           {
                       new Ingredients { Id = 1, Name = "Flour" }
                           });
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _recipeService.UpdateRecipe(recipeId, updateRecipeDto);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(recipeId);
            result.Header.Should().Be(updateRecipeDto.Header);
            result.BodyText.Should().Be(updateRecipeDto.BodyText);
            result.Ingredients.Should().BeEquivalentTo(updateRecipeDto.Ingredients);
        }

        [Fact]
        public async Task UpdateRecipe_ShouldThrowArgumentException_WhenNoIngredientsProvided()
        {
            // Arrange
            var recipeId = 1;
            var updateRecipeDto = new RecipeRequestDTO
            {
                Header = "Updated Pancakes",
                BodyText = "Updated delicious pancakes recipe",
                Ingredients = null // No ingredients provided
            };

            var existingRecipe = new Recipes
            {
                Id = recipeId,
                Header = "Old Pancakes",
                BodyText = "Old pancakes recipe",
                UserId = "123"
            };

            _mockUnitOfWork.Setup(u => u.Recipes.GetByIdAsync(recipeId)).ReturnsAsync(existingRecipe);

            // Act
            Func<Task> action = async () => await _recipeService.UpdateRecipe(recipeId, updateRecipeDto);

            // Assert
            await action.Should().ThrowAsync<ArgumentException>()
                        .WithMessage("A recipe must have at least one ingredient.");
        }

        [Fact]
        public async Task UpdateRecipe_ShouldThrowArgumentException_WhenInvalidIngredientIdsProvided()
        {
            // Arrange
            var recipeId = 1;
            var updateRecipeDto = new RecipeRequestDTO
            {
                Header = "Updated Pancakes",
                BodyText = "Updated delicious pancakes recipe",
                Ingredients = new List<RecipeIngredientRequestDTO>
                {
                    new RecipeIngredientRequestDTO { IngredientId = 99, Quantity = 1, Unit = "tbsp" } // Invalid ingredient ID
                }
            };

            var existingRecipe = new Recipes
            {
                Id = recipeId,
                Header = "Old Pancakes",
                BodyText = "Old pancakes recipe",
                UserId = "123"
            };

            _mockUnitOfWork.Setup(u => u.Recipes.GetByIdAsync(recipeId)).ReturnsAsync(existingRecipe);
            _mockUnitOfWork.Setup(u => u.Ingredients.FindAsync(It.IsAny<Func<Ingredients, bool>>()))
                           .ReturnsAsync(new List<Ingredients>()); // No matching ingredients found

            // Act
            Func<Task> action = async () => await _recipeService.UpdateRecipe(recipeId, updateRecipeDto);

            // Assert
            await action.Should().ThrowAsync<ArgumentException>()
                        .WithMessage("One or more ingredient IDs are invalid.");
        }



        [Fact]
        public async Task DeleteRecipe_ShouldRemoveRecipeAndIngredients()
        {
            // Arrange
            var recipeId = 1;
            var recipe = new Recipes { Id = recipeId, Header = "Pancakes", BodyText = "Delicious pancakes" };
            var ingredients = new List<Recipes_Ingredients>
            {
                new Recipes_Ingredients { RecipeId = recipeId, IngredientId = 1, Quantity = 2, Unit = "cups" }
            };

            _mockUnitOfWork.Setup(u => u.Recipes.GetByIdAsync(recipeId)).ReturnsAsync(recipe);
            _mockUnitOfWork.Setup(u => u.Recipes_Ingredients.FindAsync(It.IsAny<Func<Recipes_Ingredients, bool>>())).ReturnsAsync(ingredients);

            // Act
            var result = await _recipeService.DeleteRecipe(recipeId);

            // Assert
            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.Recipes.Delete(recipe), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task GetRecipeDetails_ShouldReturnNull_WhenRecipeDoesNotExist()
        {
            // Arrange
            var recipeId = 99;
            _mockUnitOfWork.Setup(u => u.Recipes.GetByIdAsync(recipeId)).ReturnsAsync((Recipes)null);

            // Act
            var result = await _recipeService.GetRecipeDetails(recipeId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateRecipe_ShouldReturnNull_WhenRecipeDoesNotExist()
        {
            // Arrange
            var recipeId = 99;
            var updateRecipeDto = new RecipeRequestDTO { /* ... */ };
            _mockUnitOfWork.Setup(u => u.Recipes.GetByIdAsync(recipeId)).ReturnsAsync((Recipes)null);

            // Act
            var result = await _recipeService.UpdateRecipe(recipeId, updateRecipeDto);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteRecipe_ShouldReturnFalse_WhenRecipeDoesNotExist()
        {
            // Arrange
            var recipeId = 99;
            _mockUnitOfWork.Setup(u => u.Recipes.GetByIdAsync(recipeId)).ReturnsAsync((Recipes)null);

            // Act
            var result = await _recipeService.DeleteRecipe(recipeId);

            // Assert
            result.Should().BeFalse();
        }


    }
}
