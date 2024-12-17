using Asp.Versioning;
using BackEngin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace BackEngin.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/recipes")]
    [ApiController]
    public class RecipeController : ApiControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService) : base()
        {
            _recipeService = recipeService;
        }

        // GET /recipes
        [HttpGet]
        public async Task<IActionResult> GetRecipes([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 10;

                var recipes = await _recipeService.GetRecipes(pageNumber, pageSize);
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // POST /recipes
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeRequestDTO recipeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid request data.", errors = ModelState });

            if (recipeDto == null)
            {
                return BadRequest(new { message = "Invalid recipe data." });
            }

            try
            {
                var recipe = new CreateRecipeDTO
                {
                    Header = recipeDto.Header,
                    BodyText = recipeDto.BodyText,
                    Ingredients = recipeDto.Ingredients
                };

                string userId = await GetActiveUserId();
                var createdRecipe = await _recipeService.CreateRecipe(userId, recipe);
                if (createdRecipe == null) return BadRequest(new { message = "Recipe creation failed." });


                return CreatedAtAction(nameof(GetRecipeDetails), new { recipeId = createdRecipe.Id }, createdRecipe);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }


        // GET /recipes/{recipeId}
        [HttpGet("{recipeId}")]
        public async Task<IActionResult> GetRecipeDetails(int recipeId)
        {
            try
            {
                var recipe = await _recipeService.GetRecipeDetails(recipeId);
                if (recipe == null)
                {
                    return NotFound(new { message = "Recipe not found." });
                }

                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // PUT /recipes/{recipeId}
        [HttpPut("{recipeId}")]
        [Authorize]
        public async Task<IActionResult> UpdateRecipe(int recipeId, [FromBody] RecipeRequestDTO updateRecipeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid request data.", errors = ModelState });

            try
            {
                var recipeOwner = await _recipeService.GetOwner(recipeId);
                if (recipeOwner == null)
                {
                    return NotFound(new { message = "Recipe does not exist." });
                }

                if (!await CanUserAccess(recipeOwner))
                {
                    return Unauthorized(new { message = "Unauthorized to access this recipe." });
                }

                var updatedRecipe = await _recipeService.UpdateRecipe(recipeId, updateRecipeDto);
                if (updatedRecipe == null)
                {
                    return NotFound(new { message = "Recipe update failed." });
                }

                return Ok(updatedRecipe);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // DELETE /recipes/{recipeId}
        [HttpDelete("{recipeId}")]
        [Authorize]
        public async Task<IActionResult> DeleteRecipe(int recipeId)
        {
            try
            {
                var recipeOwner = await _recipeService.GetOwner(recipeId);
                if (recipeOwner == null)
                {
                    return NotFound(new { message = "Recipe does not exist." });
                }

                if (!await CanUserAccess(recipeOwner))
                {
                    return Unauthorized(new { message = "You are not authorized to delete this recipe." });
                }

                var result = await _recipeService.DeleteRecipe(recipeId);
                if (!result)
                {
                    return NotFound(new { message = "Recipe deletion failed." });
                }

                return Ok(new { message = "Recipe deleted successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRecipes(
            [FromQuery] RecipeSearchParams searchParams,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid request data.", errors = ModelState });
            try
            {
                var result = await _recipeService.SearchRecipes(searchParams, pageNumber, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

    }
}
