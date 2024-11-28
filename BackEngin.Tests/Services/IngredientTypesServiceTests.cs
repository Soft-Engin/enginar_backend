using BackEngin.Services;
using BackEngin.Services.Interfaces;
using DataAccess.Repositories;
using DataAccess.Repositories.IRepositories; // Ensure correct namespace
using FluentAssertions;
using Models;
using Models.DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BackEngin.Tests.Services
{
    public class IngredientTypesServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IngredientTypesService _ingredientTypesService;

        public IngredientTypesServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _ingredientTypesService = new IngredientTypesService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetIngredientTypesPaginatedAsync_ShouldReturnPaginatedIngredientTypes()
        {
            // Arrange
            var ingredientTypes = new List<IngredientTypes>
            {
                new IngredientTypes { Id = 1, Name = "Grain", Description = "Grains description" },
                new IngredientTypes { Id = 2, Name = "Dairy", Description = "Dairy description" }
            };
            var pageNumber = 1;
            var pageSize = 2;

            _mockUnitOfWork.Setup(u => u.IngredientTypes.GetPaginatedAsync(null, pageNumber, pageSize))
                           .ReturnsAsync((ingredientTypes, 5));

            // Act
            var result = await _ingredientTypesService.GetIngredientTypesPaginatedAsync(pageNumber, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Count().Should().Be(pageSize);
            result.TotalCount.Should().Be(5);
            result.PageNumber.Should().Be(pageNumber);
            result.PageSize.Should().Be(pageSize);
            result.Items.First().Name.Should().Be("Grain");
        }

        [Fact]
        public async Task GetIngredientTypeByIdAsync_ShouldReturnIngredientType_WhenExists()
        {
            // Arrange
            var ingredientTypeId = 1;
            var ingredientType = new IngredientTypes
            {
                Id = ingredientTypeId,
                Name = "Grain",
                Description = "Grains description"
            };

            _mockUnitOfWork.Setup(u => u.IngredientTypes.GetByIdAsync(ingredientTypeId))
                           .ReturnsAsync(ingredientType);

            // Act
            var result = await _ingredientTypesService.GetIngredientTypeByIdAsync(ingredientTypeId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(ingredientTypeId);
            result.Name.Should().Be("Grain");
        }

        [Fact]
        public async Task GetIngredientTypeByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            var ingredientTypeId = 99;
            _mockUnitOfWork.Setup(u => u.IngredientTypes.GetByIdAsync(ingredientTypeId))
                           .ReturnsAsync((IngredientTypes)null);

            // Act
            var result = await _ingredientTypesService.GetIngredientTypeByIdAsync(ingredientTypeId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateIngredientTypeAsync_ShouldReturnIngredientTypeId_WhenSuccessful()
        {
            // Arrange
            var ingredientTypeDto = new IngredientTypeDTO
            {
                Name = "Spice",
                Description = "Spices description"
            };

            var ingredientType = new IngredientTypes
            {
                Id = 3,
                Name = "Spice",
                Description = "Spices description"
            };

            // Mock the IngredientTypes repository's AddAsync method
            _mockUnitOfWork.Setup(u => u.IngredientTypes.AddAsync(It.IsAny<IngredientTypes>()))
                           .Callback<IngredientTypes>(it => it.Id = 3) // Simulate DB-generated Id
                           .Returns(Task.CompletedTask)
                           .Verifiable();

            // Mock the UnitOfWork's CompleteAsync method to return 1 (indicating success)
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                           .ReturnsAsync(1)
                           .Verifiable();

            // Act
            var result = await _ingredientTypesService.CreateIngredientTypeAsync(ingredientTypeDto);

            // Assert
            result.Should().Be(3); // Expected to be the assigned Id
        }
        [Fact]
        public async Task CreateIngredientTypeAsync_ShouldReturnNull_WhenExceptionOccurs()
        {
            // Arrange
            var ingredientTypeDto = new IngredientTypeDTO
            {
                Name = "Spice",
                Description = "Spices description"
            };

            _mockUnitOfWork.Setup(u => u.IngredientTypes.AddAsync(It.IsAny<IngredientTypes>()))
                           .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _ingredientTypesService.CreateIngredientTypeAsync(ingredientTypeDto);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateIngredientTypeAsync_ShouldReturnTrue_WhenSuccessful()
        {
            // Arrange
            var ingredientTypeId = 1;
            var ingredientTypeDto = new IngredientTypeDTO
            {
                Name = "Herb",
                Description = "Herbs description"
            };

            var existingIngredientType = new IngredientTypes
            {
                Id = ingredientTypeId,
                Name = "Spice",
                Description = "Spices description"
            };

            _mockUnitOfWork.Setup(u => u.IngredientTypes.GetByIdAsync(ingredientTypeId))
                           .ReturnsAsync(existingIngredientType);

            _mockUnitOfWork.Setup(u => u.IngredientTypes.Update(existingIngredientType))
                           .Verifiable();

            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                           .ReturnsAsync(1)
                           .Verifiable();

            // Act
            var result = await _ingredientTypesService.UpdateIngredientTypeAsync(ingredientTypeId, ingredientTypeDto);

            // Assert
            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.IngredientTypes.Update(It.IsAny<IngredientTypes>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateIngredientTypeAsync_ShouldReturnNull_WhenIngredientTypeNotFound()
        {
            // Arrange
            var ingredientTypeId = 99;
            var ingredientTypeDto = new IngredientTypeDTO
            {
                Name = "Herb",
                Description = "Herbs description"
            };

            _mockUnitOfWork.Setup(u => u.IngredientTypes.GetByIdAsync(ingredientTypeId))
                           .ReturnsAsync((IngredientTypes)null);

            // Act
            var result = await _ingredientTypesService.UpdateIngredientTypeAsync(ingredientTypeId, ingredientTypeDto);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateIngredientTypeAsync_ShouldReturnFalse_WhenExceptionOccurs()
        {
            // Arrange
            var ingredientTypeId = 1;
            var ingredientTypeDto = new IngredientTypeDTO
            {
                Name = "Herb",
                Description = "Herbs description"
            };

            var existingIngredientType = new IngredientTypes
            {
                Id = ingredientTypeId,
                Name = "Spice",
                Description = "Spices description"
            };

            _mockUnitOfWork.Setup(u => u.IngredientTypes.GetByIdAsync(ingredientTypeId))
                           .ReturnsAsync(existingIngredientType);

            _mockUnitOfWork.Setup(u => u.IngredientTypes.Update(existingIngredientType))
                           .Throws(new Exception("Database error"));

            // Act
            var result = await _ingredientTypesService.UpdateIngredientTypeAsync(ingredientTypeId, ingredientTypeDto);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteIngredientTypeAsync_ShouldReturnTrue_WhenSuccessful()
        {
            // Arrange
            var ingredientTypeId = 1;
            var existingIngredientType = new IngredientTypes
            {
                Id = ingredientTypeId,
                Name = "Spice",
                Description = "Spices description"
            };

            _mockUnitOfWork.Setup(u => u.IngredientTypes.GetByIdAsync(ingredientTypeId))
                           .ReturnsAsync(existingIngredientType);

            _mockUnitOfWork.Setup(u => u.IngredientTypes.Delete(existingIngredientType))
                           .Verifiable();

            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                           .ReturnsAsync(1)
                           .Verifiable();

            // Act
            var result = await _ingredientTypesService.DeleteIngredientTypeAsync(ingredientTypeId);

            // Assert
            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.IngredientTypes.Delete(existingIngredientType), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteIngredientTypeAsync_ShouldReturnNull_WhenIngredientTypeNotFound()
        {
            // Arrange
            var ingredientTypeId = 99;
            _mockUnitOfWork.Setup(u => u.IngredientTypes.GetByIdAsync(ingredientTypeId))
                           .ReturnsAsync((IngredientTypes)null);

            // Act
            var result = await _ingredientTypesService.DeleteIngredientTypeAsync(ingredientTypeId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteIngredientTypeAsync_ShouldReturnFalse_WhenExceptionOccurs()
        {
            // Arrange
            var ingredientTypeId = 1;
            var existingIngredientType = new IngredientTypes
            {
                Id = ingredientTypeId,
                Name = "Spice",
                Description = "Spices description"
            };

            _mockUnitOfWork.Setup(u => u.IngredientTypes.GetByIdAsync(ingredientTypeId))
                           .ReturnsAsync(existingIngredientType);

            _mockUnitOfWork.Setup(u => u.IngredientTypes.Delete(existingIngredientType))
                           .Throws(new Exception("Database error"));

            // Act
            var result = await _ingredientTypesService.DeleteIngredientTypeAsync(ingredientTypeId);

            // Assert
            result.Should().BeFalse();
        }
    }
}
