using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DTO;

namespace BackEngin.Services.Interfaces
{
    public interface IIngredientTypesService
    {
        Task<IEnumerable<IngredientTypeIdDTO>> GetAllIngredientTypesAsync();
        Task<int?> CreateIngredientTypeAsync(IngredientTypeDTO model);
        Task<bool?> UpdateIngredientTypeAsync(int ingredientTypeId, IngredientTypeDTO model);
        Task<bool?> DeleteIngredientTypeAsync(int ingredientTypeId);
    }
}
