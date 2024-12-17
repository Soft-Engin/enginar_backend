using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DTO;

namespace BackEngin.Services.Interfaces
{
    public interface IIngredientTypesService
    {
        Task<IngredientTypeIdDTO> GetIngredientTypeByIdAsync(int ingredientTypeId);
        Task<PaginatedResponseDTO<IngredientTypeIdDTO>> GetIngredientTypesPaginatedAsync(int pageNumber, int pageSize);
        Task<int?> CreateIngredientTypeAsync(IngredientTypeDTO model);
        Task<bool?> UpdateIngredientTypeAsync(int ingredientTypeId, IngredientTypeDTO model);
        Task<bool?> DeleteIngredientTypeAsync(int ingredientTypeId);
        Task<PaginatedResponseDTO<IngredientTypeIdDTO>> SearchIngredientTypes(IngredientTypeSearchParams searchParams, int pageNumber, int pageSize);
    }
}
