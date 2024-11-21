using System.Collections.Generic;
using System.Threading.Tasks;
using BackEngin.Controllers;
using BackEngin.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Moq;
using Xunit;

public class BlogControllerTests
{
    private readonly Mock<IBlogService> _mockBlogService;
    private readonly BlogController _blogController;

    public BlogControllerTests()
    {
        _mockBlogService = new Mock<IBlogService>();
        _blogController = new BlogController(_mockBlogService.Object);
    }

    [Fact]
    public async Task GetBlogs_ShouldReturnOkWithBlogs()
    {
        // Arrange
        var blogs = new List<BlogDTO>
        {
            new BlogDTO { Id = 1, Header = "Blog 1", BodyText = "Content 1", UserId = "1" },
            new BlogDTO { Id = 2, Header = "Blog 2", BodyText = "Content 2", UserId = "2" }
        };
        _mockBlogService.Setup(s => s.GetBlogs()).ReturnsAsync(blogs);

        // Act
        var result = await _blogController.GetBlogs();

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(blogs);
    }

    [Fact]
    public async Task CreateBlog_ShouldReturnCreatedWithBlog()
    {
        // Arrange
        var createBlogDto = new CreateBlogDTO
        {
            Header = "New Blog",
            BodyText = "Blog Content",
            UserId = "123"
        };

        var createdBlog = new BlogDTO
        {
            Id = 1,
            Header = "New Blog",
            BodyText = "Blog Content",
            UserId = "123"
        };

        _mockBlogService.Setup(s => s.CreateBlog(createBlogDto)).ReturnsAsync(createdBlog);

        // Act
        var result = await _blogController.CreateBlog(createBlogDto);

        // Assert
        var createdResult = result as CreatedAtActionResult;
        createdResult.Should().NotBeNull();
        createdResult.StatusCode.Should().Be(201);
        createdResult.Value.Should().BeEquivalentTo(createdBlog);
    }

    [Fact]
    public async Task CreateBlog_ShouldReturnBadRequest_WhenDtoIsNull()
    {
        // Act
        var result = await _blogController.CreateBlog(null);

        // Assert
        var badRequestResult = result as BadRequestObjectResult;
        badRequestResult.Should().NotBeNull();
        badRequestResult.StatusCode.Should().Be(400);
        badRequestResult.Value.Should().Be("Invalid blog data.");
    }

    [Fact]
    public async Task GetBlogById_ShouldReturnOkWithBlog()
    {
        // Arrange
        var blogId = 1;
        var blog = new BlogDetailDTO
        {
            Id = blogId,
            Header = "Blog",
            BodyText = "Content",
            UserId = "1"
        };

        _mockBlogService.Setup(s => s.GetBlogById(blogId)).ReturnsAsync(blog);

        // Act
        var result = await _blogController.GetBlogById(blogId);

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(blog);
    }

    [Fact]
    public async Task GetBlogById_ShouldReturnNotFound_WhenBlogDoesNotExist()
    {
        // Arrange
        var blogId = 1;
        _mockBlogService.Setup(s => s.GetBlogById(blogId)).ReturnsAsync((BlogDetailDTO)null);

        // Act
        var result = await _blogController.GetBlogById(blogId);

        // Assert
        var notFoundResult = result as NotFoundResult;
        notFoundResult.Should().NotBeNull();
        notFoundResult.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task UpdateBlog_ShouldReturnOkWithUpdatedBlog()
    {
        // Arrange
        var blogId = 1;
        var updateBlogDto = new UpdateBlogDTO
        {
            Header = "Updated Blog",
            BodyText = "Updated Content"
        };

        var updatedBlog = new BlogDTO
        {
            Id = blogId,
            Header = "Updated Blog",
            BodyText = "Updated Content",
            UserId = "1"
        };

        _mockBlogService.Setup(s => s.UpdateBlog(blogId, updateBlogDto)).ReturnsAsync(updatedBlog);

        // Act
        var result = await _blogController.UpdateBlog(blogId, updateBlogDto);

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(updatedBlog);
    }

    [Fact]
    public async Task UpdateBlog_ShouldReturnNotFound_WhenBlogDoesNotExist()
    {
        // Arrange
        var blogId = 1;
        var updateBlogDto = new UpdateBlogDTO
        {
            Header = "Updated Blog",
            BodyText = "Updated Content"
        };

        _mockBlogService.Setup(s => s.UpdateBlog(blogId, updateBlogDto)).ReturnsAsync((BlogDTO)null);

        // Act
        var result = await _blogController.UpdateBlog(blogId, updateBlogDto);

        // Assert
        var notFoundResult = result as NotFoundResult;
        notFoundResult.Should().NotBeNull();
        notFoundResult.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task DeleteBlog_ShouldReturnNoContent_WhenDeletedSuccessfully()
    {
        // Arrange
        var blogId = 1;
        _mockBlogService.Setup(s => s.DeleteBlog(blogId)).ReturnsAsync(true);

        // Act
        var result = await _blogController.DeleteBlog(blogId);

        // Assert
        var noContentResult = result as NoContentResult;
        noContentResult.Should().NotBeNull();
        noContentResult.StatusCode.Should().Be(204);
    }

    [Fact]
    public async Task DeleteBlog_ShouldReturnNotFound_WhenBlogDoesNotExist()
    {
        // Arrange
        var blogId = 1;
        _mockBlogService.Setup(s => s.DeleteBlog(blogId)).ReturnsAsync(false);

        // Act
        var result = await _blogController.DeleteBlog(blogId);

        // Assert
        var notFoundResult = result as NotFoundResult;
        notFoundResult.Should().NotBeNull();
        notFoundResult.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task GetRecipeOfBlog_ShouldReturnOkWithRecipe()
    {
        // Arrange
        var blogId = 1;
        var recipe = new RecipeDTO
        {
            Id = 1,
            Header = "Recipe",
            BodyText = "Recipe Content",
            Ingredients = new List<RecipeIngredientDTO>
            {
                new RecipeIngredientDTO { IngredientId = 1, IngredientName = "Flour", Quantity = 2, Unit = "cups" }
            }
        };

        _mockBlogService.Setup(s => s.GetRecipeOfBlog(blogId)).ReturnsAsync(recipe);

        // Act
        var result = await _blogController.GetRecipeOfBlog(blogId);

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(recipe);
    }

    [Fact]
    public async Task GetRecipeOfBlog_ShouldReturnNotFound_WhenNoRecipeExists()
    {
        // Arrange
        var blogId = 1;
        _mockBlogService.Setup(s => s.GetRecipeOfBlog(blogId)).ReturnsAsync((RecipeDTO)null);

        // Act
        var result = await _blogController.GetRecipeOfBlog(blogId);

        // Assert
        var notFoundResult = result as NotFoundObjectResult;
        notFoundResult.Should().NotBeNull();
        notFoundResult.StatusCode.Should().Be(404);
        notFoundResult.Value.Should().Be("No recipe associated with this blog.");
    }
}
