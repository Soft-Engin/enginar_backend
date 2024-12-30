using Moq;
using Xunit;
using FluentAssertions;
using Models.DTO;
using Models.InteractionModels;
using DataAccess.Repositories;
using BackEngin.Services;
using System.Linq.Expressions;
using Models;
using System.Reflection.Metadata;
using BackEngin.Controllers;
using Microsoft.AspNetCore.Mvc;

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

        [Fact]
        public async Task CommentOnBlog_ShouldAddComment()
        {
            // Arrange
            var userId = "user123";
            var blogId = 1;
            var commentRequest = new CommentRequestDTO { Text = "Great blog!", Images = null };

            var timestamp = DateTime.UtcNow;

            _mockUnitOfWork.Setup(u => u.Blogs.FindAsync(It.IsAny<Func<Blogs, bool>>()))
                           .ReturnsAsync(new List<Blogs> { new Blogs { Id = blogId } });

            _mockUnitOfWork.Setup(u => u.Blog_Comments.AddAsync(It.IsAny<Blog_Comments>()))
                           .Callback<Blog_Comments>(comment => comment.CreatedAt = timestamp);

            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            _mockUnitOfWork.Setup(u => u.Blog_Comments.GetByIdAsync(It.IsAny<int>()))
                           .ReturnsAsync(new Blog_Comments
                           {
                               Id = 1,
                               BlogId = blogId,
                               UserId = userId,
                               CommentText = "Great blog!",
                               CreatedAt = timestamp
                           });

            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
                           .ReturnsAsync(new List<Users> { new Users { Id = userId, UserName = "TestUser" } });

            // Act
            var result = await _service.CommentOnBlog(userId, blogId, commentRequest);

            // Assert
            result.Should().NotBeNull();
            result.Text.Should().Be("Great blog!");
            result.Object_id.Should().Be(blogId);
            result.Timestamp.Should().Be(timestamp);
            result.UserId.Should().Be(userId);
            result.UserName.Should().Be("TestUser");

            _mockUnitOfWork.Verify(u => u.Blogs.FindAsync(It.IsAny<Func<Blogs, bool>>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.Blog_Comments.AddAsync(It.IsAny<Blog_Comments>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }
              


        [Fact]
        public async Task UpdateBlogComment_ShouldUpdateComment()
        {
            // Arrange
            var userId = "user123";
            var commentId = 1;
            var updatedComment = new CommentRequestDTO { Text = "Updated comment", Images = null };

            var existingComment = new Blog_Comments
            {
                Id = commentId,
                UserId = userId,
                BlogId = 10,
                CommentText = "Original comment",
                CreatedAt = DateTime.UtcNow
            };

            var user = new Users
            {
                Id = userId,
                UserName = "TestUser"
            };

            _mockUnitOfWork.Setup(u => u.Blog_Comments.GetByIdAsync(commentId))
                .ReturnsAsync(existingComment);

            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
                .ReturnsAsync(new List<Users> { user });

            _mockUnitOfWork.Setup(u => u.Blog_Comments.Update(existingComment)).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _service.UpdateBlogComment(userId, commentId, updatedComment);

            // Assert
            result.Should().NotBeNull();
            result.Text.Should().Be("Updated comment");
            result.UserId.Should().Be(userId);
            result.UserName.Should().Be("TestUser");
            _mockUnitOfWork.Verify(u => u.Blog_Comments.Update(existingComment), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
            _mockUnitOfWork.Verify(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()), Times.Once);
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

        [Fact]
        public async Task IsBlogLiked_ShouldReturnTrue_WhenLiked()
        {
            // Arrange
            var userId = "user123";
            var blogId = 1;

            _mockUnitOfWork.Setup(u => u.Blog_Likes.FindAsync(It.IsAny<Func<Blog_Likes, bool>>()))
                .ReturnsAsync(new List<Blog_Likes> { new Blog_Likes { BlogId = blogId, UserId = userId } });

            // Act
            var result = await _service.IsBlogLiked(userId, blogId);

            // Assert
            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.Blog_Likes.FindAsync(It.IsAny<Func<Blog_Likes, bool>>()), Times.Once);
        }

        [Fact]
        public async Task IsBlogLiked_ShouldReturnFalse_WhenNotLiked()
        {
            // Arrange
            var userId = "user123";
            var blogId = 1;

            _mockUnitOfWork.Setup(u => u.Blog_Likes.FindAsync(It.IsAny<Func<Blog_Likes, bool>>()))
                .ReturnsAsync(new List<Blog_Likes>());

            // Act
            var result = await _service.IsBlogLiked(userId, blogId);

            // Assert
            result.Should().BeFalse();
            _mockUnitOfWork.Verify(u => u.Blog_Likes.FindAsync(It.IsAny<Func<Blog_Likes, bool>>()), Times.Once);
        }


        [Fact]
        public async Task IsRecipeLiked_ShouldReturnTrue_WhenLiked()
        {
            // Arrange
            var userId = "user123";
            var recipeId = 1;

            _mockUnitOfWork.Setup(u => u.Recipe_Likes.FindAsync(It.IsAny<Func<Recipe_Likes, bool>>()))
                .ReturnsAsync(new List<Recipe_Likes> { new Recipe_Likes { RecipeId = recipeId, UserId = userId } });

            // Act
            var result = await _service.IsRecipeLiked(userId, recipeId);

            // Assert
            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.Recipe_Likes.FindAsync(It.IsAny<Func<Recipe_Likes, bool>>()), Times.Once);
        }

        [Fact]
        public async Task IsRecipeLiked_ShouldReturnFalse_WhenNotLiked()
        {
            // Arrange
            var userId = "user123";
            var recipeId = 1;

            _mockUnitOfWork.Setup(u => u.Recipe_Likes.FindAsync(It.IsAny<Func<Recipe_Likes, bool>>()))
                .ReturnsAsync(new List<Recipe_Likes>());

            // Act
            var result = await _service.IsRecipeLiked(userId, recipeId);

            // Assert
            result.Should().BeFalse();
            _mockUnitOfWork.Verify(u => u.Recipe_Likes.FindAsync(It.IsAny<Func<Recipe_Likes, bool>>()), Times.Once);
        }

        [Fact]
        public async Task GetBlogLikeCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var blogId = 1;
            _mockUnitOfWork.Setup(u => u.Blog_Likes.CountAsync(It.IsAny<Expression<Func<Blog_Likes, bool>>>()))
                .ReturnsAsync(5);

            // Act
            var result = await _service.GetBlogLikeCount(blogId);

            // Assert
            result.Should().Be(5);
            _mockUnitOfWork.Verify(u => u.Blog_Likes.CountAsync(It.IsAny<Expression<Func<Blog_Likes, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetRecipeLikeCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var recipeId = 1;
            _mockUnitOfWork.Setup(u => u.Recipe_Likes.CountAsync(It.IsAny<Expression<Func<Recipe_Likes, bool>>>()))
                .ReturnsAsync(3);

            // Act
            var result = await _service.GetRecipeLikeCount(recipeId);

            // Assert
            result.Should().Be(3);
            _mockUnitOfWork.Verify(u => u.Recipe_Likes.CountAsync(It.IsAny<Expression<Func<Recipe_Likes, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetBlogComments_ShouldReturnPaginatedComments()
        {
            // Arrange
            var blogId = 1;
            var pageNumber = 1;
            var pageSize = 2;
            var comments = new List<Blog_Comments>
                {
                    new Blog_Comments
                    {
                        Id = 1,
                        BlogId = blogId,
                        CommentText = "Comment 1",
                        UserId = "user1",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Blog_Comments
                    {
                        Id = 2,
                        BlogId = blogId,
                        CommentText = "Comment 2",
                        UserId = "user2",
                        CreatedAt = DateTime.UtcNow
                    }
            };

            _mockUnitOfWork.Setup(u => u.Blog_Comments.GetPaginatedAsync(It.IsAny<Expression<Func<Blog_Comments, bool>>>(), pageNumber, pageSize))
                .ReturnsAsync((comments, 10));

            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
            .ReturnsAsync(new List<Users>
            {
                new Users { Id = "user1", UserName = "User One" },
                new Users { Id = "user2", UserName = "User Two" }
            });

            // Act
            var result = await _service.GetBlogComments(blogId, pageNumber, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Count().Should().Be(2);
            result.TotalCount.Should().Be(10);

            var firstComment = result.Items.First();
            firstComment.UserName.Should().Be("User One");
            firstComment.UserId.Should().Be("user1");

            var secondComment = result.Items.Last();
            secondComment.UserName.Should().Be("User Two");
            secondComment.UserId.Should().Be("user2");

            _mockUnitOfWork.Verify(u => u.Blog_Comments.GetPaginatedAsync(
                It.IsAny<Expression<Func<Blog_Comments, bool>>>(), pageNumber, pageSize), Times.Once);

            _mockUnitOfWork.Verify(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()), Times.Once);
        }

        [Fact]
        public async Task GetRecipeComments_ShouldReturnPaginatedComments()
        {
            // Arrange
            var recipeId = 1;
            var pageNumber = 1;
            var pageSize = 2;
            var comments = new List<Recipe_Comments>
            {
                new Recipe_Comments
                {
                    Id = 1,
                    RecipeId = recipeId,
                    CommentText = "Comment 1",
                    UserId = "user1",
                    CreatedAt = DateTime.UtcNow
                },
                new Recipe_Comments
                {
                    Id = 2,
                    RecipeId = recipeId,
                    CommentText = "Comment 2",
                    UserId = "user2",
                    CreatedAt = DateTime.UtcNow
                }
            };

            _mockUnitOfWork.Setup(u => u.Recipe_Comments.GetPaginatedAsync(
                It.IsAny<Expression<Func<Recipe_Comments, bool>>>(), pageNumber, pageSize))
                .ReturnsAsync((comments, 8));

            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
            .ReturnsAsync(new List<Users>
            {
                new Users { Id = "user1", UserName = "User One" },
                new Users { Id = "user2", UserName = "User Two" }
            });

            // Act
            var result = await _service.GetRecipeComments(recipeId, pageNumber, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Count().Should().Be(2);
            result.TotalCount.Should().Be(8);

            var firstComment = result.Items.First();
            firstComment.UserName.Should().Be("User One");
            firstComment.UserId.Should().Be("user1");

            var secondComment = result.Items.Last();
            secondComment.UserName.Should().Be("User Two");
            secondComment.UserId.Should().Be("user2");

            _mockUnitOfWork.Verify(u => u.Recipe_Comments.GetPaginatedAsync(
                It.IsAny<Expression<Func<Recipe_Comments, bool>>>(), pageNumber, pageSize), Times.Once);

            _mockUnitOfWork.Verify(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()), Times.Once);
        }


        [Fact]
        public async Task GetBlogCommentImage_ShouldReturnImage_WhenExists()
        {
            // Arrange
            var commentId = 1;
            var imageIndex = 0;
            var expectedImage = new byte[] { 1, 2, 3, 4, 5 };
            var comment = new Blog_Comments 
            { 
                Id = commentId, 
                Images = new byte[][] { expectedImage },
                ImagesCount = 1
            };

            _mockUnitOfWork.Setup(u => u.Blog_Comments.GetByIdAsync(commentId))
                .ReturnsAsync(comment);

            // Act
            var result = await _service.GetBlogCommentImage(commentId, imageIndex);

            // Assert
            result.Should().BeEquivalentTo(expectedImage);
            _mockUnitOfWork.Verify(u => u.Blog_Comments.GetByIdAsync(commentId), Times.Once);
        }

        [Fact]
        public async Task GetBlogCommentImage_ShouldReturnNull_WhenCommentDoesNotExist()
        {
            // Arrange
            var commentId = 99;
            var imageIndex = 0;

            _mockUnitOfWork.Setup(u => u.Blog_Comments.GetByIdAsync(commentId))
                .ReturnsAsync((Blog_Comments)null);

            // Act
            var result = await _service.GetBlogCommentImage(commentId, imageIndex);

            // Assert
            result.Should().BeNull();
            _mockUnitOfWork.Verify(u => u.Blog_Comments.GetByIdAsync(commentId), Times.Once);
        }

        [Fact]
        public async Task GetBlogCommentImage_ShouldReturnNull_WhenImageIndexOutOfRange()
        {
            // Arrange
            var commentId = 1;
            var imageIndex = 1; // Out of range
            var comment = new Blog_Comments 
            { 
                Id = commentId, 
                Images = new byte[][] { new byte[] { 1, 2, 3 } },
                ImagesCount = 1
            };

            _mockUnitOfWork.Setup(u => u.Blog_Comments.GetByIdAsync(commentId))
                .ReturnsAsync(comment);

            // Act
            var result = await _service.GetBlogCommentImage(commentId, imageIndex);

            // Assert
            result.Should().BeNull();
            _mockUnitOfWork.Verify(u => u.Blog_Comments.GetByIdAsync(commentId), Times.Once);
        }

        [Fact]
        public async Task GetRecipeCommentImage_ShouldReturnImage_WhenExists()
        {
            // Arrange
            var commentId = 1;
            var imageIndex = 0;
            var expectedImage = new byte[] { 1, 2, 3, 4, 5 };
            var comment = new Recipe_Comments 
            { 
                Id = commentId, 
                Images = new byte[][] { expectedImage },
                ImagesCount = 1
            };

            _mockUnitOfWork.Setup(u => u.Recipe_Comments.GetByIdAsync(commentId))
                .ReturnsAsync(comment);

            // Act
            var result = await _service.GetRecipeCommentImage(commentId, imageIndex);

            // Assert
            result.Should().BeEquivalentTo(expectedImage);
            _mockUnitOfWork.Verify(u => u.Recipe_Comments.GetByIdAsync(commentId), Times.Once);
        }

        [Fact]
        public async Task GetRecipeCommentImage_ShouldReturnNull_WhenCommentDoesNotExist()
        {
            // Arrange
            var commentId = 99;
            var imageIndex = 0;

            _mockUnitOfWork.Setup(u => u.Recipe_Comments.GetByIdAsync(commentId))
                .ReturnsAsync((Recipe_Comments)null);

            // Act
            var result = await _service.GetRecipeCommentImage(commentId, imageIndex);

            // Assert
            result.Should().BeNull();
            _mockUnitOfWork.Verify(u => u.Recipe_Comments.GetByIdAsync(commentId), Times.Once);
        }

        [Fact]
        public async Task GetRecipeCommentImage_ShouldReturnNull_WhenImageIndexOutOfRange()
        {
            // Arrange
            var commentId = 1;
            var imageIndex = 1; // Out of range
            var comment = new Recipe_Comments 
            { 
                Id = commentId, 
                Images = new byte[][] { new byte[] { 1, 2, 3 } },
                ImagesCount = 1
            };

            _mockUnitOfWork.Setup(u => u.Recipe_Comments.GetByIdAsync(commentId))
                .ReturnsAsync(comment);

            // Act
            var result = await _service.GetRecipeCommentImage(commentId, imageIndex);

            // Assert
            result.Should().BeNull();
            _mockUnitOfWork.Verify(u => u.Recipe_Comments.GetByIdAsync(commentId), Times.Once);
        }


        // TESTS FOR EVENTS INTERACTIONS
        [Fact]
        public async Task ToggleLikeEvent_ShouldLike_WhenNotLiked()
        {
            // Arrange
            var userId = "user123";
            var eventId = 1;

            _mockUnitOfWork.Setup(u => u.Event_Likes.FindAsync(It.IsAny<Func<Event_Likes, bool>>()))
                .ReturnsAsync(new List<Event_Likes>());

            _mockUnitOfWork.Setup(u => u.Event_Likes.AddAsync(It.IsAny<Event_Likes>())).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _service.ToggleLikeEvent(userId, eventId);

            // Assert
            result.Should().BeTrue(); // Should indicate a like action
            _mockUnitOfWork.Verify(u => u.Event_Likes.AddAsync(It.IsAny<Event_Likes>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task ToggleLikeEvent_ShouldUnlike_WhenAlreadyLiked()
        {
            // Arrange
            var userId = "user123";
            var eventId = 1;

            var existingLike = new Event_Likes { Id = 1, UserId = userId, EventId = eventId };
            _mockUnitOfWork.Setup(u => u.Event_Likes.FindAsync(It.IsAny<Func<Event_Likes, bool>>()))
                .ReturnsAsync(new List<Event_Likes> { existingLike });

            _mockUnitOfWork.Setup(u => u.Event_Likes.Delete(existingLike)).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _service.ToggleLikeEvent(userId, eventId);

            // Assert
            result.Should().BeFalse(); // Should indicate an unlike action
            _mockUnitOfWork.Verify(u => u.Event_Likes.Delete(existingLike), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task CommentOnEvent_ShouldAddComment()
        {
            // Arrange
            var userId = "user123";
            var eventId = 1;
            var commentRequest = new CommentRequestDTO { Text = "Great event!", Images = null };

            var timestamp = DateTime.UtcNow;

            _mockUnitOfWork.Setup(u => u.Events.FindAsync(It.IsAny<Func<Events, bool>>()))
                .ReturnsAsync(new List<Events> { new Events { Id = eventId } });

            _mockUnitOfWork.Setup(u => u.Event_Comments.AddAsync(It.IsAny<Event_Comments>()))
                .Callback<Event_Comments>(comment => comment.CreatedAt = timestamp);

            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            _mockUnitOfWork.Setup(u => u.Event_Comments.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Event_Comments
                {
                    Id = 1,
                    EventId = eventId,
                    UserId = userId,
                    CommentText = "Great event!",
                    CreatedAt = timestamp
                });

            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
                .ReturnsAsync(new List<Users> { new Users { Id = userId, UserName = "TestUser" } });

            // Act
            var result = await _service.CommentOnEvent(userId, eventId, commentRequest);

            // Assert
            result.Should().NotBeNull();
            result.Text.Should().Be("Great event!");
            result.Object_id.Should().Be(eventId);
            result.Timestamp.Should().Be(timestamp);
            result.UserId.Should().Be(userId);
            result.UserName.Should().Be("TestUser");

            _mockUnitOfWork.Verify(u => u.Events.FindAsync(It.IsAny<Func<Events, bool>>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.Event_Comments.AddAsync(It.IsAny<Event_Comments>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateEventComment_ShouldUpdateComment()
        {
            // Arrange
            var userId = "user123";
            var commentId = 1;
            var updatedComment = new CommentRequestDTO { Text = "Updated event comment", Images = null };

            var existingComment = new Event_Comments
            {
                Id = commentId,
                UserId = userId,
                EventId = 10,
                CommentText = "Original comment",
                Images = null
            };

            _mockUnitOfWork.Setup(u => u.Event_Comments.GetByIdAsync(commentId))
                .ReturnsAsync(existingComment); // Ensure a non-null comment is returned

            _mockUnitOfWork.Setup(u => u.Event_Comments.Update(It.IsAny<Event_Comments>())).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _service.UpdateEventComment(userId, commentId, updatedComment);

            // Assert
            result.Should().NotBeNull();
            result.Text.Should().Be("Updated event comment");
            result.UserId.Should().Be(userId);
            result.Object_id.Should().Be(existingComment.EventId);
            _mockUnitOfWork.Verify(u => u.Event_Comments.Update(It.IsAny<Event_Comments>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteEventComment_ShouldDeleteComment()
        {
            // Arrange
            var userId = "user123";
            var commentId = 1;

            var existingComment = new Event_Comments
            {
                Id = commentId,
                UserId = userId,
                CommentText = "Comment to be deleted"
            };

            _mockUnitOfWork.Setup(u => u.Event_Comments.GetByIdAsync(commentId))
                .ReturnsAsync(existingComment);

            _mockUnitOfWork.Setup(u => u.Event_Comments.Delete(existingComment)).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            await _service.DeleteEventComment(userId, commentId);

            // Assert
            _mockUnitOfWork.Verify(u => u.Event_Comments.Delete(existingComment), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }


        [Fact]
        public async Task GetEventComments_ShouldReturnPaginatedComments()
        {
            // Arrange
            var eventId = 1;
            var pageNumber = 1;
            var pageSize = 2;
            var comments = new List<Event_Comments>
            {
                new Event_Comments
                {
                    Id = 1,
                    EventId = eventId,
                    CommentText = "Comment 1",
                    UserId = "user1",
                    CreatedAt = DateTime.UtcNow
                },
                new Event_Comments
                {
                    Id = 2,
                    EventId = eventId,
                    CommentText = "Comment 2",
                    UserId = "user2",
                    CreatedAt = DateTime.UtcNow
                }
            };

            _mockUnitOfWork.Setup(u => u.Event_Comments.GetPaginatedAsync(
                It.IsAny<Expression<Func<Event_Comments, bool>>>(), pageNumber, pageSize))
                .ReturnsAsync((comments, 8));

            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
                .ReturnsAsync(new List<Users>
                {
                    new Users { Id = "user1", UserName = "User One" },
                    new Users { Id = "user2", UserName = "User Two" }
                });

            // Act
            var result = await _service.GetEventComments(eventId, pageNumber, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Count().Should().Be(2);
            result.TotalCount.Should().Be(8);

            var firstComment = result.Items.First();
            firstComment.UserName.Should().Be("User One");
            firstComment.UserId.Should().Be("user1");

            var secondComment = result.Items.Last();
            secondComment.UserName.Should().Be("User Two");
            secondComment.UserId.Should().Be("user2");

            _mockUnitOfWork.Verify(u => u.Event_Comments.GetPaginatedAsync(
                It.IsAny<Expression<Func<Event_Comments, bool>>>(), pageNumber, pageSize), Times.Once);

            _mockUnitOfWork.Verify(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()), Times.Once);
        }

        [Fact]
        public async Task GetEventLikeCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var eventId = 1;
            var likeCount = 5;

            _mockUnitOfWork.Setup(u => u.Event_Likes.CountAsync(It.IsAny<Expression<Func<Event_Likes, bool>>>()))
                .ReturnsAsync(likeCount);

            // Act
            var result = await _service.GetEventLikeCount(eventId);

            // Assert
            result.Should().Be(likeCount);
            _mockUnitOfWork.Verify(u => u.Event_Likes.CountAsync(It.Is<Expression<Func<Event_Likes, bool>>>(expr => expr.Compile()(new Event_Likes { EventId = eventId }))), Times.Once);
        }

        [Fact]
        public async Task GetEventBookmarkCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var eventId = 1;
            var bookmarkCount = 3;

            _mockUnitOfWork.Setup(u => u.Event_Bookmarks.CountAsync(It.IsAny<Expression<Func<Event_Bookmarks, bool>>>()))
                .ReturnsAsync(bookmarkCount);

            // Act
            var result = await _service.GetEventBookmarkCount(eventId);

            // Assert
            result.Should().Be(bookmarkCount);
            _mockUnitOfWork.Verify(u => u.Event_Bookmarks.CountAsync(It.Is<Expression<Func<Event_Bookmarks, bool>>>(expr => expr.Compile()(new Event_Bookmarks { EventId = eventId }))), Times.Once);
        }

        [Fact]
        public async Task GetEventCommentImage_ShouldReturnImage_WhenExists()
        {
            // Arrange
            var commentId = 1;
            var imageIndex = 0;
            var expectedImage = new byte[] { 1, 2, 3, 4, 5 };
            var comment = new Event_Comments
            {
                Id = commentId,
                Images = new byte[][] { expectedImage },
                ImagesCount = 1
            };

            _mockUnitOfWork.Setup(u => u.Event_Comments.GetByIdAsync(commentId))
                .ReturnsAsync(comment);

            // Act
            var result = await _service.GetEventCommentImage(commentId, imageIndex);

            // Assert
            result.Should().BeEquivalentTo(expectedImage);
            _mockUnitOfWork.Verify(u => u.Event_Comments.GetByIdAsync(commentId), Times.Once);
        }

        [Fact]
        public async Task GetEventCommentImage_ShouldReturnNull_WhenImageDoesNotExist()
        {
            // Arrange
            var commentId = 1;
            var imageIndex = 2; // Invalid index
            var comment = new Event_Comments
            {
                Id = commentId,
                Images = new byte[][] { new byte[] { 1, 2, 3 } },
                ImagesCount = 1
            };

            _mockUnitOfWork.Setup(u => u.Event_Comments.GetByIdAsync(commentId))
                .ReturnsAsync(comment);

            // Act
            var result = await _service.GetEventCommentImage(commentId, imageIndex);

            // Assert
            result.Should().BeNull();
            _mockUnitOfWork.Verify(u => u.Event_Comments.GetByIdAsync(commentId), Times.Once);
        }
    }

}
