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
    public async Task GetBlogs_ShouldReturnPaginatedBlogs()
    {
        // Arrange
        var blogs = new List<Blogs>
        {
            new Blogs { Id = 1, Header = "Blog 1", BodyText = "Content 1", UserId = "1" },
            new Blogs { Id = 2, Header = "Blog 2", BodyText = "Content 2", UserId = "2" }
        };
        _mockUnitOfWork.Setup(u => u.Blogs.GetPaginatedAsync(null, 1, 2))
                       .ReturnsAsync((blogs, blogs.Count));

        // Act
        var result = await _blogService.GetBlogs(1, 2);

        // Assert
        result.Should().NotBeNull();
        result.Items.Count().Should().Be(2);
        result.Items.First().Header.Should().Be("Blog 1");
        result.TotalCount.Should().Be(2);
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(2);
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
                Ingredients = new List<RecipeIngredientDetailsDTO>
                {
                    new RecipeIngredientDetailsDTO { IngredientId = 1, Quantity = 2, Unit = "cups" }
                }
            }
        };

        var createdRecipe = new RecipeDetailsDTO { Id = 1, Header = "New Recipe", BodyText = "Recipe Content" };

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
    public async Task CreateBlog_ShouldThrowError_WhenInvalidIngredient()
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
                Ingredients = new List<RecipeIngredientDetailsDTO>
                {
                    new RecipeIngredientDetailsDTO { IngredientId = 99, Quantity = 2, Unit = "cups" }
                }
            }
        };

        _mockRecipeService.Setup(r => r.CreateRecipe(It.IsAny<CreateRecipeDTO>()))
                          .ThrowsAsync(new ArgumentException("One or more ingredient IDs are invalid."));

        // Act
        Func<Task> act = async () => await _blogService.CreateBlog(createBlogDTO);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("One or more ingredient IDs are invalid.");
    }

    [Fact]
    public async Task CreateBlog_ShouldCreateBlogWithoutRecipe()
    {
        // Arrange
        var createBlogDTO = new CreateBlogDTO
        {
            Header = "New Blog Without Recipe",
            BodyText = "This blog does not have an associated recipe.",
            UserId = "123",
            Recipe = null, // No recipe provided
            RecipeId = null
        };

        var newBlog = new Blogs
        {
            Id = 1,
            Header = createBlogDTO.Header,
            BodyText = createBlogDTO.BodyText,
            UserId = createBlogDTO.UserId,
            RecipeId = null // No recipe ID should be set
        };

        _mockUnitOfWork.Setup(u => u.Blogs.AddAsync(It.IsAny<Blogs>())).Callback<Blogs>(b => b.Id = 1);
        _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

        // Act
        var result = await _blogService.CreateBlog(createBlogDTO);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Header.Should().Be("New Blog Without Recipe");
        result.BodyText.Should().Be("This blog does not have an associated recipe.");
        result.RecipeId.Should().BeNull(); // RecipeId should remain null

        _mockUnitOfWork.Verify(u => u.Blogs.AddAsync(It.IsAny<Blogs>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.AtLeastOnce);
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
            Recipe = new RecipeRequestDTO
            {
                Header = "Updated Recipe",
                BodyText = "Updated Recipe Content",
                Ingredients = new List<RecipeIngredientDetailsDTO>
                {
                    new RecipeIngredientDetailsDTO { IngredientId = 1, Quantity = 2, Unit = "cups" }
                }
            }
        };

        var blog = new Blogs { Id = blogId, Header = "Old Blog", BodyText = "Old Content", RecipeId = 1 };

        _mockUnitOfWork.Setup(u => u.Blogs.GetByIdAsync(blogId)).ReturnsAsync(blog);
        _mockRecipeService.Setup(r => r.UpdateRecipe(1, updateBlogDTO.Recipe))
                          .ReturnsAsync(new RecipeDetailsDTO { Id = 1, Header = "Updated Recipe" });

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

        var recipe = new RecipeDetailsDTO
        {
            Id = 1,
            Header = "Recipe",
            BodyText = "Recipe Content",
            Ingredients = new List<RecipeIngredientDetailsDTO>
            {
                new RecipeIngredientDetailsDTO { IngredientId = 1, IngredientName = "Flour", Quantity = 2, Unit = "cups" }
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

    [Fact]
    public async Task DeleteBlog_ShouldReturnFalse_WhenRecipeDeletionFails()
    {
        // Arrange
        var blogId = 1;
        var blog = new Blogs { Id = blogId, Header = "Blog", BodyText = "Content", RecipeId = 1 };

        _mockUnitOfWork.Setup(u => u.Blogs.GetByIdAsync(blogId)).ReturnsAsync(blog);
        _mockRecipeService.Setup(r => r.DeleteRecipe(1)).ReturnsAsync(false);

        // Act
        var result = await _blogService.DeleteBlog(blogId);

        // Assert
        result.Should().BeFalse();
    }
}
