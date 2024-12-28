using BackEngin.Controllers;
using BackEngin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Moq;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BackEngin.Tests.Controllers
{
    public class IngredientsControllerTests
    {
        private readonly Mock<IIngredientsService> _mockIngredientsService;
        private readonly Mock<ClaimsPrincipal> _mockUser;
        private readonly IngredientsController _ingredientsController;

        public IngredientsControllerTests()
        {
            _mockIngredientsService = new Mock<IIngredientsService>();

            // Mock user context as Admin
            _mockUser = new Mock<ClaimsPrincipal>();
            _mockUser.Setup(u => u.IsInRole("Admin")).Returns(true);
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role))
                     .Returns(new Claim(ClaimTypes.Role, "Admin"));

            // Setup controller with mock user
            _ingredientsController = new IngredientsController(_mockIngredientsService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = _mockUser.Object }
                }
            };
        }

        [Fact]
        public async Task GetPaginatedIngredients_ShouldReturnOk_WithPaginatedIngredients()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 2;
            var paginatedResponse = new PaginatedResponseDTO<IngredientIdDTO>
            {
                Items = new List<IngredientIdDTO>
                {
                    new IngredientIdDTO
                    {
                        Id = 1,
                        Name = "Flour",
                        Type = new IngredientTypeIdDTO { Id = 1, Name = "Grain" },
                        Allergens = new List<AllergenIdDTO>()
                    },
                    new IngredientIdDTO
                    {
                        Id = 2,
                        Name = "Milk",
                        Type = new IngredientTypeIdDTO { Id = 2, Name = "Dairy" },
                        Allergens = new List<AllergenIdDTO>
                        {
                            new AllergenIdDTO { Id = 1, Name = "Lactose" }
                        }
                    }
                },
                TotalCount = 5,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            _mockIngredientsService.Setup(s => s.GetIngredientsPaginatedAsync(pageNumber, pageSize))
                                   .ReturnsAsync(paginatedResponse);

            // Act
            var result = await _ingredientsController.GetPaginatedIngredients(pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(paginatedResponse);
        }

        [Fact]
        public async Task GetIngredientById_ShouldReturnOk_WithIngredient()
        {
            // Arrange
            var ingredientId = 1;
            var ingredient = new IngredientIdDTO
            {
                Id = ingredientId,
                Name = "Flour",
                Type = new IngredientTypeIdDTO { Id = 1, Name = "Grain" },
                Allergens = new List<AllergenIdDTO>()
            };

            _mockIngredientsService.Setup(s => s.GetIngredientByIdAsync(ingredientId))
                                   .ReturnsAsync(ingredient);

            // Act
            var result = await _ingredientsController.GetIngredientById(ingredientId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(ingredient);
        }

        [Fact]
        public async Task GetIngredientById_ShouldReturnNotFound_WhenIngredientDoesNotExist()
        {
            // Arrange
            var ingredientId = 99;
            _mockIngredientsService.Setup(s => s.GetIngredientByIdAsync(ingredientId))
                                   .ReturnsAsync((IngredientIdDTO)null);

            // Act
            var result = await _ingredientsController.GetIngredientById(ingredientId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;

            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
            notFoundResult.Value.Should().BeEquivalentTo(new { message = "Ingredient not found." });
        }

        [Fact]
        public async Task CreateIngredient_ShouldReturnOk_WhenIngredientIsValid()
        {
            // Arrange
            var ingredientDto = new IngredientDTO
            {
                Name = "Sugar",
                TypeId = 1,
                AllergenIds = new List<int> { 2 }
            };
            var createdIngredientId = 3;

            _mockIngredientsService.Setup(s => s.CreateIngredientAsync(ingredientDto))
                                   .ReturnsAsync(createdIngredientId);

            // Act
            var result = await _ingredientsController.CreateIngredient(ingredientDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { Id = createdIngredientId, message = "Ingredient created successfully!" });
        }

        [Fact]
        public async Task CreateIngredient_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var ingredientDto = new IngredientDTO(); // Missing required fields
            _ingredientsController.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await _ingredientsController.CreateIngredient(ingredientDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreateIngredient_ShouldReturnBadRequest_WhenCreationFails()
        {
            // Arrange
            var ingredientDto = new IngredientDTO
            {
                Name = "Sugar",
                TypeId = 1,
                AllergenIds = new List<int> { 2 }
            };

            _mockIngredientsService.Setup(s => s.CreateIngredientAsync(ingredientDto))
                                   .ReturnsAsync((int?)null);

            // Act
            var result = await _ingredientsController.CreateIngredient(ingredientDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
            badRequestResult.Value.Should().BeEquivalentTo(new
            {
                message = "Ingredient cannot be created."
            });
        }

        [Fact]
        public async Task UpdateIngredient_ShouldReturnOk_WhenIngredientIsValid()
        {
            // Arrange
            var ingredientId = 1;
            var ingredientDto = new IngredientDTO
            {
                Name = "Brown Sugar",
                TypeId = 1,
                AllergenIds = new List<int>()
            };

            _mockIngredientsService.Setup(s => s.UpdateIngredientAsync(ingredientId, ingredientDto))
                                   .ReturnsAsync(true);

            // Act
            var result = await _ingredientsController.UpdateIngredient(ingredientId, ingredientDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { message = "Ingredient updated successfully!" });
        }

        [Fact]
        public async Task UpdateIngredient_ShouldReturnNotFound_WhenIngredientDoesNotExist()
        {
            // Arrange
            var ingredientId = 99;
            var ingredientDto = new IngredientDTO
            {
                Name = "Brown Sugar",
                TypeId = 1,
                AllergenIds = new List<int>()
            };

            _mockIngredientsService.Setup(s => s.UpdateIngredientAsync(ingredientId, ingredientDto))
                                   .ReturnsAsync((bool?)null);

            // Act
            var result = await _ingredientsController.UpdateIngredient(ingredientId, ingredientDto);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;

            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
            notFoundResult.Value.Should().BeEquivalentTo(new
            {
                message = "Ingredient does not exist."
            });
        }

        [Fact]
        public async Task UpdateIngredient_ShouldReturnBadRequest_WhenUpdateFails()
        {
            // Arrange
            var ingredientId = 1;
            var ingredientDto = new IngredientDTO
            {
                Name = "Brown Sugar",
                TypeId = 1,
                AllergenIds = new List<int>()
            };

            _mockIngredientsService.Setup(s => s.UpdateIngredientAsync(ingredientId, ingredientDto))
                                   .ReturnsAsync(false);

            // Act
            var result = await _ingredientsController.UpdateIngredient(ingredientId, ingredientDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
            badRequestResult.Value.Should().BeEquivalentTo(new
            {
                message = "Ingredient cannot be updated."
            });

        }

        [Fact]
        public async Task DeleteIngredient_ShouldReturnOk_WhenIngredientIsDeleted()
        {
            // Arrange
            var ingredientId = 1;
            _mockIngredientsService.Setup(s => s.DeleteIngredientAsync(ingredientId))
                                   .ReturnsAsync(true);

            // Act
            var result = await _ingredientsController.DeleteIngredient(ingredientId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { message = "Ingredient deleted successfully!" });
        }

        [Fact]
        public async Task DeleteIngredient_ShouldReturnBadRequest_WhenIngredientDoesNotExist()
        {
            // Arrange
            var ingredientId = 99;
            _mockIngredientsService.Setup(s => s.DeleteIngredientAsync(ingredientId))
                                   .ReturnsAsync((bool?)null);

            // Act
            var result = await _ingredientsController.DeleteIngredient(ingredientId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
            badRequestResult.Value.Should().BeEquivalentTo(new
            {
                message = "Ingredient does not exist."
            });
        }

        [Fact]
        public async Task DeleteIngredient_ShouldReturnBadRequest_WhenDeletionFails()
        {
            // Arrange
            var ingredientId = 1;
            _mockIngredientsService.Setup(s => s.DeleteIngredientAsync(ingredientId))
                                   .ReturnsAsync(false);

            // Act
            var result = await _ingredientsController.DeleteIngredient(ingredientId);

            // Assert
            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
            badRequestResult.Value.Should().BeEquivalentTo(new
            {
                message = "Ingredient cannot be deleted."
            });

        }

        [Fact]
        public async Task SearchIngredients_ShouldReturnOk_WithFilteredIngredients()
        {
            // Arrange
            var searchParams = new IngredientSearchParams
            {
                NameContains = "Flour",
                IngredientTypeIds = new List<int> { 1, 2 },
                AllergenIds = new List<int> { 3 },
                SortBy = "Name",
                SortOrder = "asc"
            };

            var paginatedIngredients = new PaginatedResponseDTO<IngredientIdDTO>
            {
                Items = new List<IngredientIdDTO>
                    {
                        new IngredientIdDTO
                        {
                            Id = 1,
                            Name = "Flour",
                            Type = new IngredientTypeIdDTO { Id = 1, Name = "Grain" },
                            Allergens = new List<AllergenIdDTO>()
                        }
                    },
                TotalCount = 1,
                PageNumber = 1,
                PageSize = 10
            };

            _mockIngredientsService.Setup(s => s.SearchIngredients(searchParams, 1, 10))
                .ReturnsAsync(paginatedIngredients);

            // Act
            var result = await _ingredientsController.SearchIngredients(
                searchParams,
                pageNumber: 1,
                pageSize: 10
            ) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(paginatedIngredients);
        }

        [Fact]
        public async Task GetBatchImage_ShouldReturnOk_WithIngredientImages()
        {
            // Arrange
            var ingredientIds = new List<int> { 1, 2, 3 };
            var ingredientImages = new List<IngredientImageDTO>
            {
                new IngredientImageDTO { Id = 1, Image = new byte[] { 1, 2, 3 } },
                new IngredientImageDTO { Id = 2, Image = new byte[] { 4, 5, 6 } },
                new IngredientImageDTO { Id = 3, Image = null }
            };

            _mockIngredientsService.Setup(s => s.GetBatchImage(ingredientIds))
                                   .ReturnsAsync(ingredientImages);

            // Act
            var result = await _ingredientsController.GetBatchImage(ingredientIds);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(ingredientImages);
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task GetBatchImage_ShouldReturnBadRequest_WhenIngredientIdsAreEmpty()
        {
            // Arrange
            var ingredientIds = new List<int>();

            // Act
            var result = await _ingredientsController.GetBatchImage(ingredientIds);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult!.Value.Should().BeEquivalentTo(new { message = "Ingredient IDs must be provided." });
            badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task GetBatchImage_ShouldReturn500_WhenExceptionOccurs()
        {
            // Arrange
            var ingredientIds = new List<int> { 1, 2, 3 };

            _mockIngredientsService.Setup(s => s.GetBatchImage(ingredientIds))
                                   .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _ingredientsController.GetBatchImage(ingredientIds);

            // Assert
            var statusCodeResult = result as ObjectResult;
            statusCodeResult.Should().NotBeNull();
            statusCodeResult!.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            statusCodeResult.Value.Should().BeEquivalentTo(new { message = "An unexpected error occurred.", details = "Database error" });
        }


    }
}
