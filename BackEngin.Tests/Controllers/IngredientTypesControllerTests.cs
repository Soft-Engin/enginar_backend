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
    public class IngredientTypesControllerTests
    {
        private readonly Mock<IIngredientTypesService> _mockIngredientTypesService;
        private readonly Mock<ClaimsPrincipal> _mockUser;
        private readonly IngredientTypesController _ingredientTypesController;

        public IngredientTypesControllerTests()
        {
            _mockIngredientTypesService = new Mock<IIngredientTypesService>();

            // Mock user context as Admin
            _mockUser = new Mock<ClaimsPrincipal>();
            _mockUser.Setup(u => u.IsInRole("Admin")).Returns(true);
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role))
                     .Returns(new Claim(ClaimTypes.Role, "Admin"));

            // Setup controller with mock user
            _ingredientTypesController = new IngredientTypesController(_mockIngredientTypesService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = _mockUser.Object }
                }
            };
        }

        [Fact]
        public async Task GetPaginatedIngredientTypes_ShouldReturnOk_WithPaginatedIngredientTypes()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 2;
            var paginatedResponse = new PaginatedResponseDTO<IngredientTypeIdDTO>
            {
                Items = new List<IngredientTypeIdDTO>
                {
                    new IngredientTypeIdDTO { Id = 1, Name = "Grain", Description = "Grains description" },
                    new IngredientTypeIdDTO { Id = 2, Name = "Dairy", Description = "Dairy description" }
                },
                TotalCount = 5,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            _mockIngredientTypesService.Setup(s => s.GetIngredientTypesPaginatedAsync(pageNumber, pageSize))
                                       .ReturnsAsync(paginatedResponse);

            // Act
            var result = await _ingredientTypesController.GetPaginatedIngredientTypes(pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(paginatedResponse);
        }

        [Fact]
        public async Task GetIngredientTypeById_ShouldReturnOk_WithIngredientType()
        {
            // Arrange
            var ingredientTypeId = 1;
            var ingredientType = new IngredientTypeIdDTO
            {
                Id = ingredientTypeId,
                Name = "Grain",
                Description = "Grains description"
            };

            _mockIngredientTypesService.Setup(s => s.GetIngredientTypeByIdAsync(ingredientTypeId))
                                       .ReturnsAsync(ingredientType);

            // Act
            var result = await _ingredientTypesController.GetIngredientTypeById(ingredientTypeId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(ingredientType);
        }

        [Fact]
        public async Task GetIngredientTypeById_ShouldReturnNotFound_WhenIngredientTypeDoesNotExist()
        {
            // Arrange
            var ingredientTypeId = 99;
            _mockIngredientTypesService.Setup(s => s.GetIngredientTypeByIdAsync(ingredientTypeId))
                                       .ReturnsAsync((IngredientTypeIdDTO)null);

            // Act
            var result = await _ingredientTypesController.GetIngredientTypeById(ingredientTypeId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;

            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
            notFoundResult.Value.Should().BeEquivalentTo(new
            {
                message = "Ingredient type not found."
            });

        }

        [Fact]
        public async Task CreateIngredientType_ShouldReturnOk_WhenIngredientTypeIsValid()
        {
            // Arrange
            var ingredientTypeDto = new IngredientTypeDTO
            {
                Name = "Spice",
                Description = "Spices description"
            };
            var createdIngredientTypeId = 3;

            _mockIngredientTypesService.Setup(s => s.CreateIngredientTypeAsync(ingredientTypeDto))
                                       .ReturnsAsync(createdIngredientTypeId);

            // Act
            var result = await _ingredientTypesController.CreateIngredientType(ingredientTypeDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { Id = createdIngredientTypeId, message = "Ingredient type created successfully!" });
        }

        [Fact]
        public async Task CreateIngredientType_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var ingredientTypeDto = new IngredientTypeDTO(); // Missing required fields
            _ingredientTypesController.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await _ingredientTypesController.CreateIngredientType(ingredientTypeDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreateIngredientType_ShouldReturnBadRequest_WhenCreationFails()
        {
            // Arrange
            var ingredientTypeDto = new IngredientTypeDTO
            {
                Name = "Spice",
                Description = "Spices description"
            };

            _mockIngredientTypesService.Setup(s => s.CreateIngredientTypeAsync(ingredientTypeDto))
                                       .ReturnsAsync((int?)null);

            // Act
            var result = await _ingredientTypesController.CreateIngredientType(ingredientTypeDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
            badRequestResult.Value.Should().BeEquivalentTo(new
            {
                message = "Ingredient type cannot be created."
            });
        }

        [Fact]
        public async Task UpdateIngredientType_ShouldReturnOk_WhenIngredientTypeIsValid()
        {
            // Arrange
            var ingredientTypeId = 1;
            var ingredientTypeDto = new IngredientTypeDTO
            {
                Name = "Herb",
                Description = "Herbs description"
            };

            _mockIngredientTypesService.Setup(s => s.UpdateIngredientTypeAsync(ingredientTypeId, ingredientTypeDto))
                                       .ReturnsAsync(true);

            // Act
            var result = await _ingredientTypesController.UpdateIngredientType(ingredientTypeId, ingredientTypeDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { message = "Ingredient type updated successfully!" });
        }

        [Fact]
        public async Task UpdateIngredientType_ShouldReturnBadRequest_WhenIngredientTypeDoesNotExist()
        {
            // Arrange
            var ingredientTypeId = 99;
            var ingredientTypeDto = new IngredientTypeDTO
            {
                Name = "Herb",
                Description = "Herbs description"
            };

            _mockIngredientTypesService.Setup(s => s.UpdateIngredientTypeAsync(ingredientTypeId, ingredientTypeDto))
                                       .ReturnsAsync((bool?)null);

            // Act
            var result = await _ingredientTypesController.UpdateIngredientType(ingredientTypeId, ingredientTypeDto);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;

            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
            notFoundResult.Value.Should().BeEquivalentTo(new
            {
                message = "Ingredient type does not exist."
            });
        }

        [Fact]
        public async Task UpdateIngredientType_ShouldReturnBadRequest_WhenUpdateFails()
        {
            // Arrange
            var ingredientTypeId = 1;
            var ingredientTypeDto = new IngredientTypeDTO
            {
                Name = "Herb",
                Description = "Herbs description"
            };

            _mockIngredientTypesService.Setup(s => s.UpdateIngredientTypeAsync(ingredientTypeId, ingredientTypeDto))
                                       .ReturnsAsync(false);

            // Act
            var result = await _ingredientTypesController.UpdateIngredientType(ingredientTypeId, ingredientTypeDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
            badRequestResult.Value.Should().BeEquivalentTo(new
            {
                message = "Ingredient type cannot be updated."
            });

        }

        [Fact]
        public async Task DeleteIngredientType_ShouldReturnOk_WhenIngredientTypeIsDeleted()
        {
            // Arrange
            var ingredientTypeId = 1;
            _mockIngredientTypesService.Setup(s => s.DeleteIngredientTypeAsync(ingredientTypeId))
                                       .ReturnsAsync(true);

            // Act
            var result = await _ingredientTypesController.DeleteIngredientType(ingredientTypeId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { message = "Ingredient type deleted successfully!" });
        }

        [Fact]
        public async Task DeleteIngredientType_ShouldReturnNotFound_WhenIngredientTypeDoesNotExist()
        {
            // Arrange
            var ingredientTypeId = 99;
            _mockIngredientTypesService.Setup(s => s.DeleteIngredientTypeAsync(ingredientTypeId))
                                       .ReturnsAsync((bool?)null);

            // Act
            var result = await _ingredientTypesController.DeleteIngredientType(ingredientTypeId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;

            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
            notFoundResult.Value.Should().BeEquivalentTo(new
            {
                message = "Ingredient type does not exist."
            });
        }

        [Fact]
        public async Task SearchIngredientTypes_ShouldReturnOk_WithFilteredIngredientTypes()
        {
            // Arrange
            var searchParams = new IngredientTypeSearchParams
            {
                NameContains = "Grain",
                DescriptionContains = "desc",
                SortBy = "Name",
                SortOrder = "asc"
            };

            var paginatedIngredientTypes = new PaginatedResponseDTO<IngredientTypeIdDTO>
            {
                Items = new List<IngredientTypeIdDTO>
                    {
                        new IngredientTypeIdDTO { Id = 1, Name = "Grain", Description = "Grains description" }
                    },
                TotalCount = 1,
                PageNumber = 1,
                PageSize = 10
            };

            _mockIngredientTypesService.Setup(s => s.SearchIngredientTypes(searchParams, 1, 10))
                .ReturnsAsync(paginatedIngredientTypes);

            // Act
            var result = await _ingredientTypesController.SearchIngredientTypes(
                searchParams,
                pageNumber: 1,
                pageSize: 10
            ) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(paginatedIngredientTypes);
        }


    }
}
