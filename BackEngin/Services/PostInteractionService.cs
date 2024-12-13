using DataAccess.Repositories;
using Models.InteractionModels;
using Models;
using BackEngin.Services.Interfaces;
using Models.DTO;

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
            
        }

        // Like Recipe
        public async Task LikeRecipe(string userId, int recipeId)
        {
            
        }       

    }

}
