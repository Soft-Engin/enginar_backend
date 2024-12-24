using Microsoft.AspNetCore.Mvc;
using BackEngin.Services.Interfaces;
using Models.DTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;
using Microsoft.IdentityModel.Tokens;

namespace BackEngin.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ingredients")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientsService _ingredientsService;

        public IngredientsController(IIngredientsService ingredientsService)
        {
            _ingredientsService = ingredientsService;
        }

        // GET /ingredients - Get paginated list of ingredients
        [HttpGet]
        public async Task<IActionResult> GetPaginatedIngredients([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 10;

                var paginatedIngredients = await _ingredientsService.GetIngredientsPaginatedAsync(pageNumber, pageSize);
                return Ok(paginatedIngredients);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("{ingredientId}")]
        public async Task<IActionResult> GetIngredientById(int ingredientId)
        {
            try
            {
                var ingredient = await _ingredientsService.GetIngredientByIdAsync(ingredientId);

                if (ingredient == null)
                {
                    return NotFound(new { message = "Ingredient not found." });
                }

                return Ok(ingredient);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }


        // POST /ingredients - Create a new ingredient (admin only)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateIngredient([FromBody] IngredientDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid request data.", errors = ModelState });

            try
            {
                var result = await _ingredientsService.CreateIngredientAsync(model);
                if (result == null)
                {
                    return BadRequest(new { message = "Ingredient cannot be created." });
                }
                return Ok(new { Id = result, message = "Ingredient created successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // PUT /ingredients/{ingredientId} - Update an ingredient (admin only)
        [HttpPut("{ingredientId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateIngredient(int ingredientId, [FromBody] IngredientDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid request data.", errors = ModelState });

            try
            {
                var result = await _ingredientsService.UpdateIngredientAsync(ingredientId, model);
                if (result == null)
                {
                    return NotFound(new { message = "Ingredient does not exist." });
                }
                if (result == false)
                {
                    return BadRequest(new { message = "Ingredient cannot be updated." });
                }
                return Ok(new { message = "Ingredient updated successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // DELETE /ingredients/{ingredientId} - Delete an ingredient (admin only)
        [HttpDelete("{ingredientId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteIngredient(int ingredientId)
        {
            try
            {
                var result = await _ingredientsService.DeleteIngredientAsync(ingredientId);
                if (result == null)
                {
                    return BadRequest(new { message = "Ingredient does not exist." });
                }
                if (result == false)
                {
                    return BadRequest(new { message = "Ingredient cannot be deleted." });
                }
                return Ok(new { message = "Ingredient deleted successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPost("getBatchImage")]
        public async Task<IActionResult> GetBatchImage([FromBody] List<int> ingredientIds)
        {
            if (ingredientIds.IsNullOrEmpty())
            {
                return BadRequest(new { message = "Ingredient IDs must be provided." });
            }            

            try
            {
                var result = await _ingredientsService.GetBatchImage(ingredientIds);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchIngredients(
            [FromQuery] IngredientSearchParams searchParams,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid request data.", errors = ModelState });

            try
            {
                var result = await _ingredientsService.SearchIngredients(searchParams, pageNumber, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

    }
}
