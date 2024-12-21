using Models.DTO;

namespace BackEngin.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<PaginatedResponseDTO<RecipeDTO>> GetRecipes(int pageNumber, int pageSize);
        Task<RecipeDetailsDTO> CreateRecipe(string userId, CreateRecipeDTO createRecipeDTO);
        Task<RecipeDetailsDTO> GetRecipeDetails(int recipeId);
        Task<RecipeDetailsDTO> UpdateRecipe(int recipeId, RecipeRequestDTO updateRecipeDTO);
        Task<bool> DeleteRecipe(int recipeId);
        Task<string?> GetOwner(int recipeId);
        Task<PaginatedResponseDTO<RecipeDTO>> SearchRecipes(RecipeSearchParams searchParams, int pageNumber, int pageSize);
        Task<byte[]?> GetRecipeBannerImage(int recipeId);
    }
}
