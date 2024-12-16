using BackEngin.Controllers;
using BackEngin.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Models.DTO;
using System.Security.Claims;
using Xunit;


namespace BackEngin.Tests.Controllers
{    
    public class PostInteractionControllerTests
    {
        private readonly Mock<IPostInteractionService> _mockInteractionService;
        private readonly Mock<ClaimsPrincipal> _mockUser;
        private readonly PostInteractionController _postInteractionController;

        public PostInteractionControllerTests()
        {
            _mockInteractionService = new Mock<IPostInteractionService>();

            // Mock user context
            _mockUser = new Mock<ClaimsPrincipal>();
            _mockUser.Setup(u => u.FindAll(ClaimTypes.NameIdentifier))
                     .Returns(new[] { new Claim(ClaimTypes.NameIdentifier, "currentUserId") });
            _mockUser.Setup(u => u.FindFirst(ClaimTypes.Role))
                     .Returns(new Claim(ClaimTypes.Role, "User"));

            // Setup controller with mock user
            _postInteractionController = new PostInteractionController(_mockInteractionService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = _mockUser.Object }
                }
            };
        }

        [Fact]
        public async Task ToggleLikeBlog_ShouldReturnOk_WhenLiked()
        {
            // Arrange
            var blogId = 1;
            _mockInteractionService.Setup(s => s.ToggleLikeBlog("currentUserId", blogId))
                                   .ReturnsAsync(true);

            // Act
            var result = await _postInteractionController.ToggleLikeBlog(blogId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { message = "Blog liked successfully." });
        }

        [Fact]
        public async Task ToggleLikeBlog_ShouldReturnOk_WhenUnliked()
        {
            // Arrange
            var blogId = 1;
            _mockInteractionService.Setup(s => s.ToggleLikeBlog("currentUserId", blogId))
                                   .ReturnsAsync(false);

            // Act
            var result = await _postInteractionController.ToggleLikeBlog(blogId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { message = "Blog unliked successfully." });
        }

        [Fact]
        public async Task ToggleBookmarkRecipe_ShouldReturnOk_WhenBookmarked()
        {
            // Arrange
            var recipeId = 1;
            _mockInteractionService.Setup(s => s.ToggleBookmarkRecipe("currentUserId", recipeId))
                                   .ReturnsAsync(true);

            // Act
            var result = await _postInteractionController.BookmarkRecipe(recipeId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { message = "Recipe bookmarked successfully." });
        }

        [Fact]
        public async Task ToggleBookmarkRecipe_ShouldReturnOk_WhenUnbookmarked()
        {
            // Arrange
            var recipeId = 1;
            _mockInteractionService.Setup(s => s.ToggleBookmarkRecipe("currentUserId", recipeId))
                                   .ReturnsAsync(false);

            // Act
            var result = await _postInteractionController.BookmarkRecipe(recipeId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { message = "Recipe unbookmarked successfully." });
        }

        [Fact]
        public async Task CommentOnBlog_ShouldReturnOk_WithComment()
        {
            // Arrange
            var blogId = 1;
            var commentRequest = new CommentRequestDTO { Text = "Great blog!", Image = null };
            var commentResponse = new CommentDTO
            {
                Id = 1,
                Recipe_blog_id = blogId,
                Text = "Great blog!",
                Timestamp = DateTime.UtcNow
            };

            _mockInteractionService.Setup(s => s.CommentOnBlog("currentUserId", blogId, commentRequest))
                                   .ReturnsAsync(commentResponse);

            // Act
            var result = await _postInteractionController.CommentOnBlog(blogId, commentRequest);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(commentResponse);
        }

        [Fact]
        public async Task UpdateBlogComment_ShouldReturnOk_WithUpdatedComment()
        {
            // Arrange
            var commentId = 1;
            var commentRequest = new CommentRequestDTO { Text = "Updated comment", Image = null };
            var updatedCommentResponse = new CommentDTO
            {
                Id = commentId,
                Recipe_blog_id = 1,
                Text = "Updated comment",
                Timestamp = DateTime.UtcNow
            };

            _mockInteractionService.Setup(s => s.UpdateBlogComment("currentUserId", commentId, commentRequest))
                                   .ReturnsAsync(updatedCommentResponse);

            // Act
            var result = await _postInteractionController.UpdateBlogComment(commentId, commentRequest);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(updatedCommentResponse);
        }

        [Fact]
        public async Task DeleteRecipeComment_ShouldReturnOk_WhenDeleted()
        {
            // Arrange
            var commentId = 1;
            _mockInteractionService.Setup(s => s.DeleteRecipeComment("currentUserId", commentId))
                                   .Verifiable();

            // Act
            var result = await _postInteractionController.DeleteRecipeComment(commentId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { message = "Recipe comment deleted successfully." });
            _mockInteractionService.Verify(s => s.DeleteRecipeComment("currentUserId", commentId), Times.Once);
        }
    }

}
