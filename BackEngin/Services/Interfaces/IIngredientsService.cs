using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DTO;

namespace BackEngin.Services.Interfaces
{
    public interface IIngredientsService
    {
        Task<IEnumerable<IngredientIdDTO>> GetAllIngredientsAsync();
        Task<int?> CreateIngredientAsync(IngredientDTO model);
        Task<bool?> UpdateIngredientAsync(int ingredientId, IngredientDTO model);
        Task<bool?> DeleteIngredientAsync(int ingredientId);
    }
}
