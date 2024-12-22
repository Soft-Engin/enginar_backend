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

            // Mock user context
            _mockUser = new Mock<ClaimsPrincipal>();
            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier))
                     .Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role))
                     .Returns(new Claim(ClaimTypes.Role, "User"));

            // Setup controller with mock user
            _recipeController = new RecipeController(_mockRecipeService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = _mockUser.Object }
                }
            };
        }


        [Fact]
        public async Task GetRecipes_ShouldReturnOk_WithPaginatedRecipes()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 2;
            var paginatedResponse = new PaginatedResponseDTO<RecipeDTO>
            {
                Items = new List<RecipeDTO>
                {
                    new RecipeDTO { Id = 1, Header = "Pancakes", BodyText = "Delicious pancakes recipe" },
                    new RecipeDTO { Id = 2, Header = "Omelette", BodyText = "Fluffy omelette recipe" }
                },
                TotalCount = 5,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            _mockRecipeService.Setup(s => s.GetRecipes(pageNumber, pageSize)).ReturnsAsync(paginatedResponse);

            // Act
            var result = await _recipeController.GetRecipes(pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(paginatedResponse);
        }


        [Fact]
        public async Task CreateRecipe_ShouldReturnCreated_WhenRecipeIsValid()
        {
            // Arrange
            var createRecipeDto = new RecipeRequestDTO
            {
                Header = "Pancakes",
                BodyText = "Delicious pancakes recipe",
                Ingredients = new List<RecipeIngredientRequestDTO>
                                {
                                    new RecipeIngredientRequestDTO { IngredientId = 1, Quantity = 2, Unit = "cups" }
                                },
                ServingSize = 1,
                PreparationTime = 1,
                Steps = new[] { "Step 1", "Step 2" }
            };

            var createdRecipe = new RecipeDetailsDTO
            {
                Id = 1,
                Header = createRecipeDto.Header,
                BodyText = createRecipeDto.BodyText,
                Ingredients = new List<RecipeIngredientDetailsDTO>
                                {
                                    new RecipeIngredientDetailsDTO
                                    {
                                        IngredientId = 1,
                                        Quantity = 2,
                                        Unit = "cups",
                                        IngredientName = "Flour" // Mocked response from service
                                    }
                                },
                ServingSize = 1,
                PreparationTime = 1,
                Steps = new[] { "Step 1", "Step 2" }
            };

            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier))
                     .Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });

            _mockRecipeService.Setup(s => s.CreateRecipe("currentUserId", It.IsAny<CreateRecipeDTO>()))
                              .ReturnsAsync(createdRecipe);

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
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;

            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
            notFoundResult.Value.Should().BeEquivalentTo(new
            {
                message = "Recipe not found."
            });
        }

        [Fact]
        public async Task UpdateRecipe_ShouldReturnBadRequest_WhenInvalidIngredientsProvided()
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

            _mockRecipeService.Setup(s => s.GetOwner(recipeId)).ReturnsAsync("currentUserId");
            _mockRecipeService.Setup(s => s.UpdateRecipe(recipeId, updateRecipeDto))
                              .ThrowsAsync(new ArgumentException("One or more ingredient IDs are invalid."));
            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier))
                     .Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role))
                     .Returns(new Claim(ClaimTypes.Role, "User"));

            // Act
            var result = await _recipeController.UpdateRecipe(recipeId, updateRecipeDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400); // StatusCode contains the HTTP status code
            badRequestResult.Value.Should().BeEquivalentTo(new
            {
                message = "One or more ingredient IDs are invalid." // Message is part of the response Value
            });
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
                Ingredients = new List<RecipeIngredientRequestDTO>
                {
                    new RecipeIngredientRequestDTO { IngredientId = 1, Quantity = 3, Unit = "cups" }
                }
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

            // Mock the Recipe Owner
            _mockRecipeService.Setup(s => s.GetOwner(recipeId)).ReturnsAsync("currentUserId");

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
            var recipeId = 99; // Non-existent recipe ID
            var updateRecipeDto = new RecipeRequestDTO
            {
                Header = "Updated Pancakes",
                BodyText = "Updated delicious pancakes recipe"
            };

            // Simulate `GetOwner` returning null (recipe not found)
            _mockRecipeService.Setup(s => s.GetOwner(recipeId)).ReturnsAsync((string)null);

            // Ensure `UpdateRecipe` is not called since `GetOwner` returns null
            _mockRecipeService.Setup(s => s.UpdateRecipe(recipeId, updateRecipeDto)).ReturnsAsync((RecipeDetailsDTO)null);

            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier))
                     .Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role))
                     .Returns(new Claim(ClaimTypes.Role, "User"));

            // Act
            var result = await _recipeController.UpdateRecipe(recipeId, updateRecipeDto);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;

            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
            notFoundResult.Value.Should().BeEquivalentTo(new
            {
                message = "Recipe does not exist."
            });
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
                Ingredients = new List<RecipeIngredientRequestDTO>
                {
                    new RecipeIngredientRequestDTO { IngredientId = 1, Quantity = 3, Unit = "cups" }
                }
            };

            _mockRecipeService.Setup(s => s.GetOwner(recipeId)).ReturnsAsync("otherUserId");
            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier))
                     .Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role))
                     .Returns(new Claim(ClaimTypes.Role, "User"));

            // Act
            var result = await _recipeController.UpdateRecipe(recipeId, updateRecipeDto);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();
            var unauthorizedResult = result as UnauthorizedObjectResult;

            unauthorizedResult.Should().NotBeNull();
            unauthorizedResult.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
            unauthorizedResult.Value.Should().BeEquivalentTo(new
            {
                message = "Unauthorized to access this recipe."
            });
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
                Ingredients = new List<RecipeIngredientRequestDTO>
                {
                    new RecipeIngredientRequestDTO { IngredientId = 1, Quantity = 3, Unit = "cups" }
                }
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

            // Mocking the recipe owner to be a different user
            _mockRecipeService.Setup(s => s.GetOwner(recipeId)).ReturnsAsync("otherUserId");

            // Mocking the current user claims (non-owner and non-admin)
            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier))
                     .Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role))
                     .Returns(new Claim(ClaimTypes.Role, "User"));

            // Act
            var result = await _recipeController.DeleteRecipe(recipeId);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();
            var unauthorizedResult = result as UnauthorizedObjectResult;

            unauthorizedResult.Should().NotBeNull();
            unauthorizedResult.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
            unauthorizedResult.Value.Should().BeEquivalentTo(new
            {
                message = "You are not authorized to delete this recipe."
            });
        }

        [Fact]
        public async Task DeleteRecipe_ShouldReturnOk_WhenUserIsOwner()
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
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task DeleteRecipe_ShouldReturnNotFound_WhenRecipeDoesNotExist()
        {
            // Arrange
            var recipeId = 99;

            _mockRecipeService.Setup(s => s.GetOwner(recipeId)).ReturnsAsync("currentUserId");
            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier))
                     .Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role))
                     .Returns(new Claim(ClaimTypes.Role, "User"));
            _mockRecipeService.Setup(s => s.DeleteRecipe(recipeId)).ReturnsAsync(false);

            // Act
            var result = await _recipeController.DeleteRecipe(recipeId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;

            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
            notFoundResult.Value.Should().BeEquivalentTo(new
            {
                message = "Recipe deletion failed."
            });
        }

        [Fact]
        public async Task CreateRecipe_ShouldReturnBadRequest_WhenCreateRecipeDtoIsNull()
        {
            // Act
            var result = await _recipeController.CreateRecipe(null);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
            badRequestResult.Value.Should().BeEquivalentTo(new
            {
                message = "Invalid recipe data."
            });
        }

        [Fact]
        public async Task CreateRecipe_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var createRecipeDto = new RecipeRequestDTO(); // Missing required fields
            _recipeController.ModelState.AddModelError("Header", "Required");

            // Act
            var result = await _recipeController.CreateRecipe(createRecipeDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task SearchRecipes_ShouldReturnOk_WithFilteredRecipes()
        {
            // Arrange
            var searchParams = new RecipeSearchParams
            {
                HeaderContains = "Pancake",
                BodyContains = "sweet",
                UserName = "user1",
                IngredientIds = new List<int> { 1 },
                AllergenIds = new List<int> { 2 },
                SortBy = "Header",
                SortOrder = "asc"
            };

            var paginatedRecipes = new PaginatedResponseDTO<RecipeDTO>
            {
                Items = new List<RecipeDTO>
                    {
                        new RecipeDTO { Id = 1, Header = "Pancake Recipe", BodyText = "A sweet pancake recipe" }
                    },
                TotalCount = 1,
                PageNumber = 1,
                PageSize = 10
            };

            _mockRecipeService.Setup(s => s.SearchRecipes(searchParams, 1, 10))
                .ReturnsAsync(paginatedRecipes);

            // Act
            var result = await _recipeController.SearchRecipes(
                searchParams,
                pageNumber: 1,
                pageSize: 10
            ) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(paginatedRecipes);
        }

        [Fact]
        public async Task GetRecipeBanner_ShouldReturnFileResult_WhenImageExists()
        {
            // Arrange
            var recipeId = 1;
            var imageData = new byte[] { 1, 2, 3, 4, 5 };

            _mockRecipeService.Setup(s => s.GetRecipeBannerImage(recipeId))
                .ReturnsAsync(imageData);

            // Act
            var result = await _recipeController.GetRecipeBanner(recipeId);

            // Assert
            var fileResult = result.Should().BeOfType<FileContentResult>().Which;
            fileResult.FileContents.Should().BeEquivalentTo(imageData);
            fileResult.ContentType.Should().Be("image/jpeg");
        }

        [Fact]
        public async Task GetRecipeBanner_ShouldReturnNotFound_WhenImageDoesNotExist()
        {
            // Arrange
            var recipeId = 1;
            _mockRecipeService.Setup(s => s.GetRecipeBannerImage(recipeId))
                .ReturnsAsync((byte[])null);

            // Act
            var result = await _recipeController.GetRecipeBanner(recipeId);

            // Assert
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Which;
            notFoundResult.Value.Should().BeEquivalentTo(new { message = "No banner image found for this recipe." });
        }

        [Fact]
        public async Task GetRecipeStepImage_ShouldReturnFileResult_WhenImageExists()
        {
            // Arrange
            var recipeId = 1;
            var stepIndex = 0;
            var imageData = new byte[] { 1, 2, 3, 4, 5 };

            _mockRecipeService.Setup(s => s.GetRecipeStepImage(recipeId, stepIndex))
                .ReturnsAsync(imageData);

            // Act
            var result = await _recipeController.GetRecipeStepImage(recipeId, stepIndex);

            // Assert
            var fileResult = result.Should().BeOfType<FileContentResult>().Which;
            fileResult.FileContents.Should().BeEquivalentTo(imageData);
            fileResult.ContentType.Should().Be("image/jpeg");
        }

        [Fact]
        public async Task GetRecipeStepImage_ShouldReturnNotFound_WhenImageDoesNotExist()
        {
            // Arrange
            var recipeId = 1;
            var stepIndex = 0;
            _mockRecipeService.Setup(s => s.GetRecipeStepImage(recipeId, stepIndex))
                .ReturnsAsync((byte[])null);

            // Act
            var result = await _recipeController.GetRecipeStepImage(recipeId, stepIndex);

            // Assert
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Which;
            notFoundResult.Value.Should().BeEquivalentTo(new { message = "No image found for step 0 of recipe 1." });
        }
    }
}
