using BackEngin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace BackEngin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("blogs")]
        public async Task<IActionResult> GetBlogs()
        {
            var blogs = await _blogService.GetBlogs();
            return Ok(blogs);
        }

        [HttpPost("create-blog")]
        [Authorize]
        public async Task<IActionResult> CreateBlog([FromBody] CreateBlogDTO createBlogDto)
        {
            if (createBlogDto == null) return BadRequest("Invalid blog data.");

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
            var updatedBlog = await _blogService.UpdateBlog(blogId, updateBlogDto);
            if (updatedBlog == null) return NotFound();
            return Ok(updatedBlog);
        }

        [HttpDelete("delete-blog/{blogId}")]
        [Authorize]
        public async Task<IActionResult> DeleteBlog(int blogId)
        {
            var result = await _blogService.DeleteBlog(blogId);
            if (!result) return NotFound();
            return NoContent();
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
