using Models.DTO;


namespace BackEngin.Services.Interfaces
{
    public interface IPostInteractionService
    {
        // Likes
        Task LikeBlog(string userId, int blogId);
        Task LikeRecipe(string userId, int recipeId);

    }
}
