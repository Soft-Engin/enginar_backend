using DataAccess.Repositories;
using Models.InteractionModels;
using Models;
using BackEngin.Services.Interfaces;
using Models.DTO;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace BackEngin.Services
{
    public class PostInteractionService : IPostInteractionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostInteractionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Toggle Like for Blog
        public async Task<bool> ToggleLikeBlog(string userId, int blogId)
        {
            var existingLike = await _unitOfWork.Blog_Likes.FindAsync(l => l.UserId == userId && l.BlogId == blogId);

            if (existingLike.Any())
            {
                // Unlike if it already exists
                _unitOfWork.Blog_Likes.Delete(existingLike.First());
                await _unitOfWork.CompleteAsync();
                return false; // Unlike
            }
            else
            {
                // Like if it doesn't exist
                var blogLike = new Blog_Likes
                {
                    UserId = userId,
                    BlogId = blogId,
                    CreatedAt = DateTime.UtcNow
                };
                await _unitOfWork.Blog_Likes.AddAsync(blogLike);
                await _unitOfWork.CompleteAsync();
                return true; // Like
            }
        }

        // Toggle Like for Recipe
        public async Task<bool> ToggleLikeRecipe(string userId, int recipeId)
        {
            var existingLike = await _unitOfWork.Recipe_Likes.FindAsync(l => l.UserId == userId && l.RecipeId == recipeId);

            if (existingLike.Any())
            {
                // Unlike if it already exists
                _unitOfWork.Recipe_Likes.Delete(existingLike.First());
                await _unitOfWork.CompleteAsync();
                return false; // Unlike
            }
            else
            {
                // Like if it doesn't exist
                var recipeLike = new Recipe_Likes
                {
                    UserId = userId,
                    RecipeId = recipeId,
                    CreatedAt = DateTime.UtcNow
                };
                await _unitOfWork.Recipe_Likes.AddAsync(recipeLike);
                await _unitOfWork.CompleteAsync();
                return true; // Like
            }
        }

        // Toggle Bookmark for Blog
        public async Task<bool> ToggleBookmarkBlog(string userId, int blogId)
        {
            var existingBookmark = await _unitOfWork.Blog_Bookmarks.FindAsync(b => b.UserId == userId && b.BlogId == blogId);

            if (existingBookmark.Any())
            {
                // Remove bookmark if it exists
                _unitOfWork.Blog_Bookmarks.Delete(existingBookmark.First());
                await _unitOfWork.CompleteAsync();
                return false; // Unbookmark
            }
            else
            {
                // Add bookmark if it doesn't exist
                var bookmark = new Blog_Bookmarks
                {
                    UserId = userId,
                    BlogId = blogId,
                    CreatedAt = DateTime.UtcNow
                };
                await _unitOfWork.Blog_Bookmarks.AddAsync(bookmark);
                await _unitOfWork.CompleteAsync();
                return true; // bookmark
            }            
        }

        // Toggle Bookmark for Recipe
        public async Task<bool> ToggleBookmarkRecipe(string userId, int recipeId)
        {
            var existingBookmark = await _unitOfWork.Recipe_Bookmarks.FindAsync(b => b.UserId == userId && b.RecipeId == recipeId);

            if (existingBookmark.Any())
            {
                // Remove bookmark if it exists
                _unitOfWork.Recipe_Bookmarks.Delete(existingBookmark.First());
                await _unitOfWork.CompleteAsync();
                return false; // Unbookmark
            }
            else
            {
                // Add bookmark if it doesn't exist
                var bookmark = new Recipe_Bookmarks
                {
                    UserId = userId,
                    RecipeId = recipeId,
                    CreatedAt = DateTime.UtcNow
                };
                await _unitOfWork.Recipe_Bookmarks.AddAsync(bookmark);
                await _unitOfWork.CompleteAsync();
                return true; // bookmark
            }
        }


        // Comment on Blog
        public async Task<CommentDTO> CommentOnBlog(string userId, int blogId, CommentRequestDTO commentRequest)
        {
            var comment = new Blog_Comments
            {
                UserId = userId,
                BlogId = blogId,
                CommentText = commentRequest.Text,
                ImageBlob = commentRequest.Image,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Blog_Comments.AddAsync(comment);
            await _unitOfWork.CompleteAsync();

            return new CommentDTO
            {
                Id = comment.Id,
                Recipe_blog_id = comment.BlogId,
                Text = comment.CommentText,
                Image = comment.ImageBlob,
                Timestamp = comment.CreatedAt
            };
        }

        // Comment on Recipe
        public async Task<CommentDTO> CommentOnRecipe(string userId, int recipeId, CommentRequestDTO commentRequest)
        {
            var comment = new Recipe_Comments
            {
                UserId = userId,
                RecipeId = recipeId,
                CommentText = commentRequest.Text,
                ImageBlob = commentRequest.Image,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Recipe_Comments.AddAsync(comment);
            await _unitOfWork.CompleteAsync();

            return new CommentDTO
            {
                Id = comment.Id,
                Recipe_blog_id = comment.RecipeId,
                Text = comment.CommentText,
                Image = comment.ImageBlob,
                Timestamp = comment.CreatedAt
            };
        }

        // Update Blog Comment
        public async Task<CommentDTO> UpdateBlogComment(string userId, int commentId, CommentRequestDTO commentRequest)
        {
            var comment = await _unitOfWork.Blog_Comments.GetByIdAsync(commentId);
            if (comment == null || comment.UserId != userId)
                throw new UnauthorizedAccessException("You cannot update this comment.");

            comment.CommentText = commentRequest.Text;
            comment.CreatedAt = DateTime.UtcNow;
            comment.ImageBlob = commentRequest.Image;

            _unitOfWork.Blog_Comments.Update(comment);
            await _unitOfWork.CompleteAsync();

            return new CommentDTO
            {
                Id = comment.Id,
                Text = comment.CommentText,
                Image = comment.ImageBlob,
                Recipe_blog_id = comment.BlogId,
                Timestamp = comment.CreatedAt
            };
        }

        // Update Recipe Comment
        public async Task<CommentDTO> UpdateRecipeComment(string userId, int commentId, CommentRequestDTO commentRequest)
        {
            var comment = await _unitOfWork.Recipe_Comments.GetByIdAsync(commentId);
            if (comment == null || comment.UserId != userId)
                throw new UnauthorizedAccessException("You cannot update this comment.");

            comment.CommentText = commentRequest.Text;
            comment.CreatedAt = DateTime.UtcNow;
            comment.ImageBlob = commentRequest.Image;

            _unitOfWork.Recipe_Comments.Update(comment);
            await _unitOfWork.CompleteAsync();

            return new CommentDTO
            {
                Id = comment.Id,
                Text = comment.CommentText,
                Image = comment.ImageBlob,
                Recipe_blog_id = comment.RecipeId,
                Timestamp = comment.CreatedAt
            };
        }

        // Delete Blog Comment
        public async Task DeleteBlogComment(string userId, int commentId)
        {
            var comment = await _unitOfWork.Blog_Comments.GetByIdAsync(commentId);
            if (comment == null || comment.UserId != userId)
                throw new UnauthorizedAccessException("You cannot delete this comment.");

            _unitOfWork.Blog_Comments.Delete(comment);
            await _unitOfWork.CompleteAsync();
        }

        // Delete Recipe Comment
        public async Task DeleteRecipeComment(string userId, int commentId)
        {
            var comment = await _unitOfWork.Recipe_Comments.GetByIdAsync(commentId);
            if (comment == null || comment.UserId != userId)
                throw new UnauthorizedAccessException("You cannot delete this comment.");

            _unitOfWork.Recipe_Comments.Delete(comment);
            await _unitOfWork.CompleteAsync();
        }

    }

}
