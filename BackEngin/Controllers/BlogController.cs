using Asp.Versioning;
using BackEngin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace BackEngin.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/blog")]
    [ApiController]
    public class BlogController : ApiControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService) : base()
        {
            _blogService = blogService;
        }

        [HttpGet("blogs")]
        public async Task<IActionResult> GetBlogs([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var blogs = await _blogService.GetBlogs(pageNumber, pageSize);
            return Ok(blogs);
        }

        [HttpPost("create-blog")]
        [Authorize]
        public async Task<IActionResult> CreateBlog([FromBody] CreateBlogDTO createBlogDto)
        {
            if (createBlogDto == null) return BadRequest("Invalid blog data.");

            createBlogDto.UserId = await GetActiveUserId();
            var createdBlog = await _blogService.CreateBlog(createBlogDto);

            return CreatedAtAction(nameof(GetBlogById), new { blogId = createdBlog.Id }, createdBlog);
        }

        [HttpGet("blog-details/{blogId}")]
        public async Task<IActionResult> GetBlogById(int blogId)
        {
            var blog = await _blogService.GetBlogById(blogId);
            if (blog == null) return NotFound();
            return Ok(blog);
        }

        [HttpPut("update-blog/{blogId}")]
        [Authorize]
        public async Task<IActionResult> UpdateBlog(int blogId, [FromBody] UpdateBlogDTO updateBlogDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var blogOwner = await _blogService.GetOwner(blogId);
            // Return NotFound if the blog doesn't exist
            if (blogOwner == null)
            {
                return NotFound();
            }

            if (!await CanUserAccess(blogOwner))
            {
                return Unauthorized();
            }

            try
            {
                var updatedBlog = await _blogService.UpdateBlog(blogId, updateBlogDto);
                if (updatedBlog == null) // Double-check if the update failed
                {
                    return NotFound();
                }
                return Ok(updatedBlog);
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


        [HttpDelete("delete-blog/{blogId}")]
        [Authorize]
        public async Task<IActionResult> DeleteBlog(int blogId)
        {
            var blogOwner = await _blogService.GetOwner(blogId);
            if (blogOwner == null)
            {
                return NotFound();
            }

            if (!await CanUserAccess(blogOwner))
            {
                return Unauthorized();
            }

            var result = await _blogService.DeleteBlog(blogId);
            if (!result)
            {
                return NotFound();
            }

            return Ok(new { message = "Blog deleted successfully!" });
        }


        [HttpGet("blog-recipe/{blogId}")]
        public async Task<IActionResult> GetRecipeOfBlog(int blogId)
        {
            var recipe = await _blogService.GetRecipeOfBlog(blogId);
            if (recipe == null) return NotFound("No recipe associated with this blog.");
            return Ok(recipe);
        }
    }
}
