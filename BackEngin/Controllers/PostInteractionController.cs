using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models;
using System.Security.Claims;
using BackEngin.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;

namespace BackEngin.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/interactions")]
    [Authorize]
    [ApiController]
    public class PostInteractionController : ApiControllerBase
    {
        private readonly IPostInteractionService _interactionService;

        public PostInteractionController(IPostInteractionService interactionService)
        {
            _interactionService = interactionService;
        }

        // the toogle like and bookmark api endpoint handle both like and unlike cases according to the current state as social media platforms

        // Toggle Like for Blog
        [HttpPost("blogs/{blogId}/toggle-like")]
        public async Task<IActionResult> ToggleLikeBlog(int blogId)
        {
            try
            {
                string userId = await GetActiveUserId();
                bool isLiked = await _interactionService.ToggleLikeBlog(userId, blogId);

                string message = isLiked ? "Blog liked successfully." : "Blog unliked successfully.";
                return Ok(new { message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        // Toggle Like for Recipe
        [HttpPost("recipes/{recipeId}/toggle-like")]
        public async Task<IActionResult> ToggleLikeRecipe(int recipeId)
        {
            try
            {
                string userId = await GetActiveUserId();
                bool isLiked = await _interactionService.ToggleLikeRecipe(userId, recipeId);

                string message = isLiked ? "Recipe liked successfully." : "Recipe unliked successfully.";
                return Ok(new { message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }


        // Bookmark a Blog
        [HttpPost("blogs/{blogId}/bookmark")]
        public async Task<IActionResult> BookmarkBlog(int blogId)
        {
            try
            {
                string userId = await GetActiveUserId();
                bool isBookmarked = await _interactionService.ToggleBookmarkBlog(userId, blogId);
                string message = isBookmarked ? "Blog bookmarked successfully." : "Blog unbookmarked successfully.";
                return Ok(new { message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Bookmark a Recipe
        [HttpPost("recipes/{recipeId}/bookmark")]
        public async Task<IActionResult> BookmarkRecipe(int recipeId)
        {
            try
            {
                string userId = await GetActiveUserId();
                bool isBookmarked = await _interactionService.ToggleBookmarkRecipe(userId, recipeId);
                string message = isBookmarked ? "Recipe bookmarked successfully." : "Recipe unbookmarked successfully.";
                return Ok(new { message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Comment on Blog
        [HttpPost("blogs/{blogId}/comment")]
        public async Task<IActionResult> CommentOnBlog(int blogId, [FromBody] CommentRequestDTO commentDto)
        {
            try
            {
                string userId = await GetActiveUserId();
                var comment = await _interactionService.CommentOnBlog(userId, blogId, commentDto);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Comment on Recipe
        [HttpPost("recipes/{recipeId}/comment")]
        public async Task<IActionResult> CommentOnRecipe(int recipeId, [FromBody] CommentRequestDTO commentDto)
        {
            try
            {
                string userId = await GetActiveUserId();
                var comment = await _interactionService.CommentOnRecipe(userId, recipeId, commentDto);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Update Blog Comment
        [HttpPut("blogs/comments/{commentId}")]
        public async Task<IActionResult> UpdateBlogComment(int commentId, [FromBody] CommentRequestDTO commentDto)
        {
            try
            {
                string userId = await GetActiveUserId();
                var updatedComment = await _interactionService.UpdateBlogComment(userId, commentId, commentDto);
                return Ok(updatedComment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Update Recipe Comment
        [HttpPut("recipes/comments/{commentId}")]
        public async Task<IActionResult> UpdateRecipeComment(int commentId, [FromBody] CommentRequestDTO commentDto)
        {
            try
            {
                string userId = await GetActiveUserId();
                var updatedComment = await _interactionService.UpdateRecipeComment(userId, commentId, commentDto);
                return Ok(updatedComment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Delete Blog Comment
        [HttpDelete("blogs/comments/{commentId}")]
        public async Task<IActionResult> DeleteBlogComment(int commentId)
        {
            try
            {
                string userId = await GetActiveUserId();
                await _interactionService.DeleteBlogComment(userId, commentId);
                return Ok(new { message = "Blog comment deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Delete Recipe Comment
        [HttpDelete("recipes/comments/{commentId}")]
        public async Task<IActionResult> DeleteRecipeComment(int commentId)
        {
            try
            {
                string userId = await GetActiveUserId();
                await _interactionService.DeleteRecipeComment(userId, commentId);
                return Ok(new { message = "Recipe comment deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

}
