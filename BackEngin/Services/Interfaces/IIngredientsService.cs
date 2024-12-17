using Models.DTO;

namespace BackEngin.Services.Interfaces
{
    public interface IIngredientsService
    {
        Task<PaginatedResponseDTO<IngredientIdDTO>> GetIngredientsPaginatedAsync(int pageNumber, int pageSize);
        Task<IngredientIdDTO> GetIngredientByIdAsync(int ingredientId);
        Task<int?> CreateIngredientAsync(IngredientDTO model);
        Task<bool?> UpdateIngredientAsync(int ingredientId, IngredientDTO model);
        Task<bool?> DeleteIngredientAsync(int ingredientId);
        Task<PaginatedResponseDTO<IngredientIdDTO>> SearchIngredients(IngredientSearchParams searchParams, int pageNumber, int pageSize);
    }
}