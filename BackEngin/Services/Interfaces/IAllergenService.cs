using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Sprache;

namespace BackEngin.Services.Interfaces
{
    public interface IAllergenService
    {
        Task<PaginatedResponseDTO<AllergenIdDTO>> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<int?> CreateAllergenAsync(AllergenDTO model);
        Task<bool?> UpdateAllergenAsync(int allergenId, AllergenDTO model);
        Task<bool?> DeleteAllergenAsync(int allergenId);
    }
}

