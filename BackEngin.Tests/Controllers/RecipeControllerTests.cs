using BackEngin.Controllers;
using BackEngin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Moq;
using FluentAssertions;

namespace BackEngin.Tests.Controllers
{
    public class RecipeControllerTests
    {
        private readonly Mock<IRecipeService> _mockRecipeService;
        private readonly RecipeController _recipeController;

        public RecipeControllerTests()
        {
            _mockRecipeService = new Mock<IRecipeService>();
            _recipeController = new RecipeController(_mockRecipeService.Object);
        }

        [Fact]
        public async Task GetRecipes_ShouldReturnOk_WithRecipes()
        {
            // Arrange
            var recipes = new List<RecipeDTO>
            {
                new RecipeDTO { Id = 1, Header = "Pancakes", BodyText = "Delicious pancakes recipe" },
                new RecipeDTO { Id = 2, Header = "Omelette", BodyText = "Fluffy omelette recipe" }
            };
            _mockRecipeService.Setup(s => s.GetRecipes()).ReturnsAsync(recipes);

            // Act
            var result = await _recipeController.GetRecipes();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(recipes);
        }

        [Fact]
        public async Task CreateRecipe_ShouldReturnCreated_WhenRecipeIsValid()
        {
            // Arrange
            var createRecipeDto = new CreateRecipeDTO
            {
                Header = "Pancakes",
                BodyText = "Delicious pancakes recipe",
                UserId = "123",
                Ingredients = new List<RecipeIngredientDTO>
                {
                    new RecipeIngredientDTO { IngredientId = 1, Quantity = 2, Unit = "cups" }
                }
            };
            var createdRecipe = new RecipeDTO
            {
                Id = 1,
                Header = createRecipeDto.Header,
                BodyText = createRecipeDto.BodyText,
                Ingredients = createRecipeDto.Ingredients
            };
            _mockRecipeService.Setup(s => s.CreateRecipe(createRecipeDto)).ReturnsAsync(createdRecipe);

            // Act
            var result = await _recipeController.CreateRecipe(createRecipeDto);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
            var createdResult = result as CreatedAtActionResult;
            createdResult.Value.Should().BeEquivalentTo(createdRecipe);
        }

        [Fact]
        public async Task GetRecipeDetails_ShouldReturnOk_WithRecipe()
        {
            // Arrange
            var recipeId = 1;
            var recipe = new RecipeDTO
            {
                Id = recipeId,
                Header = "Pancakes",
                BodyText = "Delicious pancakes recipe",
                Ingredients = new List<RecipeIngredientDTO>
                {
                    new RecipeIngredientDTO { IngredientId = 1, IngredientName = "Flour", Quantity = 2, Unit = "cups" }
                }
            };
            _mockRecipeService.Setup(s => s.GetRecipeDetails(recipeId)).ReturnsAsync(recipe);

            // Act
            var result = await _recipeController.GetRecipeDetails(recipeId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(recipe);
        }

        [Fact]
        public async Task GetRecipeDetails_ShouldReturnNotFound_WhenRecipeDoesNotExist()
        {
            // Arrange
            var recipeId = 99;
            _mockRecipeService.Setup(s => s.GetRecipeDetails(recipeId)).ReturnsAsync((RecipeDTO)null);

            // Act
            var result = await _recipeController.GetRecipeDetails(recipeId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UpdateRecipe_ShouldReturnOk_WithUpdatedRecipe()
        {
            // Arrange
            var recipeId = 1;
            var updateRecipeDto = new UpdateRecipeDTO
            {
                Header = "Updated Pancakes",
                BodyText = "Updated delicious pancakes recipe",
                Ingredients = new List<RecipeIngredientDTO>
                {
                    new RecipeIngredientDTO { IngredientId = 1, Quantity = 3, Unit = "cups" }
                }
            };
            var updatedRecipe = new RecipeDTO
            {
                Id = recipeId,
                Header = updateRecipeDto.Header,
                BodyText = updateRecipeDto.BodyText,
                Ingredients = updateRecipeDto.Ingredients
            };
            _mockRecipeService.Setup(s => s.UpdateRecipe(recipeId, updateRecipeDto)).ReturnsAsync(updatedRecipe);

            // Act
            var result = await _recipeController.UpdateRecipe(recipeId, updateRecipeDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(updatedRecipe);
        }

        [Fact]
        public async Task UpdateRecipe_ShouldReturnNotFound_WhenRecipeDoesNotExist()
        {
            // Arrange
            var recipeId = 99;
            var updateRecipeDto = new UpdateRecipeDTO
            {
                Header = "Updated Pancakes",
                BodyText = "Updated delicious pancakes recipe"
            };
            _mockRecipeService.Setup(s => s.UpdateRecipe(recipeId, updateRecipeDto)).ReturnsAsync((RecipeDTO)null);

            // Act
            var result = await _recipeController.UpdateRecipe(recipeId, updateRecipeDto);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteRecipe_ShouldReturnNoContent_WhenSuccessful()
        {
            // Arrange
            var recipeId = 1;
            _mockRecipeService.Setup(s => s.DeleteRecipe(recipeId)).ReturnsAsync(true);

            // Act
            var result = await _recipeController.DeleteRecipe(recipeId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteRecipe_ShouldReturnNotFound_WhenRecipeDoesNotExist()
        {
            // Arrange
            var recipeId = 99;
            _mockRecipeService.Setup(s => s.DeleteRecipe(recipeId)).ReturnsAsync(false);

            // Act
            var result = await _recipeController.DeleteRecipe(recipeId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
