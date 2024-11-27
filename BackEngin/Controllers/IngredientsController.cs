using Microsoft.AspNetCore.Mvc;
using BackEngin.Services.Interfaces;
using Models.DTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;

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

        // GET /ingredients - List all ingredients
        [HttpGet]
        public async Task<IActionResult> GetAllIngredients()
        {
            var ingredients = await _ingredientsService.GetAllIngredientsAsync();
            return Ok(ingredients);
        }

        // POST /ingredients - Create a new ingredient (admin only)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateIngredient([FromBody] IngredientDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _ingredientsService.CreateIngredientAsync(model);
            if (result == null)
            {
                return BadRequest("Ingredient cannot be created");
            }
            return Ok(new { Id = result, message = "Ingredient created successfully!" });
        }

        // PUT /ingredients/{ingredientId} - Update an ingredient (admin only)
        [HttpPut("{ingredientId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateIngredient(int ingredientId, [FromBody] IngredientDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _ingredientsService.UpdateIngredientAsync(ingredientId, model);
            if (result == null)
            {
                return BadRequest("Ingredient does not exist");
            }
            if (result == false)
            {
                return BadRequest("Ingredient cannot be updated");
            }
            return Ok(new { message = "Ingredient updated successfully!" });
        }

        // DELETE /ingredients/{ingredientId} - Delete an ingredient (admin only)
        [HttpDelete("{ingredientId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteIngredient(int ingredientId)
        {
            var result = await _ingredientsService.DeleteIngredientAsync(ingredientId);
            if (result == null)
            {
                return BadRequest("Ingredient does not exist");
            }
            if (result == false)
            {
                return BadRequest("Ingredient cannot be deleted");
            }
            return Ok(new { message = "Ingredient deleted successfully!" });
        }
    }
}
