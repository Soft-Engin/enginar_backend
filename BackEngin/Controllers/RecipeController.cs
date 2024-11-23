using Asp.Versioning;
using BackEngin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace BackEngin.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/recipe")]
    [ApiController]
    public class RecipeController : ApiControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService): base()
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
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeRequestDTO recipeDto)
        {
            if (recipeDto == null) return BadRequest("Invalid recipe data.");

            var recipe = new CreateRecipeDTO
            {
                Header = recipeDto.Header,
                BodyText = recipeDto.BodyText,
                UserId = await GetActiveUserId(),
                Ingredients = recipeDto.Ingredients
            };

            var createdRecipe = await _recipeService.CreateRecipe(recipe);

            return CreatedAtAction(nameof(GetRecipeDetails), new { recipeId = createdRecipe.Id }, createdRecipe);
        }


        [HttpGet("recipe-details/{recipeId}")]
        public async Task<IActionResult> GetRecipeDetails(int recipeId)
        {
            var recipe = await _recipeService.GetRecipeDetails(recipeId);
            if (recipe == null) return NotFound();
            return Ok(recipe);
        }

        [HttpPut("update-recipe/{recipeId}")]
        [Authorize]
        public async Task<IActionResult> UpdateRecipe(int recipeId, [FromBody] RecipeRequestDTO updateRecipeDto)
        {
            var recipeOwner = await _recipeService.GetOwner(recipeId);
            if (!await CanUserAccess(recipeOwner))
            {
                return Unauthorized();
            }
            var updatedRecipe = await _recipeService.UpdateRecipe(recipeId, updateRecipeDto);
            if (updatedRecipe == null) return NotFound();
            return Ok(updatedRecipe);
        }

        [HttpDelete("delete-recipe/{recipeId}")]
        [Authorize]
        public async Task<IActionResult> DeleteRecipe(int recipeId)
        {
            var recipeOwner = await _recipeService.GetOwner(recipeId);
            if (!await CanUserAccess(recipeOwner))
            {
                return Unauthorized();
            }
            var result = await _recipeService.DeleteRecipe(recipeId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
