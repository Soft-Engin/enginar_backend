using Microsoft.AspNetCore.Mvc;
using BackEngin.Services.Interfaces;
using Models.DTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;

namespace BackEngin.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ingredient-types")]
    [ApiController]
    public class IngredientTypesController : ControllerBase
    {
        private readonly IIngredientTypesService _ingredientTypesService;

        public IngredientTypesController(IIngredientTypesService ingredientTypesService)
        {
            _ingredientTypesService = ingredientTypesService;
        }

        // GET /ingredienttypes - List all ingredient types
        [HttpGet]
        public async Task<IActionResult> GetAllIngredientTypes()
        {
            var ingredientTypes = await _ingredientTypesService.GetAllIngredientTypesAsync();
            return Ok(ingredientTypes);
        }

        // POST /ingredienttypes - Create a new ingredient type (admin only)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateIngredientType([FromBody] IngredientTypeDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _ingredientTypesService.CreateIngredientTypeAsync(model);
            if (result == null)
            {
                return BadRequest("Ingredient type cannot be created");
            }
            return Ok(new { Id = result, message = "Ingredient type created successfully!" });
        }

        // PUT /ingredienttypes/{ingredientTypeId} - Update an ingredient type (admin only)
        [HttpPut("{ingredientTypeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateIngredientType(int ingredientTypeId, [FromBody] IngredientTypeDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _ingredientTypesService.UpdateIngredientTypeAsync(ingredientTypeId, model);
            if (result == null)
            {
                return BadRequest("Ingredient type does not exist");
            }
            if (result == false)
            {
                return BadRequest("Ingredient type cannot be updated");
            }
            return Ok(new { message = "Ingredient type updated successfully!" });
        }

        // DELETE /ingredienttypes/{ingredientTypeId} - Delete an ingredient type (admin only)
        [HttpDelete("{ingredientTypeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteIngredientType(int ingredientTypeId)
        {
            var result = await _ingredientTypesService.DeleteIngredientTypeAsync(ingredientTypeId);
            if (result == null)
            {
                return BadRequest("Ingredient type does not exist");
            }
            if (result == false)
            {
                return BadRequest("Ingredient type cannot be deleted");
            }
            return Ok(new { message = "Ingredient type deleted successfully!" });
        }
    }
}
