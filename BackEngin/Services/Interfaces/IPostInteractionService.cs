using Models.DTO;


namespace BackEngin.Services.Interfaces
{
    public interface IPostInteractionService
    {
        // Likes
        Task<bool> ToggleLikeBlog(string userId, int blogId);
        Task<bool> ToggleLikeRecipe(string userId, int recipeId);

        // Bookmark methods
        Task<bool> ToggleBookmarkBlog(string userId, int blogId);
        Task<bool> ToggleBookmarkRecipe(string userId, int recipeId);

        // Comment methods
        Task<CommentDTO> CommentOnBlog(string userId, int blogId, CommentRequestDTO commentRequest);
        Task<CommentDTO> CommentOnRecipe(string userId, int recipeId, CommentRequestDTO commentRequest);

        // Update comment methods
        Task<CommentDTO> UpdateBlogComment(string userId, int commentId, CommentRequestDTO commentRequest);
        Task<CommentDTO> UpdateRecipeComment(string userId, int commentId, CommentRequestDTO commentRequest);

        // Delete comment methods
        Task DeleteBlogComment(string userId, int commentId);
        Task DeleteRecipeComment(string userId, int commentId);

    }
}
