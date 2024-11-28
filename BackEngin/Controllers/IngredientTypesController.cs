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

        // GET /ingredient-types - Get paginated list of ingredient types
        [HttpGet]
        public async Task<IActionResult> GetPaginatedIngredientTypes([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var paginatedIngredientTypes = await _ingredientTypesService.GetIngredientTypesPaginatedAsync(pageNumber, pageSize);
            return Ok(paginatedIngredientTypes);
        }

        // GET /ingredient-types/{ingredientTypeId} - Get ingredient type by ID
        [HttpGet("{ingredientTypeId}")]
        public async Task<IActionResult> GetIngredientTypeById(int ingredientTypeId)
        {
            var ingredientType = await _ingredientTypesService.GetIngredientTypeByIdAsync(ingredientTypeId);

            if (ingredientType == null)
            {
                return NotFound(new { message = "Ingredient type not found" });
            }

            return Ok(ingredientType);
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
