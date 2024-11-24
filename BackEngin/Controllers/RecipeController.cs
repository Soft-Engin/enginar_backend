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

        public RecipeController(IRecipeService recipeService) : base()
        {
            _recipeService = recipeService;
        }

        [HttpGet("recipes")]
        public async Task<IActionResult> GetRecipes([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var recipes = await _recipeService.GetRecipes(pageNumber, pageSize);
            return Ok(recipes);
        }

        [HttpPost("create-recipe")]
        [Authorize]
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeRequestDTO recipeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // this probably wont even get triggered
            if (recipeDto == null) return BadRequest("Invalid recipe data.");

            try
            {
                var recipe = new CreateRecipeDTO
                {
                    Header = recipeDto.Header,
                    BodyText = recipeDto.BodyText,
                    UserId = await GetActiveUserId(),
                    Ingredients = recipeDto.Ingredients
                };

                var createdRecipe = await _recipeService.CreateRecipe(recipe);
                if (createdRecipe == null) return BadRequest("Invalid recipe data.");

                return CreatedAtAction(nameof(GetRecipeDetails), new { recipeId = createdRecipe.Id }, createdRecipe);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception details if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var recipeOwner = await _recipeService.GetOwner(recipeId);
            // Return NotFound if the recipe doesn't exist
            if (recipeOwner == null)
            {
                return NotFound();
            }
            if (!await CanUserAccess(recipeOwner))
            {
                return Unauthorized();
            }
            try
            {
                var updatedRecipe = await _recipeService.UpdateRecipe(recipeId, updateRecipeDto);
                if (updatedRecipe == null) // Double-check if the update failed
                {
                    return NotFound();
                }
                return Ok(updatedRecipe);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception details if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
            
        }

        [HttpDelete("delete-recipe/{recipeId}")]
        [Authorize]
        public async Task<IActionResult> DeleteRecipe(int recipeId)
        {
            var recipeOwner = await _recipeService.GetOwner(recipeId);
            if(recipeOwner == null)
            {
                return NotFound();
            }
            if (!await CanUserAccess(recipeOwner))
            {
                return Unauthorized();
            }
            var result = await _recipeService.DeleteRecipe(recipeId);
            if (!result) return NotFound();
            return Ok(new { message = "Recipe deleted successfully!" });
        }
    }
}
