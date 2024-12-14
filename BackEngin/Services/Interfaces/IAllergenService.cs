using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Sprache;

namespace BackEngin.Services.Interfaces
{
    public interface IAllergenService
    {
        Task<IEnumerable<AllergenIdDTO>> GetAllAllergensAsync();
        Task<int?> CreateAllergenAsync(AllergenDTO model);
        Task<bool?> UpdateAllergenAsync(int allergenId, AllergenDTO model);
        Task<bool?> DeleteAllergenAsync(int allergenId);
        Task<PaginatedResponseDTO<AllergenIdDTO>> SearchAllergens(AllergenSearchParams searchParams, int pageNumber, int pageSize);
    }
}

