using BackEngin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace BackEngin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("recipes")]
        public async Task<IActionResult> GetRecipes()
        {
            var recipes = await _recipeService.GetRecipes();
            return Ok(recipes);
        }

        [HttpPost("create-recipe")]
        [Authorize]
        public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeDTO createRecipeDto)
        {
            if (createRecipeDto == null) return BadRequest("Invalid recipe data.");

            var createdRecipe = await _recipeService.CreateRecipe(createRecipeDto);

            return CreatedAtAction(nameof(GetRecipeDetails), new { recipeId = createdRecipe.Id }, createdRecipe);
        }


        [HttpGet("recipe-details-{recipeId}")]
        public async Task<IActionResult> GetRecipeDetails(int recipeId)
        {
            var recipe = await _recipeService.GetRecipeDetails(recipeId);
            if (recipe == null) return NotFound();
            return Ok(recipe);
        }

        [HttpPut("update-recipe-{recipeId}")]
        [Authorize]
        public async Task<IActionResult> UpdateRecipe(int recipeId, [FromBody] UpdateRecipeDTO updateRecipeDto)
        {
            var updatedRecipe = await _recipeService.UpdateRecipe(recipeId, updateRecipeDto);
            if (updatedRecipe == null) return NotFound();
            return Ok(updatedRecipe);
        }

        [HttpDelete("delete-recipe{recipeId}")]
        [Authorize]
        public async Task<IActionResult> DeleteRecipe(int recipeId)
        {
            var result = await _recipeService.DeleteRecipe(recipeId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
