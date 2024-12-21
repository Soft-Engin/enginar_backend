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
            var commentRequest = new CommentRequestDTO { Text = "Great blog!", Images = null };
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
            var commentRequest = new CommentRequestDTO { Text = "Updated comment", Images = null };
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

        [Fact]
        public async Task IsBlogLiked_ShouldReturnOk_WithIsLikedAndLikeCount()
        {
            // Arrange
            var blogId = 1;
            _mockInteractionService.Setup(s => s.IsBlogLiked("currentUserId", blogId))
                                   .ReturnsAsync(true);
            _mockInteractionService.Setup(s => s.GetBlogLikeCount(blogId))
                                   .ReturnsAsync(5);

            // Act
            var result = await _postInteractionController.IsBlogLiked(blogId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { isLiked = true, likeCount = 5 });
        }

        [Fact]
        public async Task IsBlogBookmarked_ShouldReturnOk_WithIsBookmarkedAndBookmarkCount()
        {
            // Arrange
            var blogId = 1;
            _mockInteractionService.Setup(s => s.IsBlogBookmarked("currentUserId", blogId))
                                   .ReturnsAsync(true);
            _mockInteractionService.Setup(s => s.GetBlogBookmarkCount(blogId))
                                   .ReturnsAsync(3);

            // Act
            var result = await _postInteractionController.IsBlogBookmarked(blogId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { isBookmarked = true, bookmarkCount = 3 });
        }

        [Fact]
        public async Task IsRecipeLiked_ShouldReturnOk_WithIsLikedAndLikeCount()
        {
            // Arrange
            var recipeId = 1;
            _mockInteractionService.Setup(s => s.IsRecipeLiked("currentUserId", recipeId))
                                   .ReturnsAsync(false);
            _mockInteractionService.Setup(s => s.GetRecipeLikeCount(recipeId))
                                   .ReturnsAsync(7);

            // Act
            var result = await _postInteractionController.IsRecipeLiked(recipeId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { isLiked = false, likeCount = 7 });
        }

        [Fact]
        public async Task IsRecipeBookmarked_ShouldReturnOk_WithIsBookmarkedAndBookmarkCount()
        {
            // Arrange
            var recipeId = 1;
            _mockInteractionService.Setup(s => s.IsRecipeBookmarked("currentUserId", recipeId))
                                   .ReturnsAsync(false);
            _mockInteractionService.Setup(s => s.GetRecipeBookmarkCount(recipeId))
                                   .ReturnsAsync(2);

            // Act
            var result = await _postInteractionController.IsRecipeBookmarked(recipeId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { isBookmarked = false, bookmarkCount = 2 });
        }

        [Fact]
        public async Task GetBlogComments_ShouldReturnOk_WithPaginatedComments()
        {
            // Arrange
            var blogId = 1;
            var pageNumber = 1;
            var pageSize = 10;
            var paginatedComments = new PaginatedResponseDTO<CommentDTO>
            {
                Items = new List<CommentDTO>
        {
            new CommentDTO { Id = 1, Recipe_blog_id = blogId, Text = "Comment 1", Timestamp = DateTime.UtcNow },
            new CommentDTO { Id = 2, Recipe_blog_id = blogId, Text = "Comment 2", Timestamp = DateTime.UtcNow }
        },
                TotalCount = 20,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            _mockInteractionService.Setup(s => s.GetBlogComments(blogId, pageNumber, pageSize))
                                   .ReturnsAsync(paginatedComments);

            // Act
            var result = await _postInteractionController.GetBlogComments(blogId, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(paginatedComments);
        }

        [Fact]
        public async Task GetRecipeComments_ShouldReturnOk_WithPaginatedComments()
        {
            // Arrange
            var recipeId = 1;
            var pageNumber = 1;
            var pageSize = 10;
            var paginatedComments = new PaginatedResponseDTO<CommentDTO>
            {
                Items = new List<CommentDTO>
        {
            new CommentDTO { Id = 1, Recipe_blog_id = recipeId, Text = "Comment 1", Timestamp = DateTime.UtcNow },
            new CommentDTO { Id = 2, Recipe_blog_id = recipeId, Text = "Comment 2", Timestamp = DateTime.UtcNow }
        },
                TotalCount = 15,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            _mockInteractionService.Setup(s => s.GetRecipeComments(recipeId, pageNumber, pageSize))
                                   .ReturnsAsync(paginatedComments);

            // Act
            var result = await _postInteractionController.GetRecipeComments(recipeId, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(paginatedComments);
        }

        [Fact]
        public async Task IsBlogLiked_ShouldReturnBadRequest_OnException()
        {
            // Arrange
            var blogId = 1;
            _mockInteractionService.Setup(s => s.IsBlogLiked("currentUserId", blogId))
                                   .ThrowsAsync(new Exception("Some error occurred"));

            // Act
            var result = await _postInteractionController.IsBlogLiked(blogId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().BeEquivalentTo(new { message = "Some error occurred" });
        }

        [Fact]
        public async Task IsBlogBookmarked_ShouldReturnBadRequest_OnException()
        {
            // Arrange
            var blogId = 1;
            _mockInteractionService.Setup(s => s.IsBlogBookmarked("currentUserId", blogId))
                                   .ThrowsAsync(new Exception("Some error occurred"));

            // Act
            var result = await _postInteractionController.IsBlogBookmarked(blogId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().BeEquivalentTo(new { message = "Some error occurred" });
        }

        [Fact]
        public async Task IsRecipeLiked_ShouldReturnBadRequest_OnException()
        {
            // Arrange
            var recipeId = 1;
            _mockInteractionService.Setup(s => s.IsRecipeLiked("currentUserId", recipeId))
                                   .ThrowsAsync(new Exception("Some error occurred"));

            // Act
            var result = await _postInteractionController.IsRecipeLiked(recipeId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().BeEquivalentTo(new { message = "Some error occurred" });
        }

        [Fact]
        public async Task IsRecipeBookmarked_ShouldReturnBadRequest_OnException()
        {
            // Arrange
            var recipeId = 1;
            _mockInteractionService.Setup(s => s.IsRecipeBookmarked("currentUserId", recipeId))
                                   .ThrowsAsync(new Exception("Some error occurred"));

            // Act
            var result = await _postInteractionController.IsRecipeBookmarked(recipeId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().BeEquivalentTo(new { message = "Some error occurred" });
        }

        [Fact]
        public async Task GetBlogComments_ShouldReturnBadRequest_OnException()
        {
            // Arrange
            var blogId = 1;
            var pageNumber = 1;
            var pageSize = 10;
            _mockInteractionService.Setup(s => s.GetBlogComments(blogId, pageNumber, pageSize))
                                   .ThrowsAsync(new Exception("Unable to fetch comments"));

            // Act
            var result = await _postInteractionController.GetBlogComments(blogId, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().BeEquivalentTo(new { message = "Unable to fetch comments" });
        }

        [Fact]
        public async Task GetRecipeComments_ShouldReturnBadRequest_OnException()
        {
            // Arrange
            var recipeId = 1;
            var pageNumber = 1;
            var pageSize = 10;
            _mockInteractionService.Setup(s => s.GetRecipeComments(recipeId, pageNumber, pageSize))
                                   .ThrowsAsync(new Exception("Unable to fetch comments"));

            // Act
            var result = await _postInteractionController.GetRecipeComments(recipeId, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().BeEquivalentTo(new { message = "Unable to fetch comments" });
        }

        [Fact]
        public async Task ToggleLikeBlog_ShouldReturnInternalServerError_OnUnexpectedException()
        {
            // Arrange
            var blogId = 1;
            _mockInteractionService.Setup(s => s.ToggleLikeBlog("currentUserId", blogId))
                                   .ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _postInteractionController.ToggleLikeBlog(blogId);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var internalServerErrorResult = result as ObjectResult;
            internalServerErrorResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            internalServerErrorResult.Value.Should().BeEquivalentTo(new { message = "An unexpected error occurred.", error = "Unexpected error" });
        }

        [Fact]
        public async Task GetBlogComments_ShouldReturnEmpty_WhenPageOutOfRange()
        {
            // Arrange
            var blogId = 1;
            var pageNumber = 100;
            var pageSize = 10;
            var emptyPaginatedResponse = new PaginatedResponseDTO<CommentDTO>
            {
                Items = new List<CommentDTO>(),
                TotalCount = 50,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            _mockInteractionService.Setup(s => s.GetBlogComments(blogId, pageNumber, pageSize))
                                   .ReturnsAsync(emptyPaginatedResponse);

            // Act
            var result = await _postInteractionController.GetBlogComments(blogId, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(emptyPaginatedResponse);
        }

        [Fact]
        public async Task GetBlogLikeCount_ShouldReturnOk_WithLikeCount()
        {
            // Arrange
            var blogId = 1;
            var likeCount = 10;
            _mockInteractionService.Setup(s => s.GetBlogLikeCount(blogId))
                                   .ReturnsAsync(likeCount);

            // Act
            var result = await _postInteractionController.GetBlogLikeCount(blogId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { likeCount });
        }

        [Fact]
        public async Task GetBlogBookmarkCount_ShouldReturnOk_WithBookmarkCount()
        {
            // Arrange
            var blogId = 1;
            var bookmarkCount = 5;
            _mockInteractionService.Setup(s => s.GetBlogBookmarkCount(blogId))
                                   .ReturnsAsync(bookmarkCount);

            // Act
            var result = await _postInteractionController.GetBlogBookmarkCount(blogId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { bookmarkCount });
        }

        [Fact]
        public async Task GetRecipeLikeCount_ShouldReturnOk_WithLikeCount()
        {
            // Arrange
            var recipeId = 1;
            var likeCount = 15;
            _mockInteractionService.Setup(s => s.GetRecipeLikeCount(recipeId))
                                   .ReturnsAsync(likeCount);

            // Act
            var result = await _postInteractionController.GetRecipeLikeCount(recipeId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { likeCount });
        }

        [Fact]
        public async Task GetRecipeBookmarkCount_ShouldReturnOk_WithBookmarkCount()
        {
            // Arrange
            var recipeId = 1;
            var bookmarkCount = 8;
            _mockInteractionService.Setup(s => s.GetRecipeBookmarkCount(recipeId))
                                   .ReturnsAsync(bookmarkCount);

            // Act
            var result = await _postInteractionController.GetRecipeBookmarkCount(recipeId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { bookmarkCount });
        }

        [Fact]
        public async Task GetBlogLikeCount_ShouldReturnBadRequest_OnException()
        {
            // Arrange
            var blogId = 1;
            _mockInteractionService.Setup(s => s.GetBlogLikeCount(blogId))
                                   .ThrowsAsync(new Exception("Some error occurred"));

            // Act
            var result = await _postInteractionController.GetBlogLikeCount(blogId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().BeEquivalentTo(new { message = "Some error occurred" });
        }

        [Fact]
        public async Task GetBlogBookmarkCount_ShouldReturnBadRequest_OnException()
        {
            // Arrange
            var blogId = 1;
            _mockInteractionService.Setup(s => s.GetBlogBookmarkCount(blogId))
                                   .ThrowsAsync(new Exception("Some error occurred"));

            // Act
            var result = await _postInteractionController.GetBlogBookmarkCount(blogId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().BeEquivalentTo(new { message = "Some error occurred" });
        }

        [Fact]
        public async Task GetRecipeLikeCount_ShouldReturnBadRequest_OnException()
        {
            // Arrange
            var recipeId = 1;
            _mockInteractionService.Setup(s => s.GetRecipeLikeCount(recipeId))
                                   .ThrowsAsync(new Exception("Some error occurred"));

            // Act
            var result = await _postInteractionController.GetRecipeLikeCount(recipeId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().BeEquivalentTo(new { message = "Some error occurred" });
        }

        [Fact]
        public async Task GetRecipeBookmarkCount_ShouldReturnBadRequest_OnException()
        {
            // Arrange
            var recipeId = 1;
            _mockInteractionService.Setup(s => s.GetRecipeBookmarkCount(recipeId))
                                   .ThrowsAsync(new Exception("Some error occurred"));

            // Act
            var result = await _postInteractionController.GetRecipeBookmarkCount(recipeId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().BeEquivalentTo(new { message = "Some error occurred" });
        }

    }

}
