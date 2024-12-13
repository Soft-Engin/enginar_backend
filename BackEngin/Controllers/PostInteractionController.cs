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
            try
            {
                string userId = await GetActiveUserId();
                await _interactionService.LikeBlog(userId, blogId);
                return Ok(new { message = "Blog liked successfully." });
            }
            catch (ArgumentException ex)
            {
                // Handle cases such as duplicate likes
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        // Like a Recipe
        [HttpPost("recipes/{recipeId}/like")]
        public async Task<IActionResult> LikeRecipe(int recipeId)
        {
            try
            {
                string userId = await GetActiveUserId();
                await _interactionService.LikeRecipe(userId, recipeId);
                return Ok(new { message = "Recipe liked successfully." });
            }
            catch (ArgumentException ex)
            {
                // Handle cases such as duplicate likes
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
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
                await _interactionService.BookmarkBlog(userId, blogId);
                return Ok(new { message = "Blog bookmarked successfully." });
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
                await _interactionService.BookmarkRecipe(userId, recipeId);
                return Ok(new { message = "Recipe bookmarked successfully." });
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
                var comment = await _interactionService.CommentOnBlog(userId, blogId, commentDto.Text);
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
                var comment = await _interactionService.CommentOnRecipe(userId, recipeId, commentDto.Text);
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
                var updatedComment = await _interactionService.UpdateBlogComment(userId, commentId, commentDto.Text);
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
                var updatedComment = await _interactionService.UpdateRecipeComment(userId, commentId, commentDto.Text);
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
