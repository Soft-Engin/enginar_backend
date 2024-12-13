using Models.DTO;


namespace BackEngin.Services.Interfaces
{
    public interface IPostInteractionService
    {
        // Likes
        Task LikeBlog(string userId, int blogId);
        Task LikeRecipe(string userId, int recipeId);

        // Bookmark methods
        Task BookmarkBlog(string userId, int blogId);
        Task BookmarkRecipe(string userId, int recipeId);

        // Comment methods
        Task<CommentDTO> CommentOnBlog(string userId, int blogId, string commentText);
        Task<CommentDTO> CommentOnRecipe(string userId, int recipeId, string commentText);

        // Update comment methods
        Task<CommentDTO> UpdateBlogComment(string userId, int commentId, string updatedComment);
        Task<CommentDTO> UpdateRecipeComment(string userId, int commentId, string updatedComment);

        // Delete comment methods
        Task DeleteBlogComment(string userId, int commentId);
        Task DeleteRecipeComment(string userId, int commentId);

    }
}
