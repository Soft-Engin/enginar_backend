using Microsoft.AspNetCore.Mvc;
using BackEngin.Services.Interfaces;
using Models.DTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;

namespace BackEngin.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/allergens")]
    [ApiController]
    public class AllergenController : ControllerBase
    {
        private readonly IAllergenService _allergenService;

        public AllergenController(IAllergenService allergenService)
        {
            _allergenService = allergenService;
        }

        // GET /allergens - List all allergens
        [HttpGet]
        public async Task<IActionResult> GetAllAllergens()
        {
            var allergens = await _allergenService.GetAllAllergensAsync();
            return Ok(allergens);
        }

        // POST /allergens - Create a new allergen (admin only)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAllergen([FromBody] AllergenDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _allergenService.CreateAllergenAsync(model);
            return Ok(result);
        }

        // PUT /allergens/{allergenId} - Update an allergen (admin only)
        [HttpPut("{allergenId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAllergen(int allergenId, [FromBody] AllergenDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _allergenService.UpdateAllergenAsync(allergenId, model);
            return Ok(result);
        }

        // DELETE /allergens/{allergenId} - Delete an allergen (admin only)
        [HttpDelete("{allergenId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAllergen(int allergenId)
        {
            var result = await _allergenService.DeleteAllergenAsync(allergenId);
            return Ok(result);
        }
    }
}
