using BackEngin.Controllers;
using BackEngin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Moq;
using FluentAssertions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BackEngin.Tests.Controllers
{
    public class RecipeControllerTests
    {
        private readonly Mock<IRecipeService> _mockRecipeService;
        private readonly Mock<ClaimsPrincipal> _mockUser;
        private readonly RecipeController _recipeController;

        public RecipeControllerTests()
        {
            _mockRecipeService = new Mock<IRecipeService>();
            _mockUser = new Mock<ClaimsPrincipal>();

            _recipeController = new RecipeController(_mockRecipeService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = _mockUser.Object
                    }
                }
            };
        }

        [Fact]
        public async Task GetRecipes_ShouldReturnOk_WithRecipes()
        {
            // Arrange
            var recipes = new List<RecipeDetailsDTO>
            {
                new RecipeDetailsDTO { Id = 1, Header = "Pancakes", BodyText = "Delicious pancakes recipe" },
                new RecipeDetailsDTO { Id = 2, Header = "Omelette", BodyText = "Fluffy omelette recipe" }
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
            var createRecipeDto = new RecipeRequestDTO
            {
                Header = "Pancakes",
                BodyText = "Delicious pancakes recipe",
                Ingredients = new List<RecipeIngredientDetailsDTO>
                {
                    new RecipeIngredientDetailsDTO { IngredientId = 1, Quantity = 2, Unit = "cups" }
                }
            };
            var createdRecipe = new RecipeDetailsDTO
            {
                Id = 1,
                Header = createRecipeDto.Header,
                BodyText = createRecipeDto.BodyText,
                Ingredients = createRecipeDto.Ingredients
            };

            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier))
                     .Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });

            _mockRecipeService.Setup(s => s.CreateRecipe(It.Is<CreateRecipeDTO>(
                dto => dto.UserId == "currentUserId" && dto.Header == "Pancakes"))).ReturnsAsync(createdRecipe);

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
            var recipe = new RecipeDetailsDTO
            {
                Id = recipeId,
                Header = "Pancakes",
                BodyText = "Delicious pancakes recipe",
                Ingredients = new List<RecipeIngredientDetailsDTO>
                {
                    new RecipeIngredientDetailsDTO { IngredientId = 1, IngredientName = "Flour", Quantity = 2, Unit = "cups" }
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
            _mockRecipeService.Setup(s => s.GetRecipeDetails(recipeId)).ReturnsAsync((RecipeDetailsDTO)null);

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
            var updateRecipeDto = new RecipeRequestDTO
            {
                Header = "Updated Pancakes",
                BodyText = "Updated delicious pancakes recipe",
                Ingredients = new List<RecipeIngredientDetailsDTO>
                {
                    new RecipeIngredientDetailsDTO { IngredientId = 1, Quantity = 3, Unit = "cups" }
                }
            };

            var updatedRecipe = new RecipeDetailsDTO
            {
                Id = recipeId,
                Header = updateRecipeDto.Header,
                BodyText = updateRecipeDto.BodyText,
                Ingredients = updateRecipeDto.Ingredients
            };

            // Mock the Recipe Owner
            _mockRecipeService.Setup(s => s.GetOwner(recipeId)).ReturnsAsync("currentUserId");

            // Mock ClaimsPrincipal to simulate authenticated user
            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier))
                     .Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role))
                     .Returns(new Claim(ClaimTypes.Role, "User"));

            // Mock the UpdateRecipe method
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
            var updateRecipeDto = new RecipeRequestDTO
            {
                Header = "Updated Pancakes",
                BodyText = "Updated delicious pancakes recipe"
            };

            _mockRecipeService.Setup(s => s.GetOwner(recipeId)).ReturnsAsync("currentUserId"); // Simulate owner retrieval
            _mockRecipeService.Setup(s => s.UpdateRecipe(recipeId, updateRecipeDto)).ReturnsAsync((RecipeDetailsDTO)null);

            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier))
                     .Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role))
                     .Returns(new Claim(ClaimTypes.Role, "User"));

            // Act
            var result = await _recipeController.UpdateRecipe(recipeId, updateRecipeDto);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }


        [Fact]
        public async Task UpdateRecipe_ShouldReturnUnauthorized_WhenUserIsNotOwnerOrAdmin()
        {
            // Arrange
            var recipeId = 1;
            var updateRecipeDto = new RecipeRequestDTO
            {
                Header = "Updated Pancakes",
                BodyText = "Updated delicious pancakes recipe",
                Ingredients = new List<RecipeIngredientDetailsDTO>
                {
                    new RecipeIngredientDetailsDTO { IngredientId = 1, Quantity = 3, Unit = "cups" }
                }
            };

            _mockRecipeService.Setup(s => s.GetOwner(recipeId)).ReturnsAsync("otherUserId");
            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier)).Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role)).Returns(new Claim(ClaimTypes.Role, "User"));

            // Act
            var result = await _recipeController.UpdateRecipe(recipeId, updateRecipeDto);

            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task UpdateRecipe_ShouldReturnOk_WhenUserIsOwner()
        {
            // Arrange
            var recipeId = 1;
            var updateRecipeDto = new RecipeRequestDTO
            {
                Header = "Updated Pancakes",
                BodyText = "Updated delicious pancakes recipe",
                Ingredients = new List<RecipeIngredientDetailsDTO>
                {
                    new RecipeIngredientDetailsDTO { IngredientId = 1, Quantity = 3, Unit = "cups" }
                }
            };
            var updatedRecipe = new RecipeDetailsDTO
            {
                Id = recipeId,
                Header = updateRecipeDto.Header,
                BodyText = updateRecipeDto.BodyText,
                Ingredients = updateRecipeDto.Ingredients
            };

            _mockRecipeService.Setup(s => s.GetOwner(recipeId)).ReturnsAsync("currentUserId");
            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier)).Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role)).Returns(new Claim(ClaimTypes.Role, "User"));
            _mockRecipeService.Setup(s => s.UpdateRecipe(recipeId, updateRecipeDto)).ReturnsAsync(updatedRecipe);

            // Act
            var result = await _recipeController.UpdateRecipe(recipeId, updateRecipeDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(updatedRecipe);
        }

        [Fact]
        public async Task DeleteRecipe_ShouldReturnUnauthorized_WhenUserIsNotOwnerOrAdmin()
        {
            // Arrange
            var recipeId = 1;

            _mockRecipeService.Setup(s => s.GetOwner(recipeId)).ReturnsAsync("otherUserId");
            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier)).Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role)).Returns(new Claim(ClaimTypes.Role, "User"));

            // Act
            var result = await _recipeController.DeleteRecipe(recipeId);

            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task DeleteRecipe_ShouldReturnNoContent_WhenUserIsOwner()
        {
            // Arrange
            var recipeId = 1;

            _mockRecipeService.Setup(s => s.GetOwner(recipeId)).ReturnsAsync("currentUserId");
            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier)).Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role)).Returns(new Claim(ClaimTypes.Role, "User"));
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

            _mockRecipeService.Setup(s => s.GetOwner(recipeId)).ReturnsAsync("currentUserId");
            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier)).Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role)).Returns(new Claim(ClaimTypes.Role, "User"));
            _mockRecipeService.Setup(s => s.DeleteRecipe(recipeId)).ReturnsAsync(false);

            // Act
            var result = await _recipeController.DeleteRecipe(recipeId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
