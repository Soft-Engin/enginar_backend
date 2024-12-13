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

        // Like Blog
        public async Task LikeBlog(string userId, int blogId)
        {
            // Check if the user has already liked the blog
            var existingLike = await _unitOfWork.Blog_Likes.FindAsync(l => l.UserId == userId && l.BlogId == blogId);

            if (existingLike.Any())
                throw new ArgumentException("You have already liked this blog.");

            // Create a new like
            var blogLike = new Blog_Likes
            {
                UserId = userId,
                BlogId = blogId,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Blog_Likes.AddAsync(blogLike);
            await _unitOfWork.CompleteAsync();
        }

        // Like Recipe
        public async Task LikeRecipe(string userId, int recipeId)
        {
            // Check if the user has already liked the recipe
            var existingLike = await _unitOfWork.Recipe_Likes.FindAsync(l => l.UserId == userId && l.RecipeId == recipeId);

            if (existingLike.Any())
                throw new ArgumentException("You have already liked this recipe.");

            // Create a new like
            var recipeLike = new Recipe_Likes
            {
                UserId = userId,
                RecipeId = recipeId,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Recipe_Likes.AddAsync(recipeLike);
            await _unitOfWork.CompleteAsync();
        }

        // Bookmark Blog
        public async Task BookmarkBlog(string userId, int blogId)
        {
            var existingBookmark = await _unitOfWork.Blog_Bookmarks.FindAsync(b => b.UserId == userId && b.BlogId == blogId);
            if (existingBookmark.Any())
                throw new ArgumentException("Blog is already bookmarked.");

            var bookmark = new Blog_Bookmarks
            {
                UserId = userId,
                BlogId = blogId,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Blog_Bookmarks.AddAsync(bookmark);
            await _unitOfWork.CompleteAsync();
        }

        // Bookmark Recipe
        public async Task BookmarkRecipe(string userId, int recipeId)
        {
            var existingBookmark = await _unitOfWork.Recipe_Bookmarks.FindAsync(b => b.UserId == userId && b.RecipeId == recipeId);
            if (existingBookmark.Any())
                throw new ArgumentException("Recipe is already bookmarked.");

            var bookmark = new Recipe_Bookmarks
            {
                UserId = userId,
                RecipeId = recipeId,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Recipe_Bookmarks.AddAsync(bookmark);
            await _unitOfWork.CompleteAsync();
        }

        // Comment on Blog
        public async Task<CommentDTO> CommentOnBlog(string userId, int blogId, string commentText)
        {
            var comment = new Blog_Comments
            {
                UserId = userId,
                BlogId = blogId,
                CommentText = commentText,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Blog_Comments.AddAsync(comment);
            await _unitOfWork.CompleteAsync();

            return new CommentDTO
            {
                Id = comment.Id,
                Recipe_blog_id = blogId,
                Text = comment.CommentText,
                Timestamp = comment.CreatedAt
            };
        }

        // Comment on Recipe
        public async Task<CommentDTO> CommentOnRecipe(string userId, int recipeId, string commentText)
        {
            var comment = new Recipe_Comments
            {
                UserId = userId,
                RecipeId = recipeId,
                CommentText = commentText,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Recipe_Comments.AddAsync(comment);
            await _unitOfWork.CompleteAsync();

            return new CommentDTO
            {
                Id = comment.Id,
                Recipe_blog_id = recipeId,
                Text = comment.CommentText,
                Timestamp = comment.CreatedAt
            };
        }

        // Update Blog Comment
        public async Task<CommentDTO> UpdateBlogComment(string userId, int commentId, string updatedComment)
        {
            var comment = await _unitOfWork.Blog_Comments.GetByIdAsync(commentId);
            if (comment == null || comment.UserId != userId)
                throw new UnauthorizedAccessException("You cannot update this comment.");

            comment.CommentText = updatedComment;
            comment.CreatedAt = DateTime.UtcNow;

            _unitOfWork.Blog_Comments.Update(comment);
            await _unitOfWork.CompleteAsync();

            return new CommentDTO
            {
                Id = comment.Id,
                Text = updatedComment,
                Timestamp = comment.CreatedAt
            };
        }

        // Update Recipe Comment
        public async Task<CommentDTO> UpdateRecipeComment(string userId, int commentId, string updatedComment)
        {
            var comment = await _unitOfWork.Recipe_Comments.GetByIdAsync(commentId);
            if (comment == null || comment.UserId != userId)
                throw new UnauthorizedAccessException("You cannot update this comment.");

            comment.CommentText = updatedComment;
            comment.CreatedAt = DateTime.UtcNow;

            _unitOfWork.Recipe_Comments.Update(comment);
            await _unitOfWork.CompleteAsync();

            return new CommentDTO
            {
                Id = comment.Id,
                Text = updatedComment,
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
