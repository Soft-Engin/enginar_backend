using Microsoft.AspNetCore.Mvc;
using Sprache;

namespace BackEngin.Services.Interfaces
{
    public interface IAllergenService
    {
        Task<IEnumerable<AllergenDTO>> GetAllAllergensAsync();
        Task<IResult> CreateAllergenAsync(AllergenDTO model);
        Task<IResult> UpdateAllergenAsync(int allergenId, AllergenDTO model);
        Task<IResult> DeleteAllergenAsync(int allergenId);
    }
}

