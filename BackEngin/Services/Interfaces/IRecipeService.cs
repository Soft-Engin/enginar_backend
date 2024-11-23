using Models.DTO;

namespace BackEngin.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeDTO>> GetRecipes();
        Task<RecipeDTO> CreateRecipe(CreateRecipeDTO createRecipeDTO);
        Task<RecipeDTO> GetRecipeDetails(int recipeId);
        Task<RecipeDTO> UpdateRecipe(int recipeId, UpdateRecipeDTO updateRecipeDTO);
        Task<bool> DeleteRecipe(int recipeId);
        Task<string> GetOwner(int recipeId);
    }
}
