using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEngin.Services;
using BackEngin.Services.Interfaces;
using DataAccess.Repositories;
using FluentAssertions;
using Models;
using Models.DTO;
using Moq;
using Xunit;

public class BlogServiceTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IRecipeService> _mockRecipeService;
    private readonly BlogService _blogService;

    public BlogServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockRecipeService = new Mock<IRecipeService>();
        _blogService = new BlogService(_mockUnitOfWork.Object, _mockRecipeService.Object);
    }

    [Fact]
    public async Task GetBlogs_ShouldReturnAllBlogs()
    {
        // Arrange
        var blogs = new List<Blogs>
        {
            new Blogs { Id = 1, Header = "Blog 1", BodyText = "Content 1", UserId = "1" },
            new Blogs { Id = 2, Header = "Blog 2", BodyText = "Content 2", UserId = "2" }
        };
        _mockUnitOfWork.Setup(u => u.Blogs.GetAllAsync()).ReturnsAsync(blogs);

        // Act
        var result = await _blogService.GetBlogs();

        // Assert
        result.Should().NotBeNull();
        result.Count().Should().Be(2);
        result.First().Header.Should().Be("Blog 1");
    }

    [Fact]
    public async Task CreateBlog_ShouldCreateBlogWithNewRecipe()
    {
        // Arrange
        var createBlogDTO = new CreateBlogDTO
        {
            Header = "New Blog",
            BodyText = "Blog Content",
            UserId = "123",
            Recipe = new CreateRecipeDTO
            {
                Header = "New Recipe",
                BodyText = "Recipe Content",
                Ingredients = new List<RecipeIngredientDTO>
                {
                    new RecipeIngredientDTO { IngredientId = 1, Quantity = 2, Unit = "cups" }
                }
            }
        };

        var createdRecipe = new RecipeDTO { Id = 1, Header = "New Recipe", BodyText = "Recipe Content" };

        _mockRecipeService.Setup(r => r.CreateRecipe(It.IsAny<CreateRecipeDTO>())).ReturnsAsync(createdRecipe);
        _mockUnitOfWork.Setup(u => u.Blogs.AddAsync(It.IsAny<Blogs>())).Verifiable();
        _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

        // Act
        var result = await _blogService.CreateBlog(createBlogDTO);

        // Assert
        result.Should().NotBeNull();
        result.RecipeId.Should().Be(1);
        _mockUnitOfWork.Verify(u => u.Blogs.AddAsync(It.IsAny<Blogs>()), Times.Once);
    }

    [Fact]
    public async Task UpdateBlog_ShouldUpdateBlogAndAssociatedRecipe()
    {
        // Arrange
        var blogId = 1;
        var updateBlogDTO = new UpdateBlogDTO
        {
            Header = "Updated Blog",
            BodyText = "Updated Content",
            Recipe = new UpdateRecipeDTO
            {
                Header = "Updated Recipe",
                BodyText = "Updated Recipe Content",
                Ingredients = new List<RecipeIngredientDTO>
                {
                    new RecipeIngredientDTO { IngredientId = 1, Quantity = 2, Unit = "cups" }
                }
            }
        };

        var blog = new Blogs { Id = blogId, Header = "Old Blog", BodyText = "Old Content", RecipeId = 1 };

        _mockUnitOfWork.Setup(u => u.Blogs.GetByIdAsync(blogId)).ReturnsAsync(blog);
        _mockRecipeService.Setup(r => r.UpdateRecipe(1, updateBlogDTO.Recipe))
                          .ReturnsAsync(new RecipeDTO { Id = 1, Header = "Updated Recipe" });

        _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

        // Act
        var result = await _blogService.UpdateBlog(blogId, updateBlogDTO);

        // Assert
        result.Should().NotBeNull();
        result.Header.Should().Be("Updated Blog");
        result.RecipeId.Should().Be(1);
        _mockRecipeService.Verify(r => r.UpdateRecipe(1, updateBlogDTO.Recipe), Times.Once);
    }

    [Fact]
    public async Task GetBlogById_ShouldReturnBlogWithRecipeDetails()
    {
        // Arrange
        var blogId = 1;
        var blog = new Blogs { Id = blogId, Header = "Blog", BodyText = "Content", RecipeId = 1 };

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

        _mockUnitOfWork.Setup(u => u.Blogs.GetByIdAsync(blogId)).ReturnsAsync(blog);
        _mockRecipeService.Setup(r => r.GetRecipeDetails(1)).ReturnsAsync(recipe);

        // Act
        var result = await _blogService.GetBlogById(blogId);

        // Assert
        result.Should().NotBeNull();
        result.Header.Should().Be("Blog");
        result.Recipe.Should().NotBeNull();
        result.Recipe.Ingredients.First().IngredientName.Should().Be("Flour");
    }

    [Fact]
    public async Task DeleteBlog_ShouldDeleteBlogAndAssociatedRecipe()
    {
        // Arrange
        var blogId = 1;
        var blog = new Blogs { Id = blogId, Header = "Blog", BodyText = "Content", RecipeId = 1 };

        _mockUnitOfWork.Setup(u => u.Blogs.GetByIdAsync(blogId)).ReturnsAsync(blog);
        _mockRecipeService.Setup(r => r.DeleteRecipe(1)).ReturnsAsync(true);

        _mockUnitOfWork.Setup(u => u.Blogs.Delete(blog)).Verifiable();
        _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

        // Act
        var result = await _blogService.DeleteBlog(blogId);

        // Assert
        result.Should().BeTrue();
        _mockRecipeService.Verify(r => r.DeleteRecipe(1), Times.Once);
        _mockUnitOfWork.Verify(u => u.Blogs.Delete(blog), Times.Once);
    }
}
