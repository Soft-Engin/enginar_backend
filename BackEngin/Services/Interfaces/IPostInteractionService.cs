using Models.DTO;


namespace BackEngin.Services.Interfaces
{
    public interface IPostInteractionService
    {
        // Likes
        Task<bool> ToggleLikeBlog(string userId, int blogId);
        Task<bool> ToggleLikeRecipe(string userId, int recipeId);
        Task<bool> ToggleLikeEvent(string userId, int eventId);

        // Bookmark methods
        Task<bool> ToggleBookmarkBlog(string userId, int blogId);
        Task<bool> ToggleBookmarkRecipe(string userId, int recipeId);
        Task<bool> ToggleBookmarkEvent(string userId, int eventId);

        // Comment methods
        Task<CommentDTO> CommentOnBlog(string userId, int blogId, CommentRequestDTO commentRequest);
        Task<CommentDTO> CommentOnRecipe(string userId, int recipeId, CommentRequestDTO commentRequest);
        Task<CommentDTO> CommentOnEvent(string userId, int eventId, CommentRequestDTO commentRequest);

        // Update comment methods
        Task<CommentDTO> UpdateBlogComment(string userId, int commentId, CommentRequestDTO commentRequest);
        Task<CommentDTO> UpdateRecipeComment(string userId, int commentId, CommentRequestDTO commentRequest);
        Task<CommentDTO> UpdateEventComment(string userId, int commentId, CommentRequestDTO commentRequest);

        // Delete comment methods
        Task DeleteBlogComment(string userId, int commentId);
        Task DeleteRecipeComment(string userId, int commentId);
        Task DeleteEventComment(string userId, int commentId);

        // New methods
        Task<bool> IsBlogLiked(string userId, int blogId);
        Task<bool> IsBlogBookmarked(string userId, int blogId);
        Task<bool> IsRecipeLiked(string userId, int recipeId);
        Task<bool> IsRecipeBookmarked(string userId, int recipeId);

        // Count methods
        Task<int> GetBlogLikeCount(int blogId);
        Task<int> GetBlogBookmarkCount(int blogId);
        Task<int> GetRecipeLikeCount(int recipeId);
        Task<int> GetRecipeBookmarkCount(int recipeId);

        Task<PaginatedResponseDTO<CommentDTO>> GetBlogComments(int blogId, int pageNumber, int pageSize);
        Task<PaginatedResponseDTO<CommentDTO>> GetRecipeComments(int recipeId, int pageNumber, int pageSize);
        
        // Methods for retrieving comment images
        Task<byte[]?> GetBlogCommentImage(int commentId, int imageIndex);
        Task<byte[]?> GetRecipeCommentImage(int commentId, int imageIndex);
    }
}
