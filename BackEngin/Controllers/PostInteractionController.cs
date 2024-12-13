using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models;
using System.Security.Claims;
using BackEngin.Services.Interfaces;

namespace BackEngin.Controllers
{
    [Route("api/v1/posts")]
    [ApiController]
    public class PostInteractionController : ApiControllerBase
    {
        private readonly IPostInteractionService _interactionService;

        public PostInteractionController(IPostInteractionService interactionService)
        {
            _interactionService = interactionService;
        }

        // Like a Blog
        [HttpPost("blogs/{blogId}/like")]
        public async Task<IActionResult> LikeBlog(int blogId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _interactionService.LikeBlog(userId, blogId);
            return Ok(new { message = "Blog liked successfully." });
        }

        // Like a Recipe
        [HttpPost("recipes/{recipeId}/like")]
        public async Task<IActionResult> LikeRecipe(int recipeId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _interactionService.LikeRecipe(userId, recipeId);
            return Ok(new { message = "Recipe liked successfully." });
        }


    }

}
