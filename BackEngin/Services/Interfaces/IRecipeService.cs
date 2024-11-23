using Models.DTO;

namespace BackEngin.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeDetailsDTO>> GetRecipes();
        Task<RecipeDetailsDTO> CreateRecipe(CreateRecipeDTO createRecipeDTO);
        Task<RecipeDetailsDTO> GetRecipeDetails(int recipeId);
        Task<RecipeDetailsDTO> UpdateRecipe(int recipeId, RecipeRequestDTO updateRecipeDTO);
        Task<bool> DeleteRecipe(int recipeId);
        Task<string> GetOwner(int recipeId);
    }
}
