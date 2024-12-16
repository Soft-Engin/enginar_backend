using Moq;
using Xunit;
using FluentAssertions;
using Models.DTO;
using Models.InteractionModels;
using DataAccess.Repositories;
using BackEngin.Services;

namespace BackEngin.Tests.Services
{
    public class PostInteractionServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly PostInteractionService _service;

        public PostInteractionServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new PostInteractionService(_mockUnitOfWork.Object);
        }

        // Test for ToggleLikeBlog
        [Fact]
        public async Task ToggleLikeBlog_ShouldLike_WhenNotLiked()
        {
            // Arrange
            var userId = "user123";
            var blogId = 1;

            _mockUnitOfWork.Setup(u => u.Blog_Likes.FindAsync(It.IsAny<Func<Blog_Likes, bool>>()))
                .ReturnsAsync(new List<Blog_Likes>());

            _mockUnitOfWork.Setup(u => u.Blog_Likes.AddAsync(It.IsAny<Blog_Likes>())).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _service.ToggleLikeBlog(userId, blogId);

            // Assert
            result.Should().BeTrue(); // Should indicate a like action
            _mockUnitOfWork.Verify(u => u.Blog_Likes.AddAsync(It.IsAny<Blog_Likes>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task ToggleLikeBlog_ShouldUnlike_WhenAlreadyLiked()
        {
            // Arrange
            var userId = "user123";
            var blogId = 1;

            var existingLike = new Blog_Likes { Id = 1, UserId = userId, BlogId = blogId };
            _mockUnitOfWork.Setup(u => u.Blog_Likes.FindAsync(It.IsAny<Func<Blog_Likes, bool>>()))
                .ReturnsAsync(new List<Blog_Likes> { existingLike });

            _mockUnitOfWork.Setup(u => u.Blog_Likes.Delete(existingLike)).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _service.ToggleLikeBlog(userId, blogId);

            // Assert
            result.Should().BeFalse(); // Should indicate an unlike action
            _mockUnitOfWork.Verify(u => u.Blog_Likes.Delete(existingLike), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        // Test for CommentOnBlog
        [Fact]
        public async Task CommentOnBlog_ShouldAddComment()
        {
            // Arrange
            var userId = "user123";
            var blogId = 1;
            var commentRequest = new CommentRequestDTO { Text = "Great blog!", Image = null };

            _mockUnitOfWork.Setup(u => u.Blog_Comments.AddAsync(It.IsAny<Blog_Comments>())).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _service.CommentOnBlog(userId, blogId, commentRequest);

            // Assert
            result.Should().NotBeNull();
            result.Text.Should().Be("Great blog!");
            result.Recipe_blog_id.Should().Be(blogId);
            _mockUnitOfWork.Verify(u => u.Blog_Comments.AddAsync(It.IsAny<Blog_Comments>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        // Test for UpdateBlogComment
        [Fact]
        public async Task UpdateBlogComment_ShouldUpdateComment()
        {
            // Arrange
            var userId = "user123";
            var commentId = 1;
            var updatedComment = new CommentRequestDTO { Text = "Updated comment", Image = null };

            var existingComment = new Blog_Comments
            {
                Id = commentId,
                UserId = userId,
                CommentText = "Original comment"
            };

            _mockUnitOfWork.Setup(u => u.Blog_Comments.GetByIdAsync(commentId))
                .ReturnsAsync(existingComment);

            _mockUnitOfWork.Setup(u => u.Blog_Comments.Update(existingComment)).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _service.UpdateBlogComment(userId, commentId, updatedComment);

            // Assert
            result.Should().NotBeNull();
            result.Text.Should().Be("Updated comment");
            _mockUnitOfWork.Verify(u => u.Blog_Comments.Update(existingComment), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        // Test for DeleteBlogComment
        [Fact]
        public async Task DeleteBlogComment_ShouldRemoveComment()
        {
            // Arrange
            var userId = "user123";
            var commentId = 1;

            var existingComment = new Blog_Comments { Id = commentId, UserId = userId };
            _mockUnitOfWork.Setup(u => u.Blog_Comments.GetByIdAsync(commentId))
                .ReturnsAsync(existingComment);

            _mockUnitOfWork.Setup(u => u.Blog_Comments.Delete(existingComment)).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            await _service.DeleteBlogComment(userId, commentId);

            // Assert
            _mockUnitOfWork.Verify(u => u.Blog_Comments.Delete(existingComment), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        // Test for ToggleBookmarkRecipe
        [Fact]
        public async Task ToggleBookmarkRecipe_ShouldBookmark_WhenNotBookmarked()
        {
            // Arrange
            var userId = "user123";
            var recipeId = 1;

            _mockUnitOfWork.Setup(u => u.Recipe_Bookmarks.FindAsync(It.IsAny<Func<Recipe_Bookmarks, bool>>()))
                .ReturnsAsync(new List<Recipe_Bookmarks>());

            _mockUnitOfWork.Setup(u => u.Recipe_Bookmarks.AddAsync(It.IsAny<Recipe_Bookmarks>())).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _service.ToggleBookmarkRecipe(userId, recipeId);

            // Assert
            result.Should().BeTrue(); // Indicates a bookmark action
            _mockUnitOfWork.Verify(u => u.Recipe_Bookmarks.AddAsync(It.IsAny<Recipe_Bookmarks>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task ToggleBookmarkRecipe_ShouldUnbookmark_WhenAlreadyBookmarked()
        {
            // Arrange
            var userId = "user123";
            var recipeId = 1;

            var existingBookmark = new Recipe_Bookmarks { Id = 1, UserId = userId, RecipeId = recipeId };
            _mockUnitOfWork.Setup(u => u.Recipe_Bookmarks.FindAsync(It.IsAny<Func<Recipe_Bookmarks, bool>>()))
                .ReturnsAsync(new List<Recipe_Bookmarks> { existingBookmark });

            _mockUnitOfWork.Setup(u => u.Recipe_Bookmarks.Delete(existingBookmark)).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _service.ToggleBookmarkRecipe(userId, recipeId);

            // Assert
            result.Should().BeFalse(); // Indicates an unbookmark action
            _mockUnitOfWork.Verify(u => u.Recipe_Bookmarks.Delete(existingBookmark), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }
    }

}
