using BackEngin.Controllers;
using BackEngin.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Models.DTO;
using System.Security.Claims;
using Xunit;
using BackEngin.Services;


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
                Object_id = blogId,
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
                Object_id = 1,
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
        public async Task DeleteEventComment_ShouldReturnOk_WhenAuthorizedAndDeleted()
        {
            // Arrange
            var commentId = 1;
            var userId = "currentUserId";

            _mockInteractionService.Setup(s => s.GetOwnerId(commentId, ObjectType.EventComment))
                .ReturnsAsync(userId);

            _mockInteractionService.Setup(s => s.DeleteEventComment(userId, commentId))
                .Verifiable();

            // Act
            var result = await _postInteractionController.DeleteEventComment(commentId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { message = "Event comment deleted successfully." });
            _mockInteractionService.Verify(s => s.DeleteEventComment(userId, commentId), Times.Once);
        }

        [Fact]
        public async Task DeleteEventComment_ShouldReturnUnauthorized_WhenNotOwner()
        {
            // Arrange
            var commentId = 1;
            var userId = "currentUserId";
            var differentUserId = "differentUserId";

            _mockInteractionService.Setup(s => s.GetOwnerId(commentId, ObjectType.EventComment))
                .ReturnsAsync(differentUserId);

            // Act
            var result = await _postInteractionController.DeleteEventComment(commentId);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();
            var unauthorizedResult = result as UnauthorizedObjectResult;
            unauthorizedResult.Value.Should().BeEquivalentTo(new { message = "You are not authorized to delete this event." });
            _mockInteractionService.Verify(s => s.DeleteEventComment(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task DeleteRecipeComment_ShouldReturnOk_WhenAuthorizedAndDeleted()
        {
            // Arrange
            var commentId = 1;
            var userId = "currentUserId";

            _mockInteractionService.Setup(s => s.GetOwnerId(commentId, ObjectType.RecipeComment))
                .ReturnsAsync(userId);

            _mockInteractionService.Setup(s => s.DeleteRecipeComment(userId, commentId))
                .Verifiable();

            // Act
            var result = await _postInteractionController.DeleteRecipeComment(commentId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { message = "Recipe comment deleted successfully." });
            _mockInteractionService.Verify(s => s.DeleteRecipeComment(userId, commentId), Times.Once);
        }

        [Fact]
        public async Task DeleteRecipeComment_ShouldReturnUnauthorized_WhenNotOwner()
        {
            // Arrange
            var commentId = 1;
            var userId = "currentUserId";
            var differentUserId = "differentUserId";

            _mockInteractionService.Setup(s => s.GetOwnerId(commentId, ObjectType.RecipeComment))
                .ReturnsAsync(differentUserId);

            // Act
            var result = await _postInteractionController.DeleteRecipeComment(commentId);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();
            var unauthorizedResult = result as UnauthorizedObjectResult;
            unauthorizedResult.Value.Should().BeEquivalentTo(new { message = "You are not authorized to delete this event." });
            _mockInteractionService.Verify(s => s.DeleteRecipeComment(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
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
            new CommentDTO { Id = 1, Object_id = blogId, Text = "Comment 1", Timestamp = DateTime.UtcNow },
            new CommentDTO { Id = 2, Object_id = blogId, Text = "Comment 2", Timestamp = DateTime.UtcNow }
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
            new CommentDTO { Id = 1, Object_id = recipeId, Text = "Comment 1", Timestamp = DateTime.UtcNow },
            new CommentDTO { Id = 2, Object_id = recipeId, Text = "Comment 2", Timestamp = DateTime.UtcNow }
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

        [Fact]
        public async Task GetBlogCommentImage_ShouldReturnFileResult_WhenImageExists()
        {
            // Arrange
            var commentId = 1;
            var imageIndex = 0;
            var imageData = new byte[] { 1, 2, 3, 4, 5 };

            _mockInteractionService.Setup(s => s.GetBlogCommentImage(commentId, imageIndex))
                .ReturnsAsync(imageData);

            // Act
            var result = await _postInteractionController.GetBlogCommentImage(commentId, imageIndex);

            // Assert
            var fileResult = result.Should().BeOfType<FileContentResult>().Which;
            fileResult.FileContents.Should().BeEquivalentTo(imageData);
            fileResult.ContentType.Should().Be("image/jpeg");
        }

        [Fact]
        public async Task GetBlogCommentImage_ShouldReturnNotFound_WhenImageDoesNotExist()
        {
            // Arrange
            var commentId = 1;
            var imageIndex = 0;
            _mockInteractionService.Setup(s => s.GetBlogCommentImage(commentId, imageIndex))
                .ReturnsAsync((byte[])null);

            // Act
            var result = await _postInteractionController.GetBlogCommentImage(commentId, imageIndex);

            // Assert
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Which;
            notFoundResult.Value.Should().BeEquivalentTo(new { message = $"Image {imageIndex} not found for blog comment {commentId}." });
        }

        [Fact]
        public async Task GetRecipeCommentImage_ShouldReturnFileResult_WhenImageExists()
        {
            // Arrange
            var commentId = 1;
            var imageIndex = 0;
            var imageData = new byte[] { 1, 2, 3, 4, 5 };

            _mockInteractionService.Setup(s => s.GetBlogCommentImage(commentId, imageIndex))
                .ReturnsAsync((byte[])null);
            _mockInteractionService.Setup(s => s.GetRecipeCommentImage(commentId, imageIndex))
                .ReturnsAsync(imageData);

            // Act
            var result = await _postInteractionController.GetRecipeCommentImage(commentId, imageIndex);

            // Assert
            var fileResult = result.Should().BeOfType<FileContentResult>().Which;
            fileResult.FileContents.Should().BeEquivalentTo(imageData);
            fileResult.ContentType.Should().Be("image/jpeg");
        }

        [Fact]
        public async Task GetRecipeCommentImage_ShouldReturnNotFound_WhenImageDoesNotExist()
        {
            // Arrange
            var commentId = 1;
            var imageIndex = 0;
            _mockInteractionService.Setup(s => s.GetRecipeCommentImage(commentId, imageIndex))
                .ReturnsAsync((byte[])null);
            _mockInteractionService.Setup(s => s.GetBlogCommentImage(commentId, imageIndex))
                .ReturnsAsync((byte[])null);

            // Act
            var result = await _postInteractionController.GetRecipeCommentImage(commentId, imageIndex);

            // Assert
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Which;
            notFoundResult.Value.Should().BeEquivalentTo(new { message = $"Image {imageIndex} not found for recipe comment {commentId}." });
        }


        // TESTS FOR EVENT INTERACTIONS

        [Fact]
        public async Task ToggleLikeEvent_ShouldReturnOk_WhenLiked()
        {
            // Arrange
            var eventId = 1;
            _mockInteractionService.Setup(s => s.ToggleLikeEvent("currentUserId", eventId))
                                   .ReturnsAsync(true);

            // Act
            var result = await _postInteractionController.ToggleLikeEvent(eventId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { message = "Event liked successfully." });
        }

        [Fact]
        public async Task ToggleLikeEvent_ShouldReturnOk_WhenUnliked()
        {
            // Arrange
            var eventId = 1;
            _mockInteractionService.Setup(s => s.ToggleLikeEvent("currentUserId", eventId))
                                   .ReturnsAsync(false);

            // Act
            var result = await _postInteractionController.ToggleLikeEvent(eventId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { message = "Event unliked successfully." });
        }

        [Fact]
        public async Task ToggleBookmarkEvent_ShouldReturnOk_WhenBookmarked()
        {
            // Arrange
            var eventId = 1;
            _mockInteractionService.Setup(s => s.ToggleBookmarkEvent("currentUserId", eventId))
                                   .ReturnsAsync(true);

            // Act
            var result = await _postInteractionController.BookmarkEvent(eventId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { message = "Event bookmarked successfully." });
        }

        [Fact]
        public async Task ToggleBookmarkEvent_ShouldReturnOk_WhenUnbookmarked()
        {
            // Arrange
            var eventId = 1;
            _mockInteractionService.Setup(s => s.ToggleBookmarkEvent("currentUserId", eventId))
                                   .ReturnsAsync(false);

            // Act
            var result = await _postInteractionController.BookmarkEvent(eventId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { message = "Event unbookmarked successfully." });
        }

        [Fact]
        public async Task CommentOnEvent_ShouldReturnOk_WithComment()
        {
            // Arrange
            var eventId = 1;
            var commentRequest = new CommentRequestDTO { Text = "Great event!", Images = null };
            var commentResponse = new CommentDTO
            {
                Id = 1,
                Object_id = eventId,
                Text = "Great event!",
                Timestamp = DateTime.UtcNow
            };

            _mockInteractionService.Setup(s => s.CommentOnEvent("currentUserId", eventId, commentRequest))
                                   .ReturnsAsync(commentResponse);

            // Act
            var result = await _postInteractionController.CommentOnEvent(eventId, commentRequest);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(commentResponse);
        }

        [Fact]
        public async Task UpdateEventComment_ShouldReturnOk_WithUpdatedComment()
        {
            // Arrange
            var commentId = 1;
            var commentRequest = new CommentRequestDTO { Text = "Updated event comment", Images = null };
            var updatedCommentResponse = new CommentDTO
            {
                Id = commentId,
                Object_id = 1,
                Text = "Updated event comment",
                Timestamp = DateTime.UtcNow
            };

            _mockInteractionService.Setup(s => s.UpdateEventComment("currentUserId", commentId, commentRequest))
                                   .ReturnsAsync(updatedCommentResponse);

            // Act
            var result = await _postInteractionController.UpdateEventComment(commentId, commentRequest);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(updatedCommentResponse);
        }


        [Fact]
        public async Task GetEventLikeCount_ShouldReturnOk_WithLikeCount()
        {
            // Arrange
            var eventId = 1;
            var likeCount = 10;
            _mockInteractionService.Setup(s => s.GetEventLikeCount(eventId))
                                   .ReturnsAsync(likeCount);

            // Act
            var result = await _postInteractionController.GetEventLikeCount(eventId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { likeCount });
        }

        [Fact]
        public async Task GetEventBookmarkCount_ShouldReturnOk_WithBookmarkCount()
        {
            // Arrange
            var eventId = 1;
            var bookmarkCount = 8;
            _mockInteractionService.Setup(s => s.GetEventBookmarkCount(eventId))
                                   .ReturnsAsync(bookmarkCount);

            // Act
            var result = await _postInteractionController.GetEventBookmarkCount(eventId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { bookmarkCount });
        }

        [Fact]
        public async Task GetEventCommentImage_ShouldReturnFileResult_WhenImageExists()
        {
            // Arrange
            var commentId = 1;
            var imageIndex = 0;
            var imageData = new byte[] { 1, 2, 3, 4, 5 };

            _mockInteractionService.Setup(s => s.GetEventCommentImage(commentId, imageIndex))
                .ReturnsAsync(imageData);

            // Act
            var result = await _postInteractionController.GetEventCommentImage(commentId, imageIndex);

            // Assert
            var fileResult = result.Should().BeOfType<FileContentResult>().Which;
            fileResult.FileContents.Should().BeEquivalentTo(imageData);
            fileResult.ContentType.Should().Be("image/jpeg");
        }

        [Fact]
        public async Task GetEventCommentImage_ShouldReturnNotFound_WhenImageDoesNotExist()
        {
            // Arrange
            var commentId = 1;
            var imageIndex = 0;
            _mockInteractionService.Setup(s => s.GetEventCommentImage(commentId, imageIndex))
                .ReturnsAsync((byte[])null);

            // Act
            var result = await _postInteractionController.GetEventCommentImage(commentId, imageIndex);

            // Assert
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Which;
            notFoundResult.Value.Should().BeEquivalentTo(new { message = $"Image {imageIndex} not found for event comment {commentId}." });
        }
    }

}
