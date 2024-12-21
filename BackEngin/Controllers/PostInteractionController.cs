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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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

        [HttpGet("blogs/{blogId}/like-count")]
        public async Task<IActionResult> GetBlogLikeCount(int blogId)
        {
            try
            {
                int likeCount = await _interactionService.GetBlogLikeCount(blogId);
                return Ok(new { likeCount });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Check if the user has liked a blog post and the like count of the post
        [HttpGet("blogs/{blogId}/is-liked")]
        [Authorize]
        public async Task<IActionResult> IsBlogLiked(int blogId)
        {
            try
            {
                string userId = await GetActiveUserId();
                bool isLiked = await _interactionService.IsBlogLiked(userId, blogId);
                int likeCount = await _interactionService.GetBlogLikeCount(blogId);
                return Ok(new { isLiked, likeCount });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("blogs/{blogId}/bookmark-count")]
        public async Task<IActionResult> GetBlogBookmarkCount(int blogId)
        {
            try
            {
                int bookmarkCount = await _interactionService.GetBlogBookmarkCount(blogId);
                return Ok(new { bookmarkCount });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Check if the user has bookmarked a blog post and bookmark count of the post
        [HttpGet("blogs/{blogId}/is-bookmarked")]
        [Authorize]
        public async Task<IActionResult> IsBlogBookmarked(int blogId)
        {
            try
            {
                string userId = await GetActiveUserId();
                bool isBookmarked = await _interactionService.IsBlogBookmarked(userId, blogId);
                int bookmarkCount = await _interactionService.GetBlogBookmarkCount(blogId);
                return Ok(new { isBookmarked, bookmarkCount });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        // Get comments under a blog post with pagination
        [HttpGet("blogs/{blogId}/comments")]
        public async Task<IActionResult> GetBlogComments(int blogId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var comments = await _interactionService.GetBlogComments(blogId, page, pageSize);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("recipes/{recipeId}/like-count")]
        [Authorize]
        public async Task<IActionResult> GetRecipeLikeCount(int recipeId)
        {
            try
            {
                int likeCount = await _interactionService.GetRecipeLikeCount(recipeId);
                return Ok(new { likeCount });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Check if the user has liked a Recipe post and the like count of the recipe
        [HttpGet("recipes/{recipeId}/is-liked")]
        [Authorize]
        public async Task<IActionResult> IsRecipeLiked(int recipeId)
        {
            try
            {
                string userId = await GetActiveUserId();
                bool isLiked = await _interactionService.IsRecipeLiked(userId, recipeId);
                int likeCount = await _interactionService.GetRecipeLikeCount(recipeId);
                return Ok(new { isLiked, likeCount });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("recipes/{recipeId}/bookmark-count")]
        public async Task<IActionResult> GetRecipeBookmarkCount(int recipeId)
        {
            try
            {
                int bookmarkCount = await _interactionService.GetRecipeBookmarkCount(recipeId);
                return Ok(new { bookmarkCount });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        // Check if the user has bookmarked a Recipe post and the bookmark count of the recipe
        [HttpGet("recipes/{recipeId}/is-bookmarked")]
        [Authorize]
        public async Task<IActionResult> IsRecipeBookmarked(int recipeId)
        {
            try
            {
                string userId = await GetActiveUserId();
                bool isBookmarked = await _interactionService.IsRecipeBookmarked(userId, recipeId);
                int bookmarkCount = await _interactionService.GetRecipeBookmarkCount(recipeId);
                return Ok(new { isBookmarked, bookmarkCount });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Get comments under a Recipe post with pagination
        [HttpGet("recipes/{recipeId}/comments")]
        public async Task<IActionResult> GetRecipeComments(int recipeId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var comments = await _interactionService.GetRecipeComments(recipeId, page, pageSize);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("blogs/comments/{commentId}/images/{imageIndex}")]
        public async Task<IActionResult> GetBlogCommentImage(int commentId, int imageIndex)
        {
            try
            {
                var image = await _interactionService.GetBlogCommentImage(commentId, imageIndex);
                if (image == null)
                    return NotFound(new { message = $"Image {imageIndex} not found for comment {commentId}." });

                return File(image, "image/jpeg"); // Assuming JPEG format, adjust if needed
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("recipes/comments/{commentId}/images/{imageIndex}")]
        public async Task<IActionResult> GetRecipeCommentImage(int commentId, int imageIndex)
        {
            try
            {
                var image = await _interactionService.GetRecipeCommentImage(commentId, imageIndex);
                if (image == null)
                    return NotFound(new { message = $"Image {imageIndex} not found for comment {commentId}." });

                return File(image, "image/jpeg"); // Assuming JPEG format, adjust if needed
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

    }

}
