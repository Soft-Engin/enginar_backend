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
    public class BlogControllerTests
    {
        private readonly Mock<IBlogService> _mockBlogService;
        private readonly Mock<ClaimsPrincipal> _mockUser;
        private readonly BlogController _blogController;

        public BlogControllerTests()
        {
            _mockBlogService = new Mock<IBlogService>();

            // Mock user context
            _mockUser = new Mock<ClaimsPrincipal>();
            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier))
                     .Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role))
                     .Returns(new Claim(ClaimTypes.Role, "User"));

            // Setup controller with mock user
            _blogController = new BlogController(_mockBlogService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = _mockUser.Object }
                }
            };
        }

        [Fact]
        public async Task GetBlogs_ShouldReturnOk_WithPaginatedBlogs()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 2;
            var paginatedResponse = new PaginatedResponseDTO<BlogDTO>
            {
                Items = new List<BlogDTO>
                {
                    new BlogDTO { Id = 1, Header = "Blog 1", BodyText = "Content 1" },
                    new BlogDTO { Id = 2, Header = "Blog 2", BodyText = "Content 2" }
                },
                TotalCount = 5,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            _mockBlogService.Setup(s => s.GetBlogs(pageNumber, pageSize)).ReturnsAsync(paginatedResponse);

            // Act
            var result = await _blogController.GetBlogs(pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(paginatedResponse);
        }

        [Fact]
        public async Task CreateBlog_ShouldReturnCreated_WhenBlogIsValid()
        {
            // Arrange
            var createBlogDto = new CreateBlogDTO
            {
                Header = "New Blog",
                BodyText = "Blog Content",
                UserId = "currentUserId"
            };
            var createdBlog = new BlogDTO
            {
                Id = 1,
                Header = createBlogDto.Header,
                BodyText = createBlogDto.BodyText
            };

            _mockBlogService.Setup(s => s.CreateBlog(createBlogDto)).ReturnsAsync(createdBlog);

            // Act
            var result = await _blogController.CreateBlog(createBlogDto);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
            var createdResult = result as CreatedAtActionResult;
            createdResult.Value.Should().BeEquivalentTo(createdBlog);
        }

        [Fact]
        public async Task GetBlogById_ShouldReturnOk_WithBlogDetails()
        {
            // Arrange
            var blogId = 1;
            var blog = new BlogDetailDTO
            {
                Id = blogId,
                Header = "Blog 1",
                BodyText = "Content 1",
                Recipe = new RecipeDetailsDTO
                {
                    Id = 1,
                    Header = "Recipe 1",
                    BodyText = "Recipe Content"
                }
            };

            _mockBlogService.Setup(s => s.GetBlogById(blogId)).ReturnsAsync(blog);

            // Act
            var result = await _blogController.GetBlogById(blogId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(blog);
        }

        [Fact]
        public async Task UpdateBlog_ShouldReturnOk_WithUpdatedBlog()
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
                Header = updateBlogDto.Header,
                BodyText = updateBlogDto.BodyText
            };

            _mockBlogService.Setup(s => s.GetOwner(blogId)).ReturnsAsync("currentUserId");
            _mockBlogService.Setup(s => s.UpdateBlog(blogId, updateBlogDto)).ReturnsAsync(updatedBlog);

            // Act
            var result = await _blogController.UpdateBlog(blogId, updateBlogDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(updatedBlog);
        }

        [Fact]
        public async Task DeleteBlog_ShouldReturnOk_WhenDeletedSuccessfully()
        {
            // Arrange
            var blogId = 1;

            _mockBlogService.Setup(s => s.GetOwner(blogId)).ReturnsAsync("currentUserId");
            _mockBlogService.Setup(s => s.DeleteBlog(blogId)).ReturnsAsync(true);

            // Act
            var result = await _blogController.DeleteBlog(blogId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { message = "Blog deleted successfully!" });
        }

        [Fact]
        public async Task GetRecipeOfBlog_ShouldReturnOk_WithRecipeDetails()
        {
            // Arrange
            var blogId = 1;
            var recipeDetails = new RecipeDetailsDTO
            {
                Id = 1,
                Header = "Recipe 1",
                BodyText = "Recipe Content"
            };

            _mockBlogService.Setup(s => s.GetRecipeOfBlog(blogId)).ReturnsAsync(recipeDetails);

            // Act
            var result = await _blogController.GetRecipeOfBlog(blogId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(recipeDetails);
        }

        [Fact]
        public async Task UpdateBlog_ShouldReturnUnauthorized_WhenUserNotOwnerOrAdmin()
        {
            // Arrange
            var blogId = 1;
            var updateBlogDto = new UpdateBlogDTO
            {
                Header = "Updated Blog",
                BodyText = "Updated Content"
            };

            _mockBlogService.Setup(s => s.GetOwner(blogId)).ReturnsAsync("otherUserId");

            // Act
            var result = await _blogController.UpdateBlog(blogId, updateBlogDto);

            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task DeleteBlog_ShouldReturnUnauthorized_WhenUserNotOwnerOrAdmin()
        {
            // Arrange
            var blogId = 1;

            _mockBlogService.Setup(s => s.GetOwner(blogId)).ReturnsAsync("otherUserId");

            // Act
            var result = await _blogController.DeleteBlog(blogId);

            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }
    }
}
