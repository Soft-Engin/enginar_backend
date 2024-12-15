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
            try
            {
                var allergens = await _allergenService.GetAllAllergensAsync();
                return Ok(allergens);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // POST /allergens - Create a new allergen (admin only)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAllergen([FromBody] AllergenDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid request data.", errors = ModelState });

            try
            {
                var result = await _allergenService.CreateAllergenAsync(model);
                if (result == null)
                {
                    return BadRequest(new { message = "Allergen could not be created." });
                }
                return Ok(new { Id = result, message = "Allergen created successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // PUT /allergens/{allergenId} - Update an allergen (admin only)
        [HttpPut("{allergenId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAllergen(int allergenId, [FromBody] AllergenDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid request data.", errors = ModelState });

            try
            {
                var result = await _allergenService.UpdateAllergenAsync(allergenId, model);
                if (result == null)
                {
                    return NotFound(new { message = "Allergen does not exist." });
                }
                if (result == false)
                {
                    return BadRequest(new { message = "Allergen could not be updated." });
                }
                return Ok(new { message = "Allergen updated successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // DELETE /allergens/{allergenId} - Delete an allergen (admin only)
        [HttpDelete("{allergenId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAllergen(int allergenId)
        {
            try
            {
                var result = await _allergenService.DeleteAllergenAsync(allergenId);
                if (result == null)
                {
                    return NotFound(new { message = "Allergen does not exist." });
                }
                if (result == false)
                {
                    return BadRequest(new { message = "Allergen could not be deleted." });
                }
                return Ok(new { message = "Allergen deleted successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAllergens(
            [FromQuery] AllergenSearchParams searchParams,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid request data.", errors = ModelState });

            try
            {
                var result = await _allergenService.SearchAllergens(searchParams, pageNumber, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

    }
}
