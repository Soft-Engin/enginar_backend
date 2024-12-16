using Microsoft.AspNetCore.Mvc;
using BackEngin.Services.Interfaces;
using Models.DTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;
using BackEngin.Services;

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


        // GET /allergens - Get paginated list of allergens
        [HttpGet]
        public async Task<IActionResult> GetPaginatedAllergens([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 10;

                var paginatedAllergens = await _allergenService.GetPaginatedAsync(pageNumber, pageSize);
                return Ok(paginatedAllergens);
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
    }
}
